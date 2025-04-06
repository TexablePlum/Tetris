using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Threading.Tasks;

namespace Tetris.Game_Components
{
	/// <summary>
	/// Manages sound effects and music for the game.
	/// Provides methods for loading audio content and changing the background music.
	/// </summary>
	public static class SoundManager
	{
		#region Private Fields

		/// <summary>
		/// Sound effect played when a Tetronimo moves.
		/// </summary>
		private static SoundEffect soundMove;

		/// <summary>
		/// Sound effect played when a Tetronimo rotates.
		/// </summary>
		private static SoundEffect soundRotate;

		/// <summary>
		/// Sound effect played when a Tetronimo drops.
		/// </summary>
		private static SoundEffect soundDrop;

		/// <summary>
		/// Sound effect played when a line is cleared.
		/// </summary>
		private static SoundEffect soundLine;

		/// <summary>
		/// Sound effect played when the game starts or resumes.
		/// </summary>
		private static SoundEffect soundStartResume;

		/// <summary>
		/// Sound effect played when the game is over.
		/// </summary>
		private static SoundEffect soundGameover;

		/// <summary>
		/// Sound effect played when the level is increased.
		/// </summary>
		private static SoundEffect soundLevelup;

		/// <summary>
		/// Sound effect played when a button is pressed.
		/// </summary>
		private static SoundEffect soundButton;

		/// <summary>
		/// Background music for the game.
		/// </summary>
		private static Song music;

		/// <summary>
		/// Background music for the menu.
		/// </summary>
		private static Song menuMusic;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the sound effect played when a Tetronimo moves.
		/// </summary>
		public static SoundEffect SoundMove { get => soundMove; }

		/// <summary>
		/// Gets the sound effect played when a Tetronimo rotates.
		/// </summary>
		public static SoundEffect SoundRotate { get => soundRotate; }

		/// <summary>
		/// Gets the sound effect played when a Tetronimo drops.
		/// </summary>
		public static SoundEffect SoundDrop { get => soundDrop; }

		/// <summary>
		/// Gets the sound effect played when a line is cleared.
		/// </summary>
		public static SoundEffect SoundLine { get => soundLine; }

		/// <summary>
		/// Gets the sound effect played when the game starts or resumes.
		/// </summary>
		public static SoundEffect SoundStartResume { get => soundStartResume; }

		/// <summary>
		/// Gets the sound effect played when the game is over.
		/// </summary>
		public static SoundEffect SoundGameover { get => soundGameover; }

		/// <summary>
		/// Gets the sound effect played when the level is increased.
		/// </summary>
		public static SoundEffect SoundLevelup { get => soundLevelup; }

		/// <summary>
		/// Gets the sound effect played when a button is pressed.
		/// </summary>
		public static SoundEffect SoundButton { get => soundButton; }

		/// <summary>
		/// Gets the background music for the game.
		/// </summary>
		public static Song Music { get => music; }

		/// <summary>
		/// Gets the background music for the menu.
		/// </summary>
		public static Song MenuMusic { get => menuMusic; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads the audio content required for the game.
		/// </summary>
		/// <param name="content">The ContentManager used for loading audio assets.</param>
		public static void Load(ContentManager content)
		{
			soundMove = content.Load<SoundEffect>("Audio/move");
			soundRotate = content.Load<SoundEffect>("Audio/rotate");
			soundDrop = content.Load<SoundEffect>("Audio/drop");
			soundLine = content.Load<SoundEffect>("Audio/line");
			soundStartResume = content.Load<SoundEffect>("Audio/start");
			soundGameover = content.Load<SoundEffect>("Audio/gameover");
			soundLevelup = content.Load<SoundEffect>("Audio/levelup");
			soundButton = content.Load<SoundEffect>("Audio/button");
			menuMusic = content.Load<Song>("Audio/menu");
			music = content.Load<Song>("Audio/music");
		}

		/// <summary>
		/// Changes the current background music to the specified song after a delay.
		/// </summary>
		/// <param name="newSong">The new song to play.</param>
		/// <param name="delayMilliseconds">The delay in milliseconds before the new song starts.</param>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public static async Task ChangeMusic(Song newSong, int delayMilliseconds)
		{
			MediaPlayer.Stop();
			await Task.Delay(delayMilliseconds);
			MediaPlayer.Play(newSong);
		}

		#endregion
	}
}
