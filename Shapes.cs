using Microsoft.Xna.Framework;

namespace Tetris
{
	//TODO: Implementacja Inicjalizacji i Obracania kształtów Tetrisa !NIE! jest satysfakcjonująca (ze względu na jej rospasanie, sztywność i toporność), ale DZIAŁA

	public class ShapeI : Tetronimo
	{

		public ShapeI(Tile[,] grid, Point start_position, ShapeRotation shapeRotation)
			: base(grid, start_position, shapeRotation)
		{}

		public override void Initialize_Shape()
		{
			blocks.Clear(); //Czyszczenie listy punktów (dla bezpieczeństwa)

			fill_color = Color.Cyan;
			obw_Color = Color.White;

			if(shape_rotation == ShapeRotation.Zero || shape_rotation == ShapeRotation.OneEighty)
			{
				for (var i = 0; i < 4; i++)
				{
					blocks.Add(new Point(current_position.X + i, current_position.Y));
				}
			}
            else
            {
				for (var i = 0; i < 4; i++)
				{
					blocks.Add(new Point(current_position.X, current_position.Y + i));
				}
			}
        }

		public override void Rotate()
		{
			shape_axis = blocks[1]; //Punkt wokół którego obracamy kształt
			shape_rotation = (ShapeRotation)(((int)shape_rotation + 90) % 360); //Zmiana obrotu

			Clear(); //Czyszczenie poprzedniego stanu obiektu

			//Tutaj manipulujemy punktami kształtu tylko w 2 płaszczyznach wertykalnie i horyzontalnie
			if (shape_rotation == ShapeRotation.Zero || shape_rotation == ShapeRotation.OneEighty)
			{
				for (var i = -1; i < 3; i++)
				{
					blocks[i+1] = new Point(shape_axis.X + i, shape_axis.Y);
				}
			}
			else
			{
				for (var i = -1; i < 3; i++)
				{
					blocks[i + 1] = new Point(shape_axis.X, shape_axis.Y + i);
				}
			}

			Draw(); //Przerysowanie kształtu
		}
	}

	public class ShapeO : Tetronimo
	{
		public ShapeO(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.Yellow;
			obw_Color = Color.White;

			//Kształt kwadratu
			blocks.Add(new Point(current_position.X, current_position.Y));
			blocks.Add(new Point(current_position.X + 1, current_position.Y));
			blocks.Add(new Point(current_position.X, current_position.Y + 1));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
		}

		public override void Rotate()
		{
			//Kształt "0" się nie obraca, więc nie ma implementacji. Metoda zostaje aby zachować zgodność z klasą abstrakcyjną
		}
	}

	public class ShapeS : Tetronimo
	{
		public ShapeS(Tile[,] grid, Point start_position, ShapeRotation shapeRotation)
			: base(grid, start_position, shapeRotation)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.Red;
			obw_Color = Color.White;

			//Kształt S
			if(shape_rotation == ShapeRotation.Zero || shape_rotation == ShapeRotation.OneEighty)
			{
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
			}
			else
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 2));
			}
		}

		public override void Rotate()
		{
			shape_axis = blocks[1]; //Punkt wokół którego obracamy kształt
			shape_rotation = (ShapeRotation)(((int)shape_rotation + 90) % 360); //Zmiana obrotu

			Clear(); //Czyszczenie poprzedniego stanu obiektu

			//Tutaj manipulujemy punktami kształtu tylko w 2 płaszczyznach wertykalnie i horyzontalnie
			if (shape_rotation == ShapeRotation.Zero || shape_rotation == ShapeRotation.OneEighty)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y - 1);
			}
			else
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y + 1);
			}

			Draw(); //Przerysowanie kształtu
		}
	}

	public class ShapeZ : Tetronimo
	{
		public ShapeZ(Tile[,] grid, Point start_position, ShapeRotation shapeRotation)
			: base(grid, start_position, shapeRotation)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.LawnGreen;
			obw_Color = Color.White;

			//Kształt Z
			if (shape_rotation == ShapeRotation.Zero || shape_rotation == ShapeRotation.OneEighty)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
				blocks.Add(new Point(current_position.X + 2, current_position.Y + 1));
			}
			else
			{
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
			}

		}

		public override void Rotate()
		{
			shape_axis = blocks[1]; // Punkt wokół którego obracamy kształt
			shape_rotation = (ShapeRotation)(((int)shape_rotation + 90) % 360); // Zmiana obrotu

			Clear(); // Czyszczenie poprzedniego stanu obiektu

			// Manipulacja punktami kształtu w dwóch płaszczyznach wertykalnie i horyzontalnie
			if (shape_rotation == ShapeRotation.Zero || shape_rotation == ShapeRotation.OneEighty)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y + 1);
			}
			else
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X - 1, shape_axis.Y + 1);
			}

			Draw(); // Przerysowanie kształtu
		}
	}

	public class ShapeL : Tetronimo
	{
		public ShapeL(Tile[,] grid, Point start_position, ShapeRotation shapeRotation)
			: base(grid, start_position, shapeRotation)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.Orange;
			obw_Color = Color.White;

			//Kształt L
			if (shape_rotation == ShapeRotation.Zero)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
			}
			else if (shape_rotation == ShapeRotation.Ninety)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
				blocks.Add(new Point(current_position.X - 1, current_position.Y));
			}
			else if (shape_rotation == ShapeRotation.OneEighty)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y - 1));
			}
			else
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 2));
			}

		}

		public override void Rotate()
		{
			shape_axis = blocks[1]; // Punkt wokół którego obracamy kształt
			shape_rotation = (ShapeRotation)(((int)shape_rotation + 90) % 360); // Zmiana obrotu

			Clear(); // Czyszczenie poprzedniego stanu obiektu

			// Manipulacja punktami kształtu w czterech płaszczyznach
			if (shape_rotation == ShapeRotation.Zero)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X - 1, shape_axis.Y + 1);
			}
			else if (shape_rotation == ShapeRotation.Ninety)
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X - 1, shape_axis.Y - 1);
			}
			else if (shape_rotation == ShapeRotation.OneEighty)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y - 1);
			}
			else
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y + 1);
			}

			Draw(); // Przerysowanie kształtu
		}
	}

	public class ShapeJ : Tetronimo
	{
		public ShapeJ(Tile[,] grid, Point start_position, ShapeRotation shapeRotation)
			: base(grid, start_position, shapeRotation)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.HotPink;
			obw_Color = Color.White;

			//Kształt J
			if (shape_rotation == ShapeRotation.Zero)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y + 1));
			}
			else if (shape_rotation == ShapeRotation.Ninety)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
				blocks.Add(new Point(current_position.X - 1, current_position.Y + 2));
			}
			else if (shape_rotation == ShapeRotation.OneEighty)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y - 1));
			}
			else
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
			}
		}

		public override void Rotate()
		{
			shape_axis = blocks[1]; // Punkt wokół którego obracamy kształt
			shape_rotation = (ShapeRotation)(((int)shape_rotation + 90) % 360); // Zmiana obrotu

			Clear(); // Czyszczenie poprzedniego stanu obiektu

			// Manipulacja punktami kształtu w czterech płaszczyznach
			if (shape_rotation == ShapeRotation.Zero)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y + 1);
			}
			else if (shape_rotation == ShapeRotation.Ninety)
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X - 1, shape_axis.Y + 1);
			}
			else if (shape_rotation == ShapeRotation.OneEighty)
			{
				blocks[0] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X - 1, shape_axis.Y - 1);
			}
			else
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y + 1);
			}

			Draw(); // Przerysowanie kształtu
		}
	}

	public class ShapeT : Tetronimo
	{
		public ShapeT(Tile[,] grid, Point start_position, ShapeRotation shapeRotation)
			: base(grid, start_position, shapeRotation)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.MediumPurple;
			obw_Color = Color.White;

			//Kształt T
			if (shape_rotation == ShapeRotation.Zero)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
			}
			else if (shape_rotation == ShapeRotation.Ninety)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
				blocks.Add(new Point(current_position.X - 1, current_position.Y + 1));
			}
			else if (shape_rotation == ShapeRotation.OneEighty)
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y));
				blocks.Add(new Point(current_position.X + 2, current_position.Y));
				blocks.Add(new Point(current_position.X + 1, current_position.Y - 1));
			}
			else
			{
				blocks.Add(new Point(current_position.X, current_position.Y));
				blocks.Add(new Point(current_position.X, current_position.Y + 1));
				blocks.Add(new Point(current_position.X, current_position.Y + 2));
				blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
			}

		}

		public override void Rotate()
		{
			shape_axis = blocks[1]; // Punkt wokół którego obracamy kształt
			shape_rotation = (ShapeRotation)(((int)shape_rotation + 90) % 360); // Zmiana obrotu

			Clear(); // Czyszczenie poprzedniego stanu obiektu

			// Manipulacja punktami kształtu w czterech płaszczyznach
			if (shape_rotation == ShapeRotation.Zero)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X, shape_axis.Y + 1);
			}
			else if (shape_rotation == ShapeRotation.Ninety)
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X - 1, shape_axis.Y);
			}
			else if (shape_rotation == ShapeRotation.OneEighty)
			{
				blocks[0] = new Point(shape_axis.X - 1, shape_axis.Y);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X + 1, shape_axis.Y);
				blocks[3] = new Point(shape_axis.X, shape_axis.Y - 1);
			}
			else
			{
				blocks[0] = new Point(shape_axis.X, shape_axis.Y - 1);
				blocks[1] = new Point(shape_axis.X, shape_axis.Y);
				blocks[2] = new Point(shape_axis.X, shape_axis.Y + 1);
				blocks[3] = new Point(shape_axis.X + 1, shape_axis.Y);
			}

			Draw(); // Przerysowanie kształtu
		}
	}
}
