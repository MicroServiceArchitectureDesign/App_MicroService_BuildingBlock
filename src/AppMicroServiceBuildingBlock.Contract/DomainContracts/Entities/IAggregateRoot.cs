using AppMicroServiceBuildingBlock.Contract.DomainContracts.Events;

namespace AppMicroServiceBuildingBlock.Contract.DomainContracts.Entities;

public interface IAggregateRoot
{
    void ClearEvents();
    IEnumerable<IDomainEvent> GetEvents();
}