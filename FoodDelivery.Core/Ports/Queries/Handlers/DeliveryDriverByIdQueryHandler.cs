using Microsoft.EntityFrameworkCore;
using Paramore.Darker;
using FoodDelivery.Core.Adaptors.Db;


namespace FoodDelivery.Core.Ports.Queries.Handlers
{
	public class DeliveryDriverByIdQueryHandler : QueryHandlerAsync<DeliveryDriverByIdQuery, DeliveryDriverByIdQuery.Result>
	{
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;

		public DeliveryDriverByIdQueryHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
            _options = options;
		}

        public override async Task<DeliveryDriverByIdQuery.Result> ExecuteAsync(DeliveryDriverByIdQuery query, CancellationToken cancellationToken = default)
        {
            await using var uow = new FoodDeliveryDbContext(_options);
            return await uow.DeliveryDrivers
                .Where(i => i.Id == query.Id)
                .Select(i => new DeliveryDriverByIdQuery.Result(i))
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}

