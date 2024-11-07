using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris
{
	public class Main_Grid : Panel
	{
		private SpriteBatch spriteBatch;
		private GraphicsDevice graphicsDevice;

		private int rows ;
		private int columns;
		private Tile[,] grid;

		public int Rows { get => rows; set => rows = value; }
		public int Columns { get => columns; set => columns = value; }

		public Main_Grid(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Point position, int width, int height, int rows, int columns)
		   : base(spriteBatch, graphicsDevice, position, width, height)
		{
			this.spriteBatch = spriteBatch;
			this.graphicsDevice = graphicsDevice;
			this.rows = rows;
			this.columns = columns;

			Create_Grid();
		}

		private void Create_Grid() //Inicjalizacja planszy
		{
			grid = new Tile[rows, columns];
			int tile_size = Tile_Size();

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					grid[i, j] = new Tile(spriteBatch, graphicsDevice, tile_size, new Color(22,22,20), Color.Black, new Point(Position.X + i * tile_size, Position.Y + j * tile_size));
				}
			}
		}

		public void Draw_Grid()
		{
			//bazowe rysowanie panelu
			base.Draw();

			// Rysowanie kafelków
			foreach (var tile in grid)
			{
				tile.Tile_Draw();
			}
		}

		private int Tile_Size()
		{
			if (Height / Width != 2)
			{
				throw new ArgumentException("Grid is not in a 2:1 ratio.");
			}

			return Width / rows;
		}
	}
}