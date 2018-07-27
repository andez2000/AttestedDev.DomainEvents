using System;
using Acme.Domain.Setup;
using Moq;
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
	public class RaiseMultipleEventTests
	{
		private readonly DomainEvent<GoodEventMeta> _goodEvent1 = new DomainEvent<GoodEventMeta>(Guid.Parse("{8343B3D2-2109-4B14-95BD-7DCF88314001}"), 0, new GoodEventMeta());
		private readonly DomainEvent<GoodEventMeta> _goodEvent2 = new DomainEvent<GoodEventMeta>(Guid.Parse("{8343B3D2-2109-4B14-95BD-7DCF88314002}"), 0, new GoodEventMeta());
		private readonly DomainEvent<GoodEventMeta> _goodEvent3 = new DomainEvent<GoodEventMeta>(Guid.Parse("{8343B3D2-2109-4B14-95BD-7DCF88314003}"), 0, new GoodEventMeta());

		[Fact(DisplayName = "Raising multiple events shall be handled when registered")]
		public void Raising_multiple_events_shall_be_handled_when_registered()
		{
			var goodHandler = new Mock<IDomainEventHandler<GoodEventMeta>>();

			var eventRaiser = new DomainEventRaiserSutBuilder()
				.UseTestDomainEventHandlerProvider(a => a.Register(goodHandler.Object))
				.Build();

			eventRaiser.RaiseAll(new IDomainEvent<GoodEventMeta>[] {_goodEvent1, _goodEvent2, _goodEvent3});

			goodHandler.Verify(m => m.Handle(
				It.IsAny<IDomainEvent<GoodEventMeta>>()), Times.Exactly(3));
		}

		[Fact(DisplayName = "Raising multiple events shall be handled when registered async")]
		public void Raising_multiple_events_shall_be_handled_when_registered_async()
		{
			var goodHandler = new Mock<IDomainEventHandler<GoodEventMeta>>();

			var eventRaiser = new DomainEventRaiserSutBuilder()
				.UseTestDomainEventHandlerProvider(a => a.Register(goodHandler.Object))
				.Build();

			eventRaiser.RaiseAll(new IDomainEvent<GoodEventMeta>[] { _goodEvent1, _goodEvent2, _goodEvent3 });

			goodHandler.Verify(m => m.Handle(
				It.IsAny<IDomainEvent<GoodEventMeta>>()), Times.Exactly(3));
		}
	}
}
