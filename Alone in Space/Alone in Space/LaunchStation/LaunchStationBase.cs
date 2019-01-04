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
	public class LaunchStationBase : ScreenObject {
		public LaunchStationHand Hand;
		public LaunchStationHook Hook;

		public Int32 HookOpeningTime = 700;
		public Int32 HookOpeningTimeCurrent = 0;
		public Boolean IsHookOpening = false;

		public Int32 HandOpeningTime = 1300;
		public Int32 HandOpeningTimeCurrent = 0;
		public Boolean IsHandOpening = false;


		public LaunchStationBase(Game game)
			: base(game) {

		}


		public override void Initialize() {
			Hand = new LaunchStationHand(Game, this);
			Hook = new LaunchStationHook(Game, this);
			Game.Components.Add(Hook);
			Game.Components.Add(Hand);

			base.Initialize();

			Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - 2 * Texture.Width / 3 - 9, GraphicsDevice.Viewport.Height - Texture.Height);
		}

		protected override void LoadContent() {
			ContentManager content = new ContentManager(Game.Services);
			content.RootDirectory = "Content";

			sb = new SpriteBatch(GraphicsDevice);

			Texture = content.Load<Texture2D>("Textures/LaunchStationBase");

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			if (IsHookOpening) {
				HookOpeningTimeCurrent += gameTime.ElapsedGameTime.Milliseconds;
				if (HookOpeningTimeCurrent >= HookOpeningTime) {
					IsHookOpening = false;
				}
				else {
					float val = (float)HookOpeningTimeCurrent / (float)HookOpeningTime;
					Hook.Rotation = (float)(-Math.PI / 2.0 * val);
				}
			}

			if (IsHandOpening) {
				HandOpeningTimeCurrent += gameTime.ElapsedGameTime.Milliseconds;
				if (HandOpeningTimeCurrent >= HandOpeningTime) {
					IsHandOpening = false;
				}
				else {
					float val = (float)HandOpeningTimeCurrent / (float)HandOpeningTime;
					Hand.Position.X = Position.X + (float)(-110 * val);
					Hook.Position.X = Hand.Position.X + Hook.Origin.X;
				}
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
		}

		public void OpenHook() {
			IsHookOpening = true;
		}

		public void OpenHand() {
			IsHandOpening = true;
		}
	}
}
