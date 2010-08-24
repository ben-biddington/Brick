using System;
using Brick.DateAndTime;
using NUnit.Framework;

namespace Brick.Unit.Tests {
	[TestFixture]
	public class W3CDateStringExamples {
		private readonly DateTime _castroSwornInAsCubanPrimeMinister = 
			new DateTime(1959, 02, 16, 9, 0, 0, 0);

		private readonly DateTime _kyotoProtocolComesIntoForce =
			new DateTime(2005, 02, 16, 18, 37, 1, 1);

		[Test]
		public void Castro_sworn_in_as_cuban_prime_minister() {
			String result = ToW3CDate(_castroSwornInAsCubanPrimeMinister);

			Assert.That(result, Is.EqualTo("1959-02-16T09:00:00Z"));
		}

		[Test]
		public void Kyoto_protocol_comes_into_force() {
			var result = ToW3CDate(_kyotoProtocolComesIntoForce);

			Assert.That(result, Is.EqualTo("2005-02-16T18:37:01Z"));
		}

		private String ToW3CDate(DateTime what) {
			return W3CDateString.From(what);
		}
	}
}