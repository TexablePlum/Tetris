using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris
{
	public class Next_Shape_Panel : Panel
	{
		private readonly SpriteFont font;
		private readonly string next_shape_text = "Next Shape:";
		private Tile[,] grid;
		private const int tile_size = 42;

		// Lista kształtów do wyświetlenia !!!Tworzona po to aby zachować zgodnoś z metodą DrawGrid z klasy Grid!!!
		private List<Tetronimo> shapes = new List<Tetronimo>();

		// Buforowane siatki dla kształtów
		private Dictionary<Tetronimo_Shape, Tile[,]> gridCache = new();

		private Tetronimo_Shape tetronimo_shape = Tetronimo_Shape.ShapeL;

		// Właściwość do zmiany kształtu i sprawdzenia czy zmienił się
		public Tetronimo_Shape Tetronimo_Shape
		{
			get => tetronimo_shape;
			set
			{
				if (tetronimo_shape != value)
				{
					tetronimo_shape = value;
					SetGridFromCache();
				}
			}
		}

		public Next_Shape_Panel(SpriteBatch spriteBatch, ContentManager content)
			: base(spriteBatch, new Point(Score_Panel.Position.X, Score_Panel.Position.Y + Score_Panel.Height + 50), Score_Panel.Width, 150)
		{
			SpriteBatch = spriteBatch;
			font = content.Load<SpriteFont>("Fonts/Control_Panel_Font");

			// Ustawienie kolorów na transparentne (przezroczyste)
			Fill_Color = Color.Transparent;
			Obw_Color = Color.Transparent;

			// Stworzenie siatek dla wszystkich kształtów
			Create_Grids();
			// Ustawienie siatki dla aktualnego kształtu
			SetGridFromCache();

		}

		public new void Draw()
		{
			base.Draw(); // Rysowanie panelu z klasy bazowej Panel

			SpriteBatch.Begin();
			SpriteBatch.DrawString(font, next_shape_text, new Vector2(Text_X_Positioner(next_shape_text, font), Position.Y), Color.White);
			SpriteBatch.End();

			// Rysowanie siatki, jeśli zmienna tetronimo_shape jest różna od domyślnej i lista kształtów nie jest pusta
			if(tetronimo_shape != default && shapes.Count > 0)
			{
				Grid.DrawGrid(grid, Fill_Color, Obw_Color, shapes);
			}
		}



		// !!!	Metody pomocnicze:	!!!




		// Metoda zwracająca wymiary siatki dla danego kształtu
		private (int rows, int columns) GetShapeSize(Tetronimo_Shape shape)
		{
			return shape switch
			{
				Tetronimo_Shape.ShapeI => (4, 1),
				Tetronimo_Shape.ShapeO => (2, 2),
				Tetronimo_Shape.ShapeS or Tetronimo_Shape.ShapeZ or Tetronimo_Shape.ShapeL or Tetronimo_Shape.ShapeJ or Tetronimo_Shape.ShapeT => (3, 2),
				_ => (0, 0) // default
			};
		}

		// Metoda tworząca siatki dla danych kształtów i dodająca je do bufora
		private void Create_Grids()
		{
			foreach(Tetronimo_Shape shape in System.Enum.GetValues(typeof(Tetronimo_Shape)))
			{
				var (rows, columns) = GetShapeSize(shape);
				var gridPosition = new Point(Grid_X_Positioner(rows, tile_size), Position.Y + 50);
				gridCache[shape] = Grid.CreateGrid(SpriteBatch, rows, columns, tile_size, Fill_Color, Obw_Color, gridPosition);
			}
		}

		// Ustawienie siatki z bufora
		private void SetGridFromCache()
		{
			if (gridCache.TryGetValue(tetronimo_shape, out var cachedGrid))
			{
				grid = cachedGrid;
				shapes.Clear();
				shapes.Add(Create_Tetronimo_Instance(tetronimo_shape));
			}
		}

		//Metoda tworząca kształt na podstawie wybranego typu
		private Tetronimo Create_Tetronimo_Instance(Tetronimo_Shape shape)
		{
			return shape switch
			{
				Tetronimo_Shape.ShapeI => new ShapeI(new Point(0, 0)),
				Tetronimo_Shape.ShapeO => new ShapeO(new Point(0, 0)),
				Tetronimo_Shape.ShapeS => new ShapeS(new Point(0, 1)),
				Tetronimo_Shape.ShapeZ => new ShapeZ(new Point(0, 0)),
				Tetronimo_Shape.ShapeL => new ShapeL(new Point(0, 1)),
				Tetronimo_Shape.ShapeJ => new ShapeJ(new Point(0, 0)),
				Tetronimo_Shape.ShapeT => new ShapeT(new Point(1, 0)),
				_ => null // default
			};
		}
	}
}
