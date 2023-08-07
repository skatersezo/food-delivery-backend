using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Adaptors.Repositories;
using FoodDelivery.Core.Domain;


namespace FoodDelivery.Core.Ports.Commands.Handlers
{
	public class AddFoodOrderCommandHandler : RequestHandlerAsync<AddFoodOrderCommand>
	{
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public AddFoodOrderCommandHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
            _options = options;
		}

        public override async Task<AddFoodOrderCommand> HandleAsync(AddFoodOrderCommand command, CancellationToken cancellationToken = default)
        {
            await using (var uow = new FoodDeliveryDbContext(_options))
            {
                var repo = new FoodOrderRepositoryAsync(uow);
                var savedOrder = await repo.AddAsync(new FoodOrder {
                                                            FoodName = command.FoodName,
                                                            CustomerName = command.CustomerName,
                                                            Delivered = command.Delivered },
                                                            cancellationToken);

                command.FoodOrderId = savedOrder.Id;
            }

            return await base.HandleAsync(command, cancellationToken);
        }
    }
}

