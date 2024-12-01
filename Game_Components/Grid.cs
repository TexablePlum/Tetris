using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
    public static class Grid
    {

        // Statyczna metoda do tworzenia siatki kafelków o podanych parametrach
        public static Tile[,] CreateGrid(SpriteBatch spriteBatch, int rows, int columns, int tileSize, Color fillColor, Color borderColor, Point startPosition)
        {
            Tile[,] grid = new Tile[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    grid[i, j] = new Tile(spriteBatch, tileSize, fillColor, borderColor, new Point(startPosition.X + i * tileSize, startPosition.Y + j * tileSize));
                }
            }

            return grid;
        }


        // Metoda do rysowania siatki kafelków dla kształtów
        public static void DrawShapesGrid(Tile[,] grid, Color default_fill_color, Color default_obw_color, List<Tetronimo> shapes)
        {
            if (shapes == null)
            {
                foreach (var tile in grid)
                {
                    tile.Fill_Color = default_fill_color;
                    tile.Obw_Color = default_obw_color;
                    tile.Tile_Draw();
                }
            }
            else
            {
                foreach (var tile in grid)
                {
                    tile.Fill_Color = default_fill_color;
                    tile.Obw_Color = default_obw_color;
                }
                foreach (var shape in shapes)
                {
                    var fill_color = shape.Fill_Color;
                    var obw_color = shape.Obw_Color;
                    foreach (var block in shape.Blocks)
                    {
                        grid[block.X, block.Y].Fill_Color = fill_color;
                        grid[block.X, block.Y].Obw_Color = obw_color;
                    }
                }
                foreach (var tile in grid)
                {
                    tile.Tile_Draw();
                }
            }

        }


		// Metoda do rysowania siatki kafelków dla liter
		public static void DrawLettersGrid(Tile[,] grid, Color default_fill_color, Color default_obw_color, List<Letters> letters)
		{
			if (letters == null)
			{
				foreach (var tile in grid)
				{
					tile.Fill_Color = default_fill_color;
					tile.Obw_Color = default_obw_color;
					tile.Tile_Draw();
				}
			}
			else
			{
				foreach (var tile in grid)
				{
					tile.Fill_Color = default_fill_color;
					tile.Obw_Color = default_obw_color;
				}
				foreach (var letter in letters)
				{
					var fill_color = letter.Letter_Color;
					var obw_color = letter.Letter_Border_Color;
					foreach (var block in letter.Letter_Shape)
					{
						grid[block.X, block.Y].Fill_Color = fill_color;
						grid[block.X, block.Y].Obw_Color = obw_color;
					}
				}
				foreach (var tile in grid)
				{
					tile.Tile_Draw();
				}
			}

		}

	}
}
