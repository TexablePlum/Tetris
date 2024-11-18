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
			int tileSize = Width / rows;
			grid = Grid.CreateGrid(spriteBatch, rows, columns, tileSize, Color_Theme.Game_Theme.Main_Grid_Fill_Color, Color_Theme.Game_Theme.Main_Grid_Border_Color, Position);
		}

		public void Draw_Grid()
		{
			// Bazowe rysowanie panelu
			Draw();

			// Rysowanie kafelków
			Grid.DrawGrid(grid, Color_Theme.Game_Theme.Main_Grid_Fill_Color, Color_Theme.Game_Theme.Main_Grid_Border_Color, null);
		}
	}
}