using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stock
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLExpress;Initial Catalog=stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
  FROM [stock].[dbo].[login] where username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                maain main = new maain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or passoword","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1_Click(sender, e);       
            }
        }
    }
}
