using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Tetris.Game_Components;
using Tetris.States;

namespace Tetris.Scenes
{
	/// <summary>
	/// Represents the game scene that manages all UI components and the overall state of the game.
	/// This scene is responsible for initializing, loading content, updating, and drawing all UI elements.
	/// </summary>
	public class GameScene : IScenes
	{
		#region Private Fields

		/// <summary>
		/// The list of Tetromino shapes currently active in the game.
		/// </summary>
		private List<Tetronimo> shapes;

		/// <summary>
		/// The background of the game.
		/// </summary>
		private Background background;

		/// <summary>
		/// The starfield effect used in the background.
		/// </summary>
		private Starfield backgroundEffect;

		/// <summary>
		/// The main grid where Tetromino shapes are placed.
		/// </summary>
		private MainGrid mainGrid;

		/// <summary>
		/// The panel displaying the current score.
		/// </summary>
		private ScorePanel scorePanel;

		/// <summary>
		/// The panel that shows the next Tetromino shape.
		/// </summary>
		private NextShapePanel nextShapePanel;

		/// <summary>
		/// The panel displaying the current game level.
		/// </summary>
		private LevelPanel levelPanel;

		/// <summary>
		/// The panel displaying the number of lines cleared.
		/// </summary>
		private LinesCountPanel linesCountPanel;

		/// <summary>
		/// The button to start the game.
		/// </summary>
		private Button startButton;

		/// <summary>
		/// The button to pause the game.
		/// </summary>
		private Button pauseButton;

		/// <summary>
		/// The button to access the settings.
		/// </summary>
		private Button settingsButton;

		/// <summary>
		/// The current state of the game.
		/// </summary>
		private IState currentState;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the list of Tetromino shapes in the game.
		/// </summary>
		public List<Tetronimo> Shapes { get => shapes; set => shapes = value; }

		/// <summary>
		/// Gets or sets the background.
		/// </summary>
		public Background Background { get => background; set => background = value; }

		/// <summary>
		/// Gets or sets the background starfield effect.
		/// </summary>
		public Starfield BackgroundEffect { get => backgroundEffect; set => backgroundEffect = value; }

		/// <summary>
		/// Gets or sets the main grid.
		/// </summary>
		public MainGrid MainGrid { get => mainGrid; set => mainGrid = value; }

		/// <summary>
		/// Gets or sets the score panel.
		/// </summary>
		public ScorePanel ScorePanel { get => scorePanel; set => scorePanel = value; }

		/// <summary>
		/// Gets or sets the next shape panel.
		/// </summary>
		public NextShapePanel NextShapePanel { get => nextShapePanel; set => nextShapePanel = value; }

		/// <summary>
		/// Gets or sets the level panel.
		/// </summary>
		public LevelPanel LevelPanel { get => levelPanel; set => levelPanel = value; }

		/// <summary>
		/// Gets or sets the lines count panel.
		/// </summary>
		public LinesCountPanel LinesCountPanel { get => linesCountPanel; set => linesCountPanel = value; }

		/// <summary>
		/// Gets or sets the start button.
		/// </summary>
		public Button StartButton { get => startButton; set => startButton = value; }

		/// <summary>
		/// Gets or sets the pause button.
		/// </summary>
		public Button PauseButton { get => pauseButton; set => pauseButton = value; }

		/// <summary>
		/// Gets or sets the settings button.
		/// </summary>
		public Button SettingsButton { get => settingsButton; set => settingsButton = value; }

		#endregion

		#region Public UI Flags

		/// <summary>
		/// Flag indicating if the main grid should be displayed.
		/// </summary>
		public bool IsMainGrid;

		/// <summary>
		/// Flag indicating if the score panel should be displayed.
		/// </summary>
		public bool IsScorePanel;

		/// <summary>
		/// Flag indicating if the next shape panel should be displayed.
		/// </summary>
		public bool IsNextShapePanel;

		/// <summary>
		/// Flag indicating if the level panel should be displayed.
		/// </summary>
		public bool IsLevelPanel;

		/// <summary>
		/// Flag indicating if the lines count panel should be displayed.
		/// </summary>
		public bool IsLinesCountPanel;

		/// <summary>
		/// Flag indicating if the start button should be displayed.
		/// </summary>
		public bool IsStartButton;

		/// <summary>
		/// Flag indicating if the pause button should be displayed.
		/// </summary>
		public bool IsPauseButton;

		/// <summary>
		/// Flag indicating if the settings button should be displayed.
		/// </summary>
		public bool IsSettingsButton;

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes the game scene with the specified window dimensions.
		/// Creates and configures all UI components and sets the initial game state.
		/// </summary>
		/// <param name="windowWidth">The width of the game window.</param>
		/// <param name="windowHeight">The height of the game window.</param>
		public void Initialize(int windowWidth, int windowHeight)
		{
			// Initialize game UI components.
			background = new Background(new Point(0, 0), windowWidth, windowHeight);
			backgroundEffect = new Starfield(250, windowWidth, windowHeight);
			mainGrid = new MainGrid(new Point(25, 25), 42, 10, 20);
			scorePanel = new ScorePanel(new Point(500, 25), 275, 175);
			nextShapePanel = new NextShapePanel(new Point(500, 250), 275, 150);
			levelPanel = new LevelPanel(new Point(500, 450), 275, 65);
			linesCountPanel = new LinesCountPanel(new Point(500, 590), 275, 65);
			startButton = new Button(new Point(500, 740), 202, 55, "START", "Assets/button_play");
			pauseButton = new Button(new Point(720, 740), 55, 55, "", "Assets/button_pause");
			settingsButton = new Button(new Point(500, 813), 275, 55, "SETTINGS");

			// Initialize the list of Tetromino shapes.
			shapes = new List<Tetronimo>();

			// Set the initial state based on the first run setting.
			if (Game1.GameSettings.IsFirstRun)
			{
				currentState = new FirstRunState(this);
			}
			else
			{
				ResetGameElements();
				scorePanel.BestScoreValue = Game1.GameSettings.BestScoreValue;
				currentState = new GameState(this);
			}
		}

		/// <summary>
		/// Loads the content for all game UI components.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch object used for drawing.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			// Load textures and fonts for UI components.
			background.LoadContent(spriteBatch);
			backgroundEffect.LoadContent(spriteBatch);
			mainGrid.LoadContent(spriteBatch);
			scorePanel.LoadContent(spriteBatch, content);
			nextShapePanel.LoadContent(spriteBatch, content);
			levelPanel.LoadContent(spriteBatch, content);
			linesCountPanel.LoadContent(spriteBatch, content);
			startButton.LoadContent(spriteBatch, content);
			pauseButton.LoadContent(spriteBatch, content);
			settingsButton.LoadContent(spriteBatch, content);
			currentState.LoadContent(spriteBatch, content);
		}

		/// <summary>
		/// Updates the game scene, including the current state and various UI components.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime)
		{
			currentState.Update(gameTime);
			backgroundEffect.Update();

			if (IsStartButton)
				startButton.Update();

			if (IsPauseButton)
				pauseButton.Update();

			if (IsSettingsButton)
				settingsButton.Update();
		}

		/// <summary>
		/// Draws the game scene, including the background, UI components, and the current state.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Draw(GameTime gameTime)
		{
			background.DrawBackground();
			backgroundEffect.Draw();

			if (IsMainGrid)
				mainGrid.DrawShapesGrid(shapes);

			if (IsScorePanel)
				scorePanel.Draw();

			if (IsNextShapePanel)
				nextShapePanel.Draw();

			if (IsLevelPanel)
				levelPanel.Draw();

			if (IsLinesCountPanel)
				linesCountPanel.Draw();

			if (IsStartButton)
				startButton.Draw();

			if (IsPauseButton)
				pauseButton.Draw();

			if (IsSettingsButton)
				settingsButton.Draw();

			currentState.Draw(gameTime);
		}

		/// <summary>
		/// Updates the theme of all game UI components.
		/// </summary>
		/// <param name="colorTheme">The new color theme to apply.</param>
		public void UpdateTheme(ColorTheme colorTheme)
		{
			background.UpdateTheme();
			mainGrid.UpdateTheme();
			scorePanel.UpdateTheme();
			nextShapePanel.UpdateTheme();
			levelPanel.UpdateTheme();
			linesCountPanel.UpdateTheme();
			startButton.UpdateTheme();
			pauseButton.UpdateTheme();
			settingsButton.UpdateTheme();
		}

		/// <summary>
		/// Changes the current state of the game to the specified state.
		/// </summary>
		/// <param name="state">The new state to transition to.</param>
		/// <returns>The updated GameScene instance.</returns>
		public GameScene ChangeState(IState state)
		{
			// Change the game state.
			currentState = state;
			return this;
		}

		/// <summary>
		/// Resets various game elements such as scores, levels, and UI component appearances.
		/// This method can also optionally update the theme, clear Tetromino shapes, and configure button states.
		/// </summary>
		/// <param name="resetTheme">If set to true, the UI theme will be refreshed.</param>
		/// <param name="zeroScores">If set to true, the current score will be reset to zero.</param>
		/// <param name="zeroBestScores">If set to true, the best score will be reset to zero.</param>
		/// <param name="zeroLines">If set to true, the count of cleared lines will be reset to zero.</param>
		/// <param name="newLevelValue">The new starting level value.</param>
		/// <param name="makeButtonsTransparent">If set to true, button borders will be made transparent.</param>
		/// <param name="setButtonsActive">If set to true, the Start and Settings buttons will be activated.</param>
		/// <param name="showUIElements">If set to true, various UI elements (panels and buttons) will be visible.</param>
		/// <returns>The updated GameScene instance.</returns>
		public GameScene ResetGameElements(
			bool resetTheme = true,
			bool zeroScores = true,
			bool zeroBestScores = true,
			bool zeroLines = true,
			int newLevelValue = 1,
			bool makeButtonsTransparent = true,
			bool setButtonsActive = true,
			bool showUIElements = true)
		{
			// 1. Refresh the theme if required.
			if (resetTheme)
			{
				ScorePanel.UpdateTheme();
				LevelPanel.UpdateTheme();
				LinesCountPanel.UpdateTheme();
				MainGrid.UpdateTheme();
				// ... update other panels if needed.
			}

			// 2. Reset scores.
			if (zeroScores)
			{
				ScorePanel.ScoreValue = 0;
			}

			if (zeroBestScores)
			{
				ScorePanel.BestScoreValue = 0;
			}

			// 3. Reset lines count.
			if (zeroLines)
			{
				LinesCountPanel.LinesValue = 0;
			}

			// 4. Set the starting level.
			LevelPanel.LevelValue = newLevelValue;

			// 5. Optionally make button borders transparent.
			if (makeButtonsTransparent)
			{
				StartButton.ObwColor = Color.Transparent;
				PauseButton.ObwColor = Color.Transparent;
				SettingsButton.ObwColor = Color.Transparent;
			}

			// 6. Activate or deactivate Start and Settings buttons.
			if (setButtonsActive)
			{
				StartButton.ButtonStates = Button_States.Active;
				SettingsButton.ButtonStates = Button_States.Active;
			}
			else
			{
				StartButton.ButtonStates = Button_States.Inactive;
				SettingsButton.ButtonStates = Button_States.Inactive;
			}

			// 7. Clear the list of shapes and reset the next shape panel.
			shapes.Clear();
			nextShapePanel.TetronimoShape = default;

			// 8. Set the visibility of UI elements.
			IsMainGrid = showUIElements;
			IsLevelPanel = showUIElements;
			IsLinesCountPanel = showUIElements;
			IsScorePanel = showUIElements;
			IsNextShapePanel = showUIElements;
			IsStartButton = showUIElements;
			IsPauseButton = showUIElements;
			IsSettingsButton = showUIElements;

			// 9. Optionally set the game state to "Not Started" if needed.
			// currentState = new Not_Started_State(this);

			return this;
		}

		#endregion
	}
}