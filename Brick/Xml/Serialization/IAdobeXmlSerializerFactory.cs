using System.IO;

namespace Brick {
	public interface IAdobeXmlSerializerFactory {
		IAdobeXmlSerializer New(Stream stream);
	}
}