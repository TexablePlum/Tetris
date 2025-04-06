using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tetris.Game_Components;
using Tetris.Scenes;

namespace Tetris.States
{
	/// <summary>
	/// Represents the first run state of the game, where a tutorial and a welcome popup window are displayed.
	/// In this state, the user is introduced to the basic game mechanics and UI elements.
	/// </summary>
	public class FirstRunState : IState
	{
		/// <summary>
		/// The game scene on which this state operates.
		/// </summary>
		private GameScene gameScene;

		/// <summary>
		/// The popup window displayed during the tutorial.
		/// </summary>
		private PopupWindow popupWindow;

		/// <summary>
		/// Cancellation token source used to cancel asynchronous tutorial tasks.
		/// </summary>
		private CancellationTokenSource tutorialCancellationTokenSource;

		/// <summary>
		/// Initializes a new instance of the <see cref="FirstRunState"/> class and sets up the initial game scene state.
		/// </summary>
		/// <param name="gameScene">The game scene on which the tutorial will be displayed.</param>
		public FirstRunState(GameScene gameScene)
		{
			this.gameScene = gameScene;

			// Initially disable the pause button.
			gameScene.PauseButton.ButtonStates = Button_States.Inactive;

			// Create the welcome popup window.
			popupWindow = new PopupWindow(new Point(100, 200), 600, 500, 4);
		}

		/// <summary>
		/// Loads the content required by the state, including popup window assets, and starts the first step of the tutorial.
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch object used for drawing graphics.</param>
		/// <param name="content">The ContentManager used for loading assets.</param>
		public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
		{
			popupWindow.LoadContent(spriteBatch, content);
			FirstStep();
		}

		/// <summary>
		/// Draws the state elements, mainly the popup window.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Draw(GameTime gameTime)
		{
			if (popupWindow != null)
				popupWindow.Draw();
		}

		/// <summary>
		/// Updates the state logic, including the popup window updates.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime)
		{
			if (popupWindow != null)
				popupWindow.Update(gameTime);
		}

		/// <summary>
		/// Executes the first step of the tutorial, displaying the welcome message and configuring the "Next" button.
		/// </summary>
		private async void FirstStep()
		{
			// Cancel any existing tutorial tasks if they exist.
			tutorialCancellationTokenSource?.Cancel();
			tutorialCancellationTokenSource = new CancellationTokenSource();
			var token = tutorialCancellationTokenSource.Token;

			popupWindow.WindowTitle = "Welcome to Tetris!";
			popupWindow.TextHeader = $"Hello {Environment.UserName}!";
			popupWindow.WindowText =
				"This is a Tetris game clone. \n" +
				"Thank you for trying out this early development version. Before you dive in, I have a short tutorial to help you learn the basics. " +
				"I appreciate your patience and feedback as I work to make this game the best it can be. \nLet's get started! \n\nAre you ready to stack some blocks?";

			popupWindow.NextButton.OnClick = () =>
			{
				popupWindow.Step++;
				popupWindow.NextButton.ButtonStates = Button_States.Inactive;

				// Cancel tasks from the current step and move to the next.
				tutorialCancellationTokenSource?.Cancel();
				SecondStep();
			};

			await Task.Delay(3000, token);
			popupWindow.NextButton.ButtonStates = Button_States.Active;
		}

		/// <summary>
		/// Executes the second step of the tutorial, introducing the main grid of the game and its basic functions.
		/// It also starts animations on the grid and sets the logic for the "Next" button.
		/// </summary>
		private async void SecondStep()
		{
			// Cancel any existing tasks (if any).
			tutorialCancellationTokenSource?.Cancel();
			tutorialCancellationTokenSource = new CancellationTokenSource();
			var token = tutorialCancellationTokenSource.Token;

			gameScene.IsMainGrid = true;

			// Start blinking animation and grid animation.
			_ = BlinkingObw(gameScene.MainGrid, token);
			_ = GridAnimation(token);

			popupWindow.NextButton.OnClick = () =>
			{
				// Cancel tasks for the current step and clear the shapes.
				tutorialCancellationTokenSource?.Cancel();
				gameScene.Shapes.Clear();

				popupWindow.Step++;
				popupWindow.NextButton.ButtonStates = Button_States.Inactive;
				ThirdStep();
			};

			popupWindow.Position = new Point(500, 25);
			popupWindow.Width = 275;
			popupWindow.Height = 840;
			popupWindow.WindowTitle = "Main Grid";
			popupWindow.TextHeader = null;
			popupWindow.WindowText =
				"This is the Main Grid. \n" +
				"The main area of your game. You have 7 shapes at your disposal: [I, J, L, O, S, Z, T]. " +
				"You can rotate and move them freely across the board. " +
				"When you complete a full line, it gets cleared, giving you more space to continue playing.";

			await Task.Delay(3000, token);
			popupWindow.NextButton.ButtonStates = Button_States.Active;
		}

		/// <summary>
		/// Executes the third step of the tutorial, introducing the game interface elements and starting various animations.
		/// </summary>
		private async void ThirdStep()
		{
			// Cancel any existing tasks.
			tutorialCancellationTokenSource?.Cancel();
			tutorialCancellationTokenSource = new CancellationTokenSource();
			var token = tutorialCancellationTokenSource.Token;

			gameScene.IsMainGrid = false;
			gameScene.IsScorePanel = true;
			gameScene.IsNextShapePanel = true;
			gameScene.IsLevelPanel = true;
			gameScene.IsLinesCountPanel = true;
			gameScene.IsStartButton = true;
			gameScene.IsPauseButton = true;
			gameScene.IsSettingsButton = true;
			gameScene.StartButton.ButtonStates = Button_States.Inactive;
			gameScene.SettingsButton.ButtonStates = Button_States.Inactive;

			popupWindow.Position = new Point(25, 25);
			popupWindow.Width = 450;
			popupWindow.Height = 840;
			popupWindow.WindowTitle = "Game Interface";
			popupWindow.TextHeader = null;
			popupWindow.WindowText =
				"This is your Game Interface. \n\n" +
				"At the top, you will see: the Score Panel displaying your current and highest score, " +
				"the Next Shape Panel showing the upcoming shape, followed by the Level Panel which displays " +
				"your current game level (increasing by 1 every 10 cleared lines), and a panel showing the number of lines cleared. " +
				"Below these, you will find the start/pause button and the settings button.";

			// Blink various panels.
			Panel[] panelsToBlink =
			{
				gameScene.ScorePanel,
				gameScene.LevelPanel,
				gameScene.StartButton,
				gameScene.PauseButton,
				gameScene.SettingsButton
			};
			foreach (var panel in panelsToBlink)
			{
				_ = BlinkingObw(panel, token);
			}

			// Start animations for Next Shape, Score, and Lines.
			_ = NextShapeAnimation(token);
			_ = AnimateScore(0, 1500, 30, token);
			_ = AnimateLines(0, 5, 15, token);

			await Task.Delay(3000, token);
			popupWindow.NextButton.ButtonStates = Button_States.Active;
			popupWindow.NextButton.OnClick = () =>
			{
				// Cancel tasks for the current step.
				tutorialCancellationTokenSource?.Cancel();

				popupWindow.Step++;
				gameScene.NextShapePanel.TetronimoShape = default;
				FourthStep();
			};
		}

		/// <summary>
		/// Executes the fourth and final step of the tutorial, finalizing the game settings and preparing the user to start playing.
		/// After this step, settings are saved and the state changes to the main game state.
		/// </summary>
		private async void FourthStep()
		{
			tutorialCancellationTokenSource?.Cancel();
			tutorialCancellationTokenSource = new CancellationTokenSource();
			var token = tutorialCancellationTokenSource.Token;

			popupWindow.NextButton.ButtonStates = Button_States.Inactive;
			gameScene.ResetGameElements(
				resetTheme: true,
				zeroScores: true,
				zeroBestScores: true,
				zeroLines: true,
				newLevelValue: 1,
				makeButtonsTransparent: true,
				setButtonsActive: false,
				showUIElements: true
			);

			// Configure the popup window for the final tutorial message.
			popupWindow.Position = new Point(150, 275);
			popupWindow.Width = 500;
			popupWindow.Height = 350;
			popupWindow.WindowTitle = "Let's start!";
			popupWindow.WindowText = "Now you're ready to stack your first blocks. Use arrows: LEFT, RIGHT, DOWN to move, UP to rotate and SPACE to drop. Good luck and have a lot of fun!";

			await Task.Delay(3000, token);
			popupWindow.NextButton.ButtonStates = Button_States.Active;

			popupWindow.NextButton.OnClick = () =>
			{
				tutorialCancellationTokenSource?.Cancel();

				SettingsManager.SaveSettings(Game1.GameSettings);
				popupWindow = null;

				gameScene.StartButton.ButtonStates = Button_States.Active;
				gameScene.SettingsButton.ButtonStates = Button_States.Active;

				Game1.GameSettings.IsFirstRun = false;
				SettingsManager.SaveSettings(Game1.GameSettings);

				// Transition to the main game state.
				gameScene.ChangeState(new GameState(gameScene));
			};
		}

		/// <summary>
		/// Animates the score panel by incrementing the score and updating the level accordingly.
		/// </summary>
		/// <param name="start">The initial score value.</param>
		/// <param name="end">The final score value.</param>
		/// <param name="steps">The number of animation steps.</param>
		/// <param name="token">The cancellation token to cancel the animation.</param>
		private async Task AnimateScore(long start, long end, int steps, CancellationToken token)
		{
			long diff = end - start;
			long stepValue = diff / steps;
			try
			{
				for (int i = 0; i < steps; i++)
				{
					token.ThrowIfCancellationRequested();

					long currentScore = start + stepValue * (i + 1);
					gameScene.ScorePanel.ScoreValue = currentScore;

					// Determine the level (starting from level 1).
					gameScene.LevelPanel.LevelValue = (int)(currentScore / 1000) + 1;

					await Task.Delay(150, token);
				}
			}
			catch (OperationCanceledException)
			{
				// Safely end the animation.
			}
		}

		/// <summary>
		/// Animates the lines panel by incrementing the number of cleared lines.
		/// </summary>
		/// <param name="start">The starting number of lines.</param>
		/// <param name="end">The final number of lines.</param>
		/// <param name="steps">The number of animation steps.</param>
		/// <param name="token">The cancellation token to cancel the animation.</param>
		private async Task AnimateLines(int start, int end, int steps, CancellationToken token)
		{
			double diff = end - start;
			try
			{
				for (int i = 0; i <= steps; i++)
				{
					token.ThrowIfCancellationRequested();

					int currentValue = start + (int)Math.Round(diff * i / steps);
					gameScene.LinesCountPanel.LinesValue = currentValue;

					await Task.Delay(150, token);
				}
			}
			catch (OperationCanceledException)
			{
				// Safely end the animation.
			}
		}

		/// <summary>
		/// Animates the "Next Shape" panel by displaying a sequence of Tetromino shapes.
		/// </summary>
		/// <param name="token">The cancellation token to cancel the animation.</param>
		private async Task NextShapeAnimation(CancellationToken token)
		{
			TetronimoShape[] shapeSequence =
			{
				TetronimoShape.ShapeI,
				TetronimoShape.ShapeJ,
				TetronimoShape.ShapeL,
				TetronimoShape.ShapeO,
				TetronimoShape.ShapeS,
				TetronimoShape.ShapeZ,
				TetronimoShape.ShapeT
			};

			try
			{
				foreach (var shape in shapeSequence)
				{
					token.ThrowIfCancellationRequested();
					gameScene.NextShapePanel.TetronimoShape = shape;
					await Task.Delay(1000, token);
				}
			}
			catch (OperationCanceledException)
			{
				// Safely end the animation.
			}
		}

		/// <summary>
		/// Performs a grid animation by showing different shapes and their movements on the main grid.
		/// </summary>
		/// <param name="token">The cancellation token to cancel the animation.</param>
		private async Task GridAnimation(CancellationToken token)
		{
			var delay = 1000;
			void CheckCancel()
			{
				token.ThrowIfCancellationRequested();
			}

			try
			{
				CheckCancel();
				gameScene.Shapes.Add(new ShapeI());
				await Task.Delay(delay, token);

				CheckCancel();
				gameScene.Shapes[0] = new ShapeJ();
				await Task.Delay(delay, token);

				gameScene.Shapes[0] = new ShapeL();
				await Task.Delay(delay, token);

				gameScene.Shapes[0] = new ShapeO();
				await Task.Delay(delay, token);

				gameScene.Shapes[0] = new ShapeS();
				await Task.Delay(delay, token);

				gameScene.Shapes[0] = new ShapeZ();
				await Task.Delay(delay, token);

				gameScene.Shapes[0] = new ShapeT();
				await Task.Delay(delay, token);

				while (gameScene.Shapes.Count > 0 && gameScene.Shapes[0].Blocks.All(b => b.Y < 19))
				{
					CheckCancel();
					gameScene.Shapes[0].Move(MoveDirection.Down);
					await Task.Delay(100, token);
				}

				CheckCancel();
				gameScene.Shapes.Add(new ShapeO());
				await Task.Delay(500, token);

				int i = 1;
				while (gameScene.Shapes.Count > 1 && gameScene.Shapes[1].Blocks.All(b => b.Y < 19))
				{
					CheckCancel();
					gameScene.Shapes[1].Move(MoveDirection.Down);
					await Task.Delay(100, token);
					if (i % 4 == 0)
					{
						gameScene.Shapes[1].Move(MoveDirection.Left);
						await Task.Delay(100, token);
					}
					i++;
				}

				CheckCancel();
				await Task.Delay(500, token);
				gameScene.Shapes.Add(new ShapeZ());
				await Task.Delay(250, token);

				i = 1;
				while (gameScene.Shapes.Count > 2 && gameScene.Shapes[2].Blocks.All(b => b.Y < 19))
				{
					CheckCancel();
					gameScene.Shapes[2].Move(MoveDirection.Down);
					await Task.Delay(100, token);
					if (i == 5)
					{
						for (var j = 0; j < 3; j++)
						{
							CheckCancel();
							gameScene.Shapes[2].Rotate();
							await Task.Delay(500, token);
						}
					}
					if (i == 8)
					{
						gameScene.Shapes[2].Move(MoveDirection.Left);
						await Task.Delay(100, token);
					}
					i++;
				}

				CheckCancel();
				await Task.Delay(500, token);
				gameScene.Shapes.Add(new ShapeI());
				await Task.Delay(250, token);

				i = 1;
				while (gameScene.Shapes.Count > 3 && gameScene.Shapes[3].Blocks.All(b => b.Y < 19))
				{
					CheckCancel();
					gameScene.Shapes[3].Move(MoveDirection.Down);
					await Task.Delay(100, token);
					if (i % 5 == 0)
					{
						gameScene.Shapes[3].Move(MoveDirection.Right);
						await Task.Delay(100, token);
					}
					i++;
				}

				CheckCancel();
				gameScene.Shapes.Add(new ShapeFullLine(new Point(0, 19)));
				for (var j = 0; j < 3; j++)
				{
					CheckCancel();
					await Task.Delay(150, token);
					gameScene.Shapes[4].FillColor = Color.Gray;
					await Task.Delay(150, token);
					gameScene.Shapes[4].FillColor = Color.White;
				}

				foreach (var shape in gameScene.Shapes.ToList())
				{
					for (int j = shape.Blocks.Count - 1; j >= 0; j--)
					{
						if (shape.Blocks[j].Y == 19)
						{
							shape.RemoveBlock(shape.Blocks[j]);
						}
					}
				}

				foreach (var shape in gameScene.Shapes)
				{
					shape.IsActive = false;
					shape.Move(MoveDirection.Down);
				}
			}
			catch (OperationCanceledException)
			{
				// Safely end the animation.
			}
		}

		/// <summary>
		/// Animates the border (outline) of the specified panel by toggling its color.
		/// The border changes to red and then back to the default color defined in the theme.
		/// </summary>
		/// <param name="panel">The panel whose border will be animated.</param>
		/// <param name="token">The cancellation token to cancel the animation.</param>
		private async Task BlinkingObw(Panel panel, CancellationToken token)
		{
			var defaultColor = ColorTheme.GameTheme.PanelBorderColor;
			var shiningColor = Color.Red;

			try
			{
				while (true)
				{
					token.ThrowIfCancellationRequested();
					panel.ObwColor = shiningColor;
					await Task.Delay(500, token);

					token.ThrowIfCancellationRequested();
					panel.ObwColor = defaultColor;
					await Task.Delay(500, token);
				}
			}
			catch (OperationCanceledException)
			{
				// Safely end the animation.
			}
		}
	}
}
