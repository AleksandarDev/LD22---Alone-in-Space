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
	public class ScreenObject : DrawableGameComponent {
		public SpriteBatch sb;

		public Texture2D Texture;
		public Color Color = Color.White;
		public Vector2 Position;
		public float Rotation;
		public Vector2 Origin;
		public Vector2 Scale = Vector2.One;
		public SpriteEffects Effect = SpriteEffects.None;


		public ScreenObject(Game game) : base(game) { }


		protected override void LoadContent() {
			sb = new SpriteBatch(GraphicsDevice);
			
			base.LoadContent();
		}


		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, ScreenTransform.Transform);
			sb.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, Effect, 0f);
			sb.End();
		}
	}
}
