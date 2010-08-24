using System;
using System.Xml;
using Brick.DateAndTime;

namespace Brick {
    public class XmlSigner : IXmlSigner {
        private readonly String _password;

        public XmlSigner(String password) {
            _password = password;
        }

        public XmlDocument Sign(XmlDocument message) {
        	return Sign(message, NewNonce(), DefaultExpiresAt);
        }

    	private String NewNonce() {
    		return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    	}

    	public XmlDocument Sign(
            XmlDocument message,
            String nonce,
            DateTime expirationTime
        ) {
            if (message.DocumentElement == null)
                throw new ArgumentException("No root node in xdoc.");
            
            var copy = (XmlDocument)message.CloneNode(true);

            SignMessage(
				copy.DocumentElement, 
				Convert.FromBase64String(nonce), 
				expirationTime,
                new SharedSecret().From(_password)
			);

            return copy;
        }

        private static void SignMessage(
            XmlElement message,
            byte[] nonce,
            DateTime expirationTime,
            byte[] secret
        ) {
            var child = message.OwnerDocument.CreateElement(
                NodeNames.EXPIRATION,
                NamespaceUris.ADEPT
            );

            child.InnerText = W3CDateString.From(expirationTime);

            message.AppendChild(child);

            child = message.OwnerDocument.CreateElement(
                NodeNames.NONCE,
                NamespaceUris.ADEPT
            );

            child.InnerText = Convert.ToBase64String(nonce);
            message.AppendChild(child);

            var hmac = GetHmac(message, secret);

            child = message.OwnerDocument.CreateElement(
                NodeNames.HMAC,
                NamespaceUris.ADEPT
            );

            child.InnerText = Convert.ToBase64String(hmac);

            message.AppendChild(child);
        }

		private static DateTime DefaultExpiresAt {
			get { return DateTime.Now.AddMinutes(15); }
		}

        private static Byte[] GetHmac(XmlElement message, byte[] sharedSecret) {
            var hasher = new DefaultHmacProvider(sharedSecret);
            return hasher.Hash(message);
        }
    }
}