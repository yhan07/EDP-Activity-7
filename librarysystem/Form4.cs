using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace librarysystem
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        

        private void Next_Click(object sender, EventArgs e)
        {
            string name = this.username.Text;
            string answer = this.rec_answer.Text;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=1234;database=librarysystem";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "SELECT COUNT(*) from accounts where name = @name AND rec_answer = @rec_answer";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@rec_answer", answer);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {


                    Form3 g = new Form3();
                    this.Hide();

                    // Show Form2
                    g.ShowDialog();

                    // Close the current instance of Form1
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Insufficient Credentials");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}

