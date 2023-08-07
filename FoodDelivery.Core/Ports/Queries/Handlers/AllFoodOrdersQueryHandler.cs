using Microsoft.EntityFrameworkCore;
using Paramore.Darker;
using FoodDelivery.Core.Adaptors.Db;
using FoodDelivery.Core.Ports.Mappers;
using FoodDelivery.Core.ViewModels;
using AutoMapper;

namespace FoodDelivery.Core.Ports.Queries.Handlers
{
	public class AllFoodOrdersQueryHandler : QueryHandlerAsync<AllFoodOrdersQuery,AllFoodOrdersQuery.Result>
	{
        private readonly DbContextOptions<FoodDeliveryDbContext> _options;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();

        public AllFoodOrdersQueryHandler(DbContextOptions<FoodDeliveryDbContext> options)
		{
			_options = options;
		}

        public override async Task<AllFoodOrdersQuery.Result> ExecuteAsync(AllFoodOrdersQuery query, CancellationToken cancellationToken = default)
        {
            await using var uow = new FoodDeliveryDbContext(_options);
            var orders = await uow.FoodOrders
                .Select(i => _mapper.Map<FoodOrderViewModel>(i))
                .ToListAsync(cancellationToken);
            return new AllFoodOrdersQuery.Result(orders);
        }
    }
}

