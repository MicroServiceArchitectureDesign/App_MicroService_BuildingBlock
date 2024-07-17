using System.Linq.Expressions;
using AppMicroServiceBuildingBlock.Contract.DomainContracts.ValueObjects;

namespace AppMicroServiceBuildingBlock.Contract.InfrastructureContracts.Contracts;

public interface ICommandRepository<TEntity, in TKey>
    where TEntity : AggregateRoot<TKey>
    where TKey : struct,
          IComparable,
          IComparable<TKey>,
          IConvertible,
          IEquatable<TKey>,
          IFormattable
{
    void Delete(TKey id);

    void DeleteGraph(TKey id);

    void Delete(TEntity entity);

    void Insert(TEntity entity);

    Task InsertAsync(TEntity entity);

    TEntity? Get(TKey id);
    Task<TEntity?> GetAsync(TKey id);
    TEntity? Get(BusinessId businessId);
    Task<TEntity?> GetAsync(BusinessId businessId);
    TEntity? GetGraph(TKey id);
    Task<TEntity?> GetGraphAsync(TKey id);
    TEntity? GetGraph(BusinessId businessId);
    Task<TEntity?> GetGraphAsync(BusinessId businessId);
    bool Exists(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
}
