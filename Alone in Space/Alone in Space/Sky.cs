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

using SquaredEngine.Graphics;
using SquaredEngine.Diagnostics.Debug;
using SquaredEngine.Diagnostics.Log;

namespace Alone_in_Space {
	public class Sky : DrawableGameComponent {
		public GraphicsDrawer drawer;

		public Rectangle skyBounds; 

		public Color skyColorDown;
		public Color skyColorUp;
		public GraphicsDrawer.Rectangle skyBackground;

		public float level0 = 40;
		public float level1 = 75;
		public float level2 = 80;
		public float level3 = 90;
		public float level4 = 110;

		public Color level01Color = Color.SkyBlue;
		public Color level2Color = Color.Orange;
		public Color level34Color = Color.Black;

		public Int32 starSizeMin = 1;
		public Int32 starSizeMax = 3;
		public Int32 numberOfStars = 100;
		public Int32 startBufferID = -1;

		public float altitude = 0;
		public float Altitude {
			get { return this.altitude;}
			set { SetAltitude(value); }
		}


		public Sky(Game game)
			: base(game) {

		}


		public override void Initialize() {
			base.Initialize();

			skyBounds = new Rectangle(
				GraphicsDevice.Viewport.X,
				GraphicsDevice.Viewport.Y,
				GraphicsDevice.Viewport.Width,
				GraphicsDevice.Viewport.Height);

			SetAltitude(0);

			drawer = new GraphicsDrawer(GraphicsDevice);
			drawer.Initialize();

			Random random = new Random();
			List<GraphicsDrawer.IDrawable> stars = new List<GraphicsDrawer.IDrawable>();
			for(int index = 0; index < numberOfStars; ++index) {
				float size = random.Next(starSizeMin, starSizeMax);
				Vector3 position = new Vector3(random.Next(0, skyBounds.Width), random.Next(0, skyBounds.Height), 0f);

				stars.Add(new GraphicsDrawer.Ellipse(position, size, size, 0f, 6, Color.LightYellow));
			}
			startBufferID = drawer.CreateBuffer<GraphicsDrawer.IDrawable>(stars);
		}

		protected override void LoadContent() {
			

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			drawer.Begin();
			drawer.Draw(skyBackground);
			drawer.End();

			if (altitude > level2) {
				drawer.Begin();
				drawer.Draw(startBufferID);
				drawer.End();
			}

			base.Draw(gameTime);
		}


		public void UpdateSkyBackground(Color down, Color up) {
			skyBackground = new GraphicsDrawer.Rectangle(
				new Vector3(skyBounds.X, skyBounds.Y, 0),
				new Vector3(skyBounds.Width, skyBounds.Height, 0),
				up, up, down, down);
		}

		public void SetAltitude(float altitude) {
			if (altitude >= 0) {
				this.altitude = altitude;

				Color down, up;
				ResolveAltutudeColor(altitude, out down, out up);
				UpdateSkyBackground(down, up);
			}
		}

		public void ResolveAltutudeColor(float altitude, out Color down, out Color up) {
			if (altitude < level0) {
				down = up = level01Color;
			}
			else if (altitude < level1) {
				float amount = (altitude - level0)  / (level1 - level0);

				down = level01Color;
				up = Color.Lerp(level01Color, level2Color, amount);
			}
			else if (altitude < level2) {
				float amount = (altitude - level1) / (level2 - level1);

				down = Color.Lerp(level01Color, level2Color, amount);
				up = level2Color;
			}
			else if (altitude < level3) {
				float amount = (altitude - level2) / (level3 - level2);

				down = level2Color;
				up = Color.Lerp(level2Color, level34Color, amount);
			}
			else if (altitude < level4) {
				float amount = (altitude - level3) / (level4 - level3);

				down = Color.Lerp(level2Color, level34Color, amount);
				up = level34Color;
			}
			else { down = up = level34Color; }
		}
	}
}
