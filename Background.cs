using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Background
	{
		SpriteBatch spriteBatch;
		Texture2D pixel;

		private int width;
		private int height;
		private Color color_start;
		private Color color_end;

		public int Width { get => width; set => width = value; }
		public int Height { get => height; set => height = value; }
		public Color Color_start { get => color_start; set => color_start = value; }
		public Color Color_end { get => color_end; set => color_end = value; }

		public Background(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int width, int height, Color color_start, Color color_end)
		{
			this.spriteBatch = spriteBatch;
			this.width = width;
			this.height = height;
			this.color_start = color_start;
			this.color_end = color_end;

			// Stworzenie tekstury 1x1 dla rysowania
			pixel = new Texture2D(graphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		public void Draw_Background()
		{
			spriteBatch.Begin();

			// Rysowanie gradientowego tła
			for (int y = 0; y < height; y++)
			{
				// Interpolacja koloru pomiędzy color_start a color_end
				float amount = (float)y / height;
				Color currentColor = Color.Lerp(color_start, color_end, amount);

				// Rysowanie jednej linii o szerokości "width" i wysokości 1 piksel
				spriteBatch.Draw(pixel, new Rectangle(0, y, width, 1), currentColor);
			}

			spriteBatch.End();
		}
	}
}
