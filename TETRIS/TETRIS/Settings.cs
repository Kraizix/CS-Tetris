using System;
using System.Windows.Forms;

namespace TETRIS
{
    public partial class Options : Form
    {
        private WMPLib.WindowsMediaPlayer axMusicPlayer = Home.GetMediaPlayer();
        private static int gameSpeed = 20;
        private static bool clearMode = false;
        private static int clearScore = 0;
        private static bool randomMode = false;
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            label2.Text = axMusicPlayer.settings.volume.ToString();
            volumeBar.Value = axMusicPlayer.settings.volume / 10;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axMusicPlayer.settings.volume = volumeBar.Value * 10;
            label2.Text = axMusicPlayer.settings.volume.ToString();
            if (axMusicPlayer.settings.volume == 0)
            {
                label2.Text = "Muted";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axMusicPlayer.URL = Home.AssetsPath + @"\theme_menu.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();
            this.Close();
        }

        private void submitPass_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Speed")
            {
                gameSpeed = 120;
                textBox1.Text = "Mode Super Speed : Activé";
            }
            else if (textBox1.Text == "Random")
            {
                randomMode = true;
                textBox1.Text = "Game randomized";
            }
            else if (textBox1.Text == "Reset")
            {
                gameSpeed = 0;
                randomMode = false;
                textBox1.Text = "Jeu réinitialisé...";
            }
            else
            {
                textBox1.Text = "Aie...";
            }
        }

        public static int GetSpeed()
        {
            return gameSpeed;
        }

        public static void SetSpeed(int value)
        {
            gameSpeed = value;
        }

        public static bool GetRandom()
        {
            return randomMode;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                gameSpeed = 0;
            } else if (comboBox1.SelectedIndex == 1)
            {
                gameSpeed = 10;
            } else if (comboBox1.SelectedIndex == 2)
            {
                gameSpeed = 50;
            } else
            {
                gameSpeed = 100;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox2.Show();
                clearMode = true;
            } else
            {
                comboBox2.Hide();
                clearMode = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                clearScore = 1000;
            } else if (comboBox2.SelectedIndex == 1)
            {
                clearScore = 2000;
            } else if (comboBox2.SelectedIndex == 2)
            {
                clearScore = 3000;
            } else if (comboBox2.SelectedIndex == 3)
            {
                clearScore = 4000;
            } else
            {
                clearScore = 5000;
            }
        }
        public static bool getMode()
        {
            return clearMode;
        }
        public static int getClearScore()
        {
            return clearScore;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
    }
}
