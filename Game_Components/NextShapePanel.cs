using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the panel used to display the next Tetromino shape in the game.
	/// It manages the creation and caching of grids for different Tetromino shapes,
	/// and displays the current next shape with a label.
	/// </summary>
	public class NextShapePanel : Panel
	{
		#region Private Fields

		/// <summary>
		/// The font used to render the text on the panel.
		/// </summary>
		private SpriteFont font;

		/// <summary>
		/// The grid of tiles representing the next shape.
		/// </summary>
		private Tile[,] grid;

		/// <summary>
		/// The text label displayed above the next shape.
		/// </summary>
		private string nextShapeText;

		/// <summary>
		/// The fixed tile size used for the next shape grid.
		/// </summary>
		private const int tileSize = 42;

		/// <summary>
		/// The list of Tetromino shapes to be displayed.
		/// This list is created to maintain compatibility with the DrawGrid method from the Grid class.
		/// </summary>
		private List<Tetronimo> shapes;

		/// <summary>
		/// A cache of grids for different Tetromino shapes.
		/// </summary>
		private Dictionary<TetronimoShape, Tile[,]> gridCache = new();

		/// <summary>
		/// The current Tetromino shape.
		/// </summary>
		private TetronimoShape tetronimoShape;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the text label displayed on the panel.
		/// </summary>
		public string NextShapeText { get => nextShapeText; set => nextShapeText = value; }

		/// <summary>
		/// Gets or sets the current Tetromino shape.
		/// When set, the grid is updated from the cache if the shape has changed.
		/// </summary>
		public TetronimoShape TetronimoShape
		{
			get => tetronimoShape;
			set
			{
				if (tetronimoShape != value)
				{
					tetronimoShape = value;
					SetGridFromCache();
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NextShapePanel"/> class with the specified position and size.
		/// Sets a default label and initializes the shapes list.
		/// </summary>
		/// <param name="position">The position of the panel.</param>
		/// <param name="width">The width of the panel.</param>
		/// <param name="height">The height of the panel.</param>
		public NextShapePanel(Point position, int width, int height)
			: base(position, width, height)
		{
			shapes = new List<Tetronimo>();
			nextShapeText = "Next Shape:";
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the next shape panel.
		/// This includes loading the panel content, font, setting theme colors, creating grids for all Tetromino shapes,
		/// and setting the current grid from the cache.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			// Load the base panel content.
			LoadContent(spriteBatch);

			// Load the font.
			font = content.Load<SpriteFont>("Fonts/Control_Panel_Font");

			// Set panel colors to transparent.
			FillColor = ColorTheme.GameTheme.PanelTransparentFillColor;
			ObwColor = ColorTheme.GameTheme.PanelTransparentBorderColor;

			// Create grids for all Tetromino shapes.
			CreateGrids();

			// Set the grid for the current Tetromino shape from the cache.
			SetGridFromCache();
		}

		/// <summary>
		/// Draws the next shape panel.
		/// This method draws the panel background and the label, then draws the grid of tiles for the current shape if available.
		/// </summary>
		public new void Draw()
		{
			// Draw the panel background from the base Panel class.
			base.Draw();

			SpriteBatch.Begin();
			// Draw the next shape label.
			SpriteBatch.DrawString(font, nextShapeText, new Vector2(TextXPositioner(nextShapeText, font), Position.Y), Color.White);
			SpriteBatch.End();

			// If a Tetronimo shape is set and the shapes list is not empty, draw the grid with the shape.
			if (tetronimoShape != default && shapes.Count > 0)
			{
				Grid.DrawShapesGrid(grid, ColorTheme.GameTheme.PanelTransparentFillColor, ColorTheme.GameTheme.PanelTransparentBorderColor, shapes);
			}
		}

		/// <summary>
		/// Updates the theme of the next shape panel by setting the fill and border colors based on the current theme.
		/// </summary>
		public new void UpdateTheme()
		{
			FillColor = ColorTheme.GameTheme.PanelTransparentFillColor;
			ObwColor = ColorTheme.GameTheme.PanelTransparentBorderColor;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Returns the dimensions (rows and columns) for a given Tetromino shape.
		/// </summary>
		/// <param name="shape">The Tetromino shape.</param>
		/// <returns>A tuple containing the number of rows and columns for the shape's grid.</returns>
		private (int rows, int columns) GetShapeSize(TetronimoShape shape)
		{
			return shape switch
			{
				TetronimoShape.ShapeI => (4, 1),
				TetronimoShape.ShapeO => (2, 2),
				TetronimoShape.ShapeS or TetronimoShape.ShapeZ or TetronimoShape.ShapeL or TetronimoShape.ShapeJ or TetronimoShape.ShapeT => (3, 2),
				_ => (0, 0)
			};
		}

		/// <summary>
		/// Creates grids for all Tetromino shapes and adds them to the grid cache.
		/// </summary>
		private void CreateGrids()
		{
			foreach (TetronimoShape shape in System.Enum.GetValues(typeof(TetronimoShape)))
			{
				var (rows, columns) = GetShapeSize(shape);
				var gridPosition = new Point(GridXPositioner(rows, tileSize), Position.Y + 50);
				gridCache[shape] = Grid.CreateGrid(SpriteBatch, rows, columns, tileSize, FillColor, ObwColor, gridPosition);
			}
		}

		/// <summary>
		/// Sets the grid from the cache for the current Tetromino shape.
		/// Also updates the shapes list with a new Tetromino instance.
		/// </summary>
		private void SetGridFromCache()
		{
			if (gridCache.TryGetValue(tetronimoShape, out var cachedGrid))
			{
				grid = cachedGrid;
				shapes.Clear();
				shapes.Add(CreateTetronimoInstance(tetronimoShape));
			}
		}

		/// <summary>
		/// Creates an instance of a Tetromino based on the given Tetronimo shape.
		/// </summary>
		/// <param name="shape">The Tetromino shape.</param>
		/// <returns>A new instance of a Tetromino corresponding to the specified shape.</returns>
		private Tetronimo CreateTetronimoInstance(TetronimoShape shape)
		{
			return shape switch
			{
				TetronimoShape.ShapeI => new ShapeI(new Point(0, 0)),
				TetronimoShape.ShapeO => new ShapeO(new Point(0, 0)),
				TetronimoShape.ShapeS => new ShapeS(new Point(0, 1)),
				TetronimoShape.ShapeZ => new ShapeZ(new Point(0, 0)),
				TetronimoShape.ShapeL => new ShapeL(new Point(0, 1)),
				TetronimoShape.ShapeJ => new ShapeJ(new Point(0, 0)),
				TetronimoShape.ShapeT => new ShapeT(new Point(1, 0)),
				_ => null
			};
		}

		#endregion
	}
}
