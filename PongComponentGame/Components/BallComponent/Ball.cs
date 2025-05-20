using System;
using System.Drawing;
using System.Text.Json;

namespace PongComponentGame.Components.BallComponent
{
    // Komponent: Ball
    // Odpowiada za logikę piłki w grze
    public class Ball : IBall
    {
        // ======= Prywatne zmienne =======
        private int _x;
        private int _y;
        private int _size;
        private float _speedX;
        private float _speedY;
        private float _deceleration;

        // ======= Wartości domyślne =======
        private const int DefaultSize = 20;
        private const float DefaultSpeed = 8f;
        private const float DefaultDeceleration = 0.5f;

        // ======= Właściwości (gettery/settery) =======
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Size { get => _size; set => _size = value; }
        public float SpeedX { get => _speedX; set => _speedX = value; }
        public float SpeedY { get => _speedY; set => _speedY = value; }
        public float Deceleration { get => _deceleration; set => _deceleration = value; }

        // ======= Konstruktor bezargumentowy =======
        public Ball()
        {
            _size = DefaultSize;
            _speedX = DefaultSpeed;
            _speedY = 0;
            _deceleration = DefaultDeceleration;
        }

        // ======= Konstruktor z resetem pozycji =======
        public Ball(int windowWidth, int windowHeight) : this()
        {
            Reset(windowWidth, windowHeight);
        }

        // ======= Metoda ruchu =======
        public void Move()
        {
            _x += (int)_speedX;
            _y += (int)_speedY;
        }

        // ======= Metoda rysowania =======
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.White, _x, _y, _size, _size);
        }

        // ======= Reset pozycji i prędkości =======
        public void Reset(int width, int height)
        {
            Random rand = new Random();
            _x = width / 2 - _size / 2;
            _y = height / 2 - _size / 2;
            _speedX = (float)(rand.NextDouble() > 0.5 ? 1 : -1) * DefaultSpeed;
            _speedY = (float)(rand.NextDouble() * 4 - 2);
        }

        // ======= Spowolnienie po odbiciu =======
        public void Decelerate()
        {
            float newSpeedX = _speedX * (1 - _deceleration / 10);
            float newSpeedY = _speedY * (1 - _deceleration / 10);

            _speedX = Math.Abs(newSpeedX) >= 2 ? newSpeedX : Math.Sign(_speedX) * 2;
            _speedY = Math.Abs(newSpeedY) >= 2 ? newSpeedY : Math.Sign(_speedY) * 2;
        }

        // ======= Serializacja do JSON =======
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Ball Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Ball>(json);
        }
    }
}