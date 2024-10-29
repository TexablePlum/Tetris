using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Panel
	{
		private SpriteBatch spriteBatch;
		private Texture2D pixel;

		private Point position;
		private int width;
		private int height;
		private Color color = new Color (22, 22, 20);

		public Point Position { get => position; set => position = value; }
		public int Width { get => width; set => width = value; }
		public int Height { get => height; set => height = value; }
		public Color Color { get => color; set => color = value; }

		public Panel(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Point position, int width, int height)
		{
			this.position = position;
			this.spriteBatch = spriteBatch;
			this.width = width;
			this.height = height;

			// Stworzenie tekstury 1x1
			pixel = new Texture2D(graphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		public void Draw()
		{
			spriteBatch.Begin();

			// Rysowanie obwielutki
			Rectangle obw = new Rectangle(position.X - 3, position.Y - 3, width + 6 , height + 6);
			spriteBatch.Draw(pixel, obw, new Color(249, 40, 255));

			// Rysowanie panelu
			Rectangle rect = new Rectangle(position.X, position.Y, width, height);
			spriteBatch.Draw(pixel, rect, color); 

			spriteBatch.End();
		}


	}
}
