using Microsoft.EntityFrameworkCore;
using Paramore.Darker;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Ports.Mappers;
using FoodDelivery.Core.ViewModels;
using AutoMapper;

namespace FoodDelivery.Core.Ports.Queries.Handlers
{
	public class AllDeliveryDriversQueryHandler : QueryHandlerAsync<AllDeliveryDriversQuery, AllDeliveryDriversQuery.Result>
	{
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();

		public AllDeliveryDriversQueryHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
            _options = options;
		}

        public override async Task<AllDeliveryDriversQuery.Result> ExecuteAsync(AllDeliveryDriversQuery query, CancellationToken cancellationToken = default)
        {
            await using var uow = new FoodDeliveryDbContext(_options);
            var drivers = await uow.DeliveryDrivers
                .Select(i => _mapper.Map<DeliveryDriverViewModel>(i))
                .ToListAsync(cancellationToken);
            return new AllDeliveryDriversQuery.Result(drivers);
        }
    }
}

