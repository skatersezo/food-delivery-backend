using System;
using FoodDelivery.Core.Domain;

namespace FoodDelivery.Core.Adaptors.Db
{
    public static class DbInitializer
    {
        public static void Initialize(FoodDeliveryDbContext context)
        {
            
            if (context.FoodOrders.Any() || context.DeliveryDrivers.Any())
            {
                return;   // DB has been seeded
            }

            var foodOrders = new FoodOrder[]
            {
                new FoodOrder { FoodName="Fish & Chips", CustomerName="Alexander", Delivered=false },
                new FoodOrder { FoodName="Burger", CustomerName="Emily", Delivered=true },
                new FoodOrder { FoodName="Pizza", CustomerName="Daniel", Delivered=false },
                new FoodOrder { FoodName="Sushi", CustomerName="Sophia", Delivered=true },
                new FoodOrder { FoodName="Pasta", CustomerName="Liam", Delivered=false },
                new FoodOrder { FoodName="Salad", CustomerName="Olivia", Delivered=true },
                new FoodOrder { FoodName="Steak", CustomerName="Aiden", Delivered=false },
                new FoodOrder { FoodName="Tacos", CustomerName="Ava", Delivered=true },
                new FoodOrder { FoodName="Chicken Wings", CustomerName="Jackson", Delivered=false },
                new FoodOrder { FoodName="Ramen", CustomerName="Isabella", Delivered=true },
                new FoodOrder { FoodName="Sandwich", CustomerName="Mason", Delivered=false },
                new FoodOrder { FoodName="Curry", CustomerName="Emma", Delivered=true },
                new FoodOrder { FoodName="Hot Dog", CustomerName="Liam", Delivered=false },
                new FoodOrder { FoodName="Sushi", CustomerName="Mia", Delivered=true },
                new FoodOrder { FoodName="Pasta", CustomerName="Ethan", Delivered=false },
                new FoodOrder { FoodName="Salad", CustomerName="Charlotte", Delivered=true },
            };

            context.FoodOrders.AddRange(foodOrders);
            context.SaveChanges();

            var drivers = new DeliveryDriver[]
            {
                new DeliveryDriver { Name="John", Latitude=12345, Longitude=4563 },
                new DeliveryDriver { Name="Emily", Latitude=23456, Longitude=5674 },
                new DeliveryDriver { Name="Daniel", Latitude=34567, Longitude=6785 },
                new DeliveryDriver { Name="Sophia", Latitude=45678, Longitude=7896 },
                new DeliveryDriver { Name="Liam", Latitude=56789, Longitude=8907 },
                new DeliveryDriver { Name="Olivia", Latitude=67890, Longitude=9018 },
                new DeliveryDriver { Name="Aiden", Latitude=78901, Longitude=1234 },
                new DeliveryDriver { Name="Ava", Latitude=89012, Longitude=2345 },
                new DeliveryDriver { Name="Jackson", Latitude=90123, Longitude=3456 },
                new DeliveryDriver { Name="Isabella", Latitude=12345, Longitude=4567 },
                new DeliveryDriver { Name="Mason", Latitude=23456, Longitude=5678 },
                new DeliveryDriver { Name="Emma", Latitude=34567, Longitude=6789 },
            };

            context.DeliveryDrivers.AddRange(drivers);
            context.SaveChanges();

            
        }
    }
}

