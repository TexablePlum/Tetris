using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	public class Starfield
	{
		private readonly SpriteBatch sprite_batch;
		private readonly Texture2D pixel;

		private readonly List<Star> stars;

		public Starfield(SpriteBatch sprite_batch, int starCount, int screen_width, int screen_height)
		{
			this.sprite_batch = sprite_batch;

			stars = new List<Star>();

			// Stworzenie tekstury 1x1 dla rysowania
			pixel = new Texture2D(sprite_batch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });

			for (int i = 0; i < starCount; i++)
			{
				stars.Add(new Star(screen_width, screen_height));
			}
		}

		public void Update()
		{
			foreach (var star in stars)
			{
				star.Update();
			}
		}

		public void Draw()
		{
			sprite_batch.Begin();
			foreach (var star in stars)
			{
				sprite_batch.Draw(pixel, new Rectangle((int)star.Position.X, (int)star.Position.Y, star.Size, star.Size), star.Color);
			}
			sprite_batch.End();
		}

		// Zagnieżdżona klasa Star
		private class Star
		{
			private readonly int screen_width;
			private readonly int screen_height;

			private Vector2 position;
			private readonly float speed;	// Prędkość spadania
			private Color color;
			private int size;

			private float brightness;		// Jasność <0-1>
			private float flicker_speed;	// Szybkość migotania

			private static readonly Random random = new Random();

			public Vector2 Position { get => position; set => position = value; }
			public Color Color { get => color; set => color = value; }
			public int Size { get => size; set => size = value; }

			public Star(int screen_width, int screen_height)
			{
				this.screen_width = screen_width;
				this.screen_height = screen_height;

				// Losowa pozycja, prędkość i rozmiar
				position = new Vector2(random.Next(screen_width), random.Next(screen_height));
				speed = (float)(0.3f + random.NextDouble());
				size = random.Next(1, 3);

				// Migotanie
				brightness = (float)random.NextDouble();						// Początkowa jasność
				flicker_speed = 0.01f + (float)random.NextDouble() * 0.03f;		// Szybkość zmian jasności
				Update_Color();
			}

			public void Update()
			{
				// Ruch w dół
				position = new Vector2(position.X, position.Y + speed);

				// Jeśli gwiazda wyjdzie poza ekran - reset pozycji
				if (position.Y > screen_height)
				{
					position = new Vector2(random.Next(screen_width), 0);
				}

				// Migotanie
				brightness = brightness + flicker_speed;
				if (brightness >= 1 || brightness <= 0)
				{
					flicker_speed = - flicker_speed; // Odwrócenie kierunek zmiany jasności
				}
				Update_Color();
			}

			private void Update_Color()
			{
				// Aktualizacja koloru na podstawie jasności
				int intensity = (int)(255 * brightness);
				color = new Color(intensity, intensity, intensity);	// Biały kolor z jasnością
			}
		}
	}
}
