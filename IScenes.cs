using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Tetris.Game_Components;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	//Publiczny interfejs dla scen, umożliwiający inicjalizację, ładowanie zawartości, aktualizację i rysowanie scen oraz zmianę motywu kolorystycznego gry.
	//Potrzebny do łatwego przełączanie między scenam w menadżerze scen.

	public interface IScenes
	{
		void Initialize(int window_width, int window_height);
		void LoadContent(SpriteBatch spriteBatch, ContentManager content);
		void Update(GameTime gameTime);
		void Draw(GameTime gameTime);
		void Update_Theme(Color_Theme theme);
	}
}
