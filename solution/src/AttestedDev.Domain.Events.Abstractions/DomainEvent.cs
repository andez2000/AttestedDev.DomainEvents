using System;

namespace AttestedDev.Domain.Events
{
    /// <summary>
    /// Represents a domain event.
    /// </summary>
    /// <seealso cref="IDomainEvent" />
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        protected DomainEvent(Guid id, int version)
        {
            AggregateId = id;
            AggregateVersionNumber = version;
        }

        /// <inheritdoc />
        public Guid AggregateId { get; private set; }

        /// <inheritdoc />
        public int AggregateVersionNumber { get; internal set; }
    }

    /// <summary>
    /// Represents a domain event with associated event data.
    /// </summary>
    /// <typeparam name="TMetaData">The type of the data.</typeparam>
    /// <seealso cref="IDomainEvent" />
    public class DomainEvent<TMetaData> : DomainEvent, IDomainEvent<TMetaData>
        where TMetaData : EventMetaData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent{TMetaData}"/> class.
        /// </summary>
        /// <param name="aggregateId">The aggregate identifier.</param>
        /// <param name="aggregateVersionNumber">The aggregate version number.</param>
        /// <param name="data">The data.</param>
        public DomainEvent(Guid aggregateId, int aggregateVersionNumber, TMetaData data)
            : base(aggregateId, aggregateVersionNumber)
        {
            Data = data;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the event data.
        /// </summary>
        /// <value>
        /// The event data.
        /// </value>
        public TMetaData Data { get; }
    }
}
