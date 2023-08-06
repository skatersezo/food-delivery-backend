using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class DeleteDeliveryDriverByIdCommandHandler : RequestHandlerAsync<DeleteDeliveryDriverByIdCommand>
	{
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;

        public DeleteDeliveryDriverByIdCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<DeleteDeliveryDriverByIdCommand> HandleAsync(DeleteDeliveryDriverByIdCommand command, CancellationToken cancellationToken = default)
        {
            await using (var uow = new FoodDeliveryDbContext(_options))
            {
                var repo = new DeliveryDriverRepositoryAsync(uow);
                await repo.DeleteAsync(command.DriverId, cancellationToken);
            }

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

