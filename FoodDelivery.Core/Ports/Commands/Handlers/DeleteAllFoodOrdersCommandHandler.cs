using Paramore.Brighter;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class DeleteAllFoodOrdersCommandHandler : RequestHandlerAsync<DeleteAllFoodOrdersCommand>
    {
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;

        public DeleteAllFoodOrdersCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<DeleteAllFoodOrdersCommand> HandleAsync(DeleteAllFoodOrdersCommand command, CancellationToken cancellationToken = default)
        {
            await using (var uow = new FoodDeliveryDbContext(_options))
            {
                var repo = new FoodOrderRepositoryAsync(uow);
                await repo.DeleteAllAsync(cancellationToken);
            }

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

