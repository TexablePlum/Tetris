using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace Tetris
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		//Obiekty gry
		private Background background_theme;
		private Main_Grid main_grid;
		private Score_Panel score_counter;
		private Next_Shape_Panel control_panel;
		private Level_Panel level_panel;
		private Lines_Count_Panel lines_panel;
		private Button play_button;
		private Button stop_button;
		private Button settings_button;

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
			background_theme = new Background(_spriteBatch, new Point(0,0), window_width, window_height);

			//Inicjalizacja siatki
			main_grid = new Main_Grid(_spriteBatch, new Point(25, 25), 420, 840, 10, 20);

			//Inicjalizacja licznika punktów
			score_counter = new Score_Panel(_spriteBatch, Content);

			//Inicjalizacja panelu sterowania
			control_panel = new Next_Shape_Panel(_spriteBatch, Content);

			//Inicjalizacja panelu poziomu
			level_panel = new Level_Panel(_spriteBatch, Content);

			//Inicjalizacja panelu linii
			lines_panel = new Lines_Count_Panel(_spriteBatch, Content);

			//Inicjalizacja przycisku start
			play_button = new Button(_spriteBatch, Content, new Point(500, 740), 202, 55, new Color(216, 110, 204), new Color(89, 89, 89), "START", "Assets/button_play");

			//Inicjalizacja przycisku stop
			stop_button = new Button(_spriteBatch, Content, new Point(720, 740), 55, 55, new Color(216, 110, 204), new Color(89, 89, 89), "", "Assets/button_pause");
			stop_button.Button_State = Button_State.Inactive;

			//Inicjalizacja przycisku start
			settings_button = new Button(_spriteBatch, Content, new Point(500, 813), 275, 55, new Color(216, 110, 204), new Color(89, 89, 89), "SETTINGS");
			
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			play_button.Update();
			stop_button.Update();
			settings_button.Update();
			base.Update(gameTime);
			

		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// TODO: Add your drawing code here

			//Tło
			background_theme.Draw_Background(new Color(80, 19, 79), new Color(37, 6, 32));

			//Siatka
			main_grid.Draw_Grid();

			//Licznik punktów
			score_counter.Draw();

			//Panel sterowania
			control_panel.Draw();

			//Panel poziomu
			level_panel.Draw();

			//Panel linii
			lines_panel.Draw();

			//Przycisk play
			play_button.Draw();

			//Przycisk stop
			stop_button.Draw();

			//Przycisk settings
			settings_button.Draw();
			

			base.Draw(gameTime);
		}

	}
}
