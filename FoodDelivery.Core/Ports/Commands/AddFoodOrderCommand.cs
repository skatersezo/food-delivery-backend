using Paramore.Brighter;

namespace FoodDelivery.Core.Ports.Commands
{
	public class AddFoodOrderCommand : Command
	{
        public string FoodName { get; }
        public string CustomerName { get; }
        public bool Delivered { get; }
		public int FoodOrderId { get; set; } //out

        public AddFoodOrderCommand(string foodName, string customerName, bool delivered = false) : base(Guid.NewGuid())
		{
			FoodName = foodName;
			CustomerName = customerName;
			Delivered = delivered;
		}
	}
}

