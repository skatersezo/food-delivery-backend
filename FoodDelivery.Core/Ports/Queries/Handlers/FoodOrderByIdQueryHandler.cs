using Microsoft.EntityFrameworkCore;
using Paramore.Darker;
using FoodDelivery.Core.Adaptors.Db;

namespace FoodDelivery.Core.Ports.Queries.Handlers
{
	public class FoodOrderByIdQueryHandler : QueryHandlerAsync<FoodOrderByIdQuery, FoodOrderByIdQuery.Result>
	{
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;

        public FoodOrderByIdQueryHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
            _options = options;
		}

        public override async Task<FoodOrderByIdQuery.Result> ExecuteAsync(FoodOrderByIdQuery query, CancellationToken cancellationToken = default)
        {
            await using var uow = new FoodDeliveryDbContext(_options);
            return await uow.FoodOrders
                .Where(i => i.Id == query.FoodOrderId)
                .Select(i => new FoodOrderByIdQuery.Result(i))
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}

