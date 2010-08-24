using System.Xml;

namespace Brick.Xml.Signing {
    public interface IXmlSigner {
        XmlDocument Sign(XmlDocument message);
    }
}
