using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.States
{
	/// <summary>
	/// Represents a game state.
	/// Implementing classes provide functionality to load content, update game logic, and render the state.
	/// </summary>
	public interface IState
	{
		/// <summary>
		/// Loads all necessary content (such as textures, fonts, etc.) for the state.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing.</param>
		/// <param name="content">The ContentManager for loading assets.</param>
		void LoadContent(SpriteBatch spriteBatch, ContentManager content);

		/// <summary>
		/// Updates the game logic for this state.
		/// </summary>
		/// <param name="gameTime">Provides timing values for the update cycle.</param>
		void Update(GameTime gameTime);

		/// <summary>
		/// Draws the current state to the screen.
		/// </summary>
		/// <param name="gameTime">Provides timing values for the draw cycle.</param>
		void Draw(GameTime gameTime);
	}
}
