using System.Text.Json;
using System.Windows.Forms;
using System.IO;

namespace PongComponentGame.Components.ConfigComponent
{
    // Komponent: Konfiguracja gry
    public class GameConfig : IConfigurable
    {
        // ======= Prywatne zmienne =======
        private int _windowWidth;
        private int _windowHeight;
        private int _ballSpeed;
        private int _pointsToWin;
        private bool _isAgainstAI;
        private string _player1Name;
        private string _player2Name;
        private Keys _player1UpKey;
        private Keys _player1DownKey;
        private Keys _player2UpKey;
        private Keys _player2DownKey;

        // ======= Wartości domyślne =======
        public GameConfig()
        {
            _windowWidth = 800;
            _windowHeight = 600;
            _ballSpeed = 8;
            _pointsToWin = 10;
            _isAgainstAI = true;
            _player1Name = "Gracz 1";
            _player2Name = "Gracz 2";
            _player1UpKey = Keys.W;
            _player1DownKey = Keys.S;
            _player2UpKey = Keys.Up;
            _player2DownKey = Keys.Down;
        }

        // ======= Właściwości =======
        public int WindowWidth { get => _windowWidth; set => _windowWidth = value; }
        public int WindowHeight { get => _windowHeight; set => _windowHeight = value; }
        public int BallSpeed { get => _ballSpeed; set => _ballSpeed = value; }
        public int PointsToWin { get => _pointsToWin; set => _pointsToWin = value; }
        public bool IsAgainstAI { get => _isAgainstAI; set => _isAgainstAI = value; }
        public string Player1Name { get => _player1Name; set => _player1Name = value; }
        public string Player2Name { get => _player2Name; set => _player2Name = value; }
        public Keys Player1UpKey { get => _player1UpKey; set => _player1UpKey = value; }
        public Keys Player1DownKey { get => _player1DownKey; set => _player1DownKey = value; }
        public Keys Player2UpKey { get => _player2UpKey; set => _player2UpKey = value; }
        public Keys Player2DownKey { get => _player2DownKey; set => _player2DownKey = value; }

        // ======= Serializacja =======
        public void SaveToFile(string path)
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(path, json);
        }

        public static GameConfig LoadFromFile(string path)
        {
            if (!File.Exists(path)) return new GameConfig();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<GameConfig>(json);
        }
    }
}
