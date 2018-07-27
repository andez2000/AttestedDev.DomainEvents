//using System;
//using Acme.Application.DependencyInjection;
//using Acme.Domain.Events;

//namespace Acme.Infrastructure.Events
//{
//    /// <summary>
//    /// Provides an event dispatching mechanism to registered DI container types.
//    /// </summary>
//    /// <seealso cref="IDomainEventDispatcher" />
//    public class DomainEventContainerDispatcher : IDomainEventDispatcher
//    {
//        private readonly IDependencyResolver _dependencyResolver;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DomainEventContainerDispatcher"/> class.
//        /// </summary>
//        /// <param name="dependencyResolver">The dependency resolver.</param>
//        /// <exception cref="ArgumentNullException">dependencyResolver</exception>
//        public DomainEventContainerDispatcher(IDependencyResolver dependencyResolver)
//        {
//            _dependencyResolver = dependencyResolver ?? throw new ArgumentNullException(nameof(dependencyResolver));
//        }

//        /// <inheritdoc />
//        public void Dispatch<TEventMetaData>(IDomainEvent<TEventMetaData> domainEvent) where TEventMetaData : EventMetaData
//        {
//            Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.Data.GetType());

//            object[] domainEventHandlers = _dependencyResolver.ResolveAll(handlerType);

//            foreach (dynamic domainEventHandler in domainEventHandlers)
//            {
//                domainEventHandler.Handle((dynamic)domainEvent);
//            }
//        }
//    }
//}
