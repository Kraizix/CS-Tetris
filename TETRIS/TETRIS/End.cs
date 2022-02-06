﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TETRIS
{
    public partial class End : Form
    {
        private WMPLib.WindowsMediaPlayer axMusicPlayer = Home.GetMediaPlayer();
        private Play playForm;
        public End(Play play)
        {
            playForm = play;
            InitializeComponent();
            label1.Text += play.GetScore();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            playForm.Reset();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axMusicPlayer.URL = Home.AssetsPath + @"\theme_menu.wav";
            axMusicPlayer.settings.setMode("loop", true);
            axMusicPlayer.controls.play();
            this.Close();
            playForm.Close();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
