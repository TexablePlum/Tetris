using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Lines_Count_Panel : Panel
	{
		private readonly SpriteFont font;
		private string lines_text = "LINES: ";
		private int lines_value = 0;
		private Color text_color;
		private Color text_value_color;

		public string Lines_Text { get => lines_text; set => lines_text = value; }
		public int Lines_Value { get => lines_value; set => lines_value = value; }

		public Lines_Count_Panel(SpriteBatch spriteBatch, ContentManager contentManager)
			: base(spriteBatch, new Point(Score_Panel.Position.X, Score_Panel.Position.Y + 565), Score_Panel.Width, 65)
		{
			SpriteBatch = spriteBatch;
			font = contentManager.Load<SpriteFont>("Fonts/Control_Panel_Font");
			Fill_Color = Color.Transparent;
			Obw_Color = Color.Transparent;
			text_color = Color_Theme.Game_Theme.Text_Color;
			text_value_color = Color_Theme.Game_Theme.Text_Counters_Color;
		}

		public new void Draw()
		{
			base.Draw();
			SpriteBatch.Begin();
			SpriteBatch.DrawString(font, lines_text, new Vector2(Double_Text_X_Positioner(lines_text, lines_value.ToString(), font).first_string_x, Text_Y_Positioner(lines_text, font)), text_color);
			SpriteBatch.DrawString(font, lines_value.ToString(), new Vector2(Double_Text_X_Positioner(lines_text, lines_value.ToString(), font).second_string_x, Text_Y_Positioner(lines_text, font)), text_value_color);
			SpriteBatch.End();
		}

		public new void Update_Theme()
		{
			text_color = Color_Theme.Game_Theme.Text_Color;
			text_value_color = Color_Theme.Game_Theme.Text_Counters_Color;
		}
	}
}
