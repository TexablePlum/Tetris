using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tetris
{
	public enum RotationStep //enum do określenia kroku obrotu
	{
		Step0 = 0,
		Step90 = 1,
		Step180 = 2,
		Step270 = 3
	}

	public enum MoveDirection //enum do określenia kierunku ruchu
	{
		Down,
		Left,
		Right
	}

	public abstract class Tetronimo
	{
		protected Tile[,] grid; //Referencja do planszy
		protected List<Point> blocks; //Lista punktów reprezentujących kształt

		protected Color fill_color;
		protected Color obw_Color;

		protected static Color default_fill_color = new Color(22, 22, 20);
		protected static Color default_obw_Color = Color.Black;

		protected Point start_position;
		protected Point pivot;	//Punkt wokół którego obracamy kształt
		protected RotationStep step;

		protected int list_pivot_element; //Indeks elementu listy będący osią obrotu

		public Tetronimo(Tile[,] grid)
		{
			this.grid = grid;

			step = RotationStep.Step0; //Domyślny krok obrotu

			blocks = new List<Point>();
			blocks.Clear(); //Czyszczenie listy punktów (dla bezpieczeństwa)

			Initialize_Shape(); 
		}

		public abstract void Initialize_Shape();

		public virtual void Rotate()
		{
			Clear(); //Czyszczenie poprzedniego stanu obiektu

			for (var i = 0; i < blocks.Count; i++)
			{
				//Obliczenie współrzędnych punktu względem Pivot (środka kształtu)
				var newX = blocks[i].X - pivot.X;
				var newY = blocks[i].Y - pivot.Y;

				//Obrót względem Pivot
				var rotatedX = - newY;
				var rotatedY = newX;

				//Przesunięcie punktu z powrotem
				blocks[i] = new Point(pivot.X + rotatedX, pivot.Y + rotatedY);
			}

			Limes_Grid_Possitioner(); //Ustawienie kształtu względem planszy jeśli ten wychodzi poza nią po obrocie

			step = (RotationStep)(((int)step + 1) % 4); //Zmiana kroku obrotu

			Draw(); //Przerysowanie aby uniknąć migotania
		}

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

			Pivot_Updater(list_pivot_element); //Ustawienie nowego punktu obrotu

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




		//Metody pomocnicze:




		protected void Clear()
		{
			//Implementacja czyszczenia poprzedniego stanu obiektu
			foreach (var block in blocks)
			{
				grid[block.X, block.Y].Fill_Color = default_fill_color;
				grid[block.X, block.Y].Obw_Color = default_obw_Color;
			}
		}

		//Jeśli podczas obrotu kształt wychodzi poza planszę
		protected void Limes_Grid_Possitioner()
		{
			//Zmienne przechowujące maksymalne wartości wyjścia poza planszę
			int maxX = 0;
			int maxY = 0;

			//Implementacja ustawienia kształtu względem planszy
			foreach (var block in blocks)
			{
				if(block.X < 0 || block.Y < 0) //Jeśli kształt wychodzi poza planszę
				{
					if(block.X < maxX)
					{
						maxX = block.X;
					}
					else if(block.Y < maxY)
					{
						maxY = block.Y;
					}
				}
			}

			//Przesunięcie kształtu
			for (var i = 0; i < blocks.Count; i++)
			{
				blocks[i] = new Point(blocks[i].X - maxX, blocks[i].Y - maxY);
			}

			Pivot_Updater(list_pivot_element); //Ustawienie nowego punktu obrotu
		}

		private void Pivot_Updater(int element)
		{
			//Implementacja ustawienia punktu wokół którego obracamy kształt
			pivot = blocks[element];
		}

	}
}