using System.Windows.Forms;

namespace PongComponentGame.Components.PaddleComponent
{
    // Komponent: Paddle sterowany przez gracza
    public class HumanPaddle : Paddle
    {
        private Keys _upKey;
        private Keys _downKey;
        private bool _moveUp;
        private bool _moveDown;

        public HumanPaddle(Keys up, Keys down, int startX)
        {
            _upKey = up;
            _downKey = down;
            X = startX;
        }

        public override void HandleInput(Keys key, bool isPressed)
        {
            if (key == _upKey) _moveUp = isPressed;
            if (key == _downKey) _moveDown = isPressed;
        }

        public override void Move(int screenHeight)
        {
            if (_moveUp) Y -= Speed;
            if (_moveDown) Y += Speed;
            base.Move(screenHeight);
        }
    }
}
