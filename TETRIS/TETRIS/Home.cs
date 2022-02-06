using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace TETRIS
{
    public partial class Home : Form
    {
        public static string AssetsPath = findAssetsPath();
        private static string findAssetsPath()
        {
            string[] folders = Path.GetDirectoryName(Application.ExecutablePath).Split('\\');
            return String.Join(@"\", folders.Take(folders.Length - 2)) + @"\assets\";
        }
        private Play play;
        private static WMPLib.WindowsMediaPlayer axMusicPlayer = new WMPLib.WindowsMediaPlayer();
        public Home()
        {
            InitializeComponent();
            axMusicPlayer.URL = AssetsPath+ @"\theme_menu.wav";
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
            axMusicPlayer.controls.stop();
            axMusicPlayer.URL = AssetsPath + @"\theme_option.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();
            f3.ShowDialog(); // Show Form3 and
            

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            play = new Play(this);
            Hide();
            play.ShowDialog();
            try
            {
                Show();
            } catch
            {
                return;
            }
            
            
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

        private void Home_Shown(object sender, EventArgs e)
        {
            if(play != null)
            {
                play.Close();
            }
        }
    }
}
