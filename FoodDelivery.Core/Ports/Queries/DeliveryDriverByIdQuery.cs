using Paramore.Darker;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.ViewModels;

namespace FoodDelivery.Core.Ports.Queries
{
    public class DeliveryDriverByIdQuery : IQuery<DeliveryDriverByIdQuery.Result>
	{
		public int Id { get;}

		public DeliveryDriverByIdQuery(int id)
		{
			Id = id;
		}

		public sealed class Result
		{
			public DeliveryDriverViewModel DeliveryDriverViewModel { get; }


            public Result(DeliveryDriver deliveryDriver)
			{
				DeliveryDriverViewModel = new DeliveryDriverViewModel(deliveryDriver.Id, deliveryDriver.Name, deliveryDriver.Latitude, deliveryDriver.Longitude);
			}
		}
	}
}

