using System.Collections.Generic;
using System.Linq;

namespace AppMicroServiceBuildingBlock.Shared.Models;

public class BasicPaginationRequest
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public Dictionary<string, string> ColumnKeyValues { get; set; } = new();

    public bool IsValidColumnKeyValues() => ColumnKeyValues.Any();
    public bool IsSingleColumnKeyValues() => ColumnKeyValues.Count > 0 && ColumnKeyValues.Count == 1;
}
