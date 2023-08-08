using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Ports.Commands;
using FoodDelivery.Core.Ports.Commands.Handlers;
using FoodDelivery.Core.ViewModels;

namespace FoodDelivery.Tests.Core.Ports.CommandHandlers
{

    [TestFixture]
	public class AddDeliveryDriverCommandHandlerTests
	{

		[Test]
		public async Task Add_Delivery_Driver_Test()
		{
			// Arrange
			const string DRIVER_CECILIA = "Cecilia";
            List<FoodOrderViewModel> orders = new List<FoodOrderViewModel>
            {
                new FoodOrderViewModel(1, "Chistorra sandwhich", "Roberto", false),
                new FoodOrderViewModel(2, "Galician octopus", "Marina", false),
                new FoodOrderViewModel(3, "Broken eggs", "Eusebio", true),
            };

            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
				.UseInMemoryDatabase("add_delivery_driver_test")
				.Options;

			var command = new AddDeliveryDriverCommand(DRIVER_CECILIA);
			var handler = new AddDeliveryDriverCommandHandler(options);

			// Act
			await handler.HandleAsync(command);

			// Assert
			using (var context = new FoodDeliveryDbContext(options))
			{
				Assert.That(context.DeliveryDrivers.Count(), Is.EqualTo(1));
				Assert.That(context.DeliveryDrivers.Single().Name, Is.EqualTo(DRIVER_CECILIA));

                var driverOrders = context.DeliveryDrivers.Single().Orders;
                Assert.That(driverOrders.Count(), Is.EqualTo(3));
                for (int i = 1; i <= 3; i++)
                {
                    Assert.That(driverOrders.Single(o => o.Id == i).FoodName, Is.EqualTo(orders.Single(o => o.Id == i).FoodName));
                    Assert.That(driverOrders.Single(o => o.Id == i).CustomerName, Is.EqualTo(orders.Single(o => o.Id == i).CustomerName));
                }
            }
		}
	}
}

