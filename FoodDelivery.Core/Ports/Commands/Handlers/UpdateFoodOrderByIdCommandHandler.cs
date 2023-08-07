using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;
using FoodDelivery.Core.Domain;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class UpdateFoodOrderByIdCommandHandler : RequestHandlerAsync<UpdateFoodOrderByIdCommand>
	{
		private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public UpdateFoodOrderByIdCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<UpdateFoodOrderByIdCommand> HandleAsync(UpdateFoodOrderByIdCommand command, CancellationToken cancellationToken = default)
        {
			await using (var uow = new FoodDeliveryDbContext(_options))
			{
				var repo = new FoodOrderRepositoryAsync(uow);
				var foodOrder = await repo.GetAsync(command.FoodOrderId, cancellationToken);

				if (command.FoodName != null)
					foodOrder.FoodName = command.FoodName;

				if (command.CustomerName != null)
					foodOrder.CustomerName = command.CustomerName;

				if (command.Delivered.HasValue)
					foodOrder.Delivered = command.Delivered.Value;

				await repo.UpdateAsync(foodOrder, cancellationToken);
			}

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

