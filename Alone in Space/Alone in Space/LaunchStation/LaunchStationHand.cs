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

namespace Alone_in_Space {
	public class LaunchStationHand : MovingScreenObject {
		LaunchStationBase LaunchBase;


		public LaunchStationHand(Game game, LaunchStationBase launchBase)
			: base(game) {
				LaunchBase = launchBase;
		}


		public override void Initialize() {
			base.Initialize();

			Position = LaunchBase.Position;
		}

		protected override void LoadContent() {
			ContentManager content = new ContentManager(Game.Services);
			content.RootDirectory = "Content";

			sb = new SpriteBatch(GraphicsDevice);

			Texture = content.Load<Texture2D>("Textures/LaunchStationHand");

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
		}
	}
}
