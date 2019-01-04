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
using System.Threading.Tasks;

namespace Alone_in_Space.Particles {
	public class ParticleSource : ScreenObject {
		public static Int32 MaxParticles = 100;

		public Queue<Particle> deadParticles;
		public List<Particle> liveParticles;

		public Action<Particle> RandomizerMethod;


		public ParticleSource(Game game)
			: base(game) {
				deadParticles = new Queue<Particle>(MaxParticles);
				liveParticles = new List<Particle>(MaxParticles);
		}


		protected override void LoadContent() {
			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			Parallel.ForEach(liveParticles, (particle) => {
				particle.Update(gameTime);
			});

			for (int index = 0; index < liveParticles.Count; index++) {
				if (!liveParticles[index].IsAlive) {
					deadParticles.Enqueue(liveParticles[index]);
					liveParticles.RemoveAt(index--);
				}
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, ScreenTransform.Transform);
			for (int index = liveParticles.Count - 1; index >= 0; index--) {
				liveParticles[index].Draw(sb);
			}
			sb.End();
			//base.Draw(gameTime);
		}

		public virtual void NewParticle() {
			if (deadParticles.Count > 0) {
				Particle particle = deadParticles.Dequeue();
				RandomizerMethod(particle);
				liveParticles.Add(particle);
			}
		}
	}
}
