using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Alone_in_Space.Particles {
	public class FireParticle : Particle {
		public static Texture2D texture;
		public override Texture2D Texture {
			get { return texture; }
			set { texture = value; }
		}

		public static Color color;
	}
}
