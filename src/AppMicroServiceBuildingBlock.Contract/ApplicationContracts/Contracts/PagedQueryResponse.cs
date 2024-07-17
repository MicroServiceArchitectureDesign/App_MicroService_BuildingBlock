namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public class PagedQueryResponse<TResponse>
{
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IReadOnlyList<TResponse> Items { get; set; } = [];
}
