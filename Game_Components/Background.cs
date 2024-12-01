using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
    public class Background
    {
        private SpriteBatch spriteBatch;
        private Texture2D pixel;

        private Color primary_color;
        private Color secondary_color;

        private Point start_point;
        private int width;
        private int height;

        public Point Start_point { get => start_point; set => start_point = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public Color Primary_color { get => primary_color; set => primary_color = value; }
        public Color Secondary_color { get => secondary_color; set => secondary_color = value; }

        public Background(Point start_point, int width, int height)
        {
            this.start_point = start_point;
            this.width = width;
            this.height = height;
        }

        public void Load_Content(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            // Stworzenie tekstury 1x1 dla rysowania
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Ustawienie kolorów tła
            primary_color = Color_Theme.Game_Theme.Background_Primary_Color;
            secondary_color = Color_Theme.Game_Theme.Background_Secondary_Color;
        }

        public void Draw_Background()
        {
            spriteBatch.Begin();

            // Rysowanie gradientowego tła
            for (int y = start_point.Y; y < start_point.Y + height; y++)
            {
                // Interpolacja koloru pomiędzy color_start a color_end
                float amount = (float)(y - start_point.Y) / height;
                Color currentColor = Color.Lerp(primary_color, secondary_color, amount);

                // Rysowanie jednej linii o szerokości "width" i wysokości 1 piksel
                spriteBatch.Draw(pixel, new Rectangle(start_point.X, y, width, 1), currentColor);
            }

            spriteBatch.End();
        }

        public void Update_Theme()
        {
            primary_color = Color_Theme.Game_Theme.Background_Primary_Color;
            secondary_color = Color_Theme.Game_Theme.Background_Secondary_Color;
        }
    }
}
