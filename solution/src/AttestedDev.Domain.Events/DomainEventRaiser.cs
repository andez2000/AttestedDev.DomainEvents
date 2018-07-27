using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestedDev.Domain.Events
{
	/// <summary>
	/// Provides a mechanism for raising events which are dispatched to registered handlers
	/// via a dispatcher.
	/// </summary>
	/// <seealso cref="IDomainEventRaiser" />
	public class DomainEventRaiser : IDomainEventRaiser
	{
		private readonly IDomainEventDispatcher _domainEventDispatcher;

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainEventRaiser"/> class.
		/// </summary>
		/// <param name="domainEventDispatcher">The domain event dispatcher responsible for dispatching the event.</param>
		/// <exception cref="ArgumentNullException">domainEventDispatcher</exception>
		public DomainEventRaiser(IDomainEventDispatcher domainEventDispatcher)
		{
			_domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
		}

		/// <inheritdoc />
		public void Raise<TEventMetaData>(IDomainEvent<TEventMetaData> @event) where TEventMetaData : EventMetaData
		{
			_domainEventDispatcher.Dispatch(@event);
		}

		/// <inheritdoc />
		public async Task RaiseAsync<TEventMetaData>(IDomainEvent<TEventMetaData> @event) 
			where TEventMetaData : EventMetaData
		{
			await _domainEventDispatcher.DispatchAsync(@event);
		}

		/// <inheritdoc />
		public void RaiseAll<TEventMetaData>(IDomainEvent<TEventMetaData>[] events) 
			where TEventMetaData : EventMetaData
		{
			_domainEventDispatcher.DispatchAll(events);
		}

		/// <inheritdoc />
		public async Task RaiseAllAsync<TEventMetaData>(IDomainEvent<TEventMetaData>[] events) where TEventMetaData : EventMetaData
		{
			await _domainEventDispatcher.DispatchAllAsync(events);
		}

		/// <inheritdoc />
		/// <inheritdoc />
		public void RaiseAll<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> events) 
			where TEventMetaData : EventMetaData
		{
			_domainEventDispatcher.DispatchAll(events);
		}

		public async Task RaiseAllAsync<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> events) where TEventMetaData : EventMetaData
		{
			await _domainEventDispatcher.DispatchAllAsync(events);
		}
	}
}
