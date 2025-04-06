using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents a basic panel used in the game UI.
	/// Provides methods for loading content, drawing the panel, and positioning text and grids.
	/// </summary>
	public class Panel
	{
		#region Private Fields

		/// <summary>
		/// The SpriteBatch used for drawing graphics.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// A 1x1 pixel texture used for drawing rectangles.
		/// </summary>
		private Texture2D pixel;

		/// <summary>
		/// The position of the panel.
		/// </summary>
		private Point position;

		/// <summary>
		/// The width of the panel.
		/// </summary>
		private int width;

		/// <summary>
		/// The height of the panel.
		/// </summary>
		private int height;

		/// <summary>
		/// The fill color of the panel.
		/// </summary>
		private Color fillColor;

		/// <summary>
		/// The border color (outline) of the panel.
		/// </summary>
		private Color obwColor;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the SpriteBatch used for drawing.
		/// </summary>
		public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }

		/// <summary>
		/// Gets the 1x1 pixel texture.
		/// </summary>
		public Texture2D Pixel { get => pixel; }

		/// <summary>
		/// Gets or sets the position of the panel.
		/// </summary>
		public Point Position { get => position; set => position = value; }

		/// <summary>
		/// Gets or sets the width of the panel.
		/// </summary>
		public int Width { get => width; set => width = value; }

		/// <summary>
		/// Gets or sets the height of the panel.
		/// </summary>
		public int Height { get => height; set => height = value; }

		/// <summary>
		/// Gets or sets the fill color of the panel.
		/// </summary>
		public Color FillColor { get => fillColor; set => fillColor = value; }

		/// <summary>
		/// Gets or sets the border color of the panel.
		/// </summary>
		public Color ObwColor { get => obwColor; set => obwColor = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Panel"/> class with the specified position and dimensions.
		/// </summary>
		/// <param name="position">The position of the panel.</param>
		/// <param name="width">The width of the panel.</param>
		/// <param name="height">The height of the panel.</param>
		public Panel(Point position, int width, int height)
		{
			this.position = position;
			this.width = width;
			this.height = height;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the panel.
		/// This includes setting the default fill and border colors and creating a 1x1 pixel texture.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		public void LoadContent(SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;

			// Set default panel colors from the current theme.
			fillColor = ColorTheme.GameTheme.PanelFillColor;
			obwColor = ColorTheme.GameTheme.PanelBorderColor;

			// Create a 1x1 texture for drawing.
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		/// <summary>
		/// Draws the panel by rendering its border and fill.
		/// </summary>
		public void Draw()
		{
			spriteBatch.Begin();

			// Draw the border (outline) of the panel.
			Rectangle obw = new Rectangle(position.X - 3, position.Y - 3, width + 6, height + 6);
			spriteBatch.Draw(pixel, obw, obwColor);

			// Draw the panel fill.
			Rectangle rect = new Rectangle(position.X, position.Y, width, height);
			spriteBatch.Draw(pixel, rect, fillColor);

			spriteBatch.End();
		}

		/// <summary>
		/// Calculates the X coordinate for centering a single line of text horizontally within the panel.
		/// </summary>
		/// <param name="text">The text to be centered.</param>
		/// <param name="font">The font used to render the text.</param>
		/// <returns>The calculated X coordinate.</returns>
		public int TextXPositioner(string text, SpriteFont font)
		{
			float textWidth = font.MeasureString(text).X;
			float centeredPosition = Position.X + (Width - textWidth) / 2;
			return (int)centeredPosition;
		}

		/// <summary>
		/// Calculates the Y coordinate for centering a single line of text vertically within the panel.
		/// </summary>
		/// <param name="text">The text to be centered.</param>
		/// <param name="font">The font used to render the text.</param>
		/// <returns>The calculated Y coordinate.</returns>
		public int TextYPositioner(string text, SpriteFont font)
		{
			float textHeight = font.MeasureString(text).Y;
			float centeredPosition = Position.Y + (Height - textHeight / 2) / 2;
			return (int)centeredPosition;
		}

		/// <summary>
		/// Calculates the X coordinates for centering two lines of text side-by-side within the panel.
		/// Returns the X coordinate for both the first and second strings.
		/// </summary>
		/// <param name="text1">The first text string.</param>
		/// <param name="text2">The second text string.</param>
		/// <param name="font">The font used to render the text.</param>
		/// <returns>A tuple containing the X positions for the first and second strings.</returns>
		public (int firstStringX, int secondStringX) DoubleTextXPositioner(string text1, string text2, SpriteFont font)
		{
			float text1Width = font.MeasureString(text1).X;
			float text2Width = font.MeasureString(text2).X;

			float sumWidth = text1Width + text2Width;

			float centeredPosition1 = Position.X + (Width - sumWidth) / 2;
			float centeredPosition2 = Position.X + (Width - sumWidth) / 2 + text1Width;

			return ((int)centeredPosition1, (int)centeredPosition2);
		}

		/// <summary>
		/// Calculates the X coordinate for centering a grid within the panel.
		/// </summary>
		/// <param name="rows">The number of rows in the grid.</param>
		/// <param name="tileSize">The size of each tile in the grid.</param>
		/// <returns>The calculated X coordinate for the grid.</returns>
		public int GridXPositioner(int rows, int tileSize)
		{
			int gridWidth = rows * tileSize;
			int centeredPosition = Position.X + (Width - gridWidth) / 2;
			return centeredPosition;
		}

		/// <summary>
		/// Updates the panel's theme by refreshing its fill and border colors from the current color theme.
		/// </summary>
		public void UpdateTheme()
		{
			fillColor = ColorTheme.GameTheme.PanelFillColor;
			obwColor = ColorTheme.GameTheme.PanelBorderColor;
		}

		#endregion
	}
}
