using AttestedDev.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Domain.Setup
{
    public class TestDomainEventHandlerProvider : IDomainEventHandlerProvider
	{
		private readonly Dictionary<Type, List<object>> _registrations = new Dictionary<Type, List<object>>();

		public TestDomainEventHandlerProvider Register<TService>(TService implementation)
		{

			if (!_registrations.TryGetValue(typeof(TService), out List<object> registrations))
			{
				registrations = new List<object>();
				_registrations.Add(typeof(TService), registrations);
			}

			registrations.Add(implementation);

			return this;
		}

		public IDomainEventHandler<TEventMetaData>[] GetAll<TEventMetaData>(Type type) where TEventMetaData : EventMetaData
		{
			var types = _registrations[type].Cast<IDomainEventHandler<TEventMetaData>>();

			return 
				types.ToArray();
		}
	}
}