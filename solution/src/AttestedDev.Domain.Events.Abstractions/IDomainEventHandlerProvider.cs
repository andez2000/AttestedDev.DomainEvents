using System;

namespace AttestedDev.Domain.Events
{
    public interface IDomainEventHandlerProvider
    {
        //IDomainEventHandler<EventMetaData> Get(Type type); // where TEventMetaData : EventMetaData;
        IDomainEventHandler<TEventMetaData>[] GetAll<TEventMetaData>(Type type) where TEventMetaData : EventMetaData;
    }
}
