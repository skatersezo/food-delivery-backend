using Paramore.Darker;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.ViewModels;


namespace FoodDelivery.Core.Ports.Queries
{
	public class FoodOrderByIdQuery : IQuery<FoodOrderByIdQuery.Result>
	{
		public int FoodOrderId { get; }

		public FoodOrderByIdQuery(int id)
		{
			FoodOrderId = id;
		}

		public sealed class Result
		{
			public FoodOrderViewModel FoodOrderViewModel { get; }

			public Result(FoodOrder foodOrder)
			{
				FoodOrderViewModel = new FoodOrderViewModel(foodOrder.Id, foodOrder.FoodName, foodOrder.CustomerName, foodOrder.Delivered);
			}
		}
	}
}

