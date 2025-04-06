using Microsoft.Xna.Framework;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents a color theme for the game, defining colors for the background, text, panels, buttons, and the main grid.
	/// </summary>
	public struct ColorTheme
	{
		/// <summary>
		/// Gets the primary background color.
		/// </summary>
		public Color BackgroundPrimaryColor { get; }

		/// <summary>
		/// Gets the secondary background color.
		/// </summary>
		public Color BackgroundSecondaryColor { get; }

		/// <summary>
		/// Gets the text color.
		/// </summary>
		public Color TextColor { get; }

		/// <summary>
		/// Gets the color used for text counters.
		/// </summary>
		public Color TextCountersColor { get; }

		/// <summary>
		/// Gets the fill color for panels.
		/// </summary>
		public Color PanelFillColor { get; }

		/// <summary>
		/// Gets the border color for panels.
		/// </summary>
		public Color PanelBorderColor { get; }

		/// <summary>
		/// Gets the active color for buttons.
		/// </summary>
		public Color ButtonActiveColor { get; }

		/// <summary>
		/// Gets the inactive color for buttons.
		/// </summary>
		public Color ButtonInactiveColor { get; }

		/// <summary>
		/// Gets the transparent fill color for panels.
		/// </summary>
		public Color PanelTransparentFillColor { get; }

		/// <summary>
		/// Gets the transparent border color for panels.
		/// </summary>
		public Color PanelTransparentBorderColor { get; }

		/// <summary>
		/// Gets the fill color for the main grid.
		/// </summary>
		public Color MainGridFillColor { get; }

		/// <summary>
		/// Gets the border color for the main grid.
		/// </summary>
		public Color MainGridBorderColor { get; }

		/// <summary>
		/// Gets or sets the current game theme.
		/// </summary>
		public static ColorTheme GameTheme { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorTheme"/> struct with the specified color values.
		/// </summary>
		/// <param name="BackgroundPrimaryColor">The primary background color.</param>
		/// <param name="BackgroundSecondaryColor">The secondary background color.</param>
		/// <param name="TextCountersColor">The color for text counters.</param>
		/// <param name="PanelBorderColor">The border color for panels.</param>
		/// <param name="ButtonActiveColor">The active button color.</param>
		public ColorTheme(Color BackgroundPrimaryColor, Color BackgroundSecondaryColor, Color TextCountersColor, Color PanelBorderColor, Color ButtonActiveColor)
		{
			this.BackgroundPrimaryColor = BackgroundPrimaryColor;
			this.BackgroundSecondaryColor = BackgroundSecondaryColor;
			TextColor = Color.White;
			this.TextCountersColor = TextCountersColor;
			PanelFillColor = new Color(22, 22, 20);
			this.PanelBorderColor = PanelBorderColor;
			this.ButtonActiveColor = ButtonActiveColor;
			ButtonInactiveColor = new Color(89, 89, 89);
			PanelTransparentFillColor = Color.Transparent;
			PanelTransparentBorderColor = Color.Transparent;
			MainGridFillColor = new Color(22, 22, 20);
			MainGridBorderColor = Color.Black;
		}

		#region Basic Themes

		/// <summary>
		/// Gets the Pink theme.
		/// </summary>
		public static ColorTheme Pink => new ColorTheme(
			BackgroundPrimaryColor: new Color(80, 19, 79),
			BackgroundSecondaryColor: new Color(37, 6, 32),
			TextCountersColor: Color.HotPink,
			PanelBorderColor: new Color(249, 40, 255),
			ButtonActiveColor: new Color(216, 110, 204)
		);

		/// <summary>
		/// Gets the Cyan theme.
		/// </summary>
		public static ColorTheme Cyan => new ColorTheme(
			BackgroundPrimaryColor: new Color(1, 83, 79),
			BackgroundSecondaryColor: new Color(2, 28, 27),
			TextCountersColor: Color.DarkCyan,
			PanelBorderColor: new Color(12, 216, 251),
			ButtonActiveColor: Color.DarkCyan
		);

		/// <summary>
		/// Gets the Yellow theme.
		/// </summary>
		public static ColorTheme Yellow => new ColorTheme(
			BackgroundPrimaryColor: new Color(80, 83, 2),
			BackgroundSecondaryColor: new Color(25, 28, 0),
			TextCountersColor: Color.Yellow,
			PanelBorderColor: new Color(253, 247, 13),
			ButtonActiveColor: new Color(171, 167, 9)
		);

		/// <summary>
		/// Gets the Green theme.
		/// </summary>
		public static ColorTheme Green => new ColorTheme(
			BackgroundPrimaryColor: new Color(16, 83, 3),
			BackgroundSecondaryColor: new Color(7, 30, 1),
			TextCountersColor: Color.GreenYellow,
			PanelBorderColor: new Color(42, 196, 10),
			ButtonActiveColor: Color.DarkGreen
		);

		#endregion

		#region Special Themes

		/// <summary>
		/// Gets the Cyber Punk theme.
		/// </summary>
		public static ColorTheme Cyber_Punk => new ColorTheme(
			BackgroundPrimaryColor: new Color(10, 10, 10),
			BackgroundSecondaryColor: new Color(28, 0, 55),
			TextCountersColor: Color.Fuchsia,
			PanelBorderColor: new Color(255, 20, 147),
			ButtonActiveColor: new Color(255, 83, 13)
		);

		/// <summary>
		/// Gets the Neon Dance theme.
		/// </summary>
		public static ColorTheme Neon_Dance => new ColorTheme(
			BackgroundPrimaryColor: new Color(8, 8, 31),
			BackgroundSecondaryColor: new Color(50, 0, 87),
			TextCountersColor: new Color(255, 85, 0),
			PanelBorderColor: new Color(0, 255, 180),
			ButtonActiveColor: new Color(255, 20, 147)
		);

		/// <summary>
		/// Gets the Lights City theme.
		/// </summary>
		public static ColorTheme Lights_City => new ColorTheme(
			BackgroundPrimaryColor: new Color(10, 10, 30),
			BackgroundSecondaryColor: new Color(50, 0, 100),
			TextCountersColor: new Color(255, 255, 0),
			PanelBorderColor: new Color(0, 200, 255),
			ButtonActiveColor: new Color(255, 0, 0)
		);

		/// <summary>
		/// Gets the Cyber Tropics theme.
		/// </summary>
		public static ColorTheme Cyber_Tropics => new ColorTheme(
			BackgroundPrimaryColor: new Color(30, 5, 50),
			BackgroundSecondaryColor: new Color(7, 46, 33),
			TextCountersColor: new Color(0, 255, 100),
			PanelBorderColor: new Color(255, 140, 0),
			ButtonActiveColor: new Color(30, 144, 255)
		);

		/// <summary>
		/// Gets the Cosmos Melody theme.
		/// </summary>
		public static ColorTheme Cosmos_Melody => new ColorTheme(
			BackgroundPrimaryColor: new Color(5, 0, 20),
			BackgroundSecondaryColor: new Color(25, 0, 66),
			TextCountersColor: new Color(173, 216, 230),
			PanelBorderColor: new Color(255, 20, 147),
			ButtonActiveColor: new Color(50, 205, 50)
		);

		/// <summary>
		/// Gets the Cyber Shine theme.
		/// </summary>
		public static ColorTheme Cyber_Shine => new ColorTheme(
			BackgroundPrimaryColor: new Color(10, 10, 30),
			BackgroundSecondaryColor: new Color(40, 0, 70),
			TextCountersColor: new Color(255, 255, 0),
			PanelBorderColor: new Color(255, 0, 255),
			ButtonActiveColor: new Color(0, 255, 180)
		);

		#endregion
	}
}
