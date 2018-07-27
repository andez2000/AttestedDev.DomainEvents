using System;

namespace AttestedDev.Domain.Events
{
    /// <summary>
    /// Defines a domain event.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the aggregate identifier.
        /// </summary>
        /// <value>
        /// The aggregate identifier.
        /// </value>
        Guid AggregateId { get; }

        /// <summary>
        /// Gets the aggregate version number.
        /// </summary>
        /// <value>
        /// The aggregate version number.
        /// </value>
        int AggregateVersionNumber { get; }
    }

    public interface IDomainEvent<out TEventMetaData> : IDomainEvent
    {
        /// <summary>
        /// Gets the event data.
        /// </summary>
        /// <value>
        /// The event data.
        /// </value>
        TEventMetaData Data { get; }
    }
}
