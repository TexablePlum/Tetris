using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tetris.Game_Components
{
	public class Animated_Logo : Main_Grid
	{
		private List<Letters> logo_letters;

		private int animation_speed;
		public int Animation_Speed { get => animation_speed; set => animation_speed = value; }

		public Animated_Logo(Point position, int tile_size, int rows, int columns, int animation_speed = 0)
			: base(position, tile_size, rows, columns)
		{
			this.animation_speed = animation_speed;
			logo_letters = new List<Letters>();
		}

		public new void Load_Content(SpriteBatch spriteBatch)
		{
			base.Load_Content(spriteBatch);
			Initialize_Logo();
			
			Obw_Color = Color_Theme.Game_Theme.Panel_Transparent_Border_Color;
			Fill_Color = Color_Theme.Game_Theme.Panel_Transparent_Fill_Color;

			if (animation_speed > 0)
			{
				Logo_Animation(animation_speed);
			}
		}

		// Inicjalizacja logo
		private void Initialize_Logo()
		{
			Letter_T letter_T = new Letter_T(new Point(0, 0), Color.Red, Color.White);
			Letter_E letter_E = new Letter_E(new Point(4, 0), Color.Orange, Color.White);
			Letter_T letter_T2 = new Letter_T(new Point(8, 0), Color.Yellow, Color.White);
			Letter_R letter_R = new Letter_R(new Point(12, 0), Color.SpringGreen, Color.White);
			Letter_I letter_I = new Letter_I(new Point(16, 0), Color.Cyan, Color.White);
			Letter_S letter_S = new Letter_S(new Point(18, 0), Color.Purple, Color.White);

			logo_letters.Add(letter_T);
			logo_letters.Add(letter_E);
			logo_letters.Add(letter_T2);
			logo_letters.Add(letter_R);
			logo_letters.Add(letter_I);
			logo_letters.Add(letter_S);
		}

		// Metoda rysująca logo
		public new void Draw()
		{
			base.Draw(); // Rysuje panel bazowy
			Draw_Letters_Grid(logo_letters); // Rysuje litery na siatce
		}

		// Asynchroniczna animacja logo
		private async void Logo_Animation(int animation_speed)
		{
			while (true)
			{
				// Przesuń kolory w kolekcji logo_letters
				Color lastColor = logo_letters[^1].Letter_Color;
				for (int i = logo_letters.Count - 1; i > 0; i--)
				{
					logo_letters[i].Letter_Color = logo_letters[i - 1].Letter_Color;
				}
				logo_letters[0].Letter_Color = lastColor;

				await Task.Delay(animation_speed);
			}
		}
	}
}
