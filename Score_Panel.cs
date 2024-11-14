using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	// Klasa panelu punktów:
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
		private readonly string best_score_text = "BEST: ";
		private long best_score_value;
		private readonly Color text_color = Color.White;
		private readonly Color values_color = Color.HotPink;

		public long Score_Value { get => score_value; set => score_value = value; }
		public long Best_Score_Value { get => best_score_value; set => best_score_value = value; }

		public static Point Position { get => position; }
		public static int Width { get => width; }
		public static int Height { get => height; }


		public Score_Panel(SpriteBatch spriteBatch, ContentManager content)
		{
			this.spriteBatch = spriteBatch;
			this.content = content;
			panel = new Panel(spriteBatch, position, width, height);
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
			spriteBatch.DrawString(main_font, score_text, new Vector2(panel.Text_X_Positioner(score_text,main_font), 40), text_color);
			spriteBatch.DrawString(main_font, score_value.ToString(), new Vector2(panel.Text_X_Positioner(score_value.ToString(),main_font), 90), values_color);
			spriteBatch.DrawString(secondary_font, best_score_text, new Vector2(panel.Double_Text_X_Positioner(best_score_text, best_score_value.ToString(), secondary_font).first_string_x, 155), text_color);
			spriteBatch.DrawString(secondary_font, best_score_value.ToString(), new Vector2(panel.Double_Text_X_Positioner(best_score_text, best_score_value.ToString(), secondary_font).second_string_x, 155), values_color);
			spriteBatch.End();
		}
	}
}
