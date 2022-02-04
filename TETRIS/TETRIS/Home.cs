using System;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace TETRIS
{
    public partial class Home : Form
    {
        private static WMPLib.WindowsMediaPlayer axMusicPlayer = new WMPLib.WindowsMediaPlayer();
        public Home()
        {
            InitializeComponent();
            axMusicPlayer.URL = @"D:\Developement\tetrics\CS-Tetris\TETRIS\TETRIS\theme_menu.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            Options f3 = new Options(); // Instantiate a Form3 object.
            f3.Show(); // Show Form3 and
            axMusicPlayer.controls.stop();
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            axMusicPlayer.URL = @"D:\Developement\tetrics\CS-Tetris\TETRIS\TETRIS\theme_option.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Play play = new Play();
            play.ShowDialog();
            Hide();
        }

        public static WindowsMediaPlayer GetMediaPlayer()
        {
            return axMusicPlayer;
        }
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("closing");
            Application.Exit();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://i.imgur.com/DHAJmLB.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
