namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public class PagedQueryRequest<TResponse> : IRequest<Result<PagedQueryResponse<TResponse>>>
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    internal int Skip => (PageNumber - 1) * PageSize;
}
