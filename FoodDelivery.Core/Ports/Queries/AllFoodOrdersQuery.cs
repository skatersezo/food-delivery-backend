using FoodDelivery.Core.ViewModels;
using Paramore.Darker;


namespace FoodDelivery.Core.Ports.Queries
{
	public class AllFoodOrdersQuery : IQuery<AllFoodOrdersQuery.Result>
	{
		public AllFoodOrdersQuery()
		{
		}

		public sealed class Result
		{
			public IEnumerable<FoodOrderViewModel> FoodOrders { get; }

			public Result(IEnumerable<FoodOrderViewModel> foodOrders)
            {
				FoodOrders = foodOrders;
			}
		}
	}
}

