using System;
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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void quit_1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
