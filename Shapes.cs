using Microsoft.Xna.Framework;

namespace Tetris
{
	public class ShapeI : Tetronimo
	{
		public ShapeI(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{}

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.Cyan;
			obw_Color = Color.White;

			for(var i=0; i<4; i++)
			{
				blocks.Add(new Point(current_position.X + i, current_position.Y));
			}
		}

		public override void Rotate()
		{
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}
	}

	public class ShapeZ : Tetronimo
	{
		public ShapeZ(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.LawnGreen;
			obw_Color = Color.White;

			//Kształt Z
			blocks.Add(new Point(current_position.X, current_position.Y));
			blocks.Add(new Point(current_position.X + 1, current_position.Y));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
			blocks.Add(new Point(current_position.X + 2, current_position.Y + 1));

		}

		public override void Rotate()
		{
			throw new System.NotImplementedException();
		}
	}

	public class ShapeS : Tetronimo
	{
		public ShapeS(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.Red;
			obw_Color = Color.White;

			//Kształt S
			blocks.Add(new Point(current_position.X, current_position.Y + 1));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
			blocks.Add(new Point(current_position.X + 1, current_position.Y));
			blocks.Add(new Point(current_position.X + 2, current_position.Y));

		}

		public override void Rotate()
		{
			throw new System.NotImplementedException();
		}
	}

	public class ShapeL : Tetronimo
	{
		public ShapeL(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.Orange;
			obw_Color = Color.White;

			//Kształt L
			blocks.Add(new Point(current_position.X, current_position.Y));
			blocks.Add(new Point(current_position.X, current_position.Y + 1));
			blocks.Add(new Point(current_position.X, current_position.Y + 2));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 2));
		}

		public override void Rotate()
		{
			throw new System.NotImplementedException();
		}
	}

	public class ShapeJ : Tetronimo
	{
		public ShapeJ(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.HotPink;
			obw_Color = Color.White;

			//Kształt J
			blocks.Add(new Point(current_position.X + 1, current_position.Y));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 2));
			blocks.Add(new Point(current_position.X, current_position.Y + 2));
		}

		public override void Rotate()
		{
			throw new System.NotImplementedException();
		}
	}

	public class ShapeT : Tetronimo
	{
		public ShapeT(Tile[,] grid, Point start_position)
			: base(grid, start_position)
		{ }

		public override void Initialize_Shape()
		{
			blocks.Clear();

			fill_color = Color.MediumPurple;
			obw_Color = Color.White;

			//Kształt T
			blocks.Add(new Point(current_position.X, current_position.Y));
			blocks.Add(new Point(current_position.X + 1, current_position.Y));
			blocks.Add(new Point(current_position.X + 2, current_position.Y));
			blocks.Add(new Point(current_position.X + 1, current_position.Y + 1));
		}

		public override void Rotate()
		{
			throw new System.NotImplementedException();
		}
	}
}
