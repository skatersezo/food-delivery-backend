using System;


namespace FoodDelivery.Core.Domain
{
    public class DeliveryDriver : IEntity
    {
        public string Name { get; set; }
        public ICollection<FoodOrder> Orders { get; set; } 
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public int Id { get; set; }
    }
}

