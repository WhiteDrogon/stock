using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stock
{
    public partial class products : Form
    {
        public products()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLExpress;Initial Catalog=stock;Integrated Security=True");
            con.Open();
            int status = 0;
            if (comboBox1.SelectedIndex == 0)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            var sqlQuery = "";
            if (recordexist(con , textBox1.Text))
            {
                sqlQuery = @"UPDATE [product] SET [productname] = '"+ textBox3.Text +"',[productstatus] = '"+ status +"' WHERE [productcode] = '"+ textBox1.Text +"'";
            }
            else
            {
        sqlQuery = @"INSERT INTO [stock].[dbo].[products] ([productcode],[productname],[productstatus]) VALUES ('" + textBox1.Text +"' , '"+ textBox3.Text +"','"+ status +"')";
                
            }
            SqlDataAdapter sda = new SqlDataAdapter("select * from [stock].[dbo].[product]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            loadata();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLExpress;Initial Catalog=stock;Integrated Security=True");
            var sqlQuery = "";
            if (recordexist(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [product] WHERE [productcode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record doesnt exist!");

            }
            
            loadata();      
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            loadata();
        }
        private bool recordexist(SqlConnection con , string productcode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select 1 from [product] WHERE [productcode] = '"+ productcode+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void loadata()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLExpress;Initial Catalog=stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("select * from [stock].[dbo].[product]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["productcode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["productname"].ToString();
                if ((int)item["productstatus"] == 0)
                {
                    dataGridView1.Rows[n].Cells[2].Value = "InActive";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }

            }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;

            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
           
        }
    }
}
