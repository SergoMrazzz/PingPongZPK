using System;
using System.Diagnostics;

namespace PongComponentGame.Components.ScoreComponent
{
    public static class ScoreTests
    {
        public static void RunAll()
        {
            Console.WriteLine("[TEST] Test komponentu ScoreSystem");
            ScoreSystem score = new ScoreSystem();
            score.Player1Name = "A";
            score.Player2Name = "B";

            score.AddPoint(true);
            score.AddPoint(false);
            score.AddPoint(false);
            Debug.Assert(score.RightScore == 2);
            Debug.Assert(score.LeftScore == 1);

            score.SaveToFile("score.json");
            var loaded = new ScoreSystem();
            loaded.LoadFromFile("score.json");
            Debug.Assert(loaded.BestScore >= 2);

            Console.WriteLine("[TEST] Score zakończone sukcesem");
        }
    }
}