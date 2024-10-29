using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		//Obiekty gry
		private Background background_theme;
		private Main_Grid main_grid;
		private Panel score_counter;

		//Zmienne rozmiaru okna
		private int window_width = 800;
		private int window_height = 900;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			// Ustawienie rozdzielczości ekranu
			_graphics.PreferredBackBufferWidth = window_width; // Wymiary szerokości
			_graphics.PreferredBackBufferHeight = window_height; // Wymiary wysokości

			// MSAA
			_graphics.PreferMultiSampling = true; // Wygładzanie krawędzi
			_graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 8;

			_graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			//Inicjalizacja tła
			background_theme = new Background(_spriteBatch, GraphicsDevice, window_width, window_height, new Color(64, 224, 208), new Color(138, 43, 226));

			//Inicjalizacja siatki
			main_grid = new Main_Grid(_spriteBatch, GraphicsDevice, new Point(25, 25), 420, 840);

			//Inicjalizacja licznika punktów
			score_counter = new Panel(_spriteBatch, GraphicsDevice, new Point(500, 25), 275, 175);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			main_grid.Update_Grid();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// TODO: Add your drawing code here

			//Tło
			background_theme.Draw_Background();

			//Siatka
			main_grid.Draw_Grid();

			//Licznik punktów
			score_counter.Draw();

			base.Draw(gameTime);
		}
	}
}
