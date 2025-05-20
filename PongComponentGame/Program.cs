using System;
using System.Windows.Forms;
using PongComponentGame.Components.ConfigComponent;
using PongComponentGame.Forms;

namespace PongComponentGame
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    var config = settingsForm.GetConfig();
                    Application.Run(new GameForm(config));
                    // tylko do testów:
                    ConfigTests.RunAll();

                }
            }
        }
    }
}