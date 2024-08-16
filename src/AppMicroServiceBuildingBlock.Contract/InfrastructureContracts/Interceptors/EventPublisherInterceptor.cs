using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AppMicroServiceBuildingBlock.Contract.InfrastructureContracts.Interceptors;

public sealed class EventPublisherInterceptor(IMediatorBus publisher) : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            _ = PublishEventsAsync(eventData.Context);
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishEventsAsync(DbContext context)
    {
        var aggregates = context.ChangeTracker.Entries<AggregateRoot>().ToList();

        foreach (var aggregate in aggregates)
        {
            var events = aggregate.Entity.GetEvents();
            foreach (var @event in events)
            {
                await publisher.Publish(@event);
            }

            aggregate.Entity.ClearEvents();
        }
    }
}