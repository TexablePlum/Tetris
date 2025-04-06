using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Specifies the steps of rotation for a Tetronimo shape.
	/// </summary>
	public enum RotationStep //enum do określenia kroku obrotu
	{
		/// <summary>
		/// Rotation step 0°.
		/// </summary>
		Step0 = 0,
		/// <summary>
		/// Rotation step 90°.
		/// </summary>
		Step90 = 1,
		/// <summary>
		/// Rotation step 180°.
		/// </summary>
		Step180 = 2,
		/// <summary>
		/// Rotation step 270°.
		/// </summary>
		Step270 = 3
	}

	/// <summary>
	/// Specifies the direction in which a Tetronimo shape can move.
	/// </summary>
	public enum MoveDirection //enum do określenia kierunku ruchu
	{
		/// <summary>
		/// Move downward.
		/// </summary>
		Down,
		/// <summary>
		/// Move to the left.
		/// </summary>
		Left,
		/// <summary>
		/// Move to the right.
		/// </summary>
		Right
	}

	/// <summary>
	/// Represents a Tetronimo shape in the Tetris game.
	/// Provides functionality for initializing the shape, rotating it, and moving it.
	/// </summary>
	public abstract class Tetronimo
	{
		/// <summary>
		/// List of points representing the shape.
		/// </summary>
		protected List<Point> blocks; //Lista punktów reprezentujących kształt

		/// <summary>
		/// The fill color of the shape.
		/// </summary>
		protected Color fillColor;
		/// <summary>
		/// The border color of the shape.
		/// </summary>
		protected Color obwColor;

		/// <summary>
		/// The starting position of the shape.
		/// </summary>
		protected Point startPosition; //Punkt startowy kształtu
		/// <summary>
		/// The pivot point around which the shape rotates.
		/// </summary>
		protected Point pivot;  //Punkt wokół którego obracamy kształt
		/// <summary>
		/// The index of the pivot element in the blocks list.
		/// </summary>
		protected int listPivotElement; //Indeks elementu listy będący osią obrotu

		/// <summary>
		/// The current rotation step of the shape.
		/// </summary>
		protected RotationStep step; //Krok obrotu

		/// <summary>
		/// Indicates whether the shape is active.
		/// </summary>
		protected bool isActive; //Czy kształt jest aktywny

		/// <summary>
		/// Gets the list of points representing the shape.
		/// </summary>
		public List<Point> Blocks { get { return blocks; } }
		/// <summary>
		/// Gets the pivot point of the shape.
		/// </summary>
		public Point Pivot { get { return pivot; } }
		/// <summary>
		/// Gets or sets the fill color of the shape.
		/// </summary>
		public Color FillColor { get { return fillColor; } set { fillColor = value; } }
		/// <summary>
		/// Gets or sets the border color of the shape.
		/// </summary>
		public Color ObwColor { get { return obwColor; } set { obwColor = value; } }
		/// <summary>
		/// Gets or sets a value indicating whether the shape is active.
		/// </summary>
		public bool IsActive { get { return isActive; } set { isActive = value; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="Tetronimo"/> class.
		/// Sets the default rotation step, clears the blocks list, and marks the shape as active.
		/// </summary>
		public Tetronimo()
		{
			step = RotationStep.Step0; //Domyślny krok obrotu

			blocks = new List<Point>();
			blocks.Clear(); //Czyszczenie listy punktów (dla bezpieczeństwa) 
			isActive = true; //Domyślnie kształt jest aktywny
		}

		/// <summary>
		/// Initializes the shape with the specified starting point.
		/// </summary>
		/// <param name="startPoint">The starting point for the shape.</param>
		public abstract void InitializeShape(Point startPoint);

		/// <summary>
		/// Rotates the shape by rotating each block around the pivot, adjusting its position within the grid,
		/// and updating the rotation step.
		/// </summary>
		public virtual void Rotate()
		{
			for (var i = 0; i < blocks.Count; i++)
			{
				//Obliczenie współrzędnych punktu względem Pivot (środka kształtu)
				var newX = blocks[i].X - pivot.X;
				var newY = blocks[i].Y - pivot.Y;

				//Obrót względem Pivot
				var rotatedX = -newY;
				var rotatedY = newX;

				//Przesunięcie punktu z powrotem
				blocks[i] = new Point(pivot.X + rotatedX, pivot.Y + rotatedY);
			}

			LimesGridPossitioner(); //Ustawienie kształtu względem planszy jeśli ten wychodzi poza nią po obrocie

			step = (RotationStep)(((int)step + 1) % 4); //Zmiana kroku obrotu
		}

		/// <summary>
		/// Moves the shape in the specified direction.
		/// </summary>
		/// <param name="direction">The direction in which to move the shape.</param>
		public virtual void Move(MoveDirection direction)
		{
			//Implementacja ruchu

			if (direction == MoveDirection.Down) //Ruch w dół
			{
				for (var i = 0; i < blocks.Count; i++)
				{
					blocks[i] = new Point(blocks[i].X, blocks[i].Y + 1);
				}
			}
			else if (direction == MoveDirection.Left) //Ruch w lewo
			{
				for (var i = 0; i < blocks.Count; i++)
				{
					blocks[i] = new Point(blocks[i].X - 1, blocks[i].Y);
				}
			}
			else if (direction == MoveDirection.Right) //Ruch w prawo
			{
				for (var i = 0; i < blocks.Count; i++)
				{
					blocks[i] = new Point(blocks[i].X + 1, blocks[i].Y);
				}
			}
			else
			{
				throw new System.Exception("Nieprawidłowy kierunek ruchu!");
			}

			PivotUpdater(listPivotElement); //Ustawienie nowego punktu obrotu
		}

		//Metody pomocnicze:

		/// <summary>
		/// Adjusts the shape's position if it extends beyond the grid boundaries.
		/// Assumes a grid with 10 columns and 20 rows.
		/// </summary>
		protected void LimesGridPossitioner()
		{
			// Załóżmy, że plansza ma 10 kolumn i 20 wierszy
			int gridWidth = 10;
			int gridHeight = 20;

			int shiftX = 0;
			int shiftY = 0;

			// Korekta dla lewych i górnych granic
			foreach (var block in blocks)
			{
				if (block.X < 0)
				{
					int diff = -block.X;
					if (diff > shiftX)
						shiftX = diff;
				}
				if (block.Y < 0)
				{
					int diff = -block.Y;
					if (diff > shiftY)
						shiftY = diff;
				}
			}
			// Przesuwamy kształt, aby nie wychodził na lewo ani górę
			for (int i = 0; i < blocks.Count; i++)
			{
				blocks[i] = new Point(blocks[i].X + shiftX, blocks[i].Y + shiftY);
			}

			// Korekta dla prawej i dolnej granicy
			int maxX = int.MinValue;
			int maxY = int.MinValue;
			foreach (var block in blocks)
			{
				if (block.X > maxX)
					maxX = block.X;
				if (block.Y > maxY)
					maxY = block.Y;
			}
			if (maxX >= gridWidth)
			{
				int diff = maxX - gridWidth + 1;
				for (int i = 0; i < blocks.Count; i++)
				{
					blocks[i] = new Point(blocks[i].X - diff, blocks[i].Y);
				}
			}
			if (maxY >= gridHeight)
			{
				int diff = maxY - gridHeight + 1;
				for (int i = 0; i < blocks.Count; i++)
				{
					blocks[i] = new Point(blocks[i].X, blocks[i].Y - diff);
				}
			}

			PivotUpdater(listPivotElement);
		}

		/// <summary>
		/// Updates the pivot point based on the specified index in the blocks list.
		/// </summary>
		/// <param name="element">The index of the pivot element.</param>
		private void PivotUpdater(int element)
		{
			if (isActive)
			{
				//Implementacja ustawienia punktu wokół którego obracamy kształt
				pivot = blocks[element];
			}
		}

		/// <summary>
		/// Removes the specified block from the shape.
		/// </summary>
		/// <param name="block">The block (point) to remove.</param>
		public void RemoveBlock(Point block)
		{
			if (blocks.Contains(block))
			{
				blocks.Remove(block);
			}
		}
	}
}
