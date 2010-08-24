using System.IO;

namespace Brick.Xml.Serialization {
	public class AdobeXmlSerializerFactory : IAdobeXmlSerializerFactory {
		public IAdobeXmlSerializer New(Stream stream) {
			return new AdobeXmlSerializer(stream);
		}
	}
}