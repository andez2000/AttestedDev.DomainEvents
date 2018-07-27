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
    /// These tests are focused on synchronous calls through the raise->dispatch stack where a handler will throw
    /// exceptions to ensure errors are handled accordingly.
    /// </para>
    /// </summary>
    [Trait("Integration Tests", "")]
	public class RaiseSingleEventWithErrorsTests
	{
		private readonly DomainEvent<BadEventMeta> _badEvent = new DomainEvent<BadEventMeta>(
			Guid.Parse("{8343B3D2-2109-4B14-95BD-7DCF88314A0B}"), 0, new BadEventMeta());

		[Fact(DisplayName = "Event dispatcher should handle errors from event handlers")]
		public void Event_dispatch_errors_should_be_handled()
		{
			var errorHandler = new Mock<IDomainEventDispatchErrorHandler>();
			var badHandler = new Mock<IDomainEventHandler<BadEventMeta>>();
			badHandler.Setup(m => m.Handle(
					It.IsAny<IDomainEvent<BadEventMeta>>()))
				.Throws<InvalidOperationException>();

			var eventRaiser = new DomainEventRaiserSutBuilder()
				.UseTestDomainEventHandlerProvider(builder => builder.Register(badHandler.Object))
				.WithDomainEventDispatchErrorHandler(errorHandler.Object)
				.Build();

			eventRaiser.Raise(_badEvent);

			errorHandler.Verify(m => m.HandleError(
				It.Is<IDomainEventHandler<BadEventMeta>>(h => h == badHandler.Object),
				It.IsAny<InvalidOperationException>(),
				It.Is<IDomainEvent<BadEventMeta>>(e => e == _badEvent)));

			badHandler.Verify(m => m.Handle(
				It.IsAny<IDomainEvent<BadEventMeta>>()), Times.Once);
		}

		[Fact(DisplayName = "Event dispatcher should handle errors from event handlers async")]
		public async void Event_dispatch_errors_should_be_handled_async()
		{
			var errorHandler = new Mock<IDomainEventDispatchErrorHandler>();
			var badHandler = new Mock<IDomainEventHandler<BadEventMeta>>();
			badHandler.Setup(m => m.Handle(
					It.IsAny<IDomainEvent<BadEventMeta>>()))
				.Throws<InvalidOperationException>();

			var eventRaiser = new DomainEventRaiserSutBuilder()
				.UseTestDomainEventHandlerProvider(builder => builder.Register(badHandler.Object))
				.WithDomainEventDispatchErrorHandler(errorHandler.Object)
				.Build();

			await eventRaiser.RaiseAsync(_badEvent);

			errorHandler.Verify(m => m.HandleError(
				It.Is<IDomainEventHandler<BadEventMeta>>(h => h == badHandler.Object),
				It.IsAny<InvalidOperationException>(),
				It.Is<IDomainEvent<BadEventMeta>>(e => e == _badEvent)));

			badHandler.Verify(m => m.Handle(
				It.IsAny<IDomainEvent<BadEventMeta>>()), Times.Once);
		}
	}
}
