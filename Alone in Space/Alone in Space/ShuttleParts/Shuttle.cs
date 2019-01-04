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
using SquaredEngine.Input;

using Alone_in_Space.Particles;

namespace Alone_in_Space {
	public class Shuttle : MovingScreenObject {
		public static float rotationStep = 0.001f;

		public SmokeSource smokeSource;

		public float Altitude = 0;

		public float torque = 1.9f;
		public float maxSpeed = 18f;

		public KeyboardController keyboard;

		public ShuttleRocket FirstRocket;
		public ShuttleRocket SecondRocket;
		public ShuttleTank Tank;
		public FireSource Flame;


		public Shuttle(Game game)
			: base(game) {

		}

		public override void Initialize() {
			keyboard = new KeyboardController(Game);
			Game.Components.Add(keyboard);

			Tank = new ShuttleTank(Game, this);
			FirstRocket = new ShuttleRocket(Game, this);
			SecondRocket = new ShuttleRocket(Game, this);
			// Rocket - Tank - Rocket
			Game.Components.Add(FirstRocket);
			Game.Components.Add(Tank);
			Game.Components.Add(SecondRocket);

			Flame = new FireSource(Game, this);
			FireParticle.color = Color.White;
			Flame.Offset = new Vector2(-11, 116);
			Flame.fireWidth = 2;
			Game.Components.Add(Flame);

			base.Initialize();

			// Smoke in front of rockets and tank
			smokeSource = new SmokeSource(Game);
			smokeSource.Initialize();
			SmokeParticle.color = Color.White;
			smokeSource.SmokeOrigin = new Vector2(SmokeParticle.texture.Width / 2, SmokeParticle.texture.Height / 2);
			smokeSource.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 55, GraphicsDevice.Viewport.Height - 50);
			smokeSource.DrawOrder = Int32.MaxValue;
			Game.Components.Add(smokeSource);

			Direction.Y = 1;
			Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
			//Rotation = (float)(Math.PI / 2);
			Scale = new Vector2(0.65f);
			Position = new Vector2(GraphicsDevice.Viewport.Width / 2, 0);//GraphicsDevice.Viewport.Height + Texture.Width);
			//Effect = SpriteEffects.FlipVertically;
		}

		protected override void LoadContent() {
			ContentManager content = new ContentManager(Game.Services);
			content.RootDirectory = "Content";

			sb = new SpriteBatch(GraphicsDevice);

			Texture = content.Load<Texture2D>("Textures/Shuttle");

			SmokeParticle.texture = content.Load<Texture2D>("Textures/Smoke");
			FireParticle.texture = content.Load<Texture2D>("Textures/FireParticle");
			
			base.LoadContent();
		}


		public override void Update(GameTime gameTime) {
			float currentTorque = torque + FirstRocket.Torque + SecondRocket.Torque + Tank.Torque;

			if (keyboard.IsHeld(Keys.Up)) {
				ApplyTorque(gameTime, currentTorque);
				for (int index = 0; index < 3; index++) {
					FirstRocket.Flame.NewParticle();
					SecondRocket.Flame.NewParticle();
				}
				Flame.NewParticle();

				if (Altitude < 3) {
					smokeSource.NewParticle();
				}
				Random random = new Random();
				ScreenTransform.Transform *= Matrix.CreateTranslation(new Vector3(random.Next(-3, 4) / 3f, random.Next(-3, 4) / 5f, 0));
			}

			if (keyboard.IsHeld(Keys.Down)) {
				ApplyTorque(gameTime, -(torque / 5f));
			}
			if (keyboard.IsHeld(Keys.Left)) {
				ApplyRotation(gameTime, true);
			}
			if (keyboard.IsHeld(Keys.Right)) {
				ApplyRotation(gameTime, false);
			}
			if (keyboard.IsClicked(Keys.Space)) {
				TriggerDetach();
			}

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
			ScreenTransform.Transform *= Matrix.CreateTranslation(new Vector3(velocity / 10f, 0));


			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
		}

		public void ApplyRotation(GameTime gameTime, Boolean isLeft) {
			Rotation += (isLeft ? -rotationStep : rotationStep) * gameTime.ElapsedGameTime.Milliseconds / 20f;
			Direction.Y = (float)Math.Cos(Rotation);
			Direction.X = -(float)Math.Sin(Rotation);
		}

		public void ApplyTorque(GameTime gameTime,  float amount) {
			Speed += amount * gameTime.ElapsedGameTime.Milliseconds / 1000f;
		}

		public void TriggerDetach() {
			if (FirstRocket.IsAttched) {
				FirstRocket.IsAttched = false;
			}
			else if (SecondRocket.IsAttched) {
				SecondRocket.IsAttched = false;
			}
			else if (Tank.IsAttched) {
				Tank.IsAttched = false;
			}
		}

		public override string ToString() {
			return String.Format("Position: {4}\nVelocity: {0}\nTorque: {8}\nAngle: {1}\n\nSmoke particles: {2}/{3}\n\n{5}\n\n{6}\n\n{7}",
				Direction * Speed, Rotation, smokeSource.liveParticles.Count, smokeSource.deadParticles.Count, Position, FirstRocket, SecondRocket, Tank, torque + FirstRocket.Torque + SecondRocket.Torque + Tank.Torque);
		}
	}
}
