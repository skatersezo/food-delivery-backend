using Paramore.Brighter;


namespace FoodDelivery.Core.Ports.Commands
{
	public class DeleteFoodOrderByIdCommand : Command
	{
		public int FoodOrderId { get; }

		public DeleteFoodOrderByIdCommand(int foodOrderId) : base(Guid.NewGuid())
		{
			FoodOrderId = foodOrderId;
		}
	}
}

