using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris
{
	public class Level_Panel : Panel
	{
		private readonly SpriteFont font;

		private int level_value = 0;
		private string next_shape_text = "LEVEL: ";

		public int Level_Value { get => level_value; set => level_value = value; }

		public string Next_Shape_Text { get => next_shape_text; set => next_shape_text = value; }

		public Level_Panel(SpriteBatch spriteBatch, ContentManager contentManager)
			:base(spriteBatch, new Point(Score_Panel.Position.X, Score_Panel.Position.Y + 425), Score_Panel.Width, 65)
		{
			SpriteBatch = spriteBatch;
			font = contentManager.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");
		}

		public new void Draw()
		{
			base.Draw(); // Rysowanie panelu z klasy bazowej Panel
			SpriteBatch.Begin();
			SpriteBatch.DrawString(font, next_shape_text, new Vector2(Double_Text_X_Positioner(next_shape_text,level_value.ToString(), font).first_string_x, Text_Y_Positioner(next_shape_text, font)), Color.White);
			SpriteBatch.DrawString(font, level_value.ToString(), new Vector2(Double_Text_X_Positioner(next_shape_text, level_value.ToString(), font).second_string_x, Text_Y_Positioner(next_shape_text, font)), Color.HotPink);
			SpriteBatch.End();
		}

	}
}
