using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using Brick.Xml.Serialization;

namespace Brick {
	public class DefaultHmacProvider : IHmacProvider {
		private readonly Byte[] _sharedSecret;
		private readonly IAdobeXmlSerializerFactory _serializerFactory;

		public DefaultHmacProvider(byte[] sharedSecret) : 
			this(sharedSecret, new AdobeXmlSerializerFactory()) {}

		public DefaultHmacProvider(
			byte[] sharedSecret, 
			IAdobeXmlSerializerFactory serializerFactory
		) {
			_sharedSecret = sharedSecret;
			_serializerFactory = serializerFactory;
		}

		public byte[] Hash(XmlElement what) {
			var hmac = new HMACSHA1(_sharedSecret);

			using (var stream = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write)) {
				With(stream).Serialize(what);
			}

			return hmac.Hash;
		}

		private IAdobeXmlSerializer With(Stream stream) {
			return _serializerFactory.New(stream);
		}
	}
}