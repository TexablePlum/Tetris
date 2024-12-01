using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
    public class Main_Grid : Panel
    {
        private SpriteBatch spriteBatch;

        private int tile_size;
        private int rows;
        private int columns;
        private Tile[,] grid;   //Tablica kafelków

        public Main_Grid(Point position, int tile_size, int rows, int columns)
           : base(position, Calculate_Panel_Size(tile_size, rows, columns).width, Calculate_Panel_Size(tile_size, rows, columns).height)
        {
            this.tile_size = tile_size;
            this.rows = rows;
            this.columns = columns;
        }

        public new void Load_Content(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            // Wczytanie zawartości panelu
            base.Load_Content(spriteBatch);

            // Stworzenie planszy
            Create_Grid();
        }

        private void Create_Grid() //Inicjalizacja planszy
        {
            grid = Grid.CreateGrid(spriteBatch, rows, columns, tile_size, Color_Theme.Game_Theme.Main_Grid_Fill_Color, Color_Theme.Game_Theme.Main_Grid_Border_Color, Position);
        }

        //Rysowanie kształtów na planszy
        public void Draw_Shapes_Grid(List<Tetronimo> shapes)
        {
            // Bazowe rysowanie panelu
            Draw();

            // Rysowanie kafelków
            Grid.DrawShapesGrid(grid, Color_Theme.Game_Theme.Main_Grid_Fill_Color, Color_Theme.Game_Theme.Main_Grid_Border_Color, shapes);
        }

        //Rysowanie liter na planszy
		public void Draw_Letters_Grid(List<Letters> letters)
		{
			// Bazowe rysowanie panelu
			Draw();

			// Rysowanie kafelków
			Grid.DrawLettersGrid(grid, Color.Transparent, Color.Transparent, letters);
		}


        //Obliczanie rozmiaru panelu
        private static (int width, int height )Calculate_Panel_Size(int tile_size, int rows, int columns)
        {
            var width = rows * tile_size;
            var height = columns * tile_size;
            return (width, height);
        }
	}
}