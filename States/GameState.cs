using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tetris.Game_Components;
using Tetris.Scenes;

namespace Tetris.States
{
	/// <summary>
	/// Represents the main game state where gameplay occurs.
	/// This class handles user input, tetromino movement and rotation, line clearing, scoring, and game over logic.
	/// </summary>
	public class GameState : IState
	{
		#region Fields

		/// <summary>
		/// The current game scene.
		/// </summary>
		private GameScene gameScene;

		/// <summary>
		/// The current keyboard state.
		/// </summary>
		private KeyboardState currentKeyboardState;
		/// <summary>
		/// The previous keyboard state.
		/// </summary>
		private KeyboardState previousKeyboardState;

		/// <summary>
		/// Indicates whether the game is paused.
		/// </summary>
		private bool isPaused = true;

		/// <summary>
		/// The current active tetromino.
		/// </summary>
		private Tetronimo currentTetromino;
		/// <summary>
		/// The tetromino to be shown in the next shape panel.
		/// </summary>
		private Tetronimo nextTetromino;

		/// <summary>
		/// Array of available tetromino shapes for random selection.
		/// </summary>
		private TetronimoShape[] availableShapes =
		{
			TetronimoShape.ShapeI,
			TetronimoShape.ShapeO,
			TetronimoShape.ShapeS,
			TetronimoShape.ShapeZ,
			TetronimoShape.ShapeL,
			TetronimoShape.ShapeJ,
			TetronimoShape.ShapeT
		};

		/// <summary>
		/// Random number generator.
		/// </summary>
		private Random random = new Random();

		/// <summary>
		/// Timer used for automatic tetromino falling.
		/// </summary>
		private float fallTimer = 0f;
		/// <summary>
		/// Interval (in seconds) between automatic drops.
		/// </summary>
		private float fallInterval = 1.0f;

		// ===== VARIABLES FOR LINE CLEARING =====

		/// <summary>
		/// List of complete line indices to clear.
		/// </summary>
		private List<int> linesToClear = new List<int>();

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the GameState class with the specified game scene.
		/// Sets the initial state (paused) and configures UI buttons.
		/// </summary>
		/// <param name="gameScene">The current game scene.</param>
		public GameState(GameScene gameScene)
		{
			this.gameScene = gameScene;
			currentKeyboardState = Keyboard.GetState();
			previousKeyboardState = currentKeyboardState;

			// The game starts paused.
			isPaused = true;
			gameScene.StartButton.ButtonStates = Button_States.Active;
			gameScene.SettingsButton.ButtonStates = Button_States.Active;
			gameScene.PauseButton.ButtonStates = Button_States.Inactive;

			// Configure button actions.
			gameScene.StartButton.OnClick = async () =>
			{
				if (nextTetromino == null)
					nextTetromino = CreateRandomTetromino();
				gameScene.NextShapePanel.TetronimoShape = GetEnumFromTetromino(nextTetromino);
				await ResumeWithCountdown();
			};
			gameScene.PauseButton.OnClick = () => { PauseGame(); };
		}

		#endregion

		#region Load and Draw

		/// <summary>
		/// Loads content if need.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch used for drawing.</param>
		/// <param name="content">The ContentManager to load assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			// Load resources if needed.
		}

		/// <summary>
		/// Draw content if need.
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		public void Draw(GameTime gameTime)
		{
			// Draw if needed.
		}

		#endregion

		#region Update

		/// <summary>
		/// Updates the game state, handling input, tetromino falling, line clearing, and game over logic.
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		public void Update(GameTime gameTime)
		{
			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			HandleInput();

			// Process falling if game is not paused.
			if (!isPaused)
			{
				fallTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (fallTimer >= fallInterval)
				{
					fallTimer = 0f;
					if (currentTetromino != null && CanMove(currentTetromino, MoveDirection.Down))
					{
						currentTetromino.Move(MoveDirection.Down);
					}
					else
					{
						if (currentTetromino != null)
						{
							FixTetromino(currentTetromino);
							// After fixing, check for complete lines.
							var lines = GetCompleteLines();
							if (lines.Count > 0)
							{
								// Immediately finalize line clearing.
								FinalizeLineClear(lines);
								return;
							}
						}
						if (CheckGameOver())
						{
							_ = GameOver();
						}
						else
						{
							SpawnTetromino();
						}
					}
				}
			}
		}

		#endregion

		#region Input Handling

		/// <summary>
		/// Handles user input from the keyboard.
		/// Processes pause/resume, movement, rotation, and dropping of tetrominoes.
		/// </summary>
		private void HandleInput()
		{
			if (IsKeyPressed(Keys.P))
			{
				if (isPaused)
					_ = ResumeWithCountdown();
				else
					PauseGame();
			}
			if (!isPaused)
			{
				if (IsKeyPressed(Keys.Left) && currentTetromino != null)
				{
					if (CanMove(currentTetromino, MoveDirection.Left))
					{
						currentTetromino.Move(MoveDirection.Left);
						SoundManager.SoundMove.Play();
					}
				}
				if (IsKeyPressed(Keys.Right) && currentTetromino != null)
				{
					if (CanMove(currentTetromino, MoveDirection.Right))
					{
						currentTetromino.Move(MoveDirection.Right);
						SoundManager.SoundMove.Play();
					}
				}
				if (IsKeyPressed(Keys.Down) && currentTetromino != null)
				{
					if (CanMove(currentTetromino, MoveDirection.Down))
					{
						currentTetromino.Move(MoveDirection.Down);
						SoundManager.SoundMove.Play();
						fallTimer = 0f;
						// Award 1 point for manual downward movement.
						gameScene.ScorePanel.ScoreValue += 1;
						if (gameScene.ScorePanel.ScoreValue > gameScene.ScorePanel.BestScoreValue)
							gameScene.ScorePanel.BestScoreValue = gameScene.ScorePanel.ScoreValue;
					}
				}
				if (IsKeyPressed(Keys.Up) && currentTetromino != null)
				{
					if (CanRotate(currentTetromino))
					{
						currentTetromino.Rotate();
						SoundManager.SoundRotate.Play();
					}
				}
				if (IsKeyPressed(Keys.Space) && currentTetromino != null)
				{
					DropTetromino();
					SoundManager.SoundDrop.Play();
				}
			}
		}

		/// <summary>
		/// Returns true if the specified key is pressed in the current frame but not in the previous frame.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>True if the key was just pressed; otherwise, false.</returns>
		private bool IsKeyPressed(Keys key)
		{
			return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
		}

		#endregion

		#region Resume and Pause

		/// <summary>
		/// Resumes the game after a 3-second countdown.
		/// Disables start and settings buttons, plays the resume sound, waits, changes the music, and spawns a tetromino if needed.
		/// </summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		private async Task ResumeWithCountdown()
		{
			gameScene.StartButton.ButtonStates = Button_States.Inactive;
			gameScene.SettingsButton.ButtonStates = Button_States.Inactive;
			MediaPlayer.Stop();
			SoundManager.SoundStartResume.Play();
			await Task.Delay(3000);
			_ = SoundManager.ChangeMusic(SoundManager.Music, 200);

			isPaused = false;
			gameScene.PauseButton.ButtonStates = Button_States.Active;

			if (currentTetromino == null)
				SpawnTetromino();
		}

		/// <summary>
		/// Pauses the game, stops music, and adjusts the UI buttons accordingly.
		/// </summary>
		private void PauseGame()
		{
			isPaused = true;
			MediaPlayer.Stop();
			gameScene.StartButton.ButtonStates = Button_States.Active;
			gameScene.SettingsButton.ButtonStates = Button_States.Active;
			gameScene.PauseButton.ButtonStates = Button_States.Inactive;
			_ = SoundManager.ChangeMusic(SoundManager.MenuMusic, 200);
		}

		#endregion

		#region Movement, Drop, and Fix Tetromino

		/// <summary>
		/// Determines whether the specified tetromino can move in the given direction.
		/// Checks for grid boundaries and collisions with fixed tetrominoes.
		/// </summary>
		/// <param name="tet">The tetromino to check.</param>
		/// <param name="direction">The direction of movement.</param>
		/// <returns>True if the tetromino can move; otherwise, false.</returns>
		private bool CanMove(Tetronimo tet, MoveDirection direction)
		{
			int offsetX = 0, offsetY = 0;
			switch (direction)
			{
				case MoveDirection.Left:
					offsetX = -1;
					break;
				case MoveDirection.Right:
					offsetX = 1;
					break;
				case MoveDirection.Down:
					offsetY = 1;
					break;
			}
			int rows = gameScene.MainGrid.Rows;
			int cols = gameScene.MainGrid.Columns;
			foreach (var block in tet.Blocks)
			{
				int newX = block.X + offsetX;
				int newY = block.Y + offsetY;
				if (newX < 0 || newX >= rows || newY < 0 || newY >= cols)
					return false;
				foreach (var other in gameScene.Shapes)
				{
					if (other == tet)
						continue;
					if (other.Blocks.Any(b => b.X == newX && b.Y == newY))
						return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Determines whether the tetromino can rotate without colliding with fixed blocks.
		/// </summary>
		/// <param name="tet">The tetromino to test rotation on.</param>
		/// <returns>True if the tetromino can rotate; otherwise, false.</returns>
		private bool CanRotate(Tetronimo tet)
		{
			var testBlocks = new List<Point>(tet.Blocks);
			Point pivot = tet.Pivot;
			// Rotate each block around the pivot.
			for (int i = 0; i < testBlocks.Count; i++)
			{
				var b = testBlocks[i];
				int relX = b.X - pivot.X;
				int relY = b.Y - pivot.Y;
				int rotatedX = -relY;
				int rotatedY = relX;
				testBlocks[i] = new Point(pivot.X + rotatedX, pivot.Y + rotatedY);
			}
			foreach (var pos in testBlocks)
			{
				if (gameScene.Shapes.Any(t => t != tet && t.Blocks.Any(b => b.X == pos.X && b.Y == pos.Y)))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Drops the current tetromino instantly until it cannot move down further.
		/// Awards points based on the distance dropped, fixes the tetromino, and processes line clearing.
		/// </summary>
		private void DropTetromino()
		{
			int dropDistance = 0;
			while (currentTetromino != null && CanMove(currentTetromino, MoveDirection.Down))
			{
				currentTetromino.Move(MoveDirection.Down);
				dropDistance++;
			}
			// Award points for manual drop.
			if (dropDistance > 0)
			{
				gameScene.ScorePanel.ScoreValue += dropDistance;
				if (gameScene.ScorePanel.ScoreValue > gameScene.ScorePanel.BestScoreValue)
					gameScene.ScorePanel.BestScoreValue = gameScene.ScorePanel.ScoreValue;
			}
			if (currentTetromino != null)
			{
				FixTetromino(currentTetromino);
				var lines = GetCompleteLines();
				if (lines.Count > 0)
				{
					FinalizeLineClear(lines);
					return;
				}
			}
			if (CheckGameOver())
			{
				_ = GameOver();
			}
			else
			{
				SpawnTetromino();
			}
		}

		/// <summary>
		/// Fixes the tetromino by marking it as inactive.
		/// </summary>
		/// <param name="tet">The tetromino to fix.</param>
		private void FixTetromino(Tetronimo tet)
		{
			tet.IsActive = false;
		}

		/// <summary>
		/// Updates the best score if the current score exceeds it.
		/// </summary>
		private void UpdateBestScore()
		{
			if (gameScene.ScorePanel.ScoreValue > gameScene.ScorePanel.BestScoreValue)
				gameScene.ScorePanel.BestScoreValue = gameScene.ScorePanel.ScoreValue;
		}

		#endregion

		#region Tetromino Spawning

		/// <summary>
		/// Spawns a new tetromino. The current tetromino is replaced by the next tetromino,
		/// and a new tetromino is created for the preview.
		/// </summary>
		private void SpawnTetromino()
		{
			if (nextTetromino == null)
				nextTetromino = CreateRandomTetromino();
			currentTetromino = nextTetromino;
			if (gameScene.Shapes == null)
				gameScene.Shapes = new List<Tetronimo>();
			gameScene.Shapes.Add(currentTetromino);

			nextTetromino = CreateRandomTetromino();
			gameScene.NextShapePanel.TetronimoShape = GetEnumFromTetromino(nextTetromino);
		}

		/// <summary>
		/// Creates a random tetromino.
		/// </summary>
		/// <returns>A new tetromino instance.</returns>
		private Tetronimo CreateRandomTetromino()
		{
			var shapeEnum = availableShapes[random.Next(availableShapes.Length)];
			return CreateTetrominoByEnum(shapeEnum);
		}

		/// <summary>
		/// Creates a tetromino of the specified shape.
		/// </summary>
		/// <param name="shapeEnum">The tetromino shape.</param>
		/// <returns>A new tetromino instance.</returns>
		private Tetronimo CreateTetrominoByEnum(TetronimoShape shapeEnum)
		{
			switch (shapeEnum)
			{
				case TetronimoShape.ShapeI: return new ShapeI();
				case TetronimoShape.ShapeO: return new ShapeO();
				case TetronimoShape.ShapeS: return new ShapeS();
				case TetronimoShape.ShapeZ: return new ShapeZ();
				case TetronimoShape.ShapeL: return new ShapeL();
				case TetronimoShape.ShapeJ: return new ShapeJ();
				case TetronimoShape.ShapeT: return new ShapeT();
				default: return null;
			}
		}

		/// <summary>
		/// Returns the tetromino shape enum corresponding to the specified tetromino.
		/// </summary>
		/// <param name="tet">The tetromino instance.</param>
		/// <returns>The tetromino shape enum.</returns>
		private TetronimoShape GetEnumFromTetromino(Tetronimo tet)
		{
			if (tet is ShapeI) return TetronimoShape.ShapeI;
			if (tet is ShapeO) return TetronimoShape.ShapeO;
			if (tet is ShapeS) return TetronimoShape.ShapeS;
			if (tet is ShapeZ) return TetronimoShape.ShapeZ;
			if (tet is ShapeL) return TetronimoShape.ShapeL;
			if (tet is ShapeJ) return TetronimoShape.ShapeJ;
			if (tet is ShapeT) return TetronimoShape.ShapeT;
			return TetronimoShape.None;
		}

		#endregion

		#region Line Clearing

		/// <summary>
		/// Determines which lines are complete (only fixed tetrominoes are considered).
		/// </summary>
		/// <returns>A list of complete line indices.</returns>
		private List<int> GetCompleteLines()
		{
			var completeLines = new List<int>();
			int rows = gameScene.MainGrid.Rows;
			int cols = gameScene.MainGrid.Columns;
			for (int y = 0; y < cols; y++)
			{
				bool lineComplete = true;
				for (int x = 0; x < rows; x++)
				{
					bool occupied = gameScene.Shapes.Any(t =>
						!t.IsActive && t.Blocks.Any(b => b.X == x && b.Y == y));
					if (!occupied)
					{
						lineComplete = false;
						break;
					}
				}
				if (lineComplete)
					completeLines.Add(y);
			}
			return completeLines;
		}

		/// <summary>
		/// Finalizes the line clear process: plays the clear sound, clears the lines, moves blocks above, updates score and level, and spawns a new tetromino.
		/// </summary>
		/// <param name="lines">The list of complete line indices.</param>
		private void FinalizeLineClear(List<int> lines)
		{
			int clearedLines = lines.Count;

			ClearLines(lines);
			SoundManager.SoundLine.Play();
			MoveBlocksAbove(lines);

			// Update the line counter.
			gameScene.LinesCountPanel.LinesValue += clearedLines;

			// Calculate points for cleared lines: points = 200 * number of lines - 100.
			int lineClearPoints = (clearedLines > 0) ? (200 * clearedLines - 100) : 0;
			gameScene.ScorePanel.ScoreValue += lineClearPoints;

			// Update best score.
			if (gameScene.ScorePanel.ScoreValue > gameScene.ScorePanel.BestScoreValue)
				gameScene.ScorePanel.BestScoreValue = gameScene.ScorePanel.ScoreValue;

			// Update level: increase level by 1 for every 10 lines cleared.
			int newLevel = gameScene.LinesCountPanel.LinesValue / 10 + 1;
			if (newLevel > gameScene.LevelPanel.LevelValue)
			{
				gameScene.LevelPanel.LevelValue = newLevel;
				SoundManager.SoundLevelup.Play();
				fallInterval = Math.Max(0.1f, fallInterval / 2);
			}

			if (CheckGameOver())
			{
				_ = GameOver();
			}
			else
			{
				SpawnTetromino();
			}
		}

		/// <summary>
		/// Clears the specified complete lines from fixed tetromino blocks.
		/// </summary>
		/// <param name="lines">The list of line indices to clear.</param>
		private void ClearLines(List<int> lines)
		{
			foreach (var tet in gameScene.Shapes)
			{
				tet.Blocks.RemoveAll(b => lines.Contains(b.Y));
			}
			gameScene.Shapes.RemoveAll(t => t.Blocks.Count == 0);
		}

		/// <summary>
		/// Moves blocks above the cleared lines downward by the number of cleared lines below them.
		/// </summary>
		/// <param name="lines">The list of cleared line indices.</param>
		private void MoveBlocksAbove(List<int> lines)
		{
			lines.Sort();
			foreach (var tet in gameScene.Shapes)
			{
				if (tet.IsActive)
					continue;
				for (int i = 0; i < tet.Blocks.Count; i++)
				{
					Point b = tet.Blocks[i];
					int drop = lines.Count(line => line > b.Y);
					tet.Blocks[i] = new Point(b.X, b.Y + drop);
				}
			}
		}

		#endregion

		#region Game Over

		/// <summary>
		/// Checks if the game is over by determining if any fixed tetromino has a block at the top row.
		/// </summary>
		/// <returns>True if game over condition is met; otherwise, false.</returns>
		private bool CheckGameOver()
		{
			foreach (var tet in gameScene.Shapes)
			{
				if (tet.IsActive)
					continue;
				if (tet.Blocks.Any(b => b.Y == 0))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Handles the game over state: stops the game, plays the game over sound, waits, resets game elements,
		/// clears tetrominoes, resets UI buttons, changes music to menu music, and saves settings.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		private async Task GameOver()
		{
			isPaused = true;
			gameScene.PauseButton.ButtonStates = Button_States.Inactive;
			MediaPlayer.Stop();
			SoundManager.SoundGameover.Play();
			await Task.Delay(3000);

			gameScene.ResetGameElements(
				resetTheme: true,
				zeroScores: true,
				zeroBestScores: false,
				zeroLines: true,
				newLevelValue: 1,
				makeButtonsTransparent: true,
				setButtonsActive: true,
				showUIElements: true
			);
			gameScene.Shapes.Clear();
			currentTetromino = null;
			nextTetromino = null;

			gameScene.StartButton.ButtonStates = Button_States.Active;
			gameScene.SettingsButton.ButtonStates = Button_States.Active;
			gameScene.PauseButton.ButtonStates = Button_States.Inactive;

			_ = SoundManager.ChangeMusic(SoundManager.MenuMusic, 1000);

			Game1.GameSettings.BestScoreValue = gameScene.ScorePanel.BestScoreValue;
			SettingsManager.SaveSettings(Game1.GameSettings);
		}

		#endregion
	}
}
