using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents a starfield effect for the game background.
	/// The starfield consists of a collection of stars that move downward and flicker.
	/// </summary>
	public class Starfield
	{
		#region Private Fields

		/// <summary>
		/// The SpriteBatch used for drawing.
		/// </summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// A 1x1 pixel texture used for drawing stars.
		/// </summary>
		private Texture2D pixel;

		/// <summary>
		/// The collection of stars in the starfield.
		/// </summary>
		private readonly List<Star> stars;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Starfield"/> class with a specified number of stars and screen dimensions.
		/// </summary>
		/// <param name="starCount">The number of stars to generate.</param>
		/// <param name="screenWidth">The width of the screen.</param>
		/// <param name="screenHeight">The height of the screen.</param>
		public Starfield(int starCount, int screenWidth, int screenHeight)
		{
			stars = new List<Star>();

			for (int i = 0; i < starCount; i++)
			{
				stars.Add(new Star(screenWidth, screenHeight));
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the content required for the starfield, including creating a 1x1 pixel texture.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing.</param>
		public void LoadContent(SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;

			// Create a 1x1 texture for drawing.
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}

		/// <summary>
		/// Updates the state of each star in the starfield.
		/// </summary>
		public void Update()
		{
			foreach (var star in stars)
			{
				star.Update();
			}
		}

		/// <summary>
		/// Draws the starfield by rendering each star.
		/// </summary>
		public void Draw()
		{
			spriteBatch.Begin();
			foreach (var star in stars)
			{
				spriteBatch.Draw(pixel, new Rectangle((int)star.Position.X, (int)star.Position.Y, star.Size, star.Size), star.Color);
			}
			spriteBatch.End();
		}

		#endregion

		#region Nested Star Class

		/// <summary>
		/// Represents an individual star within the starfield.
		/// Each star moves downward and flickers by changing its brightness.
		/// </summary>
		private class Star
		{
			#region Private Fields

			/// <summary>
			/// The width of the screen.
			/// </summary>
			private readonly int screenWidth;

			/// <summary>
			/// The height of the screen.
			/// </summary>
			private readonly int screenHeight;

			/// <summary>
			/// The current position of the star.
			/// </summary>
			private Vector2 position;

			/// <summary>
			/// The speed at which the star moves downward.
			/// </summary>
			private readonly float speed;

			/// <summary>
			/// The current color of the star.
			/// </summary>
			private Color color;

			/// <summary>
			/// The size (width and height) of the star.
			/// </summary>
			private int size;

			/// <summary>
			/// The current brightness level of the star (from 0 to 1).
			/// </summary>
			private float brightness;

			/// <summary>
			/// The speed at which the star flickers (changes brightness).
			/// </summary>
			private float flickerSpeed;

			/// <summary>
			/// A shared random number generator for all stars.
			/// </summary>
			private static readonly Random random = new Random();

			#endregion

			#region Public Properties

			/// <summary>
			/// Gets or sets the position of the star.
			/// </summary>
			public Vector2 Position { get => position; set => position = value; }

			/// <summary>
			/// Gets or sets the color of the star.
			/// </summary>
			public Color Color { get => color; set => color = value; }

			/// <summary>
			/// Gets or sets the size of the star.
			/// </summary>
			public int Size { get => size; set => size = value; }

			#endregion

			#region Constructors

			/// <summary>
			/// Initializes a new instance of the <see cref="Star"/> class with random position, speed, size, and flicker properties.
			/// </summary>
			/// <param name="screenWidth">The width of the screen.</param>
			/// <param name="screenHeight">The height of the screen.</param>
			public Star(int screenWidth, int screenHeight)
			{
				this.screenWidth = screenWidth;
				this.screenHeight = screenHeight;

				// Set a random position, speed, and size.
				position = new Vector2(random.Next(screenWidth), random.Next(screenHeight));
				speed = (float)(0.3f + random.NextDouble());
				size = random.Next(1, 3);

				// Initialize flicker properties.
				brightness = (float)random.NextDouble();
				flickerSpeed = 0.01f + (float)random.NextDouble() * 0.03f;
				UpdateColor();
			}

			#endregion

			#region Public Methods

			/// <summary>
			/// Updates the star's position and brightness. Resets the position if the star moves off-screen.
			/// </summary>
			public void Update()
			{
				// Move the star downward.
				position = new Vector2(position.X, position.Y + speed);

				// Reset position if the star goes off the bottom of the screen.
				if (position.Y > screenHeight)
				{
					position = new Vector2(random.Next(screenWidth), 0);
				}

				// Update the brightness for flickering effect.
				brightness = brightness + flickerSpeed;
				if (brightness >= 1 || brightness <= 0)
				{
					flickerSpeed = -flickerSpeed; // Reverse the flicker direction.
				}
				UpdateColor();
			}

			#endregion

			#region Private Methods

			/// <summary>
			/// Updates the star's color based on its current brightness.
			/// </summary>
			private void UpdateColor()
			{
				int intensity = (int)(255 * brightness);
				color = new Color(intensity, intensity, intensity);
			}

			#endregion
		}

		#endregion
	}
}
