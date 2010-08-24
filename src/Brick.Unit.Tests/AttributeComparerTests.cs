using System.Xml;
using Brick.Xml;
using NUnit.Framework;

namespace Brick.Unit.Tests {
    [TestFixture]
    public class AttributeComparerTests : UnitTest {
        private const int GREATER_THAN = 1;
        private const int LESS_THAN = -1;
        private const int EQUAL = 0;
		XmlDocument xmlDocument;

		protected override void BeforeEach() {
			xmlDocument = new XmlDocument();
		}

        [Test]
        public void Attributes_should_be_considered_the_same_if_local_name_matches() {
			var attribute = NewAttribute("anAttribute");
            
            var result = new AttributeComparer().Compare(attribute, attribute);

            Assert.That(result, Is.EqualTo(EQUAL));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically() {
			var left = NewAttribute("aaaa");
			var right = NewAttribute("bbbb");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(LESS_THAN));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically_2() {
			var left = NewAttribute("bbbb");
			var right = NewAttribute("aaaa");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(GREATER_THAN));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically_3() {
			var left = NewAttribute("aaaa");
			var right = NewAttribute("aaaaa");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(LESS_THAN));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically_4() {
			var left = NewAttribute("aaaaa");
			var right = NewAttribute("aaaa");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(GREATER_THAN));
        }

		private XmlAttribute NewAttribute(string value) {
			return xmlDocument.CreateAttribute(value);
		}
    }
}
