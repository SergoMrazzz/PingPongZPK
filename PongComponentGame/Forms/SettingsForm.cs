using System;
using System.Drawing;
using System.Windows.Forms;
using PongComponentGame.Components.ConfigComponent;

namespace PongComponentGame.Forms
{
    public class SettingsForm : Form
    {
        private TextBox player1Box, player2Box;
        private ComboBox speedBox, modeBox;
        private Button okButton, cancelButton;
        private GameConfig _config;

        public SettingsForm()
        {
            _config = new GameConfig();
            InitUI();
        }

        private void InitUI()
        {
            this.Text = "Ustawienia gry";
            this.ClientSize = new Size(280, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label p1Label = new Label { Text = "Gracz 1:", Location = new Point(10, 20), AutoSize = true };
            player1Box = new TextBox { Location = new Point(100, 18), Width = 150, Text = _config.Player1Name };

            Label p2Label = new Label { Text = "Gracz 2 / AI:", Location = new Point(10, 60), AutoSize = true };
            player2Box = new TextBox { Location = new Point(100, 58), Width = 150, Text = _config.Player2Name };

            Label speedLabel = new Label { Text = "Prędkość piłki:", Location = new Point(10, 100), AutoSize = true };
            speedBox = new ComboBox { Location = new Point(100, 98), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            speedBox.Items.AddRange(new string[] { "Wolna", "Średnia", "Szybka" });
            speedBox.SelectedIndex = 1;

            Label modeLabel = new Label { Text = "Tryb gry:", Location = new Point(10, 140), AutoSize = true };
            modeBox = new ComboBox { Location = new Point(100, 138), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            modeBox.Items.AddRange(new string[] { "Gracz vs Gracz", "Gracz vs AI" });
            modeBox.SelectedIndex = 1;

            okButton = new Button { Text = "OK", Location = new Point(50, 190), Width = 80 };
            cancelButton = new Button { Text = "Anuluj", Location = new Point(150, 190), Width = 80 };

            okButton.Click += OkButton_Click;
            cancelButton.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[]
            {
                p1Label, player1Box,
                p2Label, player2Box,
                speedLabel, speedBox,
                modeLabel, modeBox,
                okButton, cancelButton
            });
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _config.Player1Name = string.IsNullOrWhiteSpace(player1Box.Text) ? "Gracz 1" : player1Box.Text;
            _config.Player2Name = string.IsNullOrWhiteSpace(player2Box.Text) ? (modeBox.SelectedIndex == 1 ? "AI" : "Gracz 2") : player2Box.Text;
            _config.IsAgainstAI = modeBox.SelectedIndex == 1;

            // ZAMIANA SWITCH => IF/ELSE DLA C# 7.3
            if (speedBox.SelectedIndex == 0)
                _config.BallSpeed = 5;
            else if (speedBox.SelectedIndex == 1)
                _config.BallSpeed = 8;
            else if (speedBox.SelectedIndex == 2)
                _config.BallSpeed = 12;
            else
                _config.BallSpeed = 8;

            this.DialogResult = DialogResult.OK;
        }

        public GameConfig GetConfig() => _config;
    }
}
