using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barbie_on_the_Kama
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
