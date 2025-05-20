using System;
using System.Drawing;
using System.IO;
using System.Text.Json;

namespace PongComponentGame.Components.ScoreComponent
{
    // Komponent: System punktacji
    public class ScoreSystem : IScore
    {
        // ======= Prywatne zmienne =======
        private int _leftScore;
        private int _rightScore;
        private int _bestScore;
        private string _bestScorePlayer;
        private string _player1Name;
        private string _player2Name;

        // ======= Właściwości =======
        public int LeftScore => _leftScore;
        public int RightScore => _rightScore;
        public int BestScore => _bestScore;
        public string BestScorePlayer => _bestScorePlayer;
        public string Player1Name { get => _player1Name; set => _player1Name = value; }
        public string Player2Name { get => _player2Name; set => _player2Name = value; }

        // ======= Konstruktor =======
        public ScoreSystem()
        {
            _leftScore = 0;
            _rightScore = 0;
            _bestScore = 0;
            _bestScorePlayer = "Brak";
            _player1Name = "Gracz 1";
            _player2Name = "Gracz 2";
        }

        // ======= Dodawanie punktu =======
        public void AddPoint(bool isLeft)
        {
            if (isLeft) _leftScore++;
            else _rightScore++;
            UpdateBestScore();
        }

        // ======= Rysowanie wyniku =======
        public void Draw(Graphics g, int screenWidth)
        {
            var font = new Font("Arial", 16);
            string left = $"{_player1Name}: {_leftScore}";
            string right = $"{_player2Name}: {_rightScore}";
            var size1 = g.MeasureString(left, font);
            var size2 = g.MeasureString(right, font);
            g.DrawString(left, font, Brushes.White, screenWidth / 4 - size1.Width / 2, 20);
            g.DrawString(right, font, Brushes.White, 3 * screenWidth / 4 - size2.Width / 2, 20);
        }

        // ======= Reset punktów =======
        public void Reset()
        {
            _leftScore = 0;
            _rightScore = 0;
        }

        // ======= Sprawdzenie i aktualizacja najlepszego wyniku =======
        private void UpdateBestScore()
        {
            int max = Math.Max(_leftScore, _rightScore);
            string player = max == _leftScore ? _player1Name : _player2Name;
            if (max > _bestScore)
            {
                _bestScore = max;
                _bestScorePlayer = player;
            }
        }

        // ======= Serializacja (JSON do pliku) =======
        public void SaveToFile(string path)
        {
            try
            {
                var json = JsonSerializer.Serialize(this);
                File.WriteAllText(path, json);
            }
            catch { }
        }

        public void LoadFromFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    var loaded = JsonSerializer.Deserialize<ScoreSystem>(json);
                    _bestScore = loaded._bestScore;
                    _bestScorePlayer = loaded._bestScorePlayer;
                }
            }
            catch { }
        }
    }
}