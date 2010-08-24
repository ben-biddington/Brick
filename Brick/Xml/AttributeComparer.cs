using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Brick.Xml {
	public class AttributeComparer : IComparer {
		public int Compare(Object x, Object y) {
			var lhs = (XmlAttribute)x;
			var rhs = (XmlAttribute)y;
			
			var ls = lhs.NamespaceURI;
			var rs = rhs.NamespaceURI;
			
			if (ls == rs) {
				ls = lhs.LocalName;
				rs = rhs.LocalName;
			}

			var larr = Encoding.UTF8.GetBytes(ls);
			var rarr = Encoding.UTF8.GetBytes(rs);

			var len = Math.Min(larr.Length, rarr.Length);
			
			for (var i = 0; i < len; i++) {
				if (larr[i] < rarr[i])
					return -1;

				if (larr[i] > rarr[i])
					return 1;
			}

			if (larr.Length < rarr.Length)
				return -1;
			
			if (larr.Length > rarr.Length)
				return 1;

			return 0;
		}
	}
}