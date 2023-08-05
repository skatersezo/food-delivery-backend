using System;
using FoodDelivery.Core.Domain;

namespace FoodDelivery.Core.Ports.Repositories
{
	public interface IRepositoryAsync<T> where T : IEntity
    {
		Task<T> AddAsync(T newEntity, CancellationToken ct = default(CancellationToken));
		Task DeleteAsync(int entityId, CancellationToken ct = default(CancellationToken));
		Task<T> GetAsync(int entityId, CancellationToken ct = default(CancellationToken));
		Task UpdateAsync(T updatedEntity, CancellationToken ct = default(CancellationToken));
	}
}

