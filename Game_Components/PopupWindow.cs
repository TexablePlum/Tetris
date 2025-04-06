using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents a popup window used in the game to display messages, instructions, or tutorials.
	/// The popup window contains a title, header, main text, and a next button to navigate through multiple steps.
	/// </summary>
	public class PopupWindow : Panel
	{
		#region Private Fields

		/// <summary>
		/// The font used for rendering text in the popup window.
		/// </summary>
		private SpriteFont font;

		/// <summary>
		/// The title text of the popup window.
		/// </summary>
		private string windowTitle;

		/// <summary>
		/// The header text of the popup window.
		/// </summary>
		private string textHeader;

		/// <summary>
		/// The main body text of the popup window.
		/// </summary>
		private string windowText;

		/// <summary>
		/// The button used to proceed to the next step in the popup window.
		/// </summary>
		private Button nextButton;

		/// <summary>
		/// The current step number.
		/// </summary>
		private int step;

		/// <summary>
		/// The total number of steps in the popup sequence.
		/// </summary>
		private int steps;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the title text of the popup window.
		/// </summary>
		public string WindowTitle { get => windowTitle; set => windowTitle = value; }

		/// <summary>
		/// Gets or sets the header text of the popup window.
		/// </summary>
		public string TextHeader { get => textHeader; set => textHeader = value; }

		/// <summary>
		/// Gets or sets the main body text of the popup window.
		/// </summary>
		public string WindowText { get => windowText; set => windowText = value; }

		/// <summary>
		/// Gets or sets the current step number in the popup sequence.
		/// </summary>
		public int Step { get => step; set => step = value; }

		/// <summary>
		/// Gets or sets the total number of steps in the popup sequence.
		/// </summary>
		public int Steps { get => steps; set => steps = value; }

		/// <summary>
		/// Gets or sets the next button used to proceed through the popup steps.
		/// </summary>
		public Button NextButton { get => nextButton; set => nextButton = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PopupWindow"/> class with the specified position, size, and number of steps.
		/// </summary>
		/// <param name="position">The position of the popup window.</param>
		/// <param name="width">The width of the popup window.</param>
		/// <param name="height">The height of the popup window.</param>
		/// <param name="steps">The total number of steps in the popup sequence.</param>
		public PopupWindow(Point position, int width, int height, int steps)
			: base(position, width, height)
		{
			this.steps = steps;
			step = 1;
			nextButton = new Button(new Point(position.X + width - 60, position.Y + height - 60), 35, 35, "", "Assets/button_play");
			nextButton.ButtonStates = Button_States.Inactive;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the popup window, including its font and the next button.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			base.LoadContent(spriteBatch);
			font = content.Load<SpriteFont>("Fonts/Control_Panel_Font");
			nextButton.LoadContent(spriteBatch, content);
		}

		/// <summary>
		/// Updates the popup window, including positioning and updating the next button.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime)
		{
			nextButton.Position = new Point(Position.X + Width - 60, Position.Y + Height - 60);
			nextButton.Update();
		}

		/// <summary>
		/// Draws the popup window including its border, background, title, header, main text, step indicator, and next button.
		/// </summary>
		public new void Draw()
		{
			SpriteBatch.Begin();

			// Draw the border.
			Rectangle obw = new Rectangle(Position.X - 3, Position.Y - 3, Width + 6, Height + 6);
			SpriteBatch.Draw(Pixel, obw, ObwColor);

			// Draw the panel (window) background.
			Rectangle rect = new Rectangle(Position.X, Position.Y + 25, Width, Height - 25);
			SpriteBatch.Draw(Pixel, rect, FillColor);

			if (font != null)
			{
				if (windowTitle != null)
					SpriteBatch.DrawString(font, windowTitle, new Vector2(TextXPositioner(windowTitle, font), Position.Y + 3), ColorTheme.GameTheme.TextColor);
				if (textHeader != null)
				{
					SpriteBatch.DrawString(font, textHeader, new Vector2(Position.X + 25, Position.Y + 50), ObwColor);
				}
				if (windowText != null)
				{
					var textSize = Width - 50;
					var wrappedText = WrapText(font, windowText, textSize);
					SpriteBatch.DrawString(font, wrappedText, new Vector2(Position.X + 25, Position.Y + 100), ColorTheme.GameTheme.TextColor);
				}
				SpriteBatch.DrawString(font, $"{step}/{steps}", new Vector2(Position.X + Width - 120, Position.Y + Height - 50), ColorTheme.GameTheme.TextColor);
			}

			SpriteBatch.End();

			nextButton.Draw();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Wraps a single line of text so that it fits within the specified maximum line width.
		/// </summary>
		/// <param name="spriteFont">The SpriteFont used for measuring text.</param>
		/// <param name="textLine">The line of text to wrap.</param>
		/// <param name="maxLineWidth">The maximum width of the text line.</param>
		/// <returns>The wrapped text line.</returns>
		private string WrapSingleLine(SpriteFont spriteFont, string textLine, float maxLineWidth)
		{
			if (string.IsNullOrEmpty(textLine))
				return string.Empty;

			string[] words = textLine.Split(' ');
			StringBuilder sb = new StringBuilder();

			float spaceWidth = spriteFont.MeasureString(" ").X;
			float lineWidth = 0f;

			foreach (string word in words)
			{
				Vector2 size = spriteFont.MeasureString(word);

				if (lineWidth + size.X < maxLineWidth)
				{
					sb.Append(word + " ");
					lineWidth += size.X + spaceWidth;
				}
				else
				{
					sb.Append("\n" + word + " ");
					lineWidth = size.X + spaceWidth;
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Wraps the provided text so that each line fits within the specified maximum line width.
		/// Handles text with multiple lines.
		/// </summary>
		/// <param name="spriteFont">The SpriteFont used for measuring text.</param>
		/// <param name="text">The text to wrap.</param>
		/// <param name="maxLineWidth">The maximum width of each line.</param>
		/// <returns>The wrapped text.</returns>
		private string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;

			var lines = text.Split('\n');

			StringBuilder finalText = new StringBuilder();
			for (int i = 0; i < lines.Length; i++)
			{
				string wrappedLine = WrapSingleLine(spriteFont, lines[i], maxLineWidth);
				finalText.Append(wrappedLine);

				if (i < lines.Length - 1)
				{
					finalText.Append("\n");
				}
			}

			return finalText.ToString();
		}

		#endregion
	}
}
