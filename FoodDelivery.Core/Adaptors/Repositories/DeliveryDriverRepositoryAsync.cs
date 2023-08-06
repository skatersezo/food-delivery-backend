using FoodDelivery.Core.Domain;
using FoodDelivery.Core.Ports.Repositories;
using FoodDelivery.Core.Adaptors.Db;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Core.Adaptors.Repositories
{
    public class DeliveryDriverRepositoryAsync : IRepositoryAsync<DeliveryDriver>
	{
        private readonly FoodDeliveryDbContext _uow;

		public DeliveryDriverRepositoryAsync(FoodDeliveryDbContext uow)
		{
            _uow = uow;
		}

        public async Task<DeliveryDriver> AddAsync(DeliveryDriver deliveryDriver, CancellationToken ct = default)
        {
            var savedItem = _uow.DeliveryDrivers.Add(deliveryDriver);
            await _uow.SaveChangesAsync(ct);
            return savedItem.Entity;
        }

        public async Task DeleteAsync(int deliveryDriverId, CancellationToken ct = default)
        {
            var deliveryDriverItem = await _uow.DeliveryDrivers.SingleAsync(i => i.Id == deliveryDriverId);
            _uow.Remove(deliveryDriverItem);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task DeleteAllAsync(CancellationToken ct = default)
        {
            _uow.DeliveryDrivers.RemoveRange(await _uow.DeliveryDrivers.ToListAsync(ct));
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<DeliveryDriver> GetAsync(int deliveryDriverId, CancellationToken ct = default)
        {
            return await _uow.DeliveryDrivers.SingleAsync(i => i.Id == deliveryDriverId);
        }

        public async Task UpdateAsync(DeliveryDriver deliveryDriver, CancellationToken ct = default)
        {
            await _uow.SaveChangesAsync(ct);
        }
    }
}

