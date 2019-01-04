using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SquaredEngine.Input;
using SquaredEngine.Diagnostics.Debug;
using SquaredEngine.Diagnostics.Log;


namespace Alone_in_Space.Scenes {
	public class GameStart : Scene {
		SpriteBatch spriteBatch;

		TimeDebuger timeDebuger;
		SpriteFont debugFont;

		KeyboardController keyboard;

		Sky gameSky;
		Shuttle player;
		LaunchStationBase launchStation;

		Texture2D grassTexture;
		Vector2 grassPosition;
		Rectangle grassRect;

		UI.NavigationControl Navigation;

		Int32 countdown = 10;
		float countdownTimer = 0;


		public GameStart(Game game) : base(game) {

		}


		public override void Initialize() {
			keyboard = new KeyboardController(Game);
			Game.Components.Add(keyboard);

			gameSky = new Sky(Game);
			Game.Components.Add(gameSky);

			launchStation = new LaunchStationBase(Game);
			Game.Components.Add(launchStation);

			player = new Shuttle(Game);
			Game.Components.Add(player);

			Navigation = new UI.NavigationControl(Game, player);
			Game.Components.Add(Navigation);

			base.Initialize();

			timeDebuger = new TimeDebuger(Game);
			timeDebuger.Initialize();
			timeDebuger.Position = new Vector3(0, 540, 0);
		}

		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			debugFont = Game.Content.Load<SpriteFont>("Fonts/DebugFont");

			grassTexture = Game.Content.Load<Texture2D>("Textures/Grass");
			grassPosition = new Vector2(0, GraphicsDevice.Viewport.Height - 40);
			//grassRect = new Rectangle(0, GraphicsDevice.Viewport.Height - grassTexture.Height, GraphicsDevice.Viewport.Width, grassTexture.Height);

			base.LoadContent();
		}

		protected override void UnloadContent() { }

		public override void Update(GameTime gameTime) {
			if (IsActive) {
				if (countdown > 0) {
					countdownTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
					if (countdownTimer >= 1000f) {
						countdownTimer -= 1000f;
						countdown--;
					}
				}

				if (gameSky.Altitude != player.Altitude)
					gameSky.Altitude = player.Altitude;

#if DEBUG
				if (keyboard.IsClicked(Keys.H)) {
					launchStation.OpenHand();
					launchStation.OpenHook();
				}
#endif

				timeDebuger.Update(gameTime);

				base.Update(gameTime);
			}
		}

		public override void Draw(GameTime gameTime) {
			if (IsActive) {
				//GraphicsDevice.Clear(Color.Black);

				base.Draw(gameTime);

				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null, ScreenTransform.Transform);
				spriteBatch.Draw(grassTexture, grassPosition, new Rectangle(0, GraphicsDevice.Viewport.Height - grassTexture.Height, GraphicsDevice.Viewport.Width, grassTexture.Height), Color.White);
				spriteBatch.End();

				spriteBatch.Begin();
				//spriteBatch.DrawString(debugFont, "Current altitude: " + gameSky.Altitude, new Vector2(20), Color.White);
				//spriteBatch.DrawString(debugFont, "Gravity: " + Helper.GetGravity(gameSky.Altitude), new Vector2(20, 35), Color.White);
				//spriteBatch.DrawString(debugFont, player.ToString(), new Vector2(20, 50), Color.White);

				if (countdown > 0) {
					spriteBatch.DrawString(debugFont, countdown.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Color(255, 0, 0, countdown / 10f));
				}
				else {
					launchStation.OpenHook();
					launchStation.OpenHand();
				}
				spriteBatch.End();


				//timeDebuger.Draw(gameTime);
			}
		}
	}
}
