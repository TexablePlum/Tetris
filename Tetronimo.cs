using Microsoft.Xna.Framework;

namespace Tetris
{
	//Enum do przechowywania rotacji
	public enum Rotation
	{
		Zero = 0,
		Ninety = 90,
		OneEighty = 180,
		TwoSeventy = 270
	}

	public abstract class Tetronimo
	{
		protected Tile[,] grid;
		protected Tile[] tiles;
		protected Point position;
		protected Rotation rotation;
		protected Color color;

		public Tetronimo(Tile[,] grid, int start_position, Rotation rotation)
		{
			this.grid = grid;
			position = new Point(start_position, 0);
			this.rotation = rotation;

			Initialize_Shape();
		}

		public abstract void Initialize_Shape();

		public virtual void Rotate()
		{
			//TODO: Implementacja rotacji
		}

		public virtual void Move()
		{
			//TODO: Implementacja ruchu
		}

		public virtual void Collision()
		{
			//TODO: Implementacja kolizji
		}

		public virtual void Draw()
		{
			//Implementacja rysowania
			foreach (var tile in tiles)
			{
				tile.Obw_Color = Color.White;
				tile.Fill_Color = color;
				tile.Tile_Draw();
			}
		}

	}
}