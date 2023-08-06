using Paramore.Brighter;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;
using FoodDelivery.Core.Domain;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class AddDeliveryDriverCommandHandler : RequestHandlerAsync<AddDeliveryDriverCommand>
	{
		private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public AddDeliveryDriverCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<AddDeliveryDriverCommand> HandleAsync(AddDeliveryDriverCommand command, CancellationToken cancellationToken = default)
        {
			await using (var uow = new FoodDeliveryDbContext(_options))
			{
				var repo = new DeliveryDriverRepositoryAsync(uow);
				var savedDriver = await repo.AddAsync(new DeliveryDriver { Name = command.Name }, cancellationToken);
				command.DeliveryDriverId = savedDriver.Id;

			}

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

