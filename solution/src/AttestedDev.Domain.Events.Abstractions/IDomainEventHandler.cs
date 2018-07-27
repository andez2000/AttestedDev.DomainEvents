namespace AttestedDev.Domain.Events
{
    /// <summary>
    /// Defines the interface for handling domain events.
    /// </summary>
    /// <typeparam name="TEventMetaData">The type of domain event.</typeparam>
    public interface IDomainEventHandler<TEventMetaData> where TEventMetaData : EventMetaData
    {
        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        void Handle(IDomainEvent<TEventMetaData> domainEvent);
    }
}
