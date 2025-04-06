using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the base class for letters used in the game.
	/// Contains common properties such as position, color, border color, and shape.
	/// </summary>
	public abstract class Letter
	{
		#region Private Fields

		/// <summary>
		/// The position of the letter.
		/// </summary>
		private Point letterPosition;

		/// <summary>
		/// The color of the letter.
		/// </summary>
		private Color letterColor;

		/// <summary>
		/// The border color of the letter.
		/// </summary>
		private Color letterBorderColor;

		/// <summary>
		/// The collection of points defining the shape of the letter.
		/// </summary>
		private List<Point> letterShape;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the position of the letter.
		/// </summary>
		public Point LetterPosition { get => letterPosition; set => letterPosition = value; }

		/// <summary>
		/// Gets or sets the color of the letter.
		/// </summary>
		public Color LetterColor { get => letterColor; set => letterColor = value; }

		/// <summary>
		/// Gets or sets the border color of the letter.
		/// </summary>
		public Color LetterBorderColor { get => letterBorderColor; set => letterBorderColor = value; }

		/// <summary>
		/// Gets or sets the shape of the letter as a list of points.
		/// </summary>
		public List<Point> LetterShape { get => letterShape; set => letterShape = value; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Letter"/> class with the specified position, color, and border color.
		/// </summary>
		/// <param name="letterPosition">The position of the letter.</param>
		/// <param name="letterColor">The color of the letter.</param>
		/// <param name="letterBorderColor">The border color of the letter.</param>
		public Letter(Point letterPosition, Color letterColor, Color letterBorderColor)
		{
			this.letterPosition = letterPosition;
			this.letterColor = letterColor;
			this.letterBorderColor = letterBorderColor;
			letterShape = new List<Point>();
			letterShape.Clear();
		}

		#endregion
	}

	/// <summary>
	/// Represents the letter 'T' with a specific shape.
	/// </summary>
	public class Letter_T : Letter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Letter_T"/> class with the specified position, color, and border color.
		/// The letter shape is defined by a set of points forming the letter 'T'.
		/// </summary>
		/// <param name="letterPosition">The position of the letter.</param>
		/// <param name="letterColor">The color of the letter.</param>
		/// <param name="letterBorderColor">The border color of the letter.</param>
		public Letter_T(Point letterPosition, Color letterColor, Color letterBorderColor)
			: base(letterPosition, letterColor, letterBorderColor)
		{
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 1));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 3));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 4));
		}
	}

	/// <summary>
	/// Represents the letter 'E' with a specific shape.
	/// </summary>
	public class Letter_E : Letter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Letter_E"/> class with the specified position, color, and border color.
		/// The letter shape is defined by a set of points forming the letter 'E'.
		/// </summary>
		/// <param name="letterPosition">The position of the letter.</param>
		/// <param name="letterColor">The color of the letter.</param>
		/// <param name="letterBorderColor">The border color of the letter.</param>
		public Letter_E(Point letterPosition, Color letterColor, Color letterBorderColor)
			: base(letterPosition, letterColor, letterBorderColor)
		{
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 1));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 3));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 4));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 4));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 4));
		}
	}

	/// <summary>
	/// Represents the letter 'R' with a specific shape.
	/// </summary>
	public class Letter_R : Letter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Letter_R"/> class with the specified position, color, and border color.
		/// The letter shape is defined by a set of points forming the letter 'R'.
		/// </summary>
		/// <param name="letterPosition">The position of the letter.</param>
		/// <param name="letterColor">The color of the letter.</param>
		/// <param name="letterBorderColor">The border color of the letter.</param>
		public Letter_R(Point letterPosition, Color letterColor, Color letterBorderColor)
			: base(letterPosition, letterColor, letterBorderColor)
		{
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 1));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 3));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 4));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 1));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 3));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 4));
		}
	}

	/// <summary>
	/// Represents the letter 'I' with a specific shape.
	/// </summary>
	public class Letter_I : Letter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Letter_I"/> class with the specified position, color, and border color.
		/// The letter shape is defined by a set of points forming the letter 'I'.
		/// </summary>
		/// <param name="letterPosition">The position of the letter.</param>
		/// <param name="letterColor">The color of the letter.</param>
		/// <param name="letterBorderColor">The border color of the letter.</param>
		public Letter_I(Point letterPosition, Color letterColor, Color letterBorderColor)
			: base(letterPosition, letterColor, letterBorderColor)
		{
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 1));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 3));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 4));
		}
	}

	/// <summary>
	/// Represents the letter 'S' with a specific shape.
	/// </summary>
	public class Letter_S : Letter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Letter_S"/> class with the specified position, color, and border color.
		/// The letter shape is defined by a set of points forming the letter 'S'.
		/// </summary>
		/// <param name="letterPosition">The position of the letter.</param>
		/// <param name="letterColor">The color of the letter.</param>
		/// <param name="letterBorderColor">The border color of the letter.</param>
		public Letter_S(Point letterPosition, Color letterColor, Color letterBorderColor)
			: base(letterPosition, letterColor, letterBorderColor)
		{
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 1));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 2));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 3));
			LetterShape.Add(new Point(letterPosition.X + 1, letterPosition.Y + 4));
			LetterShape.Add(new Point(letterPosition.X + 2, letterPosition.Y + 4));
			LetterShape.Add(new Point(letterPosition.X, letterPosition.Y + 4));
		}
	}
}
