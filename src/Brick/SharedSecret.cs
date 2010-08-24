using System;
using System.Security.Cryptography;
using System.Text;

namespace Brick {
	public class SharedSecret {
		public Byte[] From(String password) {
			return SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(password));
		}
	}
}