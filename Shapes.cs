using Microsoft.Xna.Framework;

namespace Tetris
{
	public class ShapeI : Tetronimo
	{
		public ShapeI(Tile[,] grid, int start_position, Rotation rotation)
			: base(grid, start_position, rotation)
		{
			color = new Color (81, 225, 252);
		}

		public override void Initialize_Shape()
		{
			tiles = new Tile[4];
			if (rotation == Rotation.Zero || rotation == Rotation.OneEighty)
			{
				for (int i = 0; i < 4; i++)
				{
					tiles[i] = grid[position.X, position.Y + i];
					tiles[i].Fill_Color = color;
					tiles[i].Obw_Color = Color.White;
				}
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					tiles[i] = grid[position.X + i, position.Y];
					tiles[i].Fill_Color = color;
					tiles[i].Obw_Color = Color.White;
				}
			}
		}
	}
}
