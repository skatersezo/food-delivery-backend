using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Commands.Handlers;
using FoodDelivery.Core.Ports.Commands;


namespace FoodDelivery.Tests.Core.Ports.CommandHandlers
{
    [TestFixture]
	public class UpdateFoodOrderCommandHandlerTests
	{

		[Test]
		public async Task Test_Updating_Food_Name()
		{
			// Arrange
			const string FOOD_NAME = "Spagetti bolognesa";

			var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
				.UseInMemoryDatabase("test_update_food_order_name")
				.Options;

			var foodOrder = new FoodOrder { FoodName = "wrong name", CustomerName = "Alberto" };
			using (var context = new FoodDeliveryDbContext(options))
			{
				context.FoodOrders.Add(foodOrder);
				context.SaveChanges();
			}

			var command = new UpdateFoodOrderByIdCommand(foodOrder.Id, FOOD_NAME);
			var commandHandler = new UpdateFoodOrderByIdCommandHandler(options);

			// Act
			await commandHandler.HandleAsync(command);

			// Assert
			using (var context = new FoodDeliveryDbContext(options))
			{
				Assert.That(context.FoodOrders.Single().FoodName, Is.EqualTo(FOOD_NAME));
				Assert.That(context.FoodOrders.Single().CustomerName, Is.EqualTo(foodOrder.CustomerName));
				Assert.That(context.FoodOrders.Single().Delivered, Is.False);
			}
		}

        [Test]
        public async Task Test_Updating_Food_Order_To_Delivered()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("test_update_food_order_status")
                .Options;

            var foodOrder = new FoodOrder { FoodName = "Tasty food", CustomerName = "Alberto", Delivered = false };
            using (var context = new FoodDeliveryDbContext(options))
            {
                context.FoodOrders.Add(foodOrder);
                context.SaveChanges();
            }

            var command = new UpdateFoodOrderByIdCommand(foodOrder.Id, delivered: true);
            var commandHandler = new UpdateFoodOrderByIdCommandHandler(options);

            // Act
            await commandHandler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.FoodOrders.Single().FoodName, Is.EqualTo(foodOrder.FoodName));
                Assert.That(context.FoodOrders.Single().CustomerName, Is.EqualTo(foodOrder.CustomerName));
                Assert.That(context.FoodOrders.Single().Delivered, Is.True);
            }
        }

        [Test]
        public async Task Test_Updating_Food_Order_Customer_Name()
        {
            // Arrange
            const string CUSTOMER_NAME = "Luisa";

            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("test_update_food_order_status")
                .Options;

            var foodOrder = new FoodOrder { FoodName = "Tasty food", CustomerName = "wrong name" };
            using (var context = new FoodDeliveryDbContext(options))
            {
                context.FoodOrders.Add(foodOrder);
                context.SaveChanges();
            }

            var command = new UpdateFoodOrderByIdCommand(foodOrder.Id, customerName: CUSTOMER_NAME);
            var commandHandler = new UpdateFoodOrderByIdCommandHandler(options);

            // Act
            await commandHandler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.FoodOrders.Single().FoodName, Is.EqualTo(foodOrder.FoodName));
                Assert.That(context.FoodOrders.Single().CustomerName, Is.EqualTo(CUSTOMER_NAME));
                Assert.That(context.FoodOrders.Single().Delivered, Is.False);
            }
        }

        [Test]
        public async Task Test_Updating_Food_Order_Customer_Name_And_Food_Name()
        {
            // Arrange
            const string CUSTOMER_NAME = "Luisa";
            const string FOOD_NAME = "Paella";

            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("test_update_food_order_status")
                .Options;

            var foodOrder = new FoodOrder { FoodName = "wrong food", CustomerName = "wrong name" };
            using (var context = new FoodDeliveryDbContext(options))
            {
                context.FoodOrders.Add(foodOrder);
                context.SaveChanges();
            }

            var command = new UpdateFoodOrderByIdCommand(foodOrder.Id, foodName: FOOD_NAME, customerName: CUSTOMER_NAME);
            var commandHandler = new UpdateFoodOrderByIdCommandHandler(options);

            // Act
            await commandHandler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.FoodOrders.Single().FoodName, Is.EqualTo(FOOD_NAME));
                Assert.That(context.FoodOrders.Single().CustomerName, Is.EqualTo(CUSTOMER_NAME));
                Assert.That(context.FoodOrders.Single().Delivered, Is.False);
            }
        }
    }
}

