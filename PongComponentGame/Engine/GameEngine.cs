using System;
using System.Drawing;
using System.Windows.Forms;
using PongComponentGame.Components.BallComponent;
using PongComponentGame.Components.PaddleComponent;
using PongComponentGame.Components.ScoreComponent;
using PongComponentGame.Components.ConfigComponent;

namespace PongComponentGame.Engine
{
    // Silnik gry - łączy komponenty
    public class GameEngine
    {
        private readonly IBall _ball;
        private readonly IPaddle _leftPaddle;
        private readonly IPaddle _rightPaddle;
        private readonly IScore _scoreSystem;
        private readonly IConfigurable _config;

        public event Action GameOver;

        public GameEngine(IConfigurable config)
        {
            _config = config;
            _ball = new Ball(_config.WindowWidth, _config.WindowHeight);
            _scoreSystem = new ScoreSystem
            {
                Player1Name = config.Player1Name,
                Player2Name = config.Player2Name
            };

            _leftPaddle = new HumanPaddle(config.Player1UpKey, config.Player1DownKey, 20);

            if (config.IsAgainstAI)
            {
                _rightPaddle = new AIPaddle(2, config.WindowWidth - 35);
            }
            else
            {
                _rightPaddle = new HumanPaddle(config.Player2UpKey, config.Player2DownKey, config.WindowWidth - 35);
            }
        }

        // Aktualizacja logiki gry
        public void Update()
        {
            _ball.Move();
            _leftPaddle.Move(_config.WindowHeight);
            _rightPaddle.Move(_config.WindowHeight);

            if (_rightPaddle is AIPaddle ai)
                ai.Update(_ball, _config.WindowHeight);

            CheckCollisions();
            CheckBoundaries();
        }

        // Rysowanie komponentów
        public void Draw(Graphics g)
        {
            _ball.Draw(g);
            _leftPaddle.Draw(g);
            _rightPaddle.Draw(g);
            _scoreSystem.Draw(g, _config.WindowWidth);
        }

        // Obsługa klawiatury
        public void HandleKey(Keys key, bool isPressed)
        {
            _leftPaddle.HandleInput(key, isPressed);
            if (!_config.IsAgainstAI)
                _rightPaddle.HandleInput(key, isPressed);
        }

        private void CheckCollisions()
        {
            if (_ball.Y <= 0 || _ball.Y + _ball.Size >= _config.WindowHeight)
                _ball.SpeedY = -_ball.SpeedY;

            if (_ball.X <= _leftPaddle.X + _leftPaddle.Width &&
                _ball.Y + _ball.Size >= _leftPaddle.Y &&
                _ball.Y <= _leftPaddle.Y + _leftPaddle.Height)
            {
                float hitPoint = (_ball.Y + _ball.Size / 2 - _leftPaddle.Y) / (float)_leftPaddle.Height;
                float angle = (hitPoint - 0.5f) * (float)Math.PI / 2;
                _ball.SpeedX = Math.Abs(_ball.SpeedX) * (float)Math.Cos(angle);
                _ball.SpeedY = Math.Abs(_ball.SpeedX) * (float)Math.Sin(angle);
                _ball.Decelerate();
            }

            if (_ball.X + _ball.Size >= _rightPaddle.X &&
                _ball.Y + _ball.Size >= _rightPaddle.Y &&
                _ball.Y <= _rightPaddle.Y + _rightPaddle.Height)
            {
                float hitPoint = (_ball.Y + _ball.Size / 2 - _rightPaddle.Y) / (float)_rightPaddle.Height;
                float angle = (hitPoint - 0.5f) * (float)Math.PI / 2;
                _ball.SpeedX = -Math.Abs(_ball.SpeedX) * (float)Math.Cos(angle);
                _ball.SpeedY = Math.Abs(_ball.SpeedX) * (float)Math.Sin(angle);
                _ball.Decelerate();
            }
        }

        private void CheckBoundaries()
        {
            if (_ball.X < 0)
            {
                _scoreSystem.AddPoint(false);
                _ball.Reset(_config.WindowWidth, _config.WindowHeight);
            }
            else if (_ball.X > _config.WindowWidth)
            {
                _scoreSystem.AddPoint(true);
                _ball.Reset(_config.WindowWidth, _config.WindowHeight);
            }

            if (_scoreSystem.LeftScore >= _config.PointsToWin ||
                _scoreSystem.RightScore >= _config.PointsToWin)
            {
                _scoreSystem.SaveToFile("score.json");
                GameOver?.Invoke();
            }
        }

        public int GetLeftScore() => _scoreSystem.LeftScore;
        public int GetRightScore() => _scoreSystem.RightScore;
        public int GetBestScore() => _scoreSystem.BestScore;
        public string GetBestPlayer() => _scoreSystem.BestScorePlayer;
    }
}
