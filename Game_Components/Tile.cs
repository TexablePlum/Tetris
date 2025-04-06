using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents a single tile used in the game grid.
	/// Each tile has a fill color, a border color, and a specified size and position.
	/// </summary>
	public class Tile
	{
		/// <summary>
		/// The SpriteBatch used for drawing the tile.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// A 1x1 texture used for drawing the tile.
		/// </summary>
		private Texture2D pixel;

		/// <summary>
		/// The size (width and height) of the tile.
		/// </summary>
		private int size;

		/// <summary>
		/// The fill color of the tile.
		/// </summary>
		private Color fillColor;

		/// <summary>
		/// The border color of the tile.
		/// </summary>
		private Color obwColor;

		/// <summary>
		/// The position of the tile.
		/// </summary>
		private Point position;

		/// <summary>
		/// Gets or sets the fill color of the tile.
		/// </summary>
		public Color FillColor { get => fillColor; set => fillColor = value; }

		/// <summary>
		/// Gets or sets the border color of the tile.
		/// </summary>
		public Color ObwColor { get => obwColor; set => obwColor = value; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class with the specified parameters.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="size">The size (width and height) of the tile.</param>
		/// <param name="fillColor">The fill color of the tile.</param>
		/// <param name="obwColor">The border color of the tile.</param>
		/// <param name="position">The position of the tile.</param>
		public Tile(SpriteBatch spriteBatch, int size, Color fillColor, Color obwColor, Point position)
		{
			this.spriteBatch = spriteBatch;
			this.size = size;
			this.fillColor = fillColor;
			this.obwColor = obwColor;
			this.position = position;

			// Create a 1x1 texture for drawing.
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		/// <summary>
		/// Draws the tile by rendering its border and fill.
		/// </summary>
		public void TileDraw()
		{
			spriteBatch.Begin();

			// Draw the border of the tile.
			Rectangle obw = new Rectangle(position.X, position.Y, size, size);
			spriteBatch.Draw(pixel, obw, obwColor);

			// Draw the inner fill of the tile.
			Rectangle rect = new Rectangle(position.X + 1, position.Y + 1, size - 2, size - 2);
			spriteBatch.Draw(pixel, rect, fillColor);

			spriteBatch.End();
		}
	}
}
