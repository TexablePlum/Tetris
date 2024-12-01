using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public enum Scene
	{
		Start_Scene,
		Game_Scene
	}

	public class Scenes_Manager
	{
		private IScenes current_scene;
		private Scene current_scene_type;

		private SpriteBatch spriteBatch;
		private ContentManager content;
		private int window_width;
		private int window_height;

		public Scenes_Manager(Scene scene_type)
		{
			Set_New_Scene(scene_type);
		}

		public void Initialize(int window_width, int window_height)
		{
			this.window_width = window_width;
			this.window_height = window_height;
			current_scene.Initialize(window_width, window_height);
		}

		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			this.spriteBatch = spriteBatch;
			this.content = content;
			current_scene.LoadContent(spriteBatch, content);
		}

		public void Update(GameTime gameTime)
		{
			current_scene.Update(gameTime);
		}

		public void Draw(GameTime gameTime)
		{
			current_scene.Draw(gameTime);
		}

		private void Set_New_Scene(Scene scene_type)
		{
				current_scene_type = scene_type;
				switch (current_scene_type)
				{
					case Scene.Start_Scene:
						current_scene = new Start_Scene();
						break;
					case Scene.Game_Scene:
						current_scene = new Game_Scene();
						break;
				}
		}

		public void Change_Scene(Scene scene_type)
		{
			if(scene_type != current_scene_type)
			{
				Set_New_Scene(scene_type);
				current_scene.Initialize(window_width, window_height);
				current_scene.LoadContent(spriteBatch, content);
			}
		}

	}
}
