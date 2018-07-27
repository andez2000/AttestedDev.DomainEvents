using System;
using System.Linq;
using Acme.Application.DependencyInjection;
using AttestedDev.Domain.Events;

namespace Acme.Infrastructure.Events
{
    public class DIContainerDomainEventHandlerProvider : IDomainEventHandlerProvider
    {
        private readonly IDependencyResolver _dependencyResolver;

        public DIContainerDomainEventHandlerProvider(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver ?? throw new ArgumentNullException(nameof(dependencyResolver));
        }

        public IDomainEventHandler<TEventMetaData>[] GetAll<TEventMetaData>(Type type) where TEventMetaData : EventMetaData
        {
            return _dependencyResolver.ResolveAll(type)
                .Cast<IDomainEventHandler<TEventMetaData>>()
                .ToArray();
        }
    }
}
