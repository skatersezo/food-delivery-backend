using Paramore.Brighter;

namespace FoodDelivery.Core.Ports.Commands
{
	public class AddDeliveryDriverCommand : Command
	{
		public string Name { get; }
		public int DeliveryDriverId { get; set; } // out prop

		public AddDeliveryDriverCommand(string name) : base(Guid.NewGuid())
		{
			Name = name;
		}
	}
}

