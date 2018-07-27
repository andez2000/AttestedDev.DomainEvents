using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestedDev.Domain.Events
{
	/// <summary>
	/// Defines a dispatcher of domain events.
	/// </summary>
	public interface IDomainEventDispatcher
	{
		/// <summary>
		/// Dispatches the specified domain event.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of domain event.</typeparam>
		/// <param name="domainEvent">The domain event.</param>
		void Dispatch<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent) where TEventMetaData : EventMetaData;

		/// <summary>
		/// Dispatches the specified domain event.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of domain event.</typeparam>
		/// <param name="domainEvent">The domain event.</param>
		/// <returns>A <see cref="Task"/>.</returns>
		Task DispatchAsync<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent) where TEventMetaData : EventMetaData;

		/// <summary>
		/// Dispatches the specified domain events.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of domain event.</typeparam>
		/// <param name="domainEvents">The domain events.</param>
		void DispatchAll<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> domainEvents) where TEventMetaData : EventMetaData;

		/// <summary>
		/// Dispatches the specified domain events.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of domain event.</typeparam>
		/// <param name="domainEvents">The domain events.</param>
		/// <returns>A <see cref="Task"/>.</returns>
		Task DispatchAllAsync<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> domainEvents) where TEventMetaData : EventMetaData;
	}
}
