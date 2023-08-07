using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Ports.Commands;
using FoodDelivery.Core.Ports.Commands.Handlers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FoodDelivery.Tests.Core.Ports.CommandHandlers
{
    [TestFixture]
    public class AddFoodOrderCommandHandlerTests
    {

        [Test]
        public async Task Add_Food_Order_Test()
        {
            // Arrange
            const string FOOD_ORDER = "Pasta Alfredo";
            const string CUSTOMER_NAME = "Alfredo";

            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("add_food_order_test")
                .Options;

            var command = new AddFoodOrderCommand(FOOD_ORDER, CUSTOMER_NAME);
            var handler = new AddFoodOrderCommandHandler(options);

            // Act
            await handler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.FoodOrders.Count(), Is.EqualTo(1));
                Assert.That(context.FoodOrders.Single().FoodName, Is.EqualTo(FOOD_ORDER));
                Assert.That(context.FoodOrders.Single().CustomerName, Is.EqualTo(CUSTOMER_NAME));
            }
        }
    }
}

