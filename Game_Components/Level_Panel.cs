using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris.Game_Components
{
    public class Level_Panel : Panel
    {
        private SpriteFont font;

        private int level_value;
        private string level_text;

        private Color text_color;
        private Color text_value_color;

        public int Level_Value { get => level_value; set => level_value = value; }
        public string Next_Shape_Text { get => level_text; set => level_text = value; }

        public Color Text_Color { get => text_color; set => text_color = value; }
        public Color Text_Value_Color { get => text_value_color; set => text_value_color = value; }

        public Level_Panel(Point position, int width, int height)
            : base(position, width, height)
        {
            level_value = 0;
            level_text = "LEVEL: ";
        }

        public void Load_Content(SpriteBatch spriteBatch, ContentManager content)
        {
            Load_Content(spriteBatch);

            // Ustawienie kolorów
            text_color = Color_Theme.Game_Theme.Text_Color;
            text_value_color = Color_Theme.Game_Theme.Text_Counters_Color;

            // Wczytanie czcionki
            font = content.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");
        }

        public new void Draw()
        {
            base.Draw(); // Rysowanie panelu z klasy bazowej Panel
            SpriteBatch.Begin();
            SpriteBatch.DrawString(font, level_text, new Vector2(Double_Text_X_Positioner(level_text, level_value.ToString(), font).first_string_x, Text_Y_Positioner(level_text, font)), text_color);
            SpriteBatch.DrawString(font, level_value.ToString(), new Vector2(Double_Text_X_Positioner(level_text, level_value.ToString(), font).second_string_x, Text_Y_Positioner(level_text, font)), text_value_color);
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
