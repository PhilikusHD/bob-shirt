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
        private readonly AddressRepository m_AddrRepo = new AddressRepository();
        private readonly CustomerRepository m_CustomerRepo = new CustomerRepository();
        private readonly ProductRepository m_ProductRepo = new ProductRepository();
        private readonly OrderRepository m_OrderRepo = new OrderRepository();
        private readonly OrderItemRepository m_OrderItemRepo = new OrderItemRepository();
        private readonly PaymentRepository m_PaymentRepo = new PaymentRepository();
        private readonly PaymentProcessorRepository m_ProcessorRepo = new PaymentProcessorRepository();

        public async Task RunAsync()
        {
            Logger.Debug("=== Starting Database Integration Tests ===");

            var productService = new ProductService(m_ProductRepo);
            var addressService = new AddressService(m_AddrRepo);
            var customerService = new CustomerService(m_CustomerRepo);
            var processorService = new PaymentProcessorService(m_ProcessorRepo);

            var orderItemService = new OrderItemService(m_OrderItemRepo, productService);
            var orderService = new OrderService(m_OrderRepo, orderItemService, productService);
            var paymentService = new PaymentService(m_PaymentRepo);

            try
            {
                using (new TestScope("Addresses"))
                    await TestAddresses(addressService);

                using (new TestScope("Customers"))
                    await TestCustomers(customerService, addressService);

                using (new TestScope("Products & Variants"))
                    await TestProducts(productService);

                using (new TestScope("Order & Cart Flow"))
                    await TestOrderAndCartFlow(orderService, orderItemService, productService, customerService);

                using (new TestScope("Payments"))
                    await TestPayment(processorService, paymentService, orderService);

                Logger.Debug("=== All Tests Completed Successfully ===");
            }
            catch (Exception ex)
            {
                Logger.Critical($"TestDB crashed: {ex.Message}");
            }
        }

        private async Task TestAddresses(AddressService service)
        {
            Logger.Debug("-> Address: Reading existing entries...");
            var a1 = await service.GetAddressByIdAsync(1);
            Logger.Debug(a1 != null ? $"Address 1 OK: {a1.Street} {a1.HouseNumber}" : "Address 1 not found.");

            var newAddr = new Address
            {
                Street = "Test Lane",
                HouseNumber = "42A",
                PostalCode = 12345,
                City = "Nowhere"
            };
            await service.AddAddressAsync(newAddr);

            var check = await service.GetAddressByIdAsync(newAddr.Id);
            Logger.Debug(check != null ? "Address write OK" : "Address write failed");

            await service.DeleteAddressAsync(newAddr.Id);
        }

        private async Task TestCustomers(CustomerService custService, AddressService addrService)
        {
            Logger.Debug("-> Customers: Reading all customers...");
            var customers = await custService.GetAllCustomersAsync();
            Logger.Debug($"Loaded {customers.Count} customers.");

            var c1 = await custService.GetCustomerByIdAsync(1);
            if (c1 != null)
            {
                Logger.Debug($"Customer 1 OK: {c1.Name} {c1.Surname}");
                var addr = await addrService.GetAddressByIdAsync(c1.AddressId);
                Logger.Debug($"Customer 1 address: {addr?.Street ?? "NULL"}");
            }

            var newCust = new Customer(999, "Test", "Dude", "test@local", 1, "0000", DateTime.UtcNow);
            await custService.AddCustomerAsync(newCust);
            var check = await custService.GetCustomerByIdAsync(newCust.Id);
            Logger.Debug(check != null ? "Customer insert OK" : "Customer insert failed");

            await custService.DeleteCustomerAsync(newCust.Id);
        }

        private async Task TestProducts(ProductService productService)
        {
            Logger.Debug("-> Products: Loading all products...");
            var all = await productService.GetAllProductsAsync();
            Logger.Debug($"Catalog has {all.Count} products.");

            // Create a temporary product
            var product = new Product { Id = 222, Name = "Temp Product", TypeId = 1, Price = 55 };
            await productService.AddProductAsync(product);

            // Create a variant for the product
            var variant = new ProductVariant
            {
                VariantId = 999,
                ProductId = product.Id,
                ColorId = 1,
                SizeId = 1,
                Stock = 10
            };
            await productService.AddVariantAsync(variant);

            Logger.Debug($"Created product {product.Name} with ID {product.Id}");

            var variants = await productService.GetVariantsForProductAsync(product.Id);
            Logger.Debug($"Product has {variants.Count} variants.");

            // DELETE: first the variant, then the product
            await productService.DeleteProductAsync(product.Id);

            Logger.Debug("Deleted temporary product and its variant successfully");
        }

        private async Task TestOrderAndCartFlow(
            OrderService orderService,
            OrderItemService itemService,
            ProductService productService,
            CustomerService customerService)
        {
            var customer = await customerService.GetCustomerByIdAsync(1);
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
            var firstProduct = (await productService.GetAllProductsAsync()).First();
            var variant = (await productService.GetVariantsForProductAsync(firstProduct.Id)).First();

            await itemService.AddLineAsync(new OrderItemLine(order.Id, variant.VariantId, "2"));
            await itemService.AddLineAsync(new OrderItemLine(order.Id, 10, "1"));

            var result = await orderService.CreateOrderAsync(order);
            Logger.Debug(result != -1 ? $"Order #{order.Id} total {order.TotalAmount}" : "Order creation failed");

            var loaded = await orderService.GetOrderByIdAsync(order.Id);
            Logger.Debug(loaded != null ? "Order fetch OK" : "Order fetch FAILED");

            await itemService.RemoveLineAsync(order.Id, variant.VariantId);
            await itemService.RemoveLineAsync(order.Id, 10);
        }

        private async Task TestPayment(
            PaymentProcessorService processorService,
            PaymentService paymentService,
            OrderService orderService)
        {
            var processors = await processorService.GetAllProcessorsAsync();
            Logger.Debug($"Available payment processors: {string.Join(", ", processors.Select(p => p.Method))}");

            var p = new Payment
            {
                Id = 999,
                OrderId = 555,
                PaymentDate = DateTime.UtcNow,
                ProcessorId = 1
            };
            await paymentService.ProcessPaymentAsync(p);

            var check = await paymentService.GetPaymentByIdAsync(p.Id);
            Logger.Debug(check != null ? "Payment added OK" : "Payment add FAILED");

            await paymentService.DeletePaymentAsync(999);
            await orderService.CancelOrderAsync(555);
        }
    }
}
