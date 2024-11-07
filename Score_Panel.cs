using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Score_Panel
	{
		private readonly ContentManager content;
		private readonly SpriteBatch spriteBatch;
		private SpriteFont main_font;
		private SpriteFont secondary_font;

		private static readonly Point position = new(500, 25);
		private static readonly int width = 275;
		private static readonly int height = 175;
		private readonly Panel panel;

		private readonly string score_text = "SCORE:";
		private long score_value;
		private readonly string best_score_text = "BEST:";
		private long best_score_value;
		private readonly Color text_color = Color.White;
		private readonly Color values_color = Color.HotPink;

		public long Score_Value { get => score_value; set => score_value = value; }
		public long Best_Score_Value { get => best_score_value; set => best_score_value = value; }
		

		public Score_Panel(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content)
		{
			this.spriteBatch = spriteBatch;
			this.content = content;
			panel = new Panel(spriteBatch, graphicsDevice, position, width, height);
			score_value = 0;
			best_score_value = 2137420; //TODO: Load best score from file
			Load();
		}

		public void Load()
		{
			main_font = content.Load<SpriteFont>("Fonts/8-bit-Operator-Main-Size");
			secondary_font = content.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");
		}

		public void Draw()
		{
			panel.Draw();
			// Rysowanie tekstu Score
			spriteBatch.Begin();
			spriteBatch.DrawString(main_font, score_text, new Vector2(horizontal_Possitioner(score_text), 40), text_color);
			spriteBatch.DrawString(main_font, score_value.ToString(), new Vector2(horizontal_Possitioner(score_value.ToString()), 90), values_color);
			spriteBatch.DrawString(secondary_font, best_score_text, new Vector2(panel.Position.X + 20, 155), text_color);
			spriteBatch.DrawString(secondary_font, best_score_value.ToString(), new Vector2(best_Score_Horizontal_Possitioner(best_score_value.ToString()), 155), values_color);
			spriteBatch.End();
		}

		// Metody do pozycjonowania tekstu 
		private int horizontal_Possitioner(string text) 
		{
			float text_width = main_font.MeasureString(text).X;
			float centered_position = panel.Position.X + (panel.Width - text_width) / 2;
			return (int)centered_position;
		}

		private int best_Score_Horizontal_Possitioner(string text)
		{
			float text_width = secondary_font.MeasureString(text).X;
			float centered_position = panel.Position.X + (panel.Width - text_width) / 2;
			return (int)centered_position + 50;
		}
	}
}
