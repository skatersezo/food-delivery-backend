using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Ports.Commands;
using FoodDelivery.Core.Ports.Commands.Handlers;

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
			}
		}
	}
}

