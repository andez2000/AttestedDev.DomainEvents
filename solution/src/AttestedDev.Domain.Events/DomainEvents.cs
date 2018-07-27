using System;

namespace AttestedDev.Domain.Events
{
	/// <summary>
	/// Provides a singleton for raising events.
	/// </summary>
	public class DomainEvents
	{
		private static readonly DomainEvents Instance = new DomainEvents();

		private IDomainEventRaiser InnerCurrent { get; set; }

		static DomainEvents()
		{
			SetRaiser(new NullDomainEventRaiser());
		}

		private DomainEvents()
		{
		}

		/// <summary>
		/// Gets the current of domain event raiser.
		/// </summary>
		public static IDomainEventRaiser Current => Instance.InnerCurrent;

		/// <summary>
		/// Sets the raiser to be used as current domain event raiser.
		/// </summary>
		/// <param name="raiser">The raiser.</param>
		/// <exception cref="System.ArgumentNullException">raiser;Argument cannot be null</exception>
		public static void SetRaiser(IDomainEventRaiser raiser)
		{
			Instance.InnerCurrent = raiser ?? throw new ArgumentNullException(nameof(raiser), "Argument cannot be null");
		}
	}
}
