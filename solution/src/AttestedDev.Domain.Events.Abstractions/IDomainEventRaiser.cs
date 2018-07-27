using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestedDev.Domain.Events
{
	/// <summary>
	/// Defines the mechanism for raising events.
	/// </summary>
	public interface IDomainEventRaiser
	{
		/// <summary>
		/// Raises the specified event.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of the event.</typeparam>
		/// <param name="event">The event.</param>
		void Raise<TEventMetaData>(IDomainEvent<TEventMetaData> @event) where TEventMetaData : EventMetaData;

		/// <summary>
		/// Asynchronously raises the specified event.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of the event.</typeparam>
		/// <param name="event">The event.</param>
		/// <returns>A <see cref="Task"/>.</returns>
		Task RaiseAsync<TEventMetaData>(IDomainEvent<TEventMetaData> @event) where TEventMetaData : EventMetaData;

		/// <summary>
		/// Raises all specified events.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of the event.</typeparam>
		/// <param name="events">The events.</param>
		void RaiseAll<TEventMetaData>(IDomainEvent<TEventMetaData>[] events) where TEventMetaData : EventMetaData;

		/// <summary>
		/// Asynchronously raises all specified events.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of the event.</typeparam>
		/// <param name="events">The events.</param>
		Task RaiseAllAsync<TEventMetaData>(IDomainEvent<TEventMetaData>[] events) where TEventMetaData : EventMetaData;


		/// <summary>
		/// Raises all specified events.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of the event.</typeparam>
		/// <param name="events">The events.</param>
		void RaiseAll<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> events)
			where TEventMetaData : EventMetaData;

		/// <summary>
		/// Asynchronously raises all specified events.
		/// </summary>
		/// <typeparam name="TEventMetaData">The type of the event.</typeparam>
		/// <param name="events">The events.</param>
		Task RaiseAllAsync<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> events)
			where TEventMetaData : EventMetaData;

	}
}
