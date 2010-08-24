using NUnit.Framework;

namespace Brick.Unit.Tests {
	public class UnitTest {
		[TestFixtureSetUp]
		public void TestFixtureSetUp() {
			BeforeAll();
		}

		[SetUp]
		public void SetUp() {
			BeforeEach();
		}

		[TearDown]
		public void TearDown() {
			AfterEach();
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown() {
			AfterAll(); // You're my wonder wall
		}

		protected virtual void BeforeAll() { }
		protected virtual void BeforeEach() { }
		protected virtual void AfterEach() { }
		protected virtual void AfterAll() { }
	}
}
