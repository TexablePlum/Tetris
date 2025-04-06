using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Provides static methods for creating and drawing a grid of tiles.
	/// </summary>
	public static class Grid
	{
		/// <summary>
		/// Creates a grid (2D array) of tiles with the specified parameters.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing the tiles.</param>
		/// <param name="rows">The number of rows in the grid.</param>
		/// <param name="columns">The number of columns in the grid.</param>
		/// <param name="tileSize">The size (width and height) of each tile.</param>
		/// <param name="fillColor">The fill color for the tiles.</param>
		/// <param name="borderColor">The border color for the tiles.</param>
		/// <param name="startPosition">The starting position (top-left) of the grid.</param>
		/// <returns>A 2D array of <see cref="Tile"/> representing the created grid.</returns>
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

		/// <summary>
		/// Draws the grid of tiles for Tetromino shapes.
		/// Updates the fill and border colors of each tile to the default values, then applies
		/// colors from the provided Tetromino shapes (if any), and finally draws each tile.
		/// </summary>
		/// <param name="grid">The 2D array of <see cref="Tile"/> to be drawn.</param>
		/// <param name="defaultFillColor">The default fill color for the tiles.</param>
		/// <param name="defaultObwColor">The default border color for the tiles.</param>
		/// <param name="shapes">A list of Tetromino shapes whose blocks override the default tile colors.
		/// If null, the grid is drawn using the default colors only.</param>
		public static void DrawShapesGrid(Tile[,] grid, Color defaultFillColor, Color defaultObwColor, List<Tetronimo> shapes)
		{
			if (shapes == null)
			{
				foreach (var tile in grid)
				{
					tile.FillColor = defaultFillColor;
					tile.ObwColor = defaultObwColor;
					tile.TileDraw();
				}
			}
			else
			{
				// Set default colors for all tiles.
				foreach (var tile in grid)
				{
					tile.FillColor = defaultFillColor;
					tile.ObwColor = defaultObwColor;
				}
				// Override colors for tiles that are part of a Tetromino shape.
				foreach (var shape in shapes)
				{
					var fillColor = shape.FillColor;
					var obwColor = shape.ObwColor;
					foreach (var block in shape.Blocks)
					{
						grid[block.X, block.Y].FillColor = fillColor;
						grid[block.X, block.Y].ObwColor = obwColor;
					}
				}
				// Draw all tiles.
				foreach (var tile in grid)
				{
					tile.TileDraw();
				}
			}
		}

		/// <summary>
		/// Draws the grid of tiles for letters.
		/// Updates the fill and border colors of each tile to the default values, then applies
		/// colors from the provided letters (if any), and finally draws each tile.
		/// </summary>
		/// <param name="grid">The 2D array of <see cref="Tile"/> to be drawn.</param>
		/// <param name="defaultFillColor">The default fill color for the tiles.</param>
		/// <param name="defaultObwColor">The default border color for the tiles.</param>
		/// <param name="letters">A list of letters whose shapes override the default tile colors.
		/// If null, the grid is drawn using the default colors only.</param>
		public static void DrawLettersGrid(Tile[,] grid, Color defaultFillColor, Color defaultObwColor, List<Letter> letters)
		{
			if (letters == null)
			{
				foreach (var tile in grid)
				{
					tile.FillColor = defaultFillColor;
					tile.ObwColor = defaultObwColor;
					tile.TileDraw();
				}
			}
			else
			{
				// Set default colors for all tiles.
				foreach (var tile in grid)
				{
					tile.FillColor = defaultFillColor;
					tile.ObwColor = defaultObwColor;
				}
				// Override colors for tiles that are part of a letter.
				foreach (var letter in letters)
				{
					var fillColor = letter.LetterColor;
					var obwColor = letter.LetterBorderColor;
					foreach (var block in letter.LetterShape)
					{
						grid[block.X, block.Y].FillColor = fillColor;
						grid[block.X, block.Y].ObwColor = obwColor;
					}
				}
				// Draw all tiles.
				foreach (var tile in grid)
				{
					tile.TileDraw();
				}
			}
		}
	}
}
