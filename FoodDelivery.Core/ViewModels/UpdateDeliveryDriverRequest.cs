using System;
namespace FoodDelivery.Core.ViewModels
{
	public class UpdateDeliveryDriverRequest
	{
        public string? Name { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public ICollection<FoodOrderViewModel>? Orders { get; set; }
    }
}

