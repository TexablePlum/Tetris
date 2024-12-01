using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
    // Klasa panelu punktów:
    public class Score_Panel
    {
        private SpriteBatch spriteBatch;
        private SpriteFont main_font;
        private SpriteFont secondary_font;
        private readonly Panel panel;

        private Point position;
        private int width;
        private int height;

        private string score_text;
        private long score_value;
        private string best_score_text;
        private long best_score_value;
        private Color text_color;
        private Color values_color;

        public long Score_Value { get => score_value; set => score_value = value; }
        public long Best_Score_Value { get => best_score_value; set => best_score_value = value; }

        public Point Position { get => position; set => position = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }


        public Score_Panel(Point position, int width, int height)
        {
            this.position = position;
            this.width = width;
            this.height = height;

            panel = new Panel(position, width, height);

            score_text = "SCORE: ";
            score_value = 0;
            best_score_text = "BEST: ";
            best_score_value = 2137420; //TODO: Load best score from file
        }

        public void Load_Content(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            // Wczytanie zawartości panelu
            panel.Load_Content(spriteBatch);

            // Wczytanie czcionek
            main_font = content.Load<SpriteFont>("Fonts/8-bit-Operator-Main-Size");
            secondary_font = content.Load<SpriteFont>("Fonts/8-bit-Operator-Secondary-Size");

            // Ustawienie kolorów tekstu
            text_color = Color_Theme.Game_Theme.Text_Color;
            values_color = Color_Theme.Game_Theme.Text_Counters_Color;
        }

        public void Draw()
        {
            panel.Draw();
            // Rysowanie tekstu Score
            spriteBatch.Begin();
            spriteBatch.DrawString(main_font, score_text, new Vector2(panel.Text_X_Positioner(score_text, main_font), 40), text_color);
            spriteBatch.DrawString(main_font, score_value.ToString(), new Vector2(panel.Text_X_Positioner(score_value.ToString(), main_font), 90), values_color);
            spriteBatch.DrawString(secondary_font, best_score_text, new Vector2(panel.Double_Text_X_Positioner(best_score_text, best_score_value.ToString(), secondary_font).first_string_x, 155), text_color);
            spriteBatch.DrawString(secondary_font, best_score_value.ToString(), new Vector2(panel.Double_Text_X_Positioner(best_score_text, best_score_value.ToString(), secondary_font).second_string_x, 155), values_color);
            spriteBatch.End();
        }

        public void Update_Theme()
        {
            panel.Update_Theme();
            text_color = Color_Theme.Game_Theme.Text_Color;
            values_color = Color_Theme.Game_Theme.Text_Counters_Color;
        }
    }
}
