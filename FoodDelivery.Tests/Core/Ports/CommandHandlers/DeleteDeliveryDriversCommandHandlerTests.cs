using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Commands.Handlers;
using FoodDelivery.Core.Ports.Commands;


namespace FoodDelivery.Tests.Core.Ports.CommandHandlers
{

	[TestFixture]
	public class DeleteDeliveryDriversCommandHandlerTests
	{

		[Test]
		public async Task Delete_Delivery_Driver_By_Id()
		{
            // Arrange
            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("Delete_delivery_driver_test")
                .Options;

			var driver = new DeliveryDriver { Name = "Andrea" };

            using (var context = new FoodDeliveryDbContext(options))
            {
                context.DeliveryDrivers.Add(driver);
                context.SaveChanges();
            }
			var command = new DeleteDeliveryDriverByIdCommand(driver.Id);
			var handler = new DeleteDeliveryDriverByIdCommandHandler(options);

			// Act
			await handler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.DeliveryDrivers.Any(d => d.Id == driver.Id), Is.False);
            }
        }


		[Test]
		public async Task Delete_All_Delivery_Drivers()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
				.UseInMemoryDatabase("Delete_all_delivery_drivers_test")
				.Options;

			using (var context = new FoodDeliveryDbContext(options))
			{
				context.DeliveryDrivers.Add(new DeliveryDriver { Name = "Rufino" });
				context.DeliveryDrivers.Add(new DeliveryDriver { Name = "Carlota" });
				context.SaveChanges();
			}

			var command = new DeleteAllDeliveryDriversCommand();
			var handler = new DeleteAllDeliveryDriversCommandHandler(options);

			// Act
			await handler.HandleAsync(command);

			// Assert
			using (var context = new FoodDeliveryDbContext(options))
			{
				Assert.That(context.DeliveryDrivers.Any(), Is.False);
			}
		}
	}
}

