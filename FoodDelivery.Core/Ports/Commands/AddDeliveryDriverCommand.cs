using Paramore.Brighter;
using FoodDelivery.Core.ViewModels;

namespace FoodDelivery.Core.Ports.Commands
{
	public class AddDeliveryDriverCommand : Command
	{
		public string Name { get; }
		public IEnumerable<FoodOrderViewModel>? Orders { get; }
		public int DeliveryDriverId { get; set; } // out prop

		public AddDeliveryDriverCommand(string name, IEnumerable<FoodOrderViewModel>? orders = null) : base(Guid.NewGuid())
		{
			Name = name;
			Orders = orders;
		}
	}
}

