using Bob.Core.Logging;
using Bob.Core.Services;
using Bob.Core.Repositories;
using Bob.Core.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;

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

                using (new TestScope("Products"))
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
            if (a1 == null)
                Logger.Error("Address 1 not found, database is inconsistent.");
            else
                Logger.Debug($"Address 1 OK: {a1.Street} {a1.HouseNumber}, {a1.City}");

            Logger.Debug("-> Address: Adding a new address...");

            var newAddr = new Address(999, "Test Lane", "42A", 12345, "Nowhere");
            await service.AddAddressAsync(newAddr);

            var check = await service.GetAddressByIdAsync(newAddr.Id);
            Logger.Debug(check != null ? "Address write OK" : "Address write failed");

            Logger.Debug("-> Address: Removing temporary address...");
            await service.DeleteAddressAsync(newAddr.Id);
        }

        private async Task TestCustomers(CustomerService custService, AddressService addrService)
        {
            Logger.Debug("-> Customers: Reading all customers...");

            var customers = await custService.GetAllCustomersAsync();
            Logger.Debug($"Loaded {customers.Count} customers.");

            Logger.Debug("-> Customers: Reading customer #1...");
            var c1 = await custService.GetCustomerByIdAsync(1);

            if (c1 != null)
            {
                Logger.Debug($"Customer 1 OK: {c1.Name} {c1.Surname}");
                var addr = await addrService.GetAddressByIdAsync(c1.AddressId);
                Logger.Debug($"Customer 1 address: {addr?.Street ?? "NULL"}");
            }

            Logger.Debug("-> Customers: Inserting test customer...");

            var newCust = new Customer(
                id: 999,
                name: "Test",
                surname: "Dude",
                email: "test@local",
                addressId: 1,
                phoneNumber: "0000",
                signupDate: DateTime.UtcNow);

            await custService.AddCustomerAsync(newCust);

            var check = await custService.GetCustomerByIdAsync(newCust.Id);
            Logger.Debug(check != null ? "Customer insert OK" : "Customer insert failed");

            Logger.Debug("-> Customers: Removing test customer...");
            await custService.DeleteCustomerAsync(newCust.Id);
        }

        private async Task TestProducts(ProductService productService)
        {
            Logger.Debug("-> Products: Loading all products...");

            var all = await productService.GetAllProductsAsync();
            Logger.Debug($"There are {all.Count} products in catalog.");

            Logger.Debug("-> Creating temporary product...");

            var template = all.Last();

            var product = new Product
            {
                Id = template.Id + 1,
                Name = "Temp Product",
                Price = template.Price,
                Color = template.Color,
                Size = template.Size
            };

            await productService.AddProductAsync(product);

            var check = await productService.GetProductByIdAsync(product.Id);
            Logger.Debug($"Created temporary product: {check?.Name ?? "NULL"}");

            Logger.Debug("-> Deleting temporary product...");
            await productService.DeleteProductAsync(product.Id);
        }

        private async Task TestOrderAndCartFlow(
            OrderService orderService,
            OrderItemService itemService,
            ProductService productService,
            CustomerService customerService)
        {
            Logger.Debug("-> Order Test Flow Start");

            var customer = await customerService.GetCustomerByIdAsync(1);
            if (customer == null)
            {
                Logger.Error("Customer 1 missing, cannot continue order test.");
                return;
            }

            Logger.Debug("Creating temporary order...");
            var order = new Order(555, customer.Id, DateTime.UtcNow, 0);

            Logger.Debug("Adding item lines...");

            await itemService.AddLineAsync(new OrderItemLine(555, 1, "2"));
            await itemService.AddLineAsync(new OrderItemLine(555, 10, "1"));

            Logger.Debug("Generating final order total...");
            var result = await orderService.CreateOrderAsync(order);
            if (result != -1)
                Logger.Debug($"Order #555 created with total {order.TotalAmount}");
            else
                Logger.Error("Order creation failed.");

            Logger.Debug("-> Order retrieval check...");
            var loaded = await orderService.GetOrderByIdAsync(555);
            Logger.Debug(loaded != null ? "Order fetch OK" : "Order fetch FAILED");

            Logger.Debug("-> Order item deletion check...");
            await itemService.RemoveLineAsync(555, 1);
            await itemService.RemoveLineAsync(555, 10);
        }

        private async Task TestPayment(
            PaymentProcessorService processorService,
            PaymentService paymentService,
            OrderService orderService)
        {
            Logger.Debug("-> Payment Processors...");
            var processors = await processorService.GetAllProcessorsAsync();
            Logger.Debug($"Available payment processors: {string.Join(", ", processors.Select(p => p.Method))}");

            Logger.Debug("-> Creating payment record...");
            var p = new Payment(900, 555, DateTime.UtcNow, 1);

            await paymentService.ProcessPaymentAsync(p);

            var check = await paymentService.GetPaymentByIdAsync(900);
            Logger.Debug(check != null ? "Payment added OK" : "Payment add FAILED");

            Logger.Debug("-> Payment deletion check...");
            await paymentService.DeletePaymentAsync(p.Id);

            Logger.Debug("-> Canceling temporary order...");
            await orderService.CancelOrderAsync(555);
        }
    }
}
