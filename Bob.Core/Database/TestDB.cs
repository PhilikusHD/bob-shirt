using Avalonia.Markup.Xaml.Templates;
using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Repositories;
using Bob.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.Database
{
    internal readonly struct TestScope : IDisposable
    {
        private readonly string m_Name;
        private readonly System.Diagnostics.Stopwatch m_Watch;

        public TestScope(string name)
        {
            m_Name = name;
            m_Watch = System.Diagnostics.Stopwatch.StartNew();
            Logger.Debug($"[Start] {name}");
        }

        public void Dispose()
        {
            m_Watch.Stop();
            Logger.Debug($"[End] {m_Name} — {m_Watch.ElapsedMilliseconds} ms");
        }
    }

    public sealed class TestDB
    {
        public async Task RunAsync()
        {
            Logger.Debug("=== Starting Database Integration Tests ===");

            try
            {
                using (new TestScope("Addresses"))
                    await TestAddresses();

                using (new TestScope("Customers"))
                    await TestCustomers();

                using (new TestScope("Products & Variants"))
                    await TestProducts();

                using (new TestScope("ProductTypes / Colors / Sizes"))
                    await TestSupportingTables();

                using (new TestScope("Order & Cart Flow"))
                    await TestOrderAndCartFlow();

                using (new TestScope("Payments"))
                    await TestPayment();

                Logger.Debug("=== All Tests Completed Successfully ===");
            }
            catch (Exception ex)
            {
                Logger.Critical($"TestDB crashed: {ex.Message}");
            }
        }

        private async Task TestAddresses()
        {
            Logger.Debug("-> Address: Reading existing entries...");
            var a1 = await AddressService.GetAddressByIdAsync(1);
            Logger.Debug(a1 != null ? $"Address 1 OK: {a1.Street} {a1.HouseNumber}" : "Address 1 not found.");

            var newAddr = new Address
            {
                Street = "Test Lane",
                HouseNumber = "42A",
                PostalCode = 12345,
                City = "Nowhere"
            };
            await AddressService.AddAddressAsync(newAddr);

            var check = await AddressService.GetAddressByIdAsync(newAddr.Id);
            Logger.Debug(check != null ? "Address write OK" : "Address write failed");

            await AddressService.DeleteAddressAsync(newAddr.Id);
        }

        private async Task TestCustomers()
        {
            Logger.Debug("-> Customers: Reading all customers...");
            var customers = await CustomerService.GetAllCustomersAsync();
            Logger.Debug($"Loaded {customers.Count} customers.");

            var c1 = await CustomerService.GetCustomerByIdAsync(1);
            if (c1 != null)
            {
                Logger.Debug($"Customer 1 OK: {c1.Name} {c1.Surname}");
                var addr = await AddressService.GetAddressByIdAsync(c1.AddressId);
                Logger.Debug($"Customer 1 address: {addr?.Street ?? "NULL"}");
            }

            var newCust = new Customer(999, "Test", "Dude", "test@local", 1, "0000", DateTime.UtcNow);
            await CustomerService.AddCustomerAsync(newCust);
            var check = await CustomerService.GetCustomerByIdAsync(newCust.Id);
            Logger.Debug(check != null ? "Customer insert OK" : "Customer insert failed");

            await CustomerService.DeleteCustomerAsync(newCust.Id);
        }

        private async Task TestSupportingTables()
        {
            Logger.Debug("-> Testing ProductTypes, Colors, Sizes...");

            // ProductType
            var newType = new ProductType { TypeId = 999, TypeName = "TestType" };
            await ProductService.AddProductTypeAsync(newType);

            var typeLoaded = await ProductService.GetProductTypeByIdAsync(newType.TypeId);
            Logger.Debug(typeLoaded != null ? $"ProductType loaded: {typeLoaded.TypeName}" : "ProductType load FAILED");

            await ProductService.DeleteProductTypeAsync(newType.TypeId);
            Logger.Debug("Deleted temporary ProductType");

            // Color
            var colors = await ProductService.GetAllColorsAsync();
            Logger.Debug($"Loaded {colors.Count} colors");

            var color = await ProductService.GetColorAsync(1);
            Logger.Debug(color != null ? $"Color 1: {color.ColorName}" : "Color 1 missing");

            // Size
            var sizes = await ProductService.GetAllSizesAsync();
            Logger.Debug($"Loaded {sizes.Count} sizes");

            var multiplier = await ProductService.GetPriceAdjustedForSize(3, 50);
            Logger.Debug($"Price adjusted for size: {multiplier}");
        }

        private async Task TestProducts()
        {
            Logger.Debug("-> Products: Loading all products...");
            var all = await ProductService.GetAllProductsAsync();
            Logger.Debug($"Catalog has {all.Count} products.");

            // Create a temporary product
            var product = new Product { ProductId = 222, Name = "Temp Product", TypeId = 1, Price = 55 };
            await ProductService.AddProductAsync(product);

            // Create a variant for the product
            var variant = new ProductVariant
            {
                VariantId = 999,
                ProductId = product.ProductId,
                ColorId = 1,
                SizeId = 1,
                Stock = 10
            };
            await ProductService.AddVariantAsync(variant);

            Logger.Debug($"Created product {product.Name} with ID {product.ProductId}");

            var variants = await ProductService.GetVariantsForProductAsync(product.ProductId);
            Logger.Debug($"Product has {variants.Count} variants.");

            var productType = await ProductService.GetProductTypeAsync(product.ProductId);
            Logger.Debug($"Product with ID {product.ProductId} has TypeID {productType.TypeId}");

            var productType2 = await ProductService.GetProductTypeByIdAsync(productType.TypeId);
            Logger.Debug($"ProductType with ID {productType.TypeId} is of name: '{productType2.TypeName}'");

            await ProductService.DeleteProductAsync(product.ProductId);
            Logger.Debug("Deleted temporary product and its variant successfully");
        }

        private async Task TestOrderAndCartFlow()
        {
            var customer = await CustomerService.GetCustomerByIdAsync(1);
            if (customer == null)
            {
                Logger.Error("Customer 1 missing, cannot continue order test.");
                return;
            }

            var order = new Order
            {
                Id = 555,
                CustomerId = customer.Id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0
            };

            // Pick existing variant for testing
            var firstProduct = (await ProductService.GetAllProductsAsync()).First();
            var variant = (await ProductService.GetVariantsForProductAsync(firstProduct.ProductId)).First();

            await OrderItemService.AddLineAsync(new OrderItemLine(order.Id, variant.VariantId, 2));
            await OrderItemService.AddLineAsync(new OrderItemLine(order.Id, 10, 1));

            var result = await OrderService.CreateOrderAsync(order);
            Logger.Debug(result != -1 ? $"Order #{order.Id} total {order.TotalAmount}" : "Order creation failed");

            var loaded = await OrderService.GetOrderByIdAsync(order.Id);
            Logger.Debug(loaded != null ? "Order fetch OK" : "Order fetch FAILED");

            await OrderItemService.RemoveLineAsync(order.Id, variant.VariantId);
            await OrderItemService.RemoveLineAsync(order.Id, 10);
        }

        private async Task TestPayment()
        {
            var processors = await PaymentProcessorService.GetAllProcessorsAsync();
            Logger.Debug($"Available payment processors: {string.Join(", ", processors.Select(p => p.Method))}");

            var p = new Payment
            {
                Id = 999,
                OrderId = 555,
                PaymentDate = DateTime.UtcNow,
                ProcessorId = 1
            };
            await PaymentService.ProcessPaymentAsync(p);

            var check = await PaymentService.GetPaymentByIdAsync(p.Id);
            Logger.Debug(check != null ? "Payment added OK" : "Payment add FAILED");

            await PaymentService.DeletePaymentAsync(999);
            await OrderService.CancelOrderAsync(555);
        }
    }
}
