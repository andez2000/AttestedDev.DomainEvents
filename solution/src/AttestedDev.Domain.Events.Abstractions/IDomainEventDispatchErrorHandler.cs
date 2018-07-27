using System;

namespace AttestedDev.Domain.Events
{
	/// <summary>
	/// 
	/// </summary>
	public interface IDomainEventDispatchErrorHandler
	{
		void HandleError<TEventMetaData>(object instance, Exception ex, IDomainEvent<TEventMetaData> dominEvent)
			where TEventMetaData : EventMetaData;
	}
}