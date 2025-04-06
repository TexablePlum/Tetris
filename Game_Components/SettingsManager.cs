using System;
using System.IO;
using System.Xml.Serialization;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Provides methods for loading and saving game settings from an XML file.
	/// Manages the settings file in the user's My Documents folder under a "Tetris" directory.
	/// </summary>
	public static class SettingsManager
	{
		#region Private Fields

		/// <summary>
		/// The folder path where the settings file is stored.
		/// </summary>
		private static readonly string settingsFolder = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"Tetris");

		/// <summary>
		/// The full file path of the settings XML file.
		/// </summary>
		private static readonly string settingsFilePath = Path.Combine(settingsFolder, "settings.xml");

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the game settings from the XML file.
		/// If the file does not exist, it creates the file with default settings.
		/// Sets the current game theme based on the loaded settings.
		/// </summary>
		/// <returns>The loaded <see cref="Settings"/> object.</returns>
		public static Settings LoadSettings()
		{
			try
			{
				// Check if the settings folder exists; if not, create it.
				if (!Directory.Exists(settingsFolder))
					Directory.CreateDirectory(settingsFolder);

				// If the settings file does not exist, create it with default settings.
				if (!File.Exists(settingsFilePath))
				{
					Settings defaultSettings = GetDefaultSettings();
					SaveSettings(defaultSettings);
					// Set default theme.
					ColorTheme.GameTheme = GetThemeFromString(defaultSettings.GameTheme);
					return defaultSettings;
				}

				// Read settings from the file.
				XmlSerializer serializer = new XmlSerializer(typeof(Settings));
				using (StreamReader reader = new StreamReader(settingsFilePath))
				{
					Settings settings = (Settings)serializer.Deserialize(reader);
					// Convert theme name to a ColorTheme.
					ColorTheme.GameTheme = GetThemeFromString(settings.GameTheme);
					return settings;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error loading settings: " + ex.Message);
				Settings defaultSettings = GetDefaultSettings();
				ColorTheme.GameTheme = GetThemeFromString(defaultSettings.GameTheme);
				return defaultSettings;
			}
		}

		/// <summary>
		/// Saves the specified game settings to the XML file.
		/// </summary>
		/// <param name="settings">The <see cref="Settings"/> object to save.</param>
		public static void SaveSettings(Settings settings)
		{
			try
			{
				if (!Directory.Exists(settingsFolder))
					Directory.CreateDirectory(settingsFolder);

				XmlSerializer serializer = new XmlSerializer(typeof(Settings));
				using (StreamWriter writer = new StreamWriter(settingsFilePath))
				{
					serializer.Serialize(writer, settings);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error saving settings: " + ex.Message);
			}
		}

		/// <summary>
		/// Converts a theme name to the corresponding <see cref="ColorTheme"/>.
		/// If the theme name is not recognized, the default theme (Lights_City) is returned.
		/// </summary>
		/// <param name="themeName">The name of the theme.</param>
		/// <returns>The corresponding <see cref="ColorTheme"/>.</returns>
		public static ColorTheme GetThemeFromString(string themeName)
		{
			switch (themeName)
			{
				case "Lights_City":
					return ColorTheme.Lights_City;
				case "Cyan":
					return ColorTheme.Cyan;
				case "Pink":
					return ColorTheme.Pink;
				case "Yellow":
					return ColorTheme.Yellow;
				case "Green":
					return ColorTheme.Green;
				case "Cyber_Punk":
					return ColorTheme.Cyber_Punk;
				case "Neon_Dance":
					return ColorTheme.Neon_Dance;
				case "Cyber_Tropics":
					return ColorTheme.Cyber_Tropics;
				case "Cosmos_Melody":
					return ColorTheme.Cosmos_Melody;
				case "Cyber_Shine":
					return ColorTheme.Cyber_Shine;
				default:
					return ColorTheme.Lights_City;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Returns the default game settings.
		/// </summary>
		/// <returns>A new <see cref="Settings"/> object with default values.</returns>
		private static Settings GetDefaultSettings()
		{
			return new Settings
			{
				BestScoreValue = 0,
				IsFirstRun = true,
				GameTheme = "Lights_City"
			};
		}

		#endregion
	}
}
