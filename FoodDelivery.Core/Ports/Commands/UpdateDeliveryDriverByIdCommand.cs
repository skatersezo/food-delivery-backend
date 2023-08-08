using Paramore.Brighter;
using FoodDelivery.Core.ViewModels;


namespace FoodDelivery.Core.Ports.Commands
{
	public class UpdateDeliveryDriverByIdCommand : Command
	{
		public int Id { get; }
		public string? DriverName { get; }
		public IEnumerable<FoodOrderViewModel>? FoodOrders {	get; }
		public long? Latitude { get; }
		public long? Longitude { get; }

		public UpdateDeliveryDriverByIdCommand(int id,string? driverName = null, IEnumerable<FoodOrderViewModel>? foodOrders = null, long? latitude = null, long? longitude = null) : base(Guid.NewGuid())
		{
			Id = id;
			DriverName = driverName;
			FoodOrders = foodOrders;
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}

