using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Queries;
using FoodDelivery.Core.Ports.Queries.Handlers;

namespace FoodDelivery.Tests.Core.Ports.QueryHandlers
{
    [TestFixture]
    public class FoodOrderQueryHandlerTests
	{
        [Test]
        public async Task Test_Retrieving_A_FoodOrder()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("retrieving_a_food_order_test")
                .Options;
            var foodOrder = new FoodOrder { FoodName = "Tortilla de patatas", CustomerName= "Rogelio" };
            await using (var context = new FoodDeliveryDbContext(dbOptions))
            {
                context.FoodOrders.Add(foodOrder);
                await context.SaveChangesAsync();
            }
            var queryHandler = new FoodOrderByIdQueryHandler(dbOptions);

            // Act
            var result = await queryHandler.ExecuteAsync(new FoodOrderByIdQuery(foodOrder.Id));
            var foodOrderViewModel = result.FoodOrderViewModel;

            // Assert
            Assert.That(foodOrder.Id, Is.EqualTo(foodOrderViewModel.Id));
            Assert.That(foodOrder.FoodName, Is.EqualTo(foodOrderViewModel.FoodName));
            Assert.That(foodOrder.CustomerName, Is.EqualTo(foodOrderViewModel.CustomerName));
            Assert.That(foodOrder.Delivered, Is.EqualTo(foodOrderViewModel.Delivered));
        }

        [Test]
        public async Task Test_Retrieving_All_DeliveryDrivers()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("retrieving_all_food_orders_test")
                .Options;

            await using (var context = new FoodDeliveryDbContext(dbOptions))
            {
                context.FoodOrders.Add(new FoodOrder { FoodName = "Pisto", CustomerName = "Rogelio" });
                context.FoodOrders.Add(new FoodOrder { FoodName = "Fideua", CustomerName = "Eugenia" });
                context.FoodOrders.Add(new FoodOrder { FoodName = "Pizza", CustomerName = "Marcelo" });
                await context.SaveChangesAsync();
            }
            var queryHandler = new AllFoodOrdersQueryHandler(dbOptions);

            // Act
            var result = await queryHandler.ExecuteAsync(new AllFoodOrdersQuery());
            var foodOrderViewModels = result.FoodOrders;

            // Assert
            Assert.That(foodOrderViewModels.Count(), Is.EqualTo(3));
        }
    }
}

