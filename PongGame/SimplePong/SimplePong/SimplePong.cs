using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimplePong
{
    public class Form1 : Form
    {
        Timer timer = new Timer();
        Rectangle ball;
        Rectangle paddle1, paddle2;
        int ballSpeedX = 5, ballSpeedY = 3;
        int paddleSpeed = 8;
        int paddle1Score = 0, paddle2Score = 0;
        bool up1, down1, up2, down2;

        public Form1()
        {
            this.Text = "Simple Pong";
            this.ClientSize = new Size(800, 600);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            ball = new Rectangle(390, 290, 20, 20);
            paddle1 = new Rectangle(20, 250, 15, 100);
            paddle2 = new Rectangle(765, 250, 15, 100);

            timer.Interval = 16;
            timer.Tick += GameLoop;
            timer.Start();

            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Ruch piłki
            ball.X += ballSpeedX;
            ball.Y += ballSpeedY;

            // Odbicia od góry/dół
            if (ball.Y <= 0 || ball.Y + ball.Height >= this.ClientSize.Height)
                ballSpeedY = -ballSpeedY;

            // Ruch paletek
            if (up1 && paddle1.Y > 0) paddle1.Y -= paddleSpeed;
            if (down1 && paddle1.Y + paddle1.Height < this.ClientSize.Height) paddle1.Y += paddleSpeed;
            if (up2 && paddle2.Y > 0) paddle2.Y -= paddleSpeed;
            if (down2 && paddle2.Y + paddle2.Height < this.ClientSize.Height) paddle2.Y += paddleSpeed;

            // Odbicie od paletki 1
            if (ball.IntersectsWith(paddle1))
                ballSpeedX = Math.Abs(ballSpeedX);

            // Odbicie od paletki 2
            if (ball.IntersectsWith(paddle2))
                ballSpeedX = -Math.Abs(ballSpeedX);

            // Punkt dla gracza
            if (ball.X < 0)
            {
                paddle2Score++;
                ResetBall();
            }
            if (ball.X > this.ClientSize.Width)
            {
                paddle1Score++;
                ResetBall();
            }

            this.Invalidate();
        }

        private void ResetBall()
        {
            ball.X = 390;
            ball.Y = 290;
            Random rand = new Random();
            ballSpeedX = rand.Next(0, 2) == 0 ? 5 : -5;
            ballSpeedY = rand.Next(-3, 4);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, ball);
            g.FillRectangle(Brushes.White, paddle1);
            g.FillRectangle(Brushes.White, paddle2);
            g.DrawString($"Gracz 1: {paddle1Score}", new Font("Arial", 14), Brushes.White, 20, 20);
            g.DrawString($"Gracz 2: {paddle2Score}", new Font("Arial", 14), Brushes.White, 650, 20);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) up1 = true;
            if (e.KeyCode == Keys.S) down1 = true;
            if (e.KeyCode == Keys.Up) up2 = true;
            if (e.KeyCode == Keys.Down) down2 = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) up1 = false;
            if (e.KeyCode == Keys.S) down1 = false;
            if (e.KeyCode == Keys.Up) up2 = false;
            if (e.KeyCode == Keys.Down) down2 = false;
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
