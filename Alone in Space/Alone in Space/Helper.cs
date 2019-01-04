using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Alone_in_Space {
	public static class Helper {
		public static Vector2 GetGravity(float r) {
			return new Vector2(0, -((9.81f * 100000 * 1) / ((r + 6) * 2500)));
		}
	}
}
