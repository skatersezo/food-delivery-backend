using Paramore.Brighter;


namespace FoodDelivery.Core.Ports.Commands
{
	public class DeleteAllDeliveryDriversCommand : Command
	{
		public DeleteAllDeliveryDriversCommand() : base(Guid.NewGuid())
		{
		}
	}
}

