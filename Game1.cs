using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Game_Components;

namespace Tetris
{
    public class Game1 : Game
	{
		private const string AppVersion = "1.0.0-Previous Alpha";

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		public static Scenes_Manager Scene;
		private SpriteFont app_version_font;

		//prywatne Zmienne rozdzielczości
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

			Scene = new Scenes_Manager(Tetris.Scene.Start_Scene);
			Scene.Initialize(window_width, window_height);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			// Ustawienie koloru tła
			Color_Theme.Game_Theme = Color_Theme.Lights_City;

			// Wczytanie audio
			Sound_Manager.Load(Content);

			Scene.LoadContent(_spriteBatch, Content);

			app_version_font = Content.Load<SpriteFont>("Fonts/Copyrights_Font");
			
		}

		protected override void Update(GameTime gameTime)
		{
			// TODO: Add your update logic here

			Scene.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// TODO: Add your drawing code here
			Scene.Draw(gameTime);

			_spriteBatch.Begin();
			_spriteBatch.DrawString(app_version_font, $"Version: {AppVersion}", new Vector2(540, window_height - 20), Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
