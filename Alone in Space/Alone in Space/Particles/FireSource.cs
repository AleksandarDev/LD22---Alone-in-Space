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

namespace Alone_in_Space.Particles {
	public class FireSource : ParticleSource {
		Random random = new Random();

		public MovingScreenObject ObjectToFollow;

		public Vector2 Offset;

		public Vector2 FireOrigin;
		public Vector2 FireParticleScale = new Vector2(0.4f);

		public Int32 fireMaxVelocity = 10;
		public Int32 fireMinVelocity = 3;
		public Int32 fireMaxRotation = 2;
		public Int32 fireMinRotation = 0;
		public Int32 fireMaxLifetime = 200;
		public Int32 fireMinLifetime = 80;

		public Int16 fireWidth = 10;


		public FireSource(Game game, MovingScreenObject objectToFollow)
			: base(game) {
				ObjectToFollow = objectToFollow;
				RandomizerMethod = FireRandomizer();
		}


		public override void Initialize() {
			base.Initialize();
		}

		protected override void LoadContent() {
			for (int index = 0; index < MaxParticles; index++) {
				deadParticles.Enqueue(new FireParticle());
			}

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			Origin = ObjectToFollow.Origin;
			Position = ObjectToFollow.Position + Offset;
			Rotation = ObjectToFollow.Rotation;

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, ScreenTransform.Transform);
			for (int index = liveParticles.Count - 1; index >= 0; index--) {
				liveParticles[index].Draw(sb);
			}
			sb.End();
		}

		public Action<Particle> FireRandomizer() {
			return new Action<Particle>((p) => {
				p.Position = Position + new Vector2(random.Next(-(fireWidth / 2), fireWidth / 2), 0) + Offset;
				p.Rotation = /*(float)(random.NextDouble() * Math.PI / 20f) + */Rotation;
				p.Velocity = ObjectToFollow.Direction * (new Vector2(random.Next(fireMinVelocity, fireMaxVelocity), random.Next(fireMinVelocity, fireMaxVelocity) * 2) / 100f);
				p.RotationAmount = (float)(random.NextDouble() * random.Next(fireMinRotation, fireMinRotation) / 1000f);
				p.CurrentLife = 0;
				p.Origin = Origin;
				p.Scale = FireParticleScale;
				p.Color = SmokeParticle.color;
				p.Lifetime = random.Next(fireMinLifetime, fireMaxLifetime);
				p.IsAlive = true;
			});
		}
	}
}
