using AppMicroServiceBuildingBlock.Contract.DomainContracts.Events;

namespace AppMicroServiceBuildingBlock.Contract.InfrastructureContracts.Contracts;

public interface IDomainEventStore
{
    void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) 
        where TEvent : IDomainEvent;
    Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) 
        where TEvent : IDomainEvent;
}

