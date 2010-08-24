using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Brick.Xml.Serialization {
	// TODO: Should this class be disposable or not?
	public class AdobeXmlSerializer : IAdobeXmlSerializer {
		private readonly Stream _outStream;
		private const byte BEGIN_ELEMENT = 1;
		private const byte END_ATTRIBUTES = 2;
		private const byte END_ELEMENT = 3;
		private const byte TEXT_NODE = 4;
		private const byte ATTRIBUTE = 5;

		public AdobeXmlSerializer(Stream stream) {
			_outStream = stream;
		}

		public void Serialize(XmlElement node) {
			if (ShouldIgnore(node))
				return;

			Serialize(BEGIN_ELEMENT);
			Serialize(node.NamespaceURI);
			Serialize(node.LocalName);

			var attrs = new XmlAttribute[node.Attributes.Count];
			node.Attributes.CopyTo(attrs, 0);
			
			Array.Sort(attrs, new AttributeComparer());
			
			foreach (var attr in attrs) {
				Serialize(attr);
			}

			Serialize(END_ATTRIBUTES);

			foreach (XmlNode child in node.ChildNodes) {
				if (child is XmlElement) {
					Serialize(child as XmlElement);
				} else {
					Serialize(child as XmlCharacterData);
				}
			}

			Serialize(END_ELEMENT);
		}

		private void Serialize(XmlNode node) {
		    if (node.NamespaceURI == NamespaceUris.XML) {
		        return;
		    }

		    Serialize(ATTRIBUTE);
		    Serialize(node.NamespaceURI);
		    Serialize(node.LocalName);
		    Serialize(node.Value);
		}

		private void Serialize(XmlCharacterData node) {
			var val = node.Value.Trim();
			var len = val.Length;
		    
            if (len <= 0) {
		        return;
		    }

		    var done = 0;
		    do {
		        var remains = Math.Min(len - done, 0x7FFF);
		        Serialize(TEXT_NODE);
		        Serialize(val.Substring(done, remains));
		        done += remains;
		    } while (done < len);
		}

		private void Serialize(String s) {
		    var bytes = Encoding.UTF8.GetBytes(s);
            Serialize((byte)(bytes.Length >> 8));
            Serialize((byte)(bytes.Length & 0xFF));
		    Serialize(bytes);
		}

		private void Serialize(byte b) {
			_outStream.WriteByte(b);
		}

		private void Serialize(byte[] array) {
			_outStream.Write(array, 0, array.Length);
		}

		private static bool ShouldIgnore(XmlNode what) {
			return (what.NamespaceURI == NamespaceUris.ADEPT) && 
                (what.LocalName == NodeNames.HMAC || what.LocalName == NodeNames.SIGNATURE);
		}
	}
}