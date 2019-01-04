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

using SquaredEngine.Diagnostics.Debug;
using SquaredEngine.Diagnostics.Log;

namespace Alone_in_Space {
	public class MovingScreenObject : ScreenObject {
		public Vector2 Direction;
		public float Speed;


		public MovingScreenObject(Game game) : base(game) { }


		public Vector2 GetVelocity(GameTime gameTime) {
			return Direction * Speed * gameTime.ElapsedGameTime.Milliseconds;
		}
	}
}
