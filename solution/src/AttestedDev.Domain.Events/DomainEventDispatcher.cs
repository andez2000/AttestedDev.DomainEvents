using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestedDev.Domain.Events
{
	/// <summary>
	/// Defines a mechanism for dispatching events to <see cref="IDomainEventHandler{TEventMetaData}"/> implementations.
	/// </summary>
	/// <seealso cref="AttestedDev.Domain.Events.IDomainEventDispatcher" />
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IDomainEventHandlerProvider _domainEventHandlerProvider;
		private readonly IDomainEventDispatchErrorHandler _domainEventDispatchErrorHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
		/// </summary>
		/// <param name="domainEventHandlerProvider">The domain event handler provider.</param>
		/// <param name="domainEventDispatchErrorHandler">The domain event dispatch error handler.</param>
		/// <exception cref="ArgumentNullException">
		/// domainEventHandlerProvider
		/// or
		/// domainEventDispatchErrorHandler
		/// </exception>
		public DomainEventDispatcher(
			IDomainEventHandlerProvider domainEventHandlerProvider,
			IDomainEventDispatchErrorHandler domainEventDispatchErrorHandler)
		{
			_domainEventHandlerProvider = domainEventHandlerProvider ?? throw new ArgumentNullException(nameof(domainEventHandlerProvider));
			_domainEventDispatchErrorHandler = domainEventDispatchErrorHandler ?? throw new ArgumentNullException(nameof(domainEventDispatchErrorHandler));
		}

		/// <inheritdoc />
		public void Dispatch<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent) where TEventMetaData : EventMetaData
		{
			DispatchEvent(domainEvent);
		}

		/// <inheritdoc />
		public async Task DispatchAsync<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent)
			where TEventMetaData : EventMetaData
		{
			await Task.Run(() => { DispatchEvent(domainEvent); });
		}

		/// <inheritdoc />
		public void DispatchAll<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> domainEvents) 
			where TEventMetaData : EventMetaData
		{
			foreach (IDomainEvent<TEventMetaData> domainEvent in domainEvents)
			{
				DispatchEvent(domainEvent);
			}
		}

		/// <inheritdoc />
		public async Task DispatchAllAsync<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> domainEvents) where TEventMetaData : EventMetaData
		{
			await Task.Run(() =>
			{
				foreach (IDomainEvent<TEventMetaData> domainEvent in domainEvents)
				{
					DispatchEvent(domainEvent);
				}
			});
		}

		private void DispatchEvent<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent)
			where TEventMetaData : EventMetaData
		{
			IDomainEventHandler<TEventMetaData>[] domainEventHandlers =
				FindRegisteredEventHandlersForEvent(domainEvent);

			dynamic currentHandler = null;

			try
			{
				foreach (dynamic domainEventHandler in domainEventHandlers)
				{
					currentHandler = domainEventHandler;

					domainEventHandler.Handle((dynamic)domainEvent);
				}
			}
			catch (Exception ex)
			{
				_domainEventDispatchErrorHandler.HandleError(currentHandler, ex, domainEvent);
			}
		}

		private IDomainEventHandler<TEventMetaData>[] FindRegisteredEventHandlersForEvent<TEventMetaData>(
			IDomainEvent<TEventMetaData> domainEvent) 
			where TEventMetaData : EventMetaData
		{
			Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.Data.GetType());

			var domainEventHandlers = _domainEventHandlerProvider.GetAll<TEventMetaData>(handlerType);

			return domainEventHandlers;
		}
	}
}
