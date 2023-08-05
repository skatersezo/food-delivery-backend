using System;


namespace FoodDelivery.Core.Domain
{
    public class DeliveryDriver : IEntity
    {
        public string Name { get; set; }
        public IEnumerable<FoodOrder> Orders = new List<FoodOrder>();
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public int Id { get; set; }
    }
}

