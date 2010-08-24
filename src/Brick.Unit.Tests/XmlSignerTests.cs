using System;
using System.IO;
using System.Xml;
using NUnit.Framework;

namespace Brick.Unit.Tests {
	[TestFixture]
    public class XmlSignerTests : UnitTest {
        private const String EPUB_FILE = @"res\theArtOfWar.epub";
		private const String PASSWORD = "barada";
	    private readonly DateTime _anyExpirationTime = new DateTime(2010, 02, 16, 11, 24, 41);

	    private XmlDocument Doc { get; set; }

	    protected override void BeforeEach() {
			Doc = NewPackagingRequest(EPUB_FILE);
		}
	
		[Test]
        public void Check_hash() {
            var doc = NewPackagingRequest(EPUB_FILE);

        	const String nonce = "TaPMeKEjVEOJrFm/KLG9AA==";
            var expirationTime = new DateTime(2010, 02, 16, 11, 24, 41);
            
			var result = new XmlSigner(PASSWORD).Sign(
				doc, 
                nonce,
				expirationTime
			);

			Assert.That(
				result.DocumentElement.LastChild.InnerText, Is.EqualTo("5RdxB1z60V3QmIPbnVUddAMrrUs="), 
				"The HMAC was not calculated as expected."
			);
        }

        [Test]
        public void Given_an_epub_file_then_when_signed_it_has_the_correct_signature() {
            const String nonce = "TaPMeKEjVEOJrFm/KLG9AA==";

            var xmlSigner = new XmlSigner(PASSWORD);

            var result = xmlSigner.Sign(Doc, nonce, _anyExpirationTime);

            Assert.That(
				result.DocumentElement.LastChild.InnerText, Is.EqualTo("5RdxB1z60V3QmIPbnVUddAMrrUs="), 
				"The HMAC was not calculated as expected."
			);
        }

		[Test]
		public void Sign_throws_format_exceoption_unless_nonce_is_base_64_encoded() {
			var xmlSigner = new XmlSigner(PASSWORD);

			var nonce = Guid.NewGuid().ToString();
			
			const String expectedMessage = "Invalid character in a Base-64 string.";

			Assert.That(
				() => xmlSigner.Sign(Doc, nonce, _anyExpirationTime), 
				Throws.TypeOf(typeof(FormatException)).
				With.Property("Message").EqualTo(expectedMessage)
			);
		}

		[Test]
		public void Sign_returns_a_new_xml_document() {
			var xmlSigner = new XmlSigner(PASSWORD);

            const String nonce = "TaPMeKEjVEOJrFm/KLG9AA==";

			var result = xmlSigner.Sign(Doc, nonce, _anyExpirationTime);

			Assert.That(
				result, 
				Is.Not.SameAs(Doc.DocumentElement), 
				"Expected Sign to return a new instance, not update the supplied one."
			);
		}

        private static XmlDocument NewPackagingRequest(String file) {
            var doc = new XmlDocument();
			var package = doc.CreateElement("package", NamespaceUris.ADEPT);
            doc.AppendChild(package);

			var dataPath = doc.CreateElement("data", NamespaceUris.ADEPT);
            dataPath.InnerText = Base64EncodeEbook(file);
            package.AppendChild(dataPath);

            return doc;
        }

        private static String Base64EncodeEbook(String file) {
        	return Convert.ToBase64String(File.ReadAllBytes(file));
        }
    }
}
