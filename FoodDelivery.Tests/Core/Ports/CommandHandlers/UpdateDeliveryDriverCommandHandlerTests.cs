using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Commands.Handlers;
using FoodDelivery.Core.Ports.Commands;
using FoodDelivery.Core.ViewModels;


namespace FoodDelivery.Tests.Core.Ports.CommandHandlers
{
    [TestFixture]
    public class UpdateDeliveryDriverCommandHandlerTests
	{
        [Test]
        public async Task Test_Updating_Delivery_Driver_Name()
        {
            // Arrange
            const string DRIVER_NAME = "Marcello";

            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("test_update_delivery_driver_name")
                .Options;

            var driver = new DeliveryDriver { Name = "wrong name", Latitude = 12324, Longitude = 123435 };
            using (var context = new FoodDeliveryDbContext(options))
            {
                context.DeliveryDrivers.Add(driver);
                context.SaveChanges();
            }

            var command = new UpdateDeliveryDriverByIdCommand(driver.Id, driverName: DRIVER_NAME);
            var commandHandler = new UpdateDeliveryDriverByIdCommandHandler(options);

            // Act
            await commandHandler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.DeliveryDrivers.Single().Name, Is.EqualTo(DRIVER_NAME));
                Assert.That(context.DeliveryDrivers.Single().Latitude, Is.EqualTo(driver.Latitude));
                Assert.That(context.DeliveryDrivers.Single().Longitude, Is.EqualTo(driver.Longitude));
            }
        }

        [Test]
        public async Task Test_Updating_Delivery_Driver_Orders()
        {
            // Arrange
            List<FoodOrderViewModel> orders = new List<FoodOrderViewModel>
            {
                new FoodOrderViewModel(1, "Chistorra sandwhich", "Roberto", false),
                new FoodOrderViewModel(2, "Galician octopus", "Marina", false),
                new FoodOrderViewModel(3, "Broken eggs", "Eusebio", true),
            };

            var options = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
                .UseInMemoryDatabase("test_update_delivery_driver_name")
                .Options;

            var driver = new DeliveryDriver { Name = "Jose", Latitude = 12324, Longitude = 123435 };
            using (var context = new FoodDeliveryDbContext(options))
            {
                context.DeliveryDrivers.Add(driver);
                context.SaveChanges();
            }

            var command = new UpdateDeliveryDriverByIdCommand(driver.Id, foodOrders: orders);
            var commandHandler = new UpdateDeliveryDriverByIdCommandHandler(options);

            // Act
            await commandHandler.HandleAsync(command);

            // Assert
            using (var context = new FoodDeliveryDbContext(options))
            {
                Assert.That(context.DeliveryDrivers.Single().Name, Is.EqualTo(driver.Name));
                Assert.That(context.DeliveryDrivers.Single().Latitude, Is.EqualTo(driver.Latitude));
                Assert.That(context.DeliveryDrivers.Single().Longitude, Is.EqualTo(driver.Longitude));

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

