using System;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Commands;
using FoodDelivery.Core.Ports.Commands.Handlers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FoodDelivery.Tests.Core.Ports.CommandHandlers
{
    [TestFixture]
    public class DeleteFoodOrderCommandHandlerTests
	{
        [Test]
        public async Task Delete_Food_Order_By_Id()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("Delete_food_order_test")
                .Options;

            var order = new FoodOrder { FoodName = "Fish & chips", CustomerName = "Brian" };

            using (var context = new FoodDeliveryDbContext(options))
            {
                context.FoodOrders.Add(order);
                context.SaveChanges();
            }
            var command = new DeleteFoodOrderByIdCommand(order.Id);
            var handler = new DeleteFoodOrderByIdCommandHandler(options);

            // Act
            await handler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.FoodOrders.Any(d => d.Id == order.Id), Is.False);
            }
        }


        [Test]
        public async Task Delete_All_Food_Orders()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("Delete_all_food_orders_test")
                .Options;

            using (var context = new FoodDeliveryDbContext(options))
            {
                context.FoodOrders.Add(new FoodOrder { FoodName = "Sunday Roast", CustomerName = "Rufino" });
                context.FoodOrders.Add(new FoodOrder { FoodName = "Tandoori Chicken", CustomerName = "Carlota" });
                context.SaveChanges();
            }

            var command = new DeleteAllFoodOrdersCommand();
            var handler = new DeleteAllFoodOrdersCommandHandler(options);

            // Act
            await handler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.FoodOrders.Any(), Is.False);
            }
        }
    }
}

