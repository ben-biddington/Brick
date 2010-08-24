using System.Xml;

namespace Brick {
    public interface IXmlSigner {
        XmlDocument Sign(XmlDocument message);
    }
}
