using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents a panel that displays the number of lines cleared in the game.
	/// This panel shows a label and the current lines count.
	/// </summary>
	public class LinesCountPanel : Panel
	{
		#region Private Fields

		/// <summary>
		/// The font used to render the text on the panel.
		/// </summary>
		private SpriteFont font;

		/// <summary>
		/// The label text displayed on the panel (e.g., "LINES: ").
		/// </summary>
		private string linesText;

		/// <summary>
		/// The current count of lines cleared.
		/// </summary>
		private int linesValue;

		/// <summary>
		/// The color used for the label text.
		/// </summary>
		private Color textColor;

		/// <summary>
		/// The color used for the lines value text.
		/// </summary>
		private Color textValueColor;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the label text displayed on the panel.
		/// </summary>
		public string LinesText { get => linesText; set => linesText = value; }

		/// <summary>
		/// Gets or sets the current lines count.
		/// </summary>
		public int LinesValue { get => linesValue; set => linesValue = value; }

		/// <summary>
		/// Gets or sets the color of the label text.
		/// </summary>
		public Color TextColor { get => textColor; set => textColor = value; }

		/// <summary>
		/// Gets or sets the color of the lines count text.
		/// </summary>
		public Color TextValueColor { get => textValueColor; set => textValueColor = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LinesCountPanel"/> class with the specified position and size.
		/// </summary>
		/// <param name="position">The position of the panel.</param>
		/// <param name="width">The width of the panel.</param>
		/// <param name="height">The height of the panel.</param>
		public LinesCountPanel(Point position, int width, int height)
			: base(position, width, height)
		{
			linesText = "LINES: ";
			linesValue = 0;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the lines count panel, including fonts and theme colors.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			// Load the base panel content.
			LoadContent(spriteBatch);

			// Load the font for the panel.
			font = content.Load<SpriteFont>("Fonts/Control_Panel_Font");

			// Set panel colors from the current theme.
			FillColor = ColorTheme.GameTheme.PanelTransparentFillColor;
			ObwColor = ColorTheme.GameTheme.PanelTransparentBorderColor;
			textColor = ColorTheme.GameTheme.TextColor;
			textValueColor = ColorTheme.GameTheme.TextCountersColor;
		}

		/// <summary>
		/// Draws the lines count panel, including the background, label text, and the lines count value.
		/// </summary>
		public new void Draw()
		{
			// Draw the base panel.
			base.Draw();

			SpriteBatch.Begin();

			// Draw the label text.
			SpriteBatch.DrawString(
				font,
				linesText,
				new Vector2(DoubleTextXPositioner(linesText, linesValue.ToString(), font).firstStringX, TextYPositioner(linesText, font)),
				textColor);

			// Draw the lines count value.
			SpriteBatch.DrawString(
				font,
				linesValue.ToString(),
				new Vector2(DoubleTextXPositioner(linesText, linesValue.ToString(), font).secondStringX, TextYPositioner(linesText, font)),
				textValueColor);

			SpriteBatch.End();
		}

		/// <summary>
		/// Updates the theme of the panel by refreshing the text colors.
		/// </summary>
		public new void UpdateTheme()
		{
			textColor = ColorTheme.GameTheme.TextColor;
			textValueColor = ColorTheme.GameTheme.TextCountersColor;
		}

		#endregion
	}
}
