using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;
using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;
using Mapster;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedQueryResponse<TResponse>> ToPagedQueryResponse<TResponse, TEntity>(
        this IQueryable<TEntity> query,
        PagedQueryRequest<TResponse> request,
        IDataProtector dataProtector,
        CancellationToken cancellationToken = default)
        where TResponse : BaseLongId, IHasHashedId
        where TEntity : LongEntity
    {
        List<TResponse> items = await query
            .OrderByDescending(p => p.Id)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ProjectToType<TResponse>()
            .ToListAsync(cancellationToken);

        items.ForEach(p =>
        {
            p.HashedId = dataProtector.Protect(p.Id + string.Empty);
        });
        var totalCount = await query.CountAsync(cancellationToken);
        var pagedQuery = new PagedQueryResponse<TResponse>()
        {
            Items = items,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((decimal)totalCount / request.PageSize)
        };
        return pagedQuery;
    }
}