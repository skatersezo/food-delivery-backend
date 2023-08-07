using Paramore.Brighter;
using FoodDelivery.Core.ViewModels;


namespace FoodDelivery.Core.Ports.Commands
{
	public class UpdateDeliveryDriverByIdCommand : Command
	{
		public int Id { get; }
		public IEnumerable<FoodOrderViewModel> FoodOrders {	get; }
		public long? Latitude { get; }
		public long? Longitude { get; }

		public UpdateDeliveryDriverByIdCommand(int id, IEnumerable<FoodOrderViewModel> foodOrders, long latitude, long longitude) : base(Guid.NewGuid())
		{
			Id = id;
			FoodOrders = foodOrders;
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}

