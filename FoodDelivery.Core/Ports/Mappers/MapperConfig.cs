using AutoMapper;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.ViewModels;

namespace FoodDelivery.Core.Ports.Mappers
{
    public class MapperConfig
	{
		
		public static Mapper InitializeAutomapper()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<FoodOrder, FoodOrderViewModel>();
				cfg.CreateMap<DeliveryDriver, DeliveryDriverViewModel>()
				.ForMember(i => i.Orders, j => j.MapFrom(h => h.Orders));
			});

			return new Mapper(config);
		}
		
	}
}

