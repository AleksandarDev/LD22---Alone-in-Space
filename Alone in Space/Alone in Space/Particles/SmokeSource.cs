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
	public class SmokeSource : ParticleSource {
		public static Random random = new Random();

		public Vector2 SmokeOrigin;

		public Int32 smokeMaxVelocity = 10;
		public Int32 smokeMinVelocity = -7;
		public Int32 smokeMaxRotationAmount = 6;
		public Int32 smokeMinRotationAmount = 3;
		public Int32 smokeMinLifetime = 500;
		public Int32 smokeMaxLifetime = 700;



		public SmokeSource(Game game)
			: base(game) {
				RandomizerMethod = SmokeRandomizer();
		}


		public override void Initialize() {
			base.Initialize();
		}

		protected override void LoadContent() {
			for (int index = 0; index < MaxParticles; index++) {
				deadParticles.Enqueue(new SmokeParticle());
			}
			
			base.LoadContent();
		}

		public Action<Particle> SmokeRandomizer() {
			return new Action<Particle>((p) => {
				p.Position = this.Position + Vector2.One * random.Next(smokeMinVelocity, smokeMaxVelocity);
				p.Rotation = (float)(random.NextDouble() * Math.PI * 2);
				p.Velocity = new Vector2(random.Next(smokeMinVelocity, smokeMaxVelocity) / 20f - 0.25f, random.Next(smokeMinVelocity, smokeMaxVelocity) / 20f - 0.25f);
				p.RotationAmount = (float)(random.NextDouble() * random.Next(smokeMinRotationAmount, smokeMaxRotationAmount) / 1000f);
				p.Origin = SmokeOrigin;
				p.CurrentLife = 0;
				p.Color = SmokeParticle.color;
				p.Lifetime = random.Next(smokeMinLifetime, smokeMaxLifetime);
				p.IsAlive = true;
			});
		}
	}
}
