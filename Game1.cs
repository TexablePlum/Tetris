using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Tetris.Game_Components;
using Tetris.Scenes;

namespace Tetris
{
	/// <summary>
	/// The main game class. Responsible for initializing, loading content, updating, and drawing the game.
	/// </summary>
	public class Game1 : Game
	{
		/// <summary>
		/// The application version.
		/// </summary>
		private const string AppVersion = "1.0.0";

		/// <summary>
		/// Global game settings.
		/// </summary>
		public static Settings GameSettings;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SpriteFont appVersionFont;

		/// <summary>
		/// Manages scenes (screen transitions and updates).
		/// </summary>
		public static ScenesManager Scene;

		// resolution variables.
		private int windowWidth = 800;
		private int windowHeight = 900;

		/// <summary>
		/// Initializes a new instance of the Game1 class.
		/// </summary>
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		/// <summary>
		/// Initializes the game. Sets up screen resolution, multi-sampling, loads game settings, and initializes the scene manager.
		/// </summary>
		protected override void Initialize()
		{
			// Load settings from file (or create default settings if necessary)
			GameSettings = SettingsManager.LoadSettings();

			// Set the preferred screen resolution.
			_graphics.PreferredBackBufferWidth = windowWidth;
			_graphics.PreferredBackBufferHeight = windowHeight;

			// Enable MSAA (Multi-Sample Anti-Aliasing) for smoother edges.
			_graphics.PreferMultiSampling = true;
			_graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
			_graphics.ApplyChanges();

			// Initialize the scene manager with the starting scene.
			Scene = new ScenesManager(new StartScene());
			Scene.Initialize(windowWidth, windowHeight);

			base.Initialize();
		}

		/// <summary>
		/// Loads game content, such as textures, audio, and fonts.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load audio resources.
			SoundManager.Load(Content);
			MediaPlayer.Play(SoundManager.MenuMusic);
			MediaPlayer.IsRepeating = true;

			// Load content for the current scene.
			Scene.LoadContent(_spriteBatch, Content);

			// Load the font used to display the application version.
			appVersionFont = Content.Load<SpriteFont>("Fonts/Copyrights_Font");
		}

		/// <summary>
		/// Updates the game state. Delegates update logic to the active scene.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Update the active scene.
			Scene.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// Draws the game. Clears the screen, draws the current scene, and renders the application version.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// Draw the active scene.
			Scene.Draw(gameTime);

			_spriteBatch.Begin();
			_spriteBatch.DrawString(appVersionFont, $"Version: {AppVersion}", new Vector2(675, windowHeight - 20), Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
