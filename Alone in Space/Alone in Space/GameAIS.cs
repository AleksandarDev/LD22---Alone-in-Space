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
	public class GameAIS : Game {
		GraphicsDeviceManager graphics;

		Scenes.GameStart gameScene;


		public GameAIS() {
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferHeight = 720;
			graphics.PreferredBackBufferWidth = 1280;
			graphics.IsFullScreen = false;
			//graphics.SynchronizeWithVerticalRetrace = false;
			graphics.ApplyChanges();

			//IsFixedTimeStep = false;

			IsMouseVisible = true;

			Content.RootDirectory = "Content";
		}

		protected override void Initialize() {
			gameScene = new Scenes.GameStart(this);
			gameScene.IsActive = true;
			Components.Add(gameScene);

			base.Initialize();
		}

		protected override void LoadContent() {
			base.LoadContent();

			//gameScene.LoadContent();
		}

		protected override void Update(GameTime gameTime) {
			base.Update(gameTime);

			gameScene.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			gameScene.Draw(gameTime);
		}
	}
}
