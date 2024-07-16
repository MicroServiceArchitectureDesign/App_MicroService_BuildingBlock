using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppMicroServiceBuildingBlock.Shared.Models;
public class BasicPaginationResponse<T>
{
    public BasicPaginationResponse()
    {

    }
    public BasicPaginationResponse(int totalCount, int totalPage, List<T> items)
    {
        TotalCount = totalCount;
        TotalPage = totalPage;
        Items = items;
    }

    public int TotalCount { get; set; }
    public int TotalPage { get; set; }
    public List<T> Items { get; set; } = new();

    public async Task<BasicPaginationResponse<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalPage = (int)Math.Ceiling((decimal)count / pageSize);
        return new BasicPaginationResponse<T>(count, totalPage, items);
    }
}