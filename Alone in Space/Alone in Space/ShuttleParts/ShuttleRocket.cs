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
using Alone_in_Space.Particles;

namespace Alone_in_Space {
	public class ShuttleRocket : MovingScreenObject {		
		public Shuttle Shuttle;
		public Boolean IsAttched = true;

		public float Altitude = 0;

		public FireSource Flame;
		//public Vector2 RocketOffset = new Vector2(85, -30);

		public float Torque {
			get { return IsAttched ? 2.9f : 0f; }
		}


		public ShuttleRocket(Game game, Shuttle shuttle)
			: base(game) {
				Shuttle = shuttle;
		}


		public override void Initialize() {
			Flame = new FireSource(Game, this);
			Flame.Offset = new Vector2(23, 177);
			//Flame.Origin = Origin + Position;
			Flame.fireWidth = 55;
			Flame.fireMaxVelocity = 50;
			Game.Components.Add(Flame);

			base.Initialize();

			Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
			Scale = Shuttle.Scale;
		}

		protected override void LoadContent() {
			ContentManager content = new ContentManager(Game.Services);
			content.RootDirectory = "Content";

			sb = new SpriteBatch(GraphicsDevice);

			Texture = content.Load<Texture2D>("Textures/ShuttleRocket");

			base.LoadContent();
		}


		public override void Update(GameTime gameTime) {
			if (IsAttched) {
				Position = Shuttle.Position;// +RocketOffset;
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

		public void ApplyRotation(float amount) {
			Rotation += amount;
			Direction.Y = (float)Math.Sin(Rotation);
			Direction.X = (float)Math.Cos(Rotation);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
		}

		public override string ToString() {
			return String.Format("Altitude: {0}\nPosition: {3}\nVelocity: {1}\nAngle: {2}",
				Altitude, Speed * Direction, Rotation, Position);
		}
	}
}
