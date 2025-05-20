using System.Windows.Forms;

namespace PongComponentGame.Components.ConfigComponent
{
    public interface IConfigurable
    {
        int WindowWidth { get; set; }
        int WindowHeight { get; set; }
        int BallSpeed { get; set; }
        int PointsToWin { get; set; }
        bool IsAgainstAI { get; set; }
        string Player1Name { get; set; }
        string Player2Name { get; set; }
        Keys Player1UpKey { get; set; }
        Keys Player1DownKey { get; set; }
        Keys Player2UpKey { get; set; }
        Keys Player2DownKey { get; set; }
    }
}