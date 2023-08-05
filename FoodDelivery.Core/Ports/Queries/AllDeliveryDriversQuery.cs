using FoodDelivery.Core.ViewModels;
using Paramore.Darker;

namespace FoodDelivery.Core.Ports.Queries
{
    public class AllDeliveryDriversQuery : IQuery<AllDeliveryDriversQuery.Result>
	{
		public AllDeliveryDriversQuery()
		{
		}

		public sealed class Result
		{
			public IEnumerable<DeliveryDriverViewModel> DeliveryDriversViewModel { get; }

			public Result(IEnumerable<DeliveryDriverViewModel> deliveryDrivers)
			{
				DeliveryDriversViewModel = deliveryDrivers;
			}
		}
	}
}

