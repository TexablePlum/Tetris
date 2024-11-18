using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris
{
	public class Level_Panel : Panel
	{
		private readonly SpriteFont font;

		private int level_value = 0;
		private string level_text = "LEVEL: ";

		private Color text_color;
		private Color text_value_color;

		public int Level_Value { get => level_value; set => level_value = value; }

		public string Next_Shape_Text { get => level_text; set => level_text = value; }

		public Level_Panel(SpriteBatch spriteBatch, ContentManager contentManager)
			:base(spriteBatch, new Point(Score_Panel.Position.X, Score_Panel.Position.Y + 425), Score_Panel.Width, 65)
		{
			SpriteBatch = spriteBatch;
			text_color = Color_Theme.Game_Theme.Text_Color;
			text_value_color = Color_Theme.Game_Theme.Text_Counters_Color;
			font = contentManager.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");
		}

		public new void Draw()
		{
			base.Draw(); // Rysowanie panelu z klasy bazowej Panel
			SpriteBatch.Begin();
			SpriteBatch.DrawString(font, level_text, new Vector2(Double_Text_X_Positioner(level_text,level_value.ToString(), font).first_string_x, Text_Y_Positioner(level_text, font)), Color_Theme.Game_Theme.Text_Color);
			SpriteBatch.DrawString(font, level_value.ToString(), new Vector2(Double_Text_X_Positioner(level_text, level_value.ToString(), font).second_string_x, Text_Y_Positioner(level_text, font)), Color_Theme.Game_Theme.Text_Counters_Color);
			SpriteBatch.End();
		}

		public new void Update_Theme()
		{
			base.Update_Theme();
			text_color = Color_Theme.Game_Theme.Text_Color;
			text_value_color = Color_Theme.Game_Theme.Text_Counters_Color;
		}

	}
}
