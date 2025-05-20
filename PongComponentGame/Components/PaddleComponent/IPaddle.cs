using System.Drawing;
using System.Windows.Forms;

namespace PongComponentGame.Components.PaddleComponent
{
    public interface IPaddle
    {
        int X { get; set; }
        int Y { get; set; }
        int Height { get; set; }
        int Width { get; }
        int Speed { get; set; }

        void Draw(Graphics g);
        void Move(int screenHeight);
        void HandleInput(Keys key, bool isPressed);
    }
}