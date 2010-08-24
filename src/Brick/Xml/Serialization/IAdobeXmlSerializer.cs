using System.Xml;

namespace Brick {
	public interface IAdobeXmlSerializer {
		void Serialize(XmlElement node);
	}
}