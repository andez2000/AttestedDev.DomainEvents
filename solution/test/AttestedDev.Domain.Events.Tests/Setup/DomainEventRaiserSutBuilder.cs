using System;
using Acme.Domain.Events;
using Moq;

namespace Acme.Domain.Setup
{
	/// <summary>
	/// Builds the <see cref="DomainEventRaiser"/> instance.
	/// <para>
	/// Given the <see cref="DomainEventRaiser"/> is a single point of contact to the client APIs to raise events
	/// the underlying implementation remains hidden.
	/// </para>
	/// </summary>
	class DomainEventRaiserSutBuilder
	{
		private IDomainEventDispatcher _domainEventDispatcher;
		private IDomainEventHandlerProvider _domainEventHandlerProvider;
		private IDomainEventDispatchErrorHandler _domainEventDispatchErrorHandler;
		private bool _useProvidedDispatcher = false;

		public DomainEventRaiserSutBuilder()
		{
			_domainEventDispatcher = new Mock<IDomainEventDispatcher>().Object;
			_domainEventDispatchErrorHandler = new Mock<IDomainEventDispatchErrorHandler>().Object;
			_domainEventHandlerProvider = new Mock<IDomainEventHandlerProvider>().Object;
		}

		internal DomainEventRaiserSutBuilder WithDomainEventDispatcher(IDomainEventDispatcher domainEventDispatcher)
		{
			_domainEventDispatcher = domainEventDispatcher;
			_useProvidedDispatcher = true;
			return this;
		}

		internal DomainEventRaiserSutBuilder WithDomainEventHandlerProvider(IDomainEventHandlerProvider domainEventHandlerProvider)
		{
			_domainEventHandlerProvider = domainEventHandlerProvider;
			return this;
		}

		internal DomainEventRaiserSutBuilder UseTestDomainEventHandlerProvider(Action<TestDomainEventHandlerProvider> builder)
		{
			var provider = new TestDomainEventHandlerProvider();

			builder.Invoke(provider);

			_domainEventHandlerProvider = provider;

			return this;
		}

		internal DomainEventRaiserSutBuilder WithDomainEventDispatchErrorHandler(IDomainEventDispatchErrorHandler domainEventDispatchErrorHandler)
		{
			_domainEventDispatchErrorHandler = domainEventDispatchErrorHandler;
			return this;
		}


		internal DomainEventRaiser Build()
		{
			DomainEventRaiser raiser = null;

			if (_useProvidedDispatcher)
			{
				raiser = new DomainEventRaiser(_domainEventDispatcher);
			}
			else
			{
				_domainEventDispatcher = new DomainEventDispatcher(_domainEventHandlerProvider, _domainEventDispatchErrorHandler);
				raiser = new DomainEventRaiser(_domainEventDispatcher);
			}

			return raiser;
		}

	}
}
