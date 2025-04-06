namespace Tetris.Game_Components
{
	/// <summary>
	/// Represents the game settings including the best score, first run flag, and selected game theme.
	/// </summary>
	public class Settings
	{
		/// <summary>
		/// Gets or sets the best score value achieved by the player.
		/// </summary>
		public long BestScoreValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this is the first run of the game.
		/// </summary>
		public bool IsFirstRun { get; set; }

		/// <summary>
		/// Gets or sets the identifier for the game theme.
		/// </summary>
		public string GameTheme { get; set; }
	}
}
