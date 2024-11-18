using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tetris
{
	public enum Button_State
	{
		Active,
		Inactive,
		ActiveHover,
		ActiveClicked
	}
	public class Button : Panel
	{
		private readonly SpriteBatch spriteBatch;
		private ContentManager contentManager;
		private SpriteFont font;                    // Czcionka przycisku
		private readonly Background background;		// Tło przycisku
		private Texture2D button_image;				// Obraz przycisku

		private MouseState mouseState;				// Stan myszki
		private MouseState previousMouseState;		// Poprzedni stan myszki
		
		private Dictionary<Button_State, (Color primary_color, Color secondary_color)> colors_cache = new();	//Cache przycisku w zależności od stanu

		private Color active_color;					// Kolor aktywnego przycisku
		private Color inactive_color;				// Kolor nieaktywnego przycisku

		private string button_text;					// Tekst przycisku
		private string button_graphic_path;         // Ścieżka do grafiki przycisku

		private Action onClick;						// Akcja wykonywana po kliknięciu przycisku

		private Button_State button_state;

		public Action OnClick { get => onClick; set => onClick = value; }
		public Button_State Button_State { get => button_state; set => button_state = value; }
		public string Button_Text { get => button_text; set => button_text = value; }

		public Button(SpriteBatch spriteBatch, ContentManager contentManager, Point position, int width, int height, string button_text = "Button", string button_graphic_path = null)
			: base(spriteBatch, position, width, height)
		{
			this.spriteBatch = spriteBatch;
			this.contentManager = contentManager;
			this.button_text = button_text;
			this.button_graphic_path = button_graphic_path;
			this.button_text = button_text;
			active_color = Color_Theme.Game_Theme.Button_Active_Color;
			inactive_color = Color_Theme.Game_Theme.Button_Inactive_Color;
			Obw_Color = Color_Theme.Game_Theme.Panel_Transparent_Border_Color; // Kolor obwódki przycisku właściwość dziedziczona z klasy Panel

			font = contentManager.Load<SpriteFont>("Fonts/Button_Font");
			if (button_graphic_path != null)
			{
				button_image = contentManager.Load<Texture2D>(button_graphic_path);
			}
			button_state = Button_State.Active;

			background = new Background(spriteBatch, position, width, height);

			Initialize_Cache();
		}

		private void Initialize_Cache()
		{
			foreach (Button_State state in System.Enum.GetValues(typeof(Button_State)))
			{
				colors_cache[state] = Get_Gradients_Fill_Colors(state, active_color, inactive_color);
			}
		}

		public void Update()
		{
			Set_Button_States();
		}
		
		public new void Draw()
		{
			spriteBatch.Begin();

			// Rysowanie obwielutki
			Rectangle obw = new Rectangle(Position.X - 3, Position.Y - 3, Width + 6, Height + 6);
			spriteBatch.Draw(Pixel, obw, Obw_Color);

			spriteBatch.End();

			var colors = colors_cache[button_state];	// Pobranie kolorów przycisku z cache w zależności od stanu przycisku
			background.Primary_color = colors.primary_color;
			background.Secondary_color = colors.secondary_color;
			background.Draw_Background();

			Draw_Button_Content();
		}

		public new void Update_Theme()
		{
			active_color = Color_Theme.Game_Theme.Button_Active_Color;
			inactive_color = Color_Theme.Game_Theme.Button_Inactive_Color;
			Initialize_Cache();
		}

		private (Color primary_color, Color secondary_color) Get_Gradients_Fill_Colors(Button_State button_state, Color active_color, Color inactive_color)
		{

			Color Lighter_Color(Color color, float amount)
			{
				float r = color.R + (255 - color.R) * amount;
				float g = color.G + (255 - color.G) * amount;
				float b = color.B + (255 - color.B) * amount;
				return new Color((int)r, (int)g, (int)b);
			}

			Color Darken_Color(Color color, float amount)
			{
				float r = color.R * (1 - amount);
				float g = color.G * (1 - amount);
				float b = color.B * (1 - amount);
				return new Color((int)r, (int)g, (int)b);
			}

			var secondary_active_color = Lighter_Color(active_color, 0.75f);

			var secondary_inactive_color = Lighter_Color(inactive_color, 0.5f);

			var hover_primary_color = Lighter_Color(active_color, 0.2f);
			var hover_secondary_color = Lighter_Color(secondary_active_color, 0.8f);

			var clicked_primary_color = Darken_Color(active_color, 0.2f);
			var clicked_secondary_color = Darken_Color(secondary_active_color, 0.2f);

			switch (button_state)
			{
				case Button_State.Active:
					return (active_color, secondary_active_color);
				case Button_State.Inactive:
					return (inactive_color, secondary_inactive_color);
				case Button_State.ActiveHover:
					return (hover_primary_color, hover_secondary_color);
				case Button_State.ActiveClicked:
					return (clicked_primary_color, clicked_secondary_color);
				default:
					return (Color.Transparent, Color.Transparent);
			}
		}

		private void Set_Button_States()
		{
			previousMouseState = mouseState;    // Zapisanie poprzedniego stanu myszki
			mouseState = Mouse.GetState();      // Pobranie stanu myszki
			var mouse_position = new Point(mouseState.X, mouseState.Y);                                            // Pobranie pozycji myszki
			var is_mouse_over = new Rectangle(Position.X, Position.Y, Width, Height).Contains(mouse_position);     // Sprawdzenie czy myszka jest w obszarze przycisku


			if ((is_mouse_over && button_state == Button_State.Active) || (is_mouse_over && button_state == Button_State.ActiveHover))	// Jeśli myszka jest nad przyciskiem
			{
				button_state = Button_State.ActiveHover;
				if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)				// Jeśli lewy przycisk myszy jest wciśnięty nad przyciskiem
				{
					button_state = Button_State.ActiveClicked;
					onClick?.Invoke();	// Wywołanie akcji po kliknięciu przycisku
				}
			}
			else if (button_state == Button_State.ActiveClicked && mouseState.LeftButton == ButtonState.Released)						// Jeśli lewy przycisk myszy został puszczony
			{
				button_state = Button_State.Active;
			}
			else if (!is_mouse_over && button_state != Button_State.Inactive)															// Jeśli myszka nie jest nad przyciskiem
			{
				button_state = Button_State.Active;
			}
		}

		private void Draw_Button_Content()
		{
			const int padding = 15;  // Odstęp wokół grafiki przycisku
			int buttonGraphicSize = Height - padding;
			Vector2 buttonTextPosition = new Vector2(Text_X_Positioner(button_text, font), Text_Y_Positioner(button_text, font));
			spriteBatch.Begin();

			if (button_image == null)
			{
				spriteBatch.DrawString(font, button_text, buttonTextPosition, Color.White);
			}
			else
			{
				// Ustalanie pozycji tekstu z przesunięciem dla grafiki
				float text_offset_X = buttonTextPosition.X - buttonGraphicSize / 2;
				spriteBatch.DrawString(font, button_text, new Vector2(text_offset_X, buttonTextPosition.Y), Color.White);

				// Obliczenie pozycji grafiki po prawej stronie tekstu
				float image_position_X = text_offset_X + font.MeasureString(button_text).X;
				spriteBatch.Draw(button_image, new Rectangle((int)image_position_X, Position.Y + padding / 2, buttonGraphicSize, buttonGraphicSize), Color.White);
			}

			spriteBatch.End();
		}
	}
}
