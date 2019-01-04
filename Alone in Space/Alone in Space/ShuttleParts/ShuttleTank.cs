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
	public class ShuttleTank : MovingScreenObject {
		public Shuttle Shuttle;
		public Boolean IsAttched = true;

		//public Vector2 TankOffset = new Vector2(80, -70);

		public float Altitude = 0;

		public float Torque {
			get { return IsAttched ? -0.8f : 0f; }
		}


		public ShuttleTank(Game game, Shuttle shuttle)
			: base(game) {
				Shuttle = shuttle;
		}


		public override void Initialize() {			
			base.Initialize();

			Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
			Scale = Shuttle.Scale;
		}

		protected override void LoadContent() {
			ContentManager content = new ContentManager(Game.Services);
			content.RootDirectory = "Content";

			sb = new SpriteBatch(GraphicsDevice);

			Texture = content.Load<Texture2D>("Textures/ShuttleTank");

			base.LoadContent();
		}


		public override void Update(GameTime gameTime) {
			//float gravity = Helper.GetGravity(Shuttle.Altitude).Y * gameTime.ElapsedGameTime.Milliseconds / 10000f;
			//Speed += gravity;

			//if (Shuttle.Altitude <= 0 && Speed < 0) {
			//    Speed = 0;
			//    Shuttle.Altitude = 0;
			//    Position.Y = GraphicsDevice.Viewport.Height - Texture.Height - 50;
			//    ScreenTransform.Transform = Matrix.Identity;
			//}

			//Vector2 velocity = GetVelocity(gameTime);

			//Position -= velocity / 10f;
			//Altitude += velocity.Y / 10000f;
			//ScreenTransform.Transform = Matrix.CreateTranslation(ScreenTransform.Transform.Translation + new Vector3(velocity / 10f, 0));
			if (IsAttched) {
				Position = Shuttle.Position;// +TankOffset;
				Altitude = Shuttle.Altitude;
				Rotation = Shuttle.Rotation;// -(float)(Math.PI / 2);
				Speed = Shuttle.Speed;
				Direction = Shuttle.Direction;
			}
			else {
				float gravity = Helper.GetGravity(Altitude).Y * gameTime.ElapsedGameTime.Milliseconds / 10000f;
				Speed += gravity;


				if (Altitude <= 0 && Speed < 0) {
					Speed = 0;
					Altitude = 0;
					Position.Y = GraphicsDevice.Viewport.Height - 2 * Texture.Height / 5 - 3;
					//ScreenTransform.Transform = Matrix.Identity;
				}

				Vector2 velocity = GetVelocity(gameTime) / 4f;

				Position -= velocity / 10f;
				Altitude += velocity.Y / 10000f;
				//ScreenTransform.Transform *= Matrix.CreateTranslation(new Vector3(velocity / 10f, 0));
			}

			
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
		}
	}
}
