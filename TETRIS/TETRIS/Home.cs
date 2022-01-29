using System;
using WMPLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

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
            axMusicPlayer.URL = @"D:\Developement\tetrics\CS-Tetris\TETRIS\TETRIS\theme_option.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Play play = new Play();
            Hide();
            play.ShowDialog();
            Close();
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
    }
}
