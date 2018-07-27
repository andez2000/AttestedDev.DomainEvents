using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttestedDev.Domain.Events
{
	public class NullDomainEventDispatcher : IDomainEventDispatcher
	{
		public void Dispatch<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent) where TEventMetaData : EventMetaData
		{
		}

		public async Task DispatchAsync<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent) where TEventMetaData : EventMetaData
		{
			await Task.CompletedTask;
		}

		public void DispatchAll<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> domainEvents) where TEventMetaData : EventMetaData
		{
		}

		public async Task DispatchAllAsync<TEventMetaData>(IEnumerable<IDomainEvent<TEventMetaData>> domainEvents) where TEventMetaData : EventMetaData
		{
			await Task.CompletedTask;
		}
	}
}
