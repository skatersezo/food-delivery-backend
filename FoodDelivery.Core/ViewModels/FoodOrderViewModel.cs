namespace FoodDelivery.Core.ViewModels
{
    public class FoodOrderViewModel
	{
        public string FoodName { get; }
        public string CustomerName { get; }
        public bool Delivered { get; }
        public int Id { get; }

        public FoodOrderViewModel(int id, string foodName, string customerName, bool delivered)
		{
            FoodName = foodName;
            CustomerName = customerName;
            Delivered = delivered;
            Id = id;
		}
	}
}

