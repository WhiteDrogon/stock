using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stock
{
    public partial class maain : Form
    {

        public maain()
        {
            InitializeComponent();
        }

        private void maain_Load(object sender, EventArgs e)
        {

        }

        private void maain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            products pro = new products();
            pro.MdiParent = this;
            pro.Show();
        }

      
    }
}
