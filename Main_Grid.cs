using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
	public class Main_Grid : Panel
	{
		private SpriteBatch spriteBatch;
		private GraphicsDevice graphicsDevice;

		private readonly int rows = 10;
		private readonly int columns = 20;
		private Tile[,] grid;
		private ShapeT shapei;

		private bool isSpacePressed = false; // Śledzenie stanu klawisza spacji

		public Main_Grid(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Point position, int width, int height)
		   : base(spriteBatch, graphicsDevice, position, width, height)
		{
			this.spriteBatch = spriteBatch;
			this.graphicsDevice = graphicsDevice;

			Create_Grid();
			shapei = new ShapeT(grid, new Point(2, 2), ShapeRotation.Zero);
		}

		private void Create_Grid() //Inicjalizacja planszy
		{
			grid = new Tile[rows, columns];
			int tile_size = Tile_Size();

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					grid[i, j] = new Tile(spriteBatch, graphicsDevice, tile_size, Color.Black, Color.White, new Point(Position.X + i * tile_size, Position.Y + j * tile_size));
				}
			}
		}

		public void Update_Grid()
		{
			KeyboardState keyboardState = Keyboard.GetState();

			// Sprawdzenie stanu klawisza spacji
			if (keyboardState.IsKeyDown(Keys.Space))
			{
				if (!isSpacePressed) // Jeśli klawisz nie był wcześniej wciśnięty
				{
					shapei.Rotate(); // Obrót kształtu
					isSpacePressed = true; // Zaznacz, że klawisz został wciśnięty
				}
			}
			else
			{
				isSpacePressed = false; // Resetuj stan, gdy klawisz zostanie zwolniony
			}
		}

		public void Draw_Grid()
		{
			//bazowe rysowanie panelu
			base.Draw();

			// Rysowanie kafelków
			foreach (var tile in grid)
			{
				tile.Tile_Draw();
			}
			shapei.Draw();
		}


		private int Tile_Size()
		{
			if (Height / Width != 2)
			{
				throw new ArgumentException("Grid is not in a 2:1 ratio.");
			}

			return Width / rows;
		}
	}
}