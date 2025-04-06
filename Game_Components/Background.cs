using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the background of the game with a gradient effect.
	/// </summary>
	public class Background
	{
		#region Private Fields

		/// <summary>
		/// The SpriteBatch used for drawing.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// A 1x1 pixel texture used for drawing rectangles.
		/// </summary>
		private Texture2D pixel;

		/// <summary>
		/// The primary color of the background.
		/// </summary>
		private Color primaryColor;

		/// <summary>
		/// The secondary color of the background.
		/// </summary>
		private Color secondaryColor;

		/// <summary>
		/// The starting point (top-left) of the background.
		/// </summary>
		private Point startPoint;

		/// <summary>
		/// The width of the background.
		/// </summary>
		private int width;

		/// <summary>
		/// The height of the background.
		/// </summary>
		private int height;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the starting point (top-left) of the background.
		/// </summary>
		public Point StartPoint { get => startPoint; set => startPoint = value; }

		/// <summary>
		/// Gets or sets the width of the background.
		/// </summary>
		public int Width { get => width; set => width = value; }

		/// <summary>
		/// Gets or sets the height of the background.
		/// </summary>
		public int Height { get => height; set => height = value; }

		/// <summary>
		/// Gets or sets the primary color of the background.
		/// </summary>
		public Color PrimaryColor { get => primaryColor; set => primaryColor = value; }

		/// <summary>
		/// Gets or sets the secondary color of the background.
		/// </summary>
		public Color SecondaryColor { get => secondaryColor; set => secondaryColor = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Background"/> class with the specified start point, width, and height.
		/// </summary>
		/// <param name="startPoint">The starting point (top-left) of the background.</param>
		/// <param name="width">The width of the background.</param>
		/// <param name="height">The height of the background.</param>
		public Background(Point startPoint, int width, int height)
		{
			this.startPoint = startPoint;
			this.width = width;
			this.height = height;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content needed by the background, including creating a 1x1 pixel texture for drawing.
		/// Also initializes the primary and secondary colors from the current color theme.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		public void LoadContent(SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;

			// Create a 1x1 pixel texture for drawing.
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });

			// Set background colors from the current color theme.
			primaryColor = ColorTheme.GameTheme.BackgroundPrimaryColor;
			secondaryColor = ColorTheme.GameTheme.BackgroundSecondaryColor;
		}

		/// <summary>
		/// Draws the background as a gradient by interpolating between the primary and secondary colors.
		/// </summary>
		public void DrawBackground()
		{
			spriteBatch.Begin();

			// Draw the gradient background line by line.
			for (int y = startPoint.Y; y < startPoint.Y + height; y++)
			{
				// Calculate the interpolation amount.
				float amount = (float)(y - startPoint.Y) / height;
				Color currentColor = Color.Lerp(primaryColor, secondaryColor, amount);

				// Draw a single pixel line with the current color.
				spriteBatch.Draw(pixel, new Rectangle(startPoint.X, y, width, 1), currentColor);
			}

			spriteBatch.End();
		}

		/// <summary>
		/// Updates the background theme by reassigning the primary and secondary colors from the current color theme.
		/// </summary>
		public void UpdateTheme()
		{
			primaryColor = ColorTheme.GameTheme.BackgroundPrimaryColor;
			secondaryColor = ColorTheme.GameTheme.BackgroundSecondaryColor;
		}

		#endregion
	}
}
