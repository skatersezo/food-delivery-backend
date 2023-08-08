using System;
namespace FoodDelivery.Core.Domain
{
	public class FoodOrder : IEntity
	{
		public string FoodName { get; set; }
		public string CustomerName { get; set; }
		public bool Delivered { get; set; }
		public int Id { get; set; }

		public DeliveryDriver Driver { get; set; }
		public int DriverId { get; set; }
	}
}

