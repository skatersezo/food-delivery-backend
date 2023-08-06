namespace FoodDelivery.Core.ViewModels
{
    public class DeliveryDriverViewModel
	{
        public int Id { get; }
        public string Name { get; }
        public IEnumerable<FoodOrderViewModel> Orders { get; } = new List<FoodOrderViewModel>();
        public long Latitude { get; }
        public long Longitude { get; }
        public string Url { get; set; }

        public DeliveryDriverViewModel(int id, string name)
		{
            Id = id;
            Name = name;
            Latitude = 0;
            Longitude = 0;
		}

        public DeliveryDriverViewModel(int id, string name, long latitude, long longitude)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
	}
}

