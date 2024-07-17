using System.Linq.Expressions;
using AppMicroServiceBuildingBlock.Contract.DomainContracts.ValueObjects;

namespace AppMicroServiceBuildingBlock.Contract.InfrastructureContracts.Contracts;

public interface IQueryRepository<TEntity, in TKey>
    where TEntity : Entity<TKey>
    where TKey : struct, IEquatable<TKey>

{
    TEntity? Get(TKey id);
    TEntity? Get(BusinessId businessId);
    TEntity? GetGraph(TKey id);
    bool Exists(Expression<Func<TEntity, bool>> expression);
    TEntity? GetGraph(BusinessId businessId);
    Task<TEntity?> GetAsync(TKey id);
    Task<TEntity?> GetGraphAsync(TKey id);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
}

