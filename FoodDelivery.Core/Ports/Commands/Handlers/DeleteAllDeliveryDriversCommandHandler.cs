using Paramore.Brighter;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class DeleteAllDeliveryDriversCommandHandler : RequestHandlerAsync<DeleteAllDeliveryDriversCommand>
	{
		private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public DeleteAllDeliveryDriversCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<DeleteAllDeliveryDriversCommand> HandleAsync(DeleteAllDeliveryDriversCommand command, CancellationToken cancellationToken = default)
        {
			await using (var uow = new FoodDeliveryDbContext(_options))
			{
				var repo = new DeliveryDriverRepositoryAsync(uow);
				await repo.DeleteAllAsync(cancellationToken);
			}

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

