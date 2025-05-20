using System;
using System.Drawing;
using System.Windows.Forms;
using PongComponentGame.Engine;
using PongComponentGame.Components.ConfigComponent;

namespace PongComponentGame.Forms
{
    public class GameForm : Form
    {
        private readonly GameEngine _engine;
        private readonly Timer _gameTimer;

        public GameForm(IConfigurable config)
        {
            _engine = new GameEngine(config);
            _engine.GameOver += OnGameOver;

            this.Text = "Pong - Komponentowa wersja";
            this.ClientSize = new Size(config.WindowWidth, config.WindowHeight);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            _gameTimer = new Timer { Interval = 16 };
            _gameTimer.Tick += (s, e) =>
            {
                _engine.Update();
                this.Invalidate();
            };
            _gameTimer.Start();
        }

        private void OnGameOver()
        {
            _gameTimer.Stop();
            MessageBox.Show($"Koniec gry!\nWynik: {_engine.GetLeftScore()} : {_engine.GetRightScore()}\nNajlepszy wynik: {_engine.GetBestPlayer()} - {_engine.GetBestScore()}",
                "Gra zakończona", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _engine.Draw(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _engine.HandleKey(e.KeyCode, true);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _engine.HandleKey(e.KeyCode, false);
        }
    }
}