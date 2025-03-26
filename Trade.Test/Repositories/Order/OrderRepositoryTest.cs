using Microsoft.EntityFrameworkCore;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Test
{
    public class OrderRepositoryTest: IDisposable
    {
        private readonly DbContextOptions<Context> _dbContextOptions;

        private decimal _orderTotalPrice1;
        private const int _orderQuantity1 = 7;
        private Order _order1;

        public OrderRepositoryTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TradeTestDB")
                .Options;

            AddData();
        }

        private void AddData()
        {
            using Context? context = new(_dbContextOptions);
            OrdersRepository repository = new(context);

            User user = new("Test name", "Test lastname", "TestEmail@gmail.com");
            context.Users.Add(user);

            Address userAddress = new(user.Id, "Country", "Region", "City", "111000", "Street", 20, 15, "");
            context.Addresses.Add(userAddress);

            Product product = new("Product 1", "Product Desc", 200.50M, 10.50M, 53);
            context.Products.Add(product);

            Order order1 = new(user.Id, userAddress.Id, "Address comment 1");
            Order order2 = new(user.Id, userAddress.Id, "Address comment 2");
            context.Orders.Add(order1);
            context.Orders.Add(order2);
            _order1 = order1;

            context.OrderItems.Add(new OrderItem(order1.Id, product.Id, _orderQuantity1));
            context.OrderItems.Add(new OrderItem(order2.Id, product.Id, 3));

            _orderTotalPrice1 = product.TotalPrice * _orderQuantity1;

            context.SaveChanges();
        }

        private OrdersRepository CreateRepository()
        {
            var context = new Context(_dbContextOptions);
            return new OrdersRepository(context);
        }

        public void Dispose()
        {
            using var context = new Context(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAll()
        {
            OrdersRepository repository = CreateRepository();

            OrderResultDTO[] result = (await repository.GetAll()).ToArray();

            Assert.NotNull(result);
            Assert.Equal("Address comment 1", result[0].Comment);
            Assert.Equal("Country", result[0].Address.Country);
            Assert.Equal(_orderTotalPrice1, result[0].TotalPrice);
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public async Task GetById()
        {
            OrdersRepository repository = CreateRepository();

            OrderResultDTO? result = await repository.GetById(_order1.Id);

            Assert.NotNull(result);
            Assert.Equal("Ожидает обработки", result.Status);
            Assert.Equal("Street", result.Address.Street);
            Assert.Equal(_orderTotalPrice1, result.TotalPrice);
        }

        [Fact]
        public async Task UpdateOrderStatus()
        {
            OrdersRepository repository = CreateRepository();

            OrderResultDTO? inProcess = await repository.UpdateOrderStatus(_order1.Id, OrderStatus.InProcess);

            Assert.NotNull(inProcess);
            Assert.Equal("В процессе обработки", inProcess.Status);

            OrderResultDTO? inDelivery = await repository.UpdateOrderStatus(_order1.Id, OrderStatus.InDelivery);

            Assert.NotNull(inProcess);
            Assert.Equal("В доставке", inProcess.Status);

            OrderResultDTO? done = await repository.UpdateOrderStatus(_order1.Id, OrderStatus.Done);

            Assert.NotNull(done);
            Assert.Equal("Завершен", done.Status);
        }
    }
}
