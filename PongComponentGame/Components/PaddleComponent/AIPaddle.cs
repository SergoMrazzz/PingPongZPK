using System.Windows.Forms;
using PongComponentGame.Components.BallComponent;

namespace PongComponentGame.Components.PaddleComponent
{
    // Komponent: Paddle sterowany przez AI
    public class AIPaddle : Paddle
    {
        private int _difficulty; // 1: łatwy, 2: średni, 3: trudny

        public AIPaddle(int difficulty, int startX)
        {
            _difficulty = difficulty;
            X = startX;
        }

        public void Update(IBall ball, int screenHeight)
        {
            if (ball.Y + ball.Size / 2 < Y + Height / 2)
                Y -= Speed / _difficulty;
            else if (ball.Y + ball.Size / 2 > Y + Height / 2)
                Y += Speed / _difficulty;

            base.Move(screenHeight);
        }

        public override void HandleInput(Keys key, bool isPressed)
        {
            // AI nie używa klawiatury
        }
    }
}