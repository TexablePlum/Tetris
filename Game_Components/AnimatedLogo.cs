using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents an animated logo that extends the MainGrid class.
	/// This logo is composed of individual letters that animate by shifting their colors.
	/// </summary>
	public class AnimatedLogo : MainGrid
	{
		#region Private Fields

		/// <summary>
		/// The collection of letters that form the logo.
		/// </summary>
		private readonly List<Letter> logoLetters;

		/// <summary>
		/// The speed of the animation in milliseconds.
		/// </summary>
		private int animationSpeed;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the animation speed (in milliseconds).
		/// </summary>
		public int AnimationSpeed { get => animationSpeed; set => animationSpeed = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AnimatedLogo"/> class with the specified position, tile size, grid dimensions, and optional animation speed.
		/// </summary>
		/// <param name="position">The starting position of the logo.</param>
		/// <param name="tileSize">The size of each tile in the grid.</param>
		/// <param name="rows">The number of rows in the grid.</param>
		/// <param name="columns">The number of columns in the grid.</param>
		/// <param name="animationSpeed">The speed of the logo animation (in milliseconds). Defaults to 0 (no animation).</param>
		public AnimatedLogo(Point position, int tileSize, int rows, int columns, int animationSpeed = 0)
			: base(position, tileSize, rows, columns)
		{
			this.animationSpeed = animationSpeed;
			logoLetters = new List<Letter>();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content for the animated logo, including base grid content and initializes the logo letters.
		/// Also sets the transparent border and fill colors from the current color theme.
		/// If an animation speed is provided, the logo animation is started.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		public new void LoadContent(SpriteBatch spriteBatch)
		{
			base.LoadContent(spriteBatch);
			InitializeLogo();

			ObwColor = ColorTheme.GameTheme.PanelTransparentBorderColor;
			FillColor = ColorTheme.GameTheme.PanelTransparentFillColor;

			if (animationSpeed > 0)
			{
				LogoAnimation(animationSpeed);
			}
		}

		/// <summary>
		/// Draws the animated logo by rendering the base panel and drawing the letters grid.
		/// </summary>
		public new void Draw()
		{
			base.Draw(); // Draw the base panel.
			DrawLettersGrid(logoLetters); // Draw the letters on the grid.
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initializes the logo letters with their respective positions and colors.
		/// </summary>
		private void InitializeLogo()
		{
			Letter_T letter_T = new Letter_T(new Point(0, 0), Color.Red, Color.White);
			Letter_E letter_E = new Letter_E(new Point(4, 0), Color.Orange, Color.White);
			Letter_T letter_T2 = new Letter_T(new Point(8, 0), Color.Yellow, Color.White);
			Letter_R letter_R = new Letter_R(new Point(12, 0), Color.SpringGreen, Color.White);
			Letter_I letter_I = new Letter_I(new Point(16, 0), Color.Cyan, Color.White);
			Letter_S letter_S = new Letter_S(new Point(18, 0), Color.Purple, Color.White);

			logoLetters.Add(letter_T);
			logoLetters.Add(letter_E);
			logoLetters.Add(letter_T2);
			logoLetters.Add(letter_R);
			logoLetters.Add(letter_I);
			logoLetters.Add(letter_S);
		}

		/// <summary>
		/// Asynchronously animates the logo by continuously shifting the colors of the letters.
		/// </summary>
		/// <param name="animationSpeed">The delay in milliseconds between animation frames.</param>
		private async void LogoAnimation(int animationSpeed)
		{
			while (true)
			{
				// Shift the colors in the logo letters collection.
				Color lastColor = logoLetters[^1].LetterColor;
				for (int i = logoLetters.Count - 1; i > 0; i--)
				{
					logoLetters[i].LetterColor = logoLetters[i - 1].LetterColor;
				}
				logoLetters[0].LetterColor = lastColor;

				await Task.Delay(animationSpeed);
			}
		}

		#endregion
	}
}
