using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
    public class Lines_Count_Panel : Panel
    {
        private SpriteFont font;
        private string lines_text;
        private int lines_value;
        private Color text_color;
        private Color text_value_color;

        public string Lines_Text { get => lines_text; set => lines_text = value; }
        public int Lines_Value { get => lines_value; set => lines_value = value; }

        public Color Text_Color { get => text_color; set => text_color = value; }
        public Color Text_Value_Color { get => text_value_color; set => text_value_color = value; }

        public Lines_Count_Panel(Point position, int width, int height)
            : base(position, width, height)
        {
            lines_text = "LINES: ";
            lines_value = 0;
        }

        public void Load_Content(SpriteBatch spriteBatch, ContentManager content)
        {
            Load_Content(spriteBatch);

            // Wczytanie czcionki
            font = content.Load<SpriteFont>("Fonts/Control_Panel_Font");

            // Ustawienie kolorów
            Fill_Color = Color_Theme.Game_Theme.Panel_Transparent_Fill_Color;
            Obw_Color = Color_Theme.Game_Theme.Panel_Transparent_Border_Color;
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
