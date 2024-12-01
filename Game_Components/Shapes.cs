using Microsoft.Xna.Framework;

namespace Tetris.Game_Components
{
    public enum Tetronimo_Shape
    {
        None,
        ShapeI,
        ShapeO,
        ShapeS,
        ShapeZ,
        ShapeL,
        ShapeJ,
        ShapeT
    }

    //Implementacja Inicjalizacji i Obracania kształtów Tetrisa: DZIAŁA

    public class ShapeI : Tetronimo
    {

        public ShapeI()
            : base()
        {
            start_position = new Point(3, 0);
            Initialize_Shape(start_position);
        }

        public ShapeI(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {
            fill_color = Color.Cyan;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y));
            blocks.Add(new Point(start_position.X + 2, start_position.Y));
            blocks.Add(new Point(start_position.X + 3, start_position.Y));

            list_pivot_element = 2;
            pivot = blocks[list_pivot_element];
        }

        public override void Rotate()
        {
            if (step == RotationStep.Step0 || step == RotationStep.Step180)
            {
                base.Rotate();
            }
            else if (step == RotationStep.Step90 || step == RotationStep.Step270)
            {

                for (var i = 0; i < blocks.Count; i++)
                {
                    //Obliczenie współrzędnych punktu względem Pivot (środka kształtu)
                    var newX = blocks[i].X - pivot.X;
                    var newY = blocks[i].Y - pivot.Y;

                    //Obrót względem Pivot
                    var rotatedX = newY;
                    var rotatedY = -newX;

                    //Przesunięcie punktu z powrotem
                    blocks[i] = new Point(pivot.X + rotatedX, pivot.Y + rotatedY);
                }

                Limes_Grid_Possitioner(); //Ustawienie kształtu względem planszy jeśli ten wychodzi poza nią po obrocie

                step = (RotationStep)(((int)step + 1) % 4); //Zmiana kroku obrotu
            }
        }

    }

    public class ShapeO : Tetronimo
    {
        public ShapeO()
            : base()
        {
            start_position = new Point(4, 0);
            Initialize_Shape(start_position);
        }

        public ShapeO(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {

            fill_color = Color.Yellow;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y));
            blocks.Add(new Point(start_position.X, start_position.Y + 1));
            blocks.Add(new Point(start_position.X + 1, start_position.Y + 1));

        }

        public override void Rotate()
        {
            //Nie obracaj
        }

    }

    public class ShapeS : Tetronimo
    {
        public ShapeS()
            : base()
        {
            start_position = new Point(3, 1);
            Initialize_Shape(start_position);
        }

        public ShapeS(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {
            fill_color = Color.Red;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y - 1));
            blocks.Add(new Point(start_position.X + 2, start_position.Y - 1));

            list_pivot_element = 1;
            pivot = blocks[list_pivot_element];
        }


    }

    public class ShapeZ : Tetronimo
    {
        public ShapeZ()
            : base()
        {
            start_position = new Point(3, 0);
            Initialize_Shape(start_position);
        }

        public ShapeZ(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {
            fill_color = Color.LawnGreen;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y + 1));
            blocks.Add(new Point(start_position.X + 2, start_position.Y + 1));

            list_pivot_element = 2;
            pivot = blocks[list_pivot_element];
        }


    }

    public class ShapeL : Tetronimo
    {
        public ShapeL()
            : base()
        {
            start_position = new Point(3, 1);
            Initialize_Shape(start_position);
        }

        public ShapeL(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {
            fill_color = Color.Orange;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X + 1, start_position.Y));
            blocks.Add(new Point(start_position.X + 2, start_position.Y));
            blocks.Add(new Point(start_position.X + 2, start_position.Y - 1));

            list_pivot_element = 1;
            pivot = blocks[list_pivot_element];
        }


    }

    public class ShapeJ : Tetronimo
    {
        public ShapeJ()
            : base()
        {
            start_position = new Point(3, 0);
            Initialize_Shape(start_position);
        }

        public ShapeJ(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {
            fill_color = Color.HotPink;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X, start_position.Y + 1));
            blocks.Add(new Point(start_position.X + 1, start_position.Y + 1));
            blocks.Add(new Point(start_position.X + 2, start_position.Y + 1));

            list_pivot_element = 2;
            pivot = blocks[list_pivot_element];
        }

    }

    public class ShapeT : Tetronimo
    {
        public ShapeT()
            : base()
        {
            start_position = new Point(4, 0);
            Initialize_Shape(start_position);
        }

        public ShapeT(Point start_position)
            : base()
        {
            this.start_position = start_position;
            Initialize_Shape(start_position);
        }

        public override void Initialize_Shape(Point start_position)
        {
            fill_color = Color.MediumPurple;
            obw_Color = Color.White;

            blocks.Add(new Point(start_position.X, start_position.Y));
            blocks.Add(new Point(start_position.X - 1, start_position.Y + 1));
            blocks.Add(new Point(start_position.X, start_position.Y + 1));
            blocks.Add(new Point(start_position.X + 1, start_position.Y + 1));

            list_pivot_element = 2;
            pivot = blocks[list_pivot_element];
        }


    }
}
