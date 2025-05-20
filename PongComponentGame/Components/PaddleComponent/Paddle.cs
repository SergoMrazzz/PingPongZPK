using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongComponentGame.Components.PaddleComponent
{
    // Abstrakcyjny komponent Paddle
    public abstract class Paddle : IPaddle
    {
        // ======= Prywatne zmienne =======
        private int _x;
        private int _y;
        private int _height;
        private int _speed;
        private const int _width = 15;

        // ======= Właściwości =======
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Height { get => _height; set => _height = value; }
        public int Width => _width;
        public int Speed { get => _speed; set => _speed = value; }

        // ======= Konstruktor bezargumentowy =======
        public Paddle()
        {
            _x = 0;
            _y = 0;
            _height = 100;
            _speed = 8;
        }

        // ======= Metoda rysująca =======
        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.White, _x, _y, _width, _height);
        }

        // ======= Ruch w górę/dół z limitem ekranu =======
        public virtual void Move(int screenHeight)
        {
            _y = Math.Max(0, Math.Min(screenHeight - _height, _y));
        }

        // ======= Obsługa klawiszy (abstrakcyjna) =======
        public abstract void HandleInput(Keys key, bool isPressed);
    }
}