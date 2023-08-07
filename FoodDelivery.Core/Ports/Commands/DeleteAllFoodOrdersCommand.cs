using Paramore.Brighter;


namespace FoodDelivery.Core.Ports.Commands
{
	public class DeleteAllFoodOrdersCommand : Command
	{
		public DeleteAllFoodOrdersCommand() : base(Guid.NewGuid())
		{
		}
	}
}

