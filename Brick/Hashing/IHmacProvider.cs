using System;
using System.Xml;

namespace Brick {
	public interface IHmacProvider {
		Byte[] Hash(XmlElement what);
	}
}