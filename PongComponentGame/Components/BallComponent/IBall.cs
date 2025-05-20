using System.Drawing;

namespace PongComponentGame.Components.BallComponent
{
    public interface IBall
    {
        int X { get; set; }
        int Y { get; set; }
        int Size { get; set; }
        float SpeedX { get; set; }
        float SpeedY { get; set; }
        float Deceleration { get; set; }

        void Move();
        void Reset(int width, int height);
        void Decelerate();
        string Serialize();
        void Draw(Graphics g);
    }
}