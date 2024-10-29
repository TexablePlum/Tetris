using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris
{
	public enum MoveDirection //enum do określenia kierunku ruchu
	{
		Down,
		Left,
		Right
	}

	public enum ShapeRotation //enum do określenia obrotu kształtu
	{
		Zero = 0,
		Ninety = 90,
		OneEighty = 180,
		TwoSeventy = 270
	}

	public abstract class Tetronimo
	{
		protected Tile[,] grid; //Referencja do planszy
		protected List<Point> blocks; //Lista punktów reprezentujących kształt

		protected Color fill_color;
		protected Color obw_Color;

		protected Point current_position;
		protected Point start_position;

		protected Point shape_axis; //Punkt wokół którego obracamy kształt
		protected ShapeRotation shape_rotation; //Obecny obrót kształtu

		public Tetronimo(Tile[,] grid, Point start_position, ShapeRotation shape_rotation = ShapeRotation.Zero)
		{
			this.grid = grid;
			this.start_position = start_position;
			this.shape_rotation = shape_rotation;
			blocks = new List<Point>(); 
			current_position = start_position;

			Initialize_Shape(); 
		}

		public abstract void Initialize_Shape();

		public abstract void Rotate();

		public virtual void Move(MoveDirection direction)
		{
			//Implementacja ruchu

			Clear(); //Czyszczenie poprzedniego stanu obiektu

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

			Draw(); //Przerysowanie aby uniknąć migotania
		}

		public virtual void Draw()
		{
			//Implementacja rysowania
			foreach (var block in blocks)
			{
				grid[block.X, block.Y].Fill_Color = fill_color;
				grid[block.X, block.Y].Obw_Color = obw_Color;
			}
		}

		protected void Clear()
		{
			//Implementacja czyszczenia poprzedniego stanu obiektu
			foreach (var block in blocks)
			{
				grid[block.X, block.Y].Fill_Color = Color.Black;
				grid[block.X, block.Y].Obw_Color = Color.White;
			}
		}

	}
}