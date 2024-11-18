using Microsoft.Xna.Framework;

namespace Tetris
{
	public struct Color_Theme
	{
		public Color Background_Primary_Color { get; }
		public Color Background_Secondary_Color { get; }

		public Color Text_Color { get; }
		public Color Text_Counters_Color { get; }

		public Color Panel_Fill_Color { get; }
		public Color Panel_Border_Color { get; }

		public Color Button_Active_Color { get; }
		public Color Button_Inactive_Color { get; }

		public Color Panel_Transparent_Fill_Color { get; }
		public Color Panel_Transparent_Border_Color { get; }

		public Color Main_Grid_Fill_Color { get; }
		public Color Main_Grid_Border_Color { get; }

		public static Color_Theme Game_Theme { get; set; }


		public Color_Theme(Color Background_Primary_Color, Color Background_Secondary_Color, Color Text_Counters_Color, Color Panel_Border_Color, Color Button_Active_Color)
		{
			this.Background_Primary_Color = Background_Primary_Color;
			this.Background_Secondary_Color = Background_Secondary_Color;
			Text_Color = Color.White;
			this.Text_Counters_Color = Text_Counters_Color;
			Panel_Fill_Color = new Color(22, 22, 20);
			this.Panel_Border_Color = Panel_Border_Color;
			this.Button_Active_Color = Button_Active_Color;
			Button_Inactive_Color = new Color(89, 89, 89);
			Panel_Transparent_Fill_Color = Color.Transparent;
			Panel_Transparent_Border_Color = Color.Transparent;
			Main_Grid_Fill_Color = new Color(22, 22, 20);
			Main_Grid_Border_Color = Color.Black;
		}

		// Motywy podstawowe

		public static Color_Theme Pink => new Color_Theme(
			Background_Primary_Color: new Color(80, 19, 79),
			Background_Secondary_Color: new Color(37, 6, 32),
			Text_Counters_Color: Color.HotPink,
			Panel_Border_Color: new Color(249, 40, 255),
			Button_Active_Color: new Color(216, 110, 204)
		);

		public static Color_Theme Cyan => new Color_Theme(
			Background_Primary_Color: new Color(1, 83, 79),
			Background_Secondary_Color: new Color(2, 28, 27),
			Text_Counters_Color: Color.DarkCyan,
			Panel_Border_Color: new Color(12,216,251),
			Button_Active_Color: Color.DarkCyan
		);

		public static Color_Theme Yellow => new Color_Theme(
			Background_Primary_Color: new Color(80, 83, 2),
			Background_Secondary_Color: new Color(25, 28, 0),
			Text_Counters_Color: Color.Yellow,
			Panel_Border_Color: new Color(253, 247, 13),
			Button_Active_Color: new Color(171, 167, 9)
		);

		public static Color_Theme Green => new Color_Theme(
			Background_Primary_Color: new Color(16 ,83 ,3),
			Background_Secondary_Color: new Color(7, 30, 1),
			Text_Counters_Color: Color.GreenYellow,
			Panel_Border_Color: new Color(42, 196, 10),
			Button_Active_Color: Color.DarkGreen
		);

		// Motywy specjalne

		public static Color_Theme Cyber_Punk => new Color_Theme(
			Background_Primary_Color: new Color(10, 10, 10),
			Background_Secondary_Color: new Color(28, 0, 55),
			Text_Counters_Color: Color.Fuchsia,
			Panel_Border_Color: new Color(255, 20, 147),
			Button_Active_Color: new Color(255, 83, 13)
		);

		public static Color_Theme Neon_Dance => new Color_Theme(
			Background_Primary_Color: new Color(10, 10, 40),
			Background_Secondary_Color: new Color(40, 0, 70),
			Text_Counters_Color: new Color(255, 85, 0),
			Panel_Border_Color: new Color(0, 255, 180),
			Button_Active_Color: new Color(255, 20, 147)
		);

		public static Color_Theme Lights_City => new Color_Theme(
			Background_Primary_Color: new Color(15, 15, 50),
			Background_Secondary_Color: new Color(50, 0, 100),
			Text_Counters_Color: new Color(255, 255, 0),
			Panel_Border_Color: new Color(0, 200, 255),
			Button_Active_Color: new Color(255, 0, 0)
		);

		public static Color_Theme Cyber_Tropics => new Color_Theme(
			Background_Primary_Color: new Color(30, 5, 50),
			Background_Secondary_Color: new Color(5, 35, 25),
			Text_Counters_Color: new Color(0, 255, 100),
			Panel_Border_Color: new Color(255, 140, 0),
			Button_Active_Color: new Color(30, 144, 255)
		);

		public static Color_Theme Cosmos_Melody => new Color_Theme(
			Background_Primary_Color: new Color(5, 0, 20),
			Background_Secondary_Color: new Color(15, 0, 40),
			Text_Counters_Color: new Color(173, 216, 230),
			Panel_Border_Color: new Color(255, 20, 147),
			Button_Active_Color: new Color(50, 205, 50)
		);

		public static Color_Theme Cyber_Shine => new Color_Theme(
			Background_Primary_Color: new Color(10, 10, 30),
			Background_Secondary_Color: new Color(40, 0, 70),
			Text_Counters_Color: new Color(255, 255, 0),
			Panel_Border_Color: new Color(255, 0, 255),
			Button_Active_Color: new Color(0, 255, 180)
		);

	}
}
