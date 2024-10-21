using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris
{
	public class Tile
	{
		private SpriteBatch spriteBatch;
		private Texture2D pixel;

		private int size;
		private Color fill_color;
		private Color obw_Color;
		private Point position;

		public Color Fill_Color { get => fill_color; set => fill_color = value; }
		public Color Obw_Color { get => obw_Color; set => obw_Color = value; }
		public Point Position { get => position; set => position = value; }

		public Tile(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int size, Color fill_color, Color obw_Color, Point position)
		{
			this.spriteBatch = spriteBatch;
			this.size = size;
			this.fill_color = fill_color;
			this.obw_Color = obw_Color;
			this.position = position;

			// Stworzenie tekstury 1x1
			pixel = new Texture2D(graphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		public void Tile_Draw()
		{
			spriteBatch.Begin();

			// Rysowanie obwielutki
			Rectangle obw = new Rectangle(position.X, position.Y, size, size);
			spriteBatch.Draw(pixel, obw, obw_Color);

			// Rysowanie kafelka
			Rectangle rect = new Rectangle(position.X + 1, position.Y + 1, size - 2, size - 2);
			spriteBatch.Draw(pixel, rect, fill_color);

			spriteBatch.End();
		}
	}
}
