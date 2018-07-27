using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestedDev.Domain.Events
{
	public class NullDomainEventRaiser : IDomainEventRaiser
	{
		public void Raise<TEventMetaData>(IDomainEvent<TEventMetaData> @event) where TEventMetaData : EventMetaData
		{
		}

		public Task RaiseAsync<TEventMetaData>(IDomainEvent<TEventMetaData> @event) where TEventMetaData : EventMetaData
		{
			return Task.CompletedTask;
		}

		public void RaiseAll<TEventMetaData>(IDomainEvent<TEventMetaData>[] events) where TEventMetaData : EventMetaData
		{
		}

		public async Task RaiseAllAsync<TEventMetaData>(IDomainEvent<TEventMetaData>[] events) where TEventMetaData : EventMetaData
		{
			await Task.CompletedTask;
		}

		public void RaiseAll<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> events) where TEventMetaData : EventMetaData
		{
		}

		public async Task RaiseAllAsync<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> events) where TEventMetaData : EventMetaData
		{
			await Task.CompletedTask;
		}
	}
}
