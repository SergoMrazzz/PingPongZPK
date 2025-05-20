using System.Drawing;

namespace PongComponentGame.Components.ScoreComponent
{
    public interface IScore
    {
        int LeftScore { get; }
        int RightScore { get; }
        int BestScore { get; }
        string BestScorePlayer { get; }
        string Player1Name { get; set; }
        string Player2Name { get; set; }

        void AddPoint(bool isLeft);
        void Reset();
        void Draw(Graphics g, int screenWidth);
        void SaveToFile(string path);
        void LoadFromFile(string path);
    }
}