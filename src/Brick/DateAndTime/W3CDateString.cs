using System;

namespace Brick.DateAndTime {
	public static class W3CDateString {
		public static String From(System.DateTime value) {
			return value.ToUniversalTime().ToString("u").Replace(" ", "T");
		}
	}
}