using Paramore.Brighter;


namespace FoodDelivery.Core.Ports.Commands
{
	public class DeleteDeliveryDriverByIdCommand : Command
	{
		public int DriverId { get; }

		public DeleteDeliveryDriverByIdCommand(int driverId) : base(Guid.NewGuid())
		{
			DriverId = driverId;
		}
	}
}

