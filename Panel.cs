using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Panel
	{
		private SpriteBatch spriteBatch;
		private readonly Texture2D pixel;

		private Point position;
		private int width;
		private int height;
		private Color fill_color = new Color (22, 22, 20);
		private Color obw_Color = new Color (249, 40, 255);


		public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
		public Texture2D Pixel { get => pixel; }
		public Point Position { get => position; set => position = value; }
		public int Width { get => width; set => width = value; }
		public int Height { get => height; set => height = value; }
		public Color Fill_Color { get => fill_color; set => fill_color = value; }
		public Color Obw_Color { get => obw_Color; set => obw_Color = value; }

		public Panel(SpriteBatch spriteBatch, Point position, int width, int height)
		{
			this.position = position;
			this.spriteBatch = spriteBatch;
			this.width = width;
			this.height = height;

			// Stworzenie tekstury 1x1
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		public void Draw()
		{
			spriteBatch.Begin();

			// Rysowanie obwielutki
			Rectangle obw = new Rectangle(position.X - 3, position.Y - 3, width + 6 , height + 6);
			spriteBatch.Draw(pixel, obw, obw_Color);

			// Rysowanie panelu
			Rectangle rect = new Rectangle(position.X, position.Y, width, height);
			spriteBatch.Draw(pixel, rect, fill_color); 

			spriteBatch.End();
		}

		// Metoda do wyśrodkowania pojedyńczego tekstu w pionie
		public int Text_X_Positioner(string text, SpriteFont font)
		{
			float text_width = font.MeasureString(text).X;
			float centered_position = Position.X + (Width - text_width) / 2;
			return (int)centered_position;
		}

		// Metoda do wyśrodkowania pojedyńczego tekstu w poziomie
		public int Text_Y_Positioner(string text, SpriteFont font)
		{
			float text_height = font.MeasureString(text).Y;
			float centered_position = Position.Y + (Height - text_height / 2) / 2;
			return (int)centered_position;
		}

		// Metoda do wyśrodkowania podwójnego tekstu w pionie
		public (int first_string_x, int second_string_x) Double_Text_X_Positioner(string text1, string text2, SpriteFont font)
		{
			float text1_width = font.MeasureString(text1).X;
			float text2_width = font.MeasureString(text2).X;

			float sum_width = text1_width + text2_width;

			float centered_position1 = Position.X + (Width - sum_width) / 2;
			float centered_position2 = Position.X + (Width - sum_width) / 2 + text1_width;

			return ((int)centered_position1, (int)centered_position2);
		}

		// Metoda do wyśrodkowania siatki w panelu
		public int Grid_X_Positioner(int rows, int tile_size)
		{
			int grid_width = rows * tile_size;
			int centered_position = Position.X + (Width - grid_width) / 2;
			return centered_position;
		}

	}
}
