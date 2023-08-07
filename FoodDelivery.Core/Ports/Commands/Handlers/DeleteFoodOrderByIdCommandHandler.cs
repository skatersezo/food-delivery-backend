using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
    public class DeleteFoodOrderByIdCommandHandler : RequestHandlerAsync<DeleteFoodOrderByIdCommand>
	{
		private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public DeleteFoodOrderByIdCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<DeleteFoodOrderByIdCommand> HandleAsync(DeleteFoodOrderByIdCommand command, CancellationToken cancellationToken = default)
        {
			await using (var uow = new FoodDeliveryDbContext(_options))
			{
				var repo = new FoodOrderRepositoryAsync(uow);
				await repo.DeleteAsync(command.FoodOrderId, cancellationToken);
			}

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

