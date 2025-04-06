using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Game_Components;

namespace Tetris.Scenes
{
	/// <summary>
	/// Represents the starting scene of the game, displaying a background, animated logo, and a play button.
	/// When the play button is clicked, the scene transitions to the main game scene.
	/// </summary>
	public class StartScene : IScenes
	{
		#region Private Fields

		/// <summary>
		/// The SpriteBatch used for drawing graphics.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// The background of the start scene.
		/// </summary>
		private Background background;

		/// <summary>
		/// The starfield effect used as a background effect.
		/// </summary>
		private Starfield backgroundEffect;

		/// <summary>
		/// The button that initiates the transition to the main game scene.
		/// </summary>
		private Button button;

		/// <summary>
		/// The animated logo displayed in the start scene.
		/// </summary>
		private AnimatedLogo logo;

		/// <summary>
		/// The font used for rendering copyright information.
		/// </summary>
		private SpriteFont copyrightFont;

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes the start scene by creating and positioning UI components.
		/// Also assigns the action for the play button.
		/// </summary>
		/// <param name="windowWidth">The width of the game window.</param>
		/// <param name="windowHeight">The height of the game window.</param>
		public void Initialize(int windowWidth, int windowHeight)
		{
			// Initialize UI components.
			background = new Background(new Point(0, 0), windowWidth, windowHeight);
			backgroundEffect = new Starfield(250, windowWidth, windowHeight);
			button = new Button(new Point(275, 650), 250, 60, "LET'S PLAY !", null);
			logo = new AnimatedLogo(new Point(43, 250), 34, 21, 5, 750);

			// Assign the action for the play button.
			button.OnClick += () =>
			{
				Game1.Scene.ChangeScene(new GameScene());
			};
		}

		/// <summary>
		/// Loads the content required by the start scene, including textures, fonts, and other assets.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			this.spriteBatch = spriteBatch;

			// Load textures and fonts for UI components.
			background.LoadContent(spriteBatch);
			backgroundEffect.LoadContent(spriteBatch);
			button.LoadContent(spriteBatch, content);
			logo.LoadContent(spriteBatch);
			copyrightFont = content.Load<SpriteFont>("Fonts/Copyrights_Font");
		}

		/// <summary>
		/// Updates the start scene by updating the background effect and the play button.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime)
		{
			backgroundEffect.Update();
			button.Update();
		}

		/// <summary>
		/// Draws the start scene, including the background, background effect, play button, animated logo, and copyright text.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Draw(GameTime gameTime)
		{
			background.DrawBackground();
			backgroundEffect.Draw();
			button.Draw();
			logo.Draw();

			spriteBatch.Begin();
			spriteBatch.DrawString(
				copyrightFont,
				$"© {DateTime.Now.Year} - TETRIS by DPMP. All rights reserved.",
				new Vector2(10, 880),
				Color.White);
			spriteBatch.End();
		}

		/// <summary>
		/// Updates the color theme for the start scene and its UI components.
		/// </summary>
		/// <param name="theme">The new color theme to apply.</param>
		public void UpdateTheme(ColorTheme theme)
		{
			ColorTheme.GameTheme = theme;
			background.UpdateTheme();
			button.UpdateTheme();
		}

		#endregion
	}
}
