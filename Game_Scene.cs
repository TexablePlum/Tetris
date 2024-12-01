using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Tetris.Game_Components;

namespace Tetris
{
	public enum Game_State
	{
		First_Run,
		Not_Started,
		Playing,
		Paused,
		Game_Over
	}

	public class Game_Scene : IScenes
	{
		//Lista kształtów w grze
		private List<Tetronimo> shapes;
		public List<Tetronimo> Shapes { get => shapes; set => shapes = value; }

		//Prywatne obiekty UI gry
		private Background background;
		private Starfield background_effect;
		private Main_Grid main_grid;
		private Score_Panel score_panel;
		private Next_Shape_Panel next_shape_panel;
		private Level_Panel level_panel;
		private Lines_Count_Panel lines_count_panel;
		private Button start_button;
		private Button pause_button;
		private Button settings_button;

		//Publiczne obiekty UI gry
		public Background Background { get => background; set => background = value; }
		public Starfield Background_Effect { get => background_effect; set => background_effect = value; }
		public Main_Grid Main_Grid { get => main_grid; set => main_grid = value; }
		public Score_Panel Score_Panel { get => score_panel; set => score_panel = value; }
		public Next_Shape_Panel Next_Shape_Panel { get => next_shape_panel; set => next_shape_panel = value; }
		public Level_Panel Level_Panel { get => level_panel; set => level_panel = value; }
		public Lines_Count_Panel Lines_Count_Panel { get => lines_count_panel; set => lines_count_panel = value; }
		public Button Start_Button { get => start_button; set => start_button = value; }
		public Button Pause_Button { get => pause_button; set => pause_button = value; }
		public Button Settings_Button { get => settings_button; set => settings_button = value; }

		public void Initialize(int window_width, int window_height)
		{

			// Inicjalizacja obiektów UI gry
			background = new Background(new Point(0, 0), window_width, window_height);
			background_effect = new Starfield(250, window_width, window_height);
			main_grid = new Main_Grid(new Point(25, 25), 42, 10, 20);
			score_panel = new Score_Panel(new Point(500, 25), 275, 175);
			next_shape_panel = new Next_Shape_Panel(new Point(500, 250), 275, 150);
			level_panel = new Level_Panel(new Point(500, 450), 275, 65);
			lines_count_panel = new Lines_Count_Panel(new Point(500, 590), 275, 65);
			start_button = new Button(new Point(500, 740), 202, 55, "START", "Assets/button_play");
			pause_button = new Button(new Point(720, 740), 55, 55, "", "Assets/button_pause");
			settings_button = new Button(new Point(500, 813), 275, 55, "SETTINGS");
		}
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{

			// Wczytanie tekstur i czcionek obiektów UI gry
			background.Load_Content(spriteBatch);
			background_effect.Load_Content(spriteBatch);
			main_grid.Load_Content(spriteBatch);
			score_panel.Load_Content(spriteBatch, content);
			next_shape_panel.Load_Content(spriteBatch, content);
			level_panel.Load_Content(spriteBatch, content);
			lines_count_panel.Load_Content(spriteBatch, content);
			start_button.Load_Content(spriteBatch, content);
			pause_button.Load_Content(spriteBatch, content);
			settings_button.Load_Content(spriteBatch, content);
		}

		public void Update(GameTime gameTime)
		{
			background_effect.Update();
			start_button.Update();
			pause_button.Update();
			settings_button.Update();
		}
		public void Draw(GameTime gameTime)
		{
			background.Draw_Background();
			background_effect.Draw();
			main_grid.Draw_Shapes_Grid(shapes);
			score_panel.Draw();
			next_shape_panel.Draw();
			level_panel.Draw();
			lines_count_panel.Draw();
			start_button.Draw();
			pause_button.Draw();
			settings_button.Draw();
		}

		public void Update_Theme(Color_Theme color_Theme)
		{
			background.Update_Theme();
			main_grid.Update_Theme();
			score_panel.Update_Theme();
			next_shape_panel.Update_Theme();
			level_panel.Update_Theme();
			lines_count_panel.Update_Theme();
			start_button.Update_Theme();
			pause_button.Update_Theme();
			settings_button.Update_Theme();
		}
	}
}
