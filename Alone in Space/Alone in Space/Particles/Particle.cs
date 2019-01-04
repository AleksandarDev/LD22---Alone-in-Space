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

namespace Alone_in_Space.Particles {
	public class Particle {
		public Boolean IsAlive = false;

		public virtual Texture2D Texture { get; set; }

		public Color Color;
		public Vector2 Position;
		public Vector2 Origin;
		public float Rotation;
		public Vector2 Scale = Vector2.One;

		public Vector2 Velocity;
		public float RotationAmount;

		public Int32 Lifetime = 0;
		public Int32 CurrentLife = 0;


		public void Update(GameTime gameTime) {
			if (IsAlive) {
				Color.A = (byte)(255 - ((float)CurrentLife / (float)Lifetime * 255));

				Rotation += RotationAmount * gameTime.ElapsedGameTime.Milliseconds;
				Position += Velocity * gameTime.ElapsedGameTime.Milliseconds;

				CurrentLife += gameTime.ElapsedGameTime.Milliseconds;
				if (CurrentLife >= Lifetime) {
					IsAlive = false;
				}
			}
		}

		public void Draw(SpriteBatch sb) {
			if (IsAlive) {
				sb.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
			}
		}
	}
}
