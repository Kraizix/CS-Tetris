using System;
using System.Windows.Forms;

namespace TETRIS
{
    public partial class Options : Form
    {
        WMPLib.WindowsMediaPlayer axMusicPlayer = Home.GetMediaPlayer();
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            label2.Text = axMusicPlayer.settings.volume.ToString();
            volumeBar.Value = axMusicPlayer.settings.volume / 10;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            axMusicPlayer.URL = @"D:\Developement\tetrics\CS-Tetris\TETRIS\TETRIS\theme_menu.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void submitPass_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "password" & volumeBar.Value == 2)
            {
                textBox1.Text = "WOHOOOOO !";
            }
            else
            {
                textBox1.Text = "Aie...";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
