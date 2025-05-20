using System;
using System.Diagnostics;
using System.IO;
using PongComponentGame.Components.ConfigComponent;

namespace PongComponentGame.Components.ConfigComponent
{
    public static class ConfigTests
    {
        public static void RunAll()
        {
            Console.WriteLine("[TEST] Rozpoczynam test komponentu GameConfig...");

            GameConfig config = new GameConfig();

            // Sprawdzenie wartości domyślnych
            Debug.Assert(config.WindowWidth == 800, "Domyślna szerokość okna powinna wynosić 800");
            Debug.Assert(config.WindowHeight == 600, "Domyślna wysokość okna powinna wynosić 600");
            Debug.Assert(config.BallSpeed == 8, "Domyślna prędkość piłki powinna wynosić 8");
            Debug.Assert(config.PointsToWin == 10, "Domyślna liczba punktów do wygranej to 10");
            Debug.Assert(config.IsAgainstAI == true, "Domyślnie gra powinna być przeciwko AI");

            // Customizacja
            config.Player1Name = "TestGracz1";
            config.Player2Name = "TestGracz2";
            config.BallSpeed = 12;
            config.IsAgainstAI = false;

            string path = "config_test.json";
            config.SaveToFile(path);

            GameConfig loaded = GameConfig.LoadFromFile(path);

            Debug.Assert(loaded.Player1Name == "TestGracz1", "Serializacja nie działa poprawnie (Player1Name)");
            Debug.Assert(loaded.Player2Name == "TestGracz2", "Serializacja nie działa poprawnie (Player2Name)");
            Debug.Assert(loaded.BallSpeed == 12, "Serializacja nie działa poprawnie (BallSpeed)");
            Debug.Assert(loaded.IsAgainstAI == false, "Serializacja nie działa poprawnie (IsAgainstAI)");

            if (File.Exists(path))
                File.Delete(path);

            Console.WriteLine("[TEST] Wszystkie testy GameConfig zakończone sukcesem!");
        }
    }
}
