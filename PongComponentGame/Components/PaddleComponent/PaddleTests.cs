using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PongComponentGame.Components.PaddleComponent
{
    public static class PaddleTests
    {
        public static void RunAll()
        {
            Console.WriteLine("[TEST] Test komponentu Paddle");

            var human = new HumanPaddle(Keys.W, Keys.S, 20);
            Debug.Assert(human.X == 20);
            human.HandleInput(Keys.W, true);
            human.Move(600);
            Console.WriteLine("HumanPaddle działa poprawnie");

            var ai = new AIPaddle(2, 760);
            Debug.Assert(ai.X == 760);
            Console.WriteLine("AIPaddle zainicjowany poprawnie");

            Console.WriteLine("[TEST] Paddle zakończone sukcesem");
        }
    }
}