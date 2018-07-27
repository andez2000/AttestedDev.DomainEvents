using Acme.Domain.Setup;
using AttestedDev.Domain.Events;
using Moq;
using System;
using Xunit;

namespace Acme.Domain.Events
{
    /// <summary>
    /// Tests around the domain event raising and dispatching.
    /// <para>
    /// This is to support both synchronous and asynchronous calls through the raise->dispatch stack.
    /// </para>
    /// </summary>
    [Trait("Integration Tests", "")]
	public class RaiseSingleEventTests
	{
		private readonly DomainEvent<GoodEventMeta> _goodEvent = new DomainEvent<GoodEventMeta>(Guid.Parse("{8343B3D2-2109-4B14-95BD-7DCF88314A0B}"), 0, new GoodEventMeta());

		[Fact(DisplayName = "Raising a single event shall be handled when registered")]
		public void Raising_a_single_event_shall_be_handled_when_registered()
		{
			var goodHandler = new Mock<IDomainEventHandler<GoodEventMeta>>();

			var eventRaiser = new DomainEventRaiserSutBuilder()
				.UseTestDomainEventHandlerProvider(a => a.Register(goodHandler.Object))
				.Build();

			eventRaiser.Raise(_goodEvent);

			goodHandler.Verify(m => m.Handle(
				It.IsAny<IDomainEvent<GoodEventMeta>>()), Times.Once);
		}

		[Fact(DisplayName = "Raising a single event shall be handled when registered async")]
		public async void Raising_a_single_event_shall_be_handled_when_registered_async()
		{
			var goodHandler = new Mock<IDomainEventHandler<GoodEventMeta>>();

			var eventRaiser = new DomainEventRaiserSutBuilder()
				.UseTestDomainEventHandlerProvider(a => a.Register(goodHandler.Object))
				.Build();

			await eventRaiser.RaiseAsync(_goodEvent);

			goodHandler.Verify(m => m.Handle(
				It.IsAny<IDomainEvent<GoodEventMeta>>()), Times.Once);
		}
	}
}
