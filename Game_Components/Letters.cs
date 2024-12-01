using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Tetris.Game_Components
{
    public abstract class Letters
    {
        private Point letter_position;
        private Color letter_color;
        private Color letter_border_color;
        private List<Point> letter_shape;

        public Point Letter_Position { get => letter_position; set => letter_position = value; }
        public Color Letter_Color { get => letter_color; set => letter_color = value; }
        public Color Letter_Border_Color { get => letter_border_color; set => letter_border_color = value; }
        public List<Point> Letter_Shape { get => letter_shape; set => letter_shape = value; }

        public Letters(Point letter_position, Color letter_color, Color letter_border_color)
        {
            this.letter_position = letter_position;
            this.letter_color = letter_color;
            this.letter_border_color = letter_border_color;
            letter_shape = new List<Point>();
            letter_shape.Clear();
        }
    }

    public class Letter_T : Letters
    {
        public Letter_T(Point letter_position, Color letter_color, Color letter_border_color) : base(letter_position, letter_color, letter_border_color)
        {
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 1));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 3));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 4));
        }
    }

    public class Letter_E : Letters
    {
        public Letter_E(Point letter_position, Color letter_color, Color letter_border_color) : base(letter_position, letter_color, letter_border_color)
        {
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 1));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 3));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 4));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 4));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 4));
        }
    }

    public class Letter_R : Letters
    {
        public Letter_R(Point letter_position, Color letter_color, Color letter_border_color) : base(letter_position, letter_color, letter_border_color)
        {
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 1));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 3));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 4));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 1));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 3));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 4));
        }
    }

    public class Letter_I : Letters
    {
        public Letter_I(Point letter_position, Color letter_color, Color letter_border_color) : base(letter_position, letter_color, letter_border_color)
        {
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 1));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 3));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 4));
        }
    }

    public class Letter_S : Letters
    {
        public Letter_S(Point letter_position, Color letter_color, Color letter_border_color) : base(letter_position, letter_color, letter_border_color)
        {
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 1));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 2));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 3));
            Letter_Shape.Add(new Point(letter_position.X + 1, letter_position.Y + 4));
            Letter_Shape.Add(new Point(letter_position.X + 2, letter_position.Y + 4));
            Letter_Shape.Add(new Point(letter_position.X, letter_position.Y + 4));
        }
    }

}
