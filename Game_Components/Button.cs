using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tetris.Game_Components
{
    /// <summary>
    /// Specifies the various states a button can be in.
    /// </summary>
    public enum Button_States
    {
        /// <summary>
        /// The button is active.
        /// </summary>
        Active,

        /// <summary>
        /// The button is inactive.
        /// </summary>
        Inactive,

        /// <summary>
        /// The button is active and the mouse is hovering over it.
        /// </summary>
        ActiveHover,

        /// <summary>
        /// The button is active and has been clicked.
        /// </summary>
        ActiveClicked
    }

    /// <summary>
    /// Represents a UI button that extends the Panel class.
    /// The button handles rendering, updating, and processing mouse interactions (hover, click)
    /// and triggers an assigned action when clicked.
    /// </summary>
    public class Button : Panel
    {
        #region Private Fields

        /// <summary>
        /// The SpriteBatch used for drawing graphics.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The font used for rendering the button text.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The background of the button.
        /// </summary>
        private readonly Background background;

        /// <summary>
        /// The texture representing the button's image.
        /// </summary>
        private Texture2D buttonImage;

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        private MouseState mouseState;

        /// <summary>
        /// The previous state of the mouse.
        /// </summary>
        private MouseState previousMouseState;

        /// <summary>
        /// A cache of colors for the button based on its state.
        /// </summary>
        private Dictionary<Button_States, (Color primaryColor, Color secondaryColor)> colorsCache = new();

        /// <summary>
        /// The color used for the active state of the button.
        /// </summary>
        private Color activeColor;

        /// <summary>
        /// The color used for the inactive state of the button.
        /// </summary>
        private Color inactiveColor;

        /// <summary>
        /// The text displayed on the button.
        /// </summary>
        private string buttonText;

        /// <summary>
        /// The file path to the button's image.
        /// </summary>
        private string buttonGraphicath;

        /// <summary>
        /// The action to be executed when the button is clicked.
        /// </summary>
        private Action onClick;

        /// <summary>
        /// The current state of the button.
        /// </summary>
        private Button_States buttonStates;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the action executed when the button is clicked.
        /// </summary>
        public Action OnClick { get => onClick; set => onClick = value; }

        /// <summary>
        /// Gets or sets the current state of the button.
        /// </summary>
        public Button_States ButtonStates { get => buttonStates; set => buttonStates = value; }

        /// <summary>
        /// Gets or sets the text displayed on the button.
        /// </summary>
        public string ButtonText { get => buttonText; set => buttonText = value; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with the specified position, size, text, and an optional image path.
        /// </summary>
        /// <param name="position">The position of the button.</param>
        /// <param name="width">The width of the button.</param>
        /// <param name="height">The height of the button.</param>
        /// <param name="buttonText">The text to display on the button. Defaults to "Button".</param>
        /// <param name="buttonGraphicPath">The file path to the button's image (optional).</param>
        public Button(Point position, int width, int height, string buttonText = "Button", string buttonGraphicPath = null)
            : base(position, width, height)
        {
            this.buttonText = buttonText;
            this.buttonGraphicath = buttonGraphicPath;
            background = new Background(position, width, height);

            // Set the default state of the button to active.
            buttonStates = Button_States.Active;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the content required by the button, including fonts, images, and background.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used for drawing graphics.</param>
        /// <param name="content">The ContentManager used for loading assets.</param>
        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            // Load the base panel content.
            LoadContent(spriteBatch);

            // Load the button background.
            background.LoadContent(spriteBatch);

            // Load the button font.
            font = content.Load<SpriteFont>("Fonts/Button_Font");

            // Load the button image if the file path is provided.
            if (buttonGraphicath != null)
            {
                buttonImage = content.Load<Texture2D>(buttonGraphicath);
            }

            // Set button colors from the current theme.
            activeColor = ColorTheme.GameTheme.ButtonActiveColor;
            inactiveColor = ColorTheme.GameTheme.ButtonInactiveColor;
            ObwColor = ColorTheme.GameTheme.PanelTransparentBorderColor; // Border color inherited from Panel.

            // Initialize the cache of button colors based on its state.
            InitializeCache();
        }

        /// <summary>
        /// Updates the button by updating its background and processing mouse interactions.
        /// </summary>
        public void Update()
        {
            // Update the button background's position and size.
            background.StartPoint = Position;
            background.Width = Width;
            background.Height = Height;

            // Process mouse input and update the button state.
            SetButtonStates();
        }

        /// <summary>
        /// Draws the button, including its border, background, and content (text and optional image).
        /// </summary>
        public new void Draw()
        {
            spriteBatch.Begin();

            // Draw the button border.
            Rectangle obw = new Rectangle(Position.X - 3, Position.Y - 3, Width + 6, Height + 6);
            spriteBatch.Draw(Pixel, obw, ObwColor);

            spriteBatch.End();

            // Retrieve colors for the current button state from the cache.
            var colors = colorsCache[buttonStates];
            background.PrimaryColor = colors.primaryColor;
            background.SecondaryColor = colors.secondaryColor;
            background.DrawBackground();

            // Draw the button content (text and image).
            DrawButtonContent();
        }

        /// <summary>
        /// Updates the button's theme by refreshing its active/inactive colors and reinitializing the color cache.
        /// </summary>
        public new void UpdateTheme()
        {
            activeColor = ColorTheme.GameTheme.ButtonActiveColor;
            inactiveColor = ColorTheme.GameTheme.ButtonInactiveColor;
            InitializeCache();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the cache of gradient colors for the button for each <see cref="Button_States"/>.
        /// </summary>
        private void InitializeCache()
        {
            foreach (Button_States state in Enum.GetValues(typeof(Button_States)))
            {
                colorsCache[state] = GetGradientsFillColors(state, activeColor, inactiveColor);
            }
        }

        /// <summary>
        /// Computes the gradient fill colors for the button based on its state.
        /// </summary>
        /// <param name="buttonState">The current state of the button.</param>
        /// <param name="activeColor">The active color of the button.</param>
        /// <param name="inactiveColor">The inactive color of the button.</param>
        /// <returns>A tuple containing the primary and secondary colors for the button.</returns>
        private static (Color primaryColor, Color secondaryColor) GetGradientsFillColors(Button_States buttonState, Color activeColor, Color inactiveColor)
        {
            Color LighterColor(Color color, float amount)
            {
                float r = color.R + (255 - color.R) * amount;
                float g = color.G + (255 - color.G) * amount;
                float b = color.B + (255 - color.B) * amount;
                return new Color((int)r, (int)g, (int)b);
            }

            Color DarkenColor(Color color, float amount)
            {
                float r = color.R * (1 - amount);
                float g = color.G * (1 - amount);
                float b = color.B * (1 - amount);
                return new Color((int)r, (int)g, (int)b);
            }

            var secondaryActiveColor = LighterColor(activeColor, 0.75f);
            var secondaryInactiveColor = LighterColor(inactiveColor, 0.5f);
            var hoverPrimaryColor = LighterColor(activeColor, 0.2f);
            var hoverSecondaryColor = LighterColor(secondaryActiveColor, 0.8f);
            var clickedPrimaryColor = DarkenColor(activeColor, 0.2f);
            var clickedSecondaryColor = DarkenColor(secondaryActiveColor, 0.2f);

            switch (buttonState)
            {
                case Button_States.Active:
                    return (activeColor, secondaryActiveColor);
                case Button_States.Inactive:
                    return (inactiveColor, secondaryInactiveColor);
                case Button_States.ActiveHover:
                    return (hoverPrimaryColor, hoverSecondaryColor);
                case Button_States.ActiveClicked:
                    return (clickedPrimaryColor, clickedSecondaryColor);
                default:
                    return (Color.Transparent, Color.Transparent);
            }
        }

        /// <summary>
        /// Processes mouse input and sets the button's state accordingly.
        /// </summary>
        private void SetButtonStates()
        {
            // Save the previous mouse state.
            previousMouseState = mouseState;
            // Get the current mouse state.
            mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            // Check if the mouse is over the button.
            var isMouseOver = new Rectangle(Position.X, Position.Y, Width, Height).Contains(mousePosition);

            if (isMouseOver && (buttonStates == Button_States.Active || buttonStates == Button_States.ActiveHover))
            {
                buttonStates = Button_States.ActiveHover;
                // Check for left mouse click.
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    buttonStates = Button_States.ActiveClicked;
                    onClick?.Invoke();               // Invoke the click action.
                    SoundManager.SoundButton.Play(); // Play the button sound.
                }
            }
            else if (buttonStates == Button_States.ActiveClicked && mouseState.LeftButton == ButtonState.Released)
            {
                buttonStates = Button_States.Active;
            }
            else if (!isMouseOver && buttonStates != Button_States.Inactive)
            {
                buttonStates = Button_States.Active;
            }
        }

        /// <summary>
        /// Draws the button content, including its text and optional image.
        /// </summary>
        private void DrawButtonContent()
        {
            const int padding = 15;  // Padding around the button image.
            int buttonGraphicSize = Height - padding;
            Vector2 buttonTextPosition = new Vector2(TextXPositioner(buttonText, font), TextYPositioner(buttonText, font));
            spriteBatch.Begin();

            if (buttonImage == null)
            {
                spriteBatch.DrawString(font, buttonText, buttonTextPosition, Color.White);
            }
            else
            {
                // Calculate text position with an offset for the image.
                float textOffsetX = buttonTextPosition.X - buttonGraphicSize / 2;
                spriteBatch.DrawString(font, buttonText, new Vector2(textOffsetX, buttonTextPosition.Y), Color.White);

                // Calculate the position for drawing the image on the right side of the text.
                float imagePositionX = textOffsetX + font.MeasureString(buttonText).X;
                spriteBatch.Draw(buttonImage, new Rectangle((int)imagePositionX, Position.Y + padding / 2, buttonGraphicSize, buttonGraphicSize), Color.White);
            }

            spriteBatch.End();
        }

        #endregion
    }
}
