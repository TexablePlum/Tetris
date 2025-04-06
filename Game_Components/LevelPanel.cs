using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the level panel in the game UI.
	/// Displays the current level along with a label.
	/// </summary>
	public class LevelPanel : Panel
	{
		#region Private Fields

		/// <summary>
		/// The font used to render the text on the panel.
		/// </summary>
		private SpriteFont font;

		/// <summary>
		/// The current level value.
		/// </summary>
		private int levelValue;

		/// <summary>
		/// The text label for the level (e.g., "LEVEL: ").
		/// </summary>
		private string levelText;

		/// <summary>
		/// The color used for the level label text.
		/// </summary>
		private Color textColor;

		/// <summary>
		/// The color used for the level value text.
		/// </summary>
		private Color textValueColor;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the current level value.
		/// </summary>
		public int LevelValue { get => levelValue; set => levelValue = value; }

		/// <summary>
		/// Gets or sets the level label text.
		/// </summary>
		public string NextShapeText { get => levelText; set => levelText = value; }

		/// <summary>
		/// Gets or sets the color of the level label text.
		/// </summary>
		public Color TextColor { get => textColor; set => textColor = value; }

		/// <summary>
		/// Gets or sets the color of the level value text.
		/// </summary>
		public Color TextValueColor { get => textValueColor; set => textValueColor = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LevelPanel"/> class with the specified position and size.
		/// </summary>
		/// <param name="position">The position of the panel.</param>
		/// <param name="width">The width of the panel.</param>
		/// <param name="height">The height of the panel.</param>
		public LevelPanel(Point position, int width, int height)
			: base(position, width, height)
		{
			levelValue = 0;
			levelText = "LEVEL: ";
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the level panel.
		/// This includes loading the font and setting the text colors.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			// Load the base panel content.
			LoadContent(spriteBatch);

			// Set text colors from the current theme.
			textColor = ColorTheme.GameTheme.TextColor;
			textValueColor = ColorTheme.GameTheme.TextCountersColor;

			// Load the font.
			font = content.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");
		}

		/// <summary>
		/// Draws the level panel including the panel background, level label, and level value.
		/// </summary>
		public new void Draw()
		{
			// Draw the panel background.
			base.Draw();
			SpriteBatch.Begin();

			// Draw the level label.
			SpriteBatch.DrawString(
				font,
				levelText,
				new Vector2(DoubleTextXPositioner(levelText, levelValue.ToString(), font).firstStringX, TextYPositioner(levelText, font)),
				textColor);

			// Draw the level value.
			SpriteBatch.DrawString(
				font,
				levelValue.ToString(),
				new Vector2(DoubleTextXPositioner(levelText, levelValue.ToString(), font).secondStringX, TextYPositioner(levelText, font)),
				textValueColor);

			SpriteBatch.End();
		}

		/// <summary>
		/// Updates the theme of the level panel by refreshing its text colors.
		/// </summary>
		public new void UpdateTheme()
		{
			base.UpdateTheme();
			textColor = ColorTheme.GameTheme.TextColor;
			textValueColor = ColorTheme.GameTheme.TextCountersColor;
		}

		#endregion
	}
}
