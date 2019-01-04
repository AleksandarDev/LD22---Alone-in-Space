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

namespace Alone_in_Space.UI {
	public class NavigationControl : ScreenObject {
		public Shuttle Shuttle;
		public Texture2D PointTexture;


		public NavigationControl(Game game, Shuttle shuttle)
			: base(game) {
				Shuttle = shuttle;
		}


		public override void Initialize() {			
			base.Initialize();

			Scale = new Vector2(0.8f);
			Position = new Vector2(GraphicsDevice.Viewport.Width - Texture.Width * Scale.X - 25, GraphicsDevice.Viewport.Height - Texture.Height * Scale.Y - 25);
			//Origin = new Vector2(Texture.Width / 2 * Scale.X, Texture.Height / 2 * Scale.Y);
		}

		protected override void LoadContent() {
			ContentManager content = new ContentManager(Game.Services);
			content.RootDirectory = "Content";

			sb = new SpriteBatch(GraphicsDevice);

			Texture = content.Load<Texture2D>("Textures/UI/UINavigation");
			PointTexture = content.Load<Texture2D>("Textures/PointTexture");

			base.LoadContent();
		}

		public override void Draw(GameTime gameTime) {
			sb.Begin();
			sb.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
			sb.Draw(PointTexture, Position + new Vector2(Texture.Width / 2, Texture.Height / 2) * Scale + new Vector2(Shuttle.Altitude, -(Shuttle.Altitude)) * Scale, Color.Red);
			sb.End();			
		}
	}
}
