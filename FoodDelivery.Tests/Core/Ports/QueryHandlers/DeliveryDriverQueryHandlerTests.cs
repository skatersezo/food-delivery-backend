using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Queries;
using FoodDelivery.Core.Ports.Queries.Handlers;


namespace FoodDelivery.Tests.Core.Ports.QueryHandlers
{
    [TestFixture]
	public class DeliveryDriverQueryHandlerTests
	{
		[Test]
		public async Task Test_Retrieving_A_DeliveryDriver()
		{
			// Arrange
			var dbOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
				.UseInMemoryDatabase("retrieving_a_deliveryDriver_test")
				.Options;
			var deliveryDriver = new DeliveryDriver { Name = "Rogelio" };
			await using (var context = new FoodDeliveryDbContext(dbOptions))
			{
				context.DeliveryDrivers.Add(deliveryDriver);
				await context.SaveChangesAsync();
			}
			var queryHandler = new DeliveryDriverByIdQueryHandler(dbOptions);

			// Act
			var result = await queryHandler.ExecuteAsync(new DeliveryDriverByIdQuery(deliveryDriver.Id));
			var deliveryDriverViewModel = result.DeliveryDriverViewModel;

			// Assert
			Assert.That(deliveryDriver.Id, Is.EqualTo(deliveryDriverViewModel.Id));
            Assert.That(deliveryDriver.Name, Is.EqualTo(deliveryDriverViewModel.Name));
            Assert.That(deliveryDriver.Orders, Is.EqualTo(deliveryDriverViewModel.Orders));
            Assert.That(deliveryDriver.Longitude, Is.EqualTo(deliveryDriverViewModel.Longitude));
            Assert.That(deliveryDriver.Latitude, Is.EqualTo(deliveryDriverViewModel.Latitude));
        }

		[Test]
		public async Task Test_Retrieving_All_DeliveryDrivers()
		{
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("retrieving_all_deliveryDrivers_test")
                .Options;

            await using (var context = new FoodDeliveryDbContext(dbOptions))
            {
                context.DeliveryDrivers.Add(new DeliveryDriver { Name = "Rogelio" });
				context.DeliveryDrivers.Add(new DeliveryDriver { Name = "Eugenia" });
                context.DeliveryDrivers.Add(new DeliveryDriver { Name = "Marcelo" });
                await context.SaveChangesAsync();
            }
            var queryHandler = new AllDeliveryDriversQueryHandler(dbOptions);

            // Act
            var result = await queryHandler.ExecuteAsync(new AllDeliveryDriversQuery());
			var deliveryDriversViewModels = result.DeliveryDriversViewModel;

			// Assert
			Assert.That(deliveryDriversViewModels.Count(), Is.EqualTo(3));
        }
	}
}

