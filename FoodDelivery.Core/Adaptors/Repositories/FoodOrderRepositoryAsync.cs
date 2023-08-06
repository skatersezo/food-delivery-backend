using FoodDelivery.Core.Ports.Repositories;
using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Adaptors.Db;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Core.Adaptors.Repositories
{
    public class FoodOrderRepositoryAsync : IRepositoryAsync<FoodOrder>
	{
		private readonly FoodDeliveryDbContext _uow;

		public FoodOrderRepositoryAsync(FoodDeliveryDbContext uow)
		{
			_uow = uow;
		}

        public async Task<FoodOrder> AddAsync(FoodOrder foodOrder, CancellationToken ct = default)
        {
            var savedItem = _uow.FoodOrders.Add(foodOrder);
            await _uow.SaveChangesAsync(ct);
            return savedItem.Entity;
        }

        public async Task DeleteAsync(int foodOrderId, CancellationToken ct = default)
        {
            var foodOrderItem = await _uow.FoodOrders.SingleAsync(i => i.Id == foodOrderId, ct);
            _uow.Remove(foodOrderItem);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task DeleteAllAsync(CancellationToken ct = default)
        {
            _uow.FoodOrders.RemoveRange(await _uow.FoodOrders.ToListAsync(ct));
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<FoodOrder> GetAsync(int foodOrderId, CancellationToken ct = default)
        {
            return await _uow.FoodOrders.SingleAsync(i => i.Id == foodOrderId, ct);
        }

        public async Task UpdateAsync(FoodOrder updatedFoodOrder, CancellationToken ct = default)
        {
            await _uow.SaveChangesAsync(ct);
        }
    }
}

