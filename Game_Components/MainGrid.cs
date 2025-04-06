using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the main grid panel where game shapes or letters are displayed.
	/// The grid is composed of tiles and its size is calculated based on the tile size and number of rows and columns.
	/// </summary>
	public class MainGrid : Panel
	{
		#region Private Fields

		/// <summary>
		/// The SpriteBatch used for drawing the grid.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// The size of each tile in the grid.
		/// </summary>
		private int tileSize;

		/// <summary>
		/// The number of rows in the grid.
		/// </summary>
		private int rows;

		/// <summary>
		/// The number of columns in the grid.
		/// </summary>
		private int columns;

		/// <summary>
		/// The 2D array representing the grid of tiles.
		/// </summary>
		private Tile[,] grid;   // Array of tiles

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the size of each tile in the grid.
		/// </summary>
		public int TileSize { get => tileSize; set => tileSize = value; }

		/// <summary>
		/// Gets or sets the number of rows in the grid.
		/// </summary>
		public int Rows { get => rows; set => rows = value; }

		/// <summary>
		/// Gets or sets the number of columns in the grid.
		/// </summary>
		public int Columns { get => columns; set => columns = value; }

		/// <summary>
		/// Gets or sets the 2D array of tiles that form the grid.
		/// </summary>
		public Tile[,] GameGrid { get => grid; set => grid = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MainGrid"/> class with the specified position, tile size, number of rows, and columns.
		/// The panel size is automatically calculated based on the tile size and grid dimensions.
		/// </summary>
		/// <param name="position">The position of the grid panel.</param>
		/// <param name="tileSize">The size of each tile in the grid.</param>
		/// <param name="rows">The number of rows in the grid.</param>
		/// <param name="columns">The number of columns in the grid.</param>
		public MainGrid(Point position, int tileSize, int rows, int columns)
		   : base(position, CalculatePanelSize(tileSize, rows, columns).width, CalculatePanelSize(tileSize, rows, columns).height)
		{
			this.tileSize = tileSize;
			this.rows = rows;
			this.columns = columns;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content for the main grid by loading the base panel content and creating the grid.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		public new void LoadContent(SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;

			// Load base panel content.
			base.LoadContent(spriteBatch);

			// Create the grid of tiles.
			CreateGrid();
		}

		/// <summary>
		/// Draws the grid of tiles with the Tetromino shapes.
		/// This method first draws the base panel, then draws the grid with shape-specific colors.
		/// </summary>
		/// <param name="shapes">A list of Tetromino shapes to be drawn on the grid.</param>
		public void DrawShapesGrid(List<Tetronimo> shapes)
		{
			// Draw the base panel.
			Draw();

			// Draw the grid of tiles with the shapes.
			Grid.DrawShapesGrid(grid, ColorTheme.GameTheme.MainGridFillColor, ColorTheme.GameTheme.MainGridBorderColor, shapes);
		}

		/// <summary>
		/// Draws the grid of tiles with the letters.
		/// This method first draws the base panel, then draws the grid with letter-specific colors.
		/// </summary>
		/// <param name="letters">A list of letters to be drawn on the grid.</param>
		public void DrawLettersGrid(List<Letter> letters)
		{
			// Draw the base panel.
			Draw();

			// Draw the grid of tiles with the letters.
			Grid.DrawLettersGrid(grid, Color.Transparent, Color.Transparent, letters);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Creates the grid of tiles using the static Grid.CreateGrid method.
		/// </summary>
		private void CreateGrid()
		{
			grid = Grid.CreateGrid(spriteBatch, rows, columns, tileSize, ColorTheme.GameTheme.MainGridFillColor, ColorTheme.GameTheme.MainGridBorderColor, Position);
		}

		/// <summary>
		/// Calculates the size of the panel based on the tile size and grid dimensions.
		/// </summary>
		/// <param name="tileSize">The size of each tile.</param>
		/// <param name="rows">The number of rows in the grid.</param>
		/// <param name="columns">The number of columns in the grid.</param>
		/// <returns>A tuple containing the calculated width and height of the panel.</returns>
		private static (int width, int height) CalculatePanelSize(int tileSize, int rows, int columns)
		{
			var width = rows * tileSize;
			var height = columns * tileSize;
			return (width, height);
		}

		#endregion
	}
}
