using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Background
	{
		SpriteBatch spriteBatch;
		Texture2D pixel;

		private Point start_point;
		private int width;
		private int height;

		public Point Start_point { get => start_point; set => start_point = value; }
		public int Width { get => width; set => width = value; }
		public int Height { get => height; set => height = value; }

		public Background(SpriteBatch spriteBatch, Point start_point,int width, int height)
		{
			this.start_point = start_point;
			this.spriteBatch = spriteBatch;
			this.width = width;
			this.height = height;

			// Stworzenie tekstury 1x1 dla rysowania
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		public void Draw_Background(Color color_start, Color color_end)
		{
			spriteBatch.Begin();

			// Rysowanie gradientowego tła
			for (int y = start_point.Y; y < start_point.Y + height; y++)
			{
				// Interpolacja koloru pomiędzy color_start a color_end
				float amount = (float)(y - start_point.Y) / height;
				Color currentColor = Color.Lerp(color_start, color_end, amount);

				// Rysowanie jednej linii o szerokości "width" i wysokości 1 piksel
				spriteBatch.Draw(pixel, new Rectangle(start_point.X, y, width, 1), currentColor);
			}

			spriteBatch.End();
		}
	}
}
