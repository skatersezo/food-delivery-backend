using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;
using FoodDelivery.Core.Domain;

namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class UpdateDeliveryDriverByIdCommandHandler : RequestHandlerAsync<UpdateDeliveryDriverByIdCommand>
	{
		private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public UpdateDeliveryDriverByIdCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<UpdateDeliveryDriverByIdCommand> HandleAsync(UpdateDeliveryDriverByIdCommand command, CancellationToken cancellationToken = default)
        {
			await using (var uow = new FoodDeliveryDbContext(_options))
			{
				var repo = new DeliveryDriverRepositoryAsync(uow);
				var deliveryDriver = await repo.GetAsync(command.Id, cancellationToken);

				if (command.Latitude != null)
					deliveryDriver.Latitude = command.Latitude.Value;

				if (command.Longitude != null)
					deliveryDriver.Longitude = command.Longitude.Value;

				if (command.FoodOrders.Any())
					foreach (var newOrder in command.FoodOrders.Where(o => !deliveryDriver.Orders.Any(f => f.Id == o.Id)))
						deliveryDriver.Orders.Append(new FoodOrder { Id = newOrder.Id, CustomerName = newOrder.CustomerName, FoodName = newOrder.FoodName, Delivered = newOrder.Delivered });

				if (deliveryDriver.Orders.Any())
				{
					foreach (var order in deliveryDriver.Orders)
					{
						var matchingOrder = command.FoodOrders.FirstOrDefault(o => o.Id == order.Id);
						order.FoodName = matchingOrder.FoodName;
						order.CustomerName = matchingOrder.CustomerName;
						order.Delivered = matchingOrder.Delivered;
					}
				}

				await repo.UpdateAsync(deliveryDriver, cancellationToken);
			}

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

