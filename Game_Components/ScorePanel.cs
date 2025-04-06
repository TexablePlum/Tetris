using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the score panel that displays the current score and the best score.
	/// Inherits from the <see cref="Panel"/> class.
	/// </summary>
	public class ScorePanel : Panel
	{
		#region Private Fields

		/// <summary>
		/// The SpriteBatch used for drawing graphics.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// The main font used to render the primary score text.
		/// </summary>
		private SpriteFont mainfont;

		/// <summary>
		/// The secondary font used to render the best score text.
		/// </summary>
		private SpriteFont secondaryFont;

		/// <summary>
		/// The label text for the score (e.g., "SCORE: ").
		/// </summary>
		private string scoreText;

		/// <summary>
		/// The current score value.
		/// </summary>
		private long scoreValue;

		/// <summary>
		/// The label text for the best score (e.g., "BEST: ").
		/// </summary>
		private string bestScoreText;

		/// <summary>
		/// The best score value.
		/// </summary>
		private long bestScoreValue;

		/// <summary>
		/// The color used for rendering text.
		/// </summary>
		private Color textColor;

		/// <summary>
		/// The color used for rendering numeric values.
		/// </summary>
		private Color valuesColor;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the current score value.
		/// </summary>
		public long ScoreValue { get => scoreValue; set => scoreValue = value; }

		/// <summary>
		/// Gets or sets the best score value.
		/// </summary>
		public long BestScoreValue { get => bestScoreValue; set => bestScoreValue = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ScorePanel"/> class with the specified position and dimensions.
		/// Sets default label texts and initial score values.
		/// </summary>
		/// <param name="position">The position of the score panel.</param>
		/// <param name="width">The width of the panel.</param>
		/// <param name="height">The height of the panel.</param>
		public ScorePanel(Point position, int width, int height)
			: base(position, width, height)
		{
			scoreText = "SCORE: ";
			scoreValue = 0;
			bestScoreText = "BEST: ";
			bestScoreValue = 2137420;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the score panel, including fonts and theme colors.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			this.spriteBatch = spriteBatch;
			// Load the base panel content.
			base.LoadContent(spriteBatch);

			// Load fonts.
			mainfont = content.Load<SpriteFont>("Fonts/8-bit-Operator-Main-Size");
			secondaryFont = content.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");

			// Set text colors from the current theme.
			textColor = ColorTheme.GameTheme.TextColor;
			valuesColor = ColorTheme.GameTheme.TextCountersColor;
		}

		/// <summary>
		/// Draws the score panel including the panel background, score label and value, and best score label and value.
		/// </summary>
		public new void Draw()
		{
			// Draw the panel background (border and fill).
			base.Draw();

			spriteBatch.Begin();
			// Draw the score label.
			spriteBatch.DrawString(mainfont, scoreText, new Vector2(TextXPositioner(scoreText, mainfont), 40), textColor);
			// Draw the current score value.
			spriteBatch.DrawString(mainfont, scoreValue.ToString(), new Vector2(TextXPositioner(scoreValue.ToString(), mainfont), 90), valuesColor);

			// Calculate positions for drawing the best score label and value.
			var positions = DoubleTextXPositioner(bestScoreText, bestScoreValue.ToString(), secondaryFont);
			spriteBatch.DrawString(secondaryFont, bestScoreText, new Vector2(positions.firstStringX, 155), textColor);
			spriteBatch.DrawString(secondaryFont, bestScoreValue.ToString(), new Vector2(positions.secondStringX, 155), valuesColor);
			spriteBatch.End();
		}

		/// <summary>
		/// Updates the theme of the score panel by refreshing text and value colors from the current theme.
		/// </summary>
		public new void UpdateTheme()
		{
			base.UpdateTheme();
			textColor = ColorTheme.GameTheme.TextColor;
			valuesColor = ColorTheme.GameTheme.TextCountersColor;
		}

		#endregion
	}
}
