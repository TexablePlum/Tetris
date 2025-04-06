using Microsoft.Xna.Framework;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Specifies the various Tetronimo shapes.
	/// </summary>
	public enum TetronimoShape
	{
		/// <summary>
		/// No shape.
		/// </summary>
		None,
		/// <summary>
		/// The I shape.
		/// </summary>
		ShapeI,
		/// <summary>
		/// The O shape.
		/// </summary>
		ShapeO,
		/// <summary>
		/// The S shape.
		/// </summary>
		ShapeS,
		/// <summary>
		/// The Z shape.
		/// </summary>
		ShapeZ,
		/// <summary>
		/// The L shape.
		/// </summary>
		ShapeL,
		/// <summary>
		/// The J shape.
		/// </summary>
		ShapeJ,
		/// <summary>
		/// The T shape.
		/// </summary>
		ShapeT,
	}

	//Implementacja Inicjalizacji i Obracania kształtów Tetrisa: DZIAŁA

	/// <summary>
	/// Represents the I Tetronimo shape.
	/// </summary>
	public class ShapeI : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeI"/> class with the default starting position.
		/// </summary>
		public ShapeI()
			: base()
		{
			startPosition = new Point(3, 0);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeI"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeI(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the I shape by creating its blocks and setting the pivot.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.Cyan;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 3, startPosition.Y));

			listPivotElement = 2;
			pivot = blocks[listPivotElement];
		}

		/// <summary>
		/// Rotates the I shape. For 0° and 180° steps, it uses the base rotation;
		/// for 90° and 270° steps, it rotates each block manually.
		/// </summary>
		public override void Rotate()
		{
			if (step == RotationStep.Step0 || step == RotationStep.Step180)
			{
				base.Rotate();
			}
			else if (step == RotationStep.Step90 || step == RotationStep.Step270)
			{
				for (var i = 0; i < blocks.Count; i++)
				{
					// Calculate coordinates relative to the pivot.
					var newX = blocks[i].X - pivot.X;
					var newY = blocks[i].Y - pivot.Y;

					// Rotate around the pivot.
					var rotatedX = newY;
					var rotatedY = -newX;

					// Translate the block back.
					blocks[i] = new Point(pivot.X + rotatedX, pivot.Y + rotatedY);
				}

				LimesGridPossitioner(); // Adjust shape position if it goes outside the grid.
				step = (RotationStep)(((int)step + 1) % 4); // Update rotation step.
			}
		}
	}

	/// <summary>
	/// Represents the O Tetronimo shape.
	/// </summary>
	public class ShapeO : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeO"/> class with the default starting position.
		/// </summary>
		public ShapeO()
			: base()
		{
			startPosition = new Point(4, 0);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeO"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeO(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the O shape by creating its blocks.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.Yellow;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y));
			blocks.Add(new Point(startPosition.X, startPosition.Y + 1));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y + 1));
		}

		/// <summary>
		/// Rotates the O shape.
		/// The O shape does not rotate.
		/// </summary>
		public override void Rotate()
		{
			// Do not rotate the O shape.
		}
	}

	/// <summary>
	/// Represents the S Tetronimo shape.
	/// </summary>
	public class ShapeS : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeS"/> class with the default starting position.
		/// </summary>
		public ShapeS()
			: base()
		{
			startPosition = new Point(3, 1);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeS"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeS(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the S shape by creating its blocks and setting the pivot.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.Red;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y - 1));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y - 1));

			listPivotElement = 1;
			pivot = blocks[listPivotElement];
		}
	}

	/// <summary>
	/// Represents the Z Tetronimo shape.
	/// </summary>
	public class ShapeZ : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeZ"/> class with the default starting position.
		/// </summary>
		public ShapeZ()
			: base()
		{
			startPosition = new Point(3, 0);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeZ"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeZ(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the Z shape by creating its blocks and setting the pivot.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.LawnGreen;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y + 1));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y + 1));

			listPivotElement = 2;
			pivot = blocks[listPivotElement];
		}
	}

	/// <summary>
	/// Represents the L Tetronimo shape.
	/// </summary>
	public class ShapeL : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeL"/> class with the default starting position.
		/// </summary>
		public ShapeL()
			: base()
		{
			startPosition = new Point(3, 1);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeL"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeL(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the L shape by creating its blocks and setting the pivot.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.Orange;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y - 1));

			listPivotElement = 1;
			pivot = blocks[listPivotElement];
		}
	}

	/// <summary>
	/// Represents the J Tetronimo shape.
	/// </summary>
	public class ShapeJ : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeJ"/> class with the default starting position.
		/// </summary>
		public ShapeJ()
			: base()
		{
			startPosition = new Point(3, 0);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeJ"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeJ(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the J shape by creating its blocks and setting the pivot.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.HotPink;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X, startPosition.Y + 1));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y + 1));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y + 1));

			listPivotElement = 2;
			pivot = blocks[listPivotElement];
		}
	}

	/// <summary>
	/// Represents the T Tetronimo shape.
	/// </summary>
	public class ShapeT : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeT"/> class with the default starting position.
		/// </summary>
		public ShapeT()
			: base()
		{
			startPosition = new Point(4, 0);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeT"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeT(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the T shape by creating its blocks and setting the pivot.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.MediumPurple;
			obwColor = Color.White;

			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X - 1, startPosition.Y + 1));
			blocks.Add(new Point(startPosition.X, startPosition.Y + 1));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y + 1));

			listPivotElement = 2;
			pivot = blocks[listPivotElement];
		}
	}

	/// <summary>
	/// Represents a Tetronimo shape that fills an entire line.
	/// </summary>
	public class ShapeFullLine : Tetronimo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeFullLine"/> class with the default starting position.
		/// </summary>
		public ShapeFullLine()
			: base()
		{
			startPosition = new Point(0, 0);
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeFullLine"/> class with the specified starting position.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public ShapeFullLine(Point startPosition)
			: base()
		{
			this.startPosition = startPosition;
			InitializeShape(startPosition);
		}

		/// <summary>
		/// Initializes the full line shape by creating a series of blocks that span the entire line.
		/// </summary>
		/// <param name="startPosition">The starting position for the shape.</param>
		public override void InitializeShape(Point startPosition)
		{
			fillColor = Color.White;
			obwColor = Color.Black;
			blocks.Add(new Point(startPosition.X, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 1, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 2, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 3, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 4, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 5, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 6, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 7, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 8, startPosition.Y));
			blocks.Add(new Point(startPosition.X + 9, startPosition.Y));
		}
	}
}
