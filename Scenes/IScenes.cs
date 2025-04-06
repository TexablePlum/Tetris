using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Tetris.Game_Components;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Scenes
{
	/// <summary>
	/// Defines the public interface for game scenes. 
	/// This interface enables initialization, content loading, updating, drawing, and updating the color theme of the scene.
	/// It is used for easily switching between scenes in the scene manager.
	/// </summary>
	public interface IScenes
	{
		/// <summary>
		/// Initializes the scene with the specified window dimensions.
		/// </summary>
		/// <param name="windowWidth">The width of the game window.</param>
		/// <param name="windowHeight">The height of the game window.</param>
		void Initialize(int windowWidth, int windowHeight);

		/// <summary>
		/// Loads the content required by the scene, including textures, fonts, and other assets.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		void LoadContent(SpriteBatch spriteBatch, ContentManager content);

		/// <summary>
		/// Updates the scene logic.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		void Update(GameTime gameTime);

		/// <summary>
		/// Draws the scene.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		void Draw(GameTime gameTime);

		/// <summary>
		/// Updates the scene to use the specified color theme.
		/// </summary>
		/// <param name="theme">The new color theme to apply to the scene.</param>
		void UpdateTheme(ColorTheme theme);
	}
}
