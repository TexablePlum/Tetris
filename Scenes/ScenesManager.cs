using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris.Scenes
{
	/// <summary>
	/// Manages the switching and updating of scenes in the game.
	/// Maintains a buffer of scenes for efficient scene switching.
	/// </summary>
	public class ScenesManager
	{
		#region Private Fields

		/// <summary>
		/// The current active scene.
		/// </summary>
		private IScenes currentScene;

		/// <summary>
		/// A buffer storing scenes for potential reuse.
		/// </summary>
		private List<IScenes> scenesBuffer = new();

		/// <summary>
		/// The SpriteBatch used for drawing graphics.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// The ContentManager used for loading game assets.
		/// </summary>
		private ContentManager content;

		/// <summary>
		/// The width of the game window.
		/// </summary>
		private int windowWidth;

		/// <summary>
		/// The height of the game window.
		/// </summary>
		private int windowHeight;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ScenesManager"/> class with the specified initial scene.
		/// The initial scene is added to the scenes buffer.
		/// </summary>
		/// <param name="sceneType">The initial scene to set as the current scene.</param>
		public ScenesManager(IScenes sceneType)
		{
			SetNewScene(sceneType);
			scenesBuffer.Add(sceneType);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes the current scene with the given window dimensions.
		/// </summary>
		/// <param name="windowWidth">The width of the game window.</param>
		/// <param name="windowHeight">The height of the game window.</param>
		public void Initialize(int windowWidth, int windowHeight)
		{
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;
			currentScene.Initialize(windowWidth, windowHeight);
		}

		/// <summary>
		/// Loads the content required by the current scene.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			this.spriteBatch = spriteBatch;
			this.content = content;
			currentScene.LoadContent(spriteBatch, content);
		}

		/// <summary>
		/// Updates the current scene.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime)
		{
			currentScene.Update(gameTime);
		}

		/// <summary>
		/// Draws the current scene.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Draw(GameTime gameTime)
		{
			currentScene.Draw(gameTime);
		}

		/// <summary>
		/// Changes the current scene to the specified scene.
		/// If the scene exists in the buffer, it will be reused; otherwise, it is initialized and loaded.
		/// </summary>
		/// <param name="sceneType">The new scene to switch to.</param>
		public void ChangeScene(IScenes sceneType)
		{
			if (sceneType != currentScene)
			{
				if (scenesBuffer.Contains(sceneType))
				{
					currentScene = sceneType;
				}
				else
				{
					SetNewScene(sceneType);
					currentScene.Initialize(windowWidth, windowHeight);
					currentScene.LoadContent(spriteBatch, content);
				}
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the current scene to the specified scene.
		/// </summary>
		/// <param name="sceneType">The scene to set as the current scene.</param>
		private void SetNewScene(IScenes sceneType)
		{
			currentScene = sceneType;
		}

		#endregion
	}
}
