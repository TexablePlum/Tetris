using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris
{
	public class Main_Grid : Panel
	{
		private SpriteBatch spriteBatch;

		private int rows ;
		private int columns;
		private Tile[,] grid;

		private Color default_fill_color = new Color(22, 22, 20);
		private Color default_obw_color = Color.Black;

		public Main_Grid(SpriteBatch spriteBatch, Point position, int width, int height, int rows, int columns)
		   : base(spriteBatch, position, width, height)
		{
			this.spriteBatch = spriteBatch;
			this.rows = rows;
			this.columns = columns;

			Create_Grid();
		}

		private void Create_Grid() //Inicjalizacja planszy
		{
			int tileSize = Tile_Size();
			grid = Grid.CreateGrid(spriteBatch, rows, columns, tileSize, default_fill_color, default_obw_color, Position);
		}

		public void Draw_Grid()
		{
			// Bazowe rysowanie panelu
			Draw();

			// Rysowanie kafelków
			Grid.DrawGrid(grid, default_fill_color, default_obw_color, null);
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