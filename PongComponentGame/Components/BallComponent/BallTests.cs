using System;
using System.Diagnostics;

namespace PongComponentGame.Components.BallComponent
{
    // Prosta aplikacja testująca komponent Ball
    public static class BallTests
    {
        public static void RunAll()
        {
            Console.WriteLine("[TEST] Rozpoczynam test komponentu Ball...");
            Ball ball = new Ball(800, 600);
            Debug.Assert(ball.Size == 20);
            Debug.Assert(ball.Deceleration == 0.5f);

            int startX = ball.X;
            ball.Move();
            Debug.Assert(ball.X != startX, "Piłka powinna się przesunąć po wywołaniu Move()");

            string json = ball.Serialize();
            Ball loaded = Ball.Deserialize(json);
            Debug.Assert(loaded.Size == ball.Size, "Serializacja działa niepoprawnie");

            Console.WriteLine("[TEST] Wszystkie testy Ball zakończone sukcesem!");
        }
    }
}