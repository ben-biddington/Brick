using System.Xml;
using Brick.Xml;
using NUnit.Framework;

namespace Brick.Unit.Tests {
    [TestFixture]
    public class AttributeComparerTests {
        private const int GREATER_THAN = 1;
        private const int LESS_THAN = -1;
        private const int EQUAL = 0;

        [Test]
        public void Attributes_should_be_considered_the_same_if_local_name_matches() {
            var xmlDocument = new XmlDocument();
            var attribute = xmlDocument.CreateAttribute("anAttribute");
            
            var result = new AttributeComparer().Compare(attribute, attribute);

            Assert.That(result, Is.EqualTo(EQUAL));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically() {
            var xmlDocument = new XmlDocument();

            var left = xmlDocument.CreateAttribute("aaaa");
            var right = xmlDocument.CreateAttribute("bbbb");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(LESS_THAN));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically_2() {
            var xmlDocument = new XmlDocument();
            var left = xmlDocument.CreateAttribute("bbbb");
            var right = xmlDocument.CreateAttribute("aaaa");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(GREATER_THAN));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically_3() {
            var xmlDocument = new XmlDocument();
            var left = xmlDocument.CreateAttribute("aaaa");
            var right = xmlDocument.CreateAttribute("aaaaa");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(LESS_THAN));
        }

        [Test]
        public void Attributes_should_be_compared_alphabetically_4() {
            var xmlDocument = new XmlDocument();
            var left = xmlDocument.CreateAttribute("aaaaa");
            var right = xmlDocument.CreateAttribute("aaaa");

            var result = new AttributeComparer().Compare(left, right);

            Assert.That(result, Is.EqualTo(GREATER_THAN));
        }
    }
}
