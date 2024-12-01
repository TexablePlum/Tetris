using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Game_Components;

namespace Tetris
{
	public class Start_Scene : IScenes
	{
		private SpriteBatch spriteBatch;

		//Prywatne obiekty UI 
		private Background background;
		private Starfield background_effect;
		private Button button;
		private Animated_Logo logo;
		private SpriteFont copyright_font;

		public void Initialize(int window_width, int window_height)
		{
			// Inicjalizacja obiektów UI
			background = new Background(new Point(0, 0), window_width, window_height);
			background_effect = new Starfield(250, window_width, window_height);
			button = new Button(new Point(275, 650), 250, 60, "LET'S PLAY !", null);
			logo = new Animated_Logo(new Point(43, 250), 34, 21, 5, 750);

			// Przypisanie akcji dla przycisku
			button.OnClick += () =>
			{
				Game1.Scene.Change_Scene(Scene.Game_Scene);
			};
		}
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			this.spriteBatch = spriteBatch;

			// Wczytanie tekstur i czcionek obiektów UI
			background.Load_Content(spriteBatch);
			background_effect.Load_Content(spriteBatch);
			button.Load_Content(spriteBatch, content);
			logo.Load_Content(spriteBatch);
			copyright_font = content.Load<SpriteFont>("Fonts/Copyrights_Font");
		}

		public void Update(GameTime gameTime)
		{
			background_effect.Update();
			button.Update();
		}
		public void Draw(GameTime gameTime)
		{
			background.Draw_Background();
			background_effect.Draw();
			button.Draw();
			logo.Draw();
			spriteBatch.Begin();
			spriteBatch.DrawString(copyright_font, $"© {DateTime.Now.Year} - TETRIS by DPMP. All rights reserved.", new Vector2(10, 880), Color.White);
			spriteBatch.End();
		}

		public void Update_Theme(Color_Theme theme)
		{
			Color_Theme.Game_Theme = theme;
			background.Update_Theme();
			button.Update_Theme();
		}
	}
}
