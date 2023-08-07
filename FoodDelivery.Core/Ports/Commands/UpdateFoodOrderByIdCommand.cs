using Paramore.Brighter;


namespace FoodDelivery.Core.Ports.Commands
{
	public class UpdateFoodOrderByIdCommand : Command
	{
		public int FoodOrderId { get; }
		public string? FoodName { get; }
		public string? CustomerName { get; }
		public bool? Delivered { get; }

		public UpdateFoodOrderByIdCommand(int foodOrderId, string? foodName = null, string? customerName = null, bool? delivered = null) : base(Guid.NewGuid())
		{
			FoodOrderId = foodOrderId;
			FoodName = foodName;
			CustomerName = customerName;
			Delivered = delivered;
		}
	}
}

