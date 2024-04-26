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

namespace librarysystem
{
    public partial class Login_form : Form
    {
        public Login_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                string username = this.textBox2.Text;
                string password = this.textBox1.Text;
                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=1234;database=librarysystem";
                try
                {
                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    string sql = "SELECT COUNT(*) from accounts where name = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {

                    Dashboard f = new Dashboard();
                    this.Hide();

                    // Show Form2
                    f.ShowDialog();

                    // Close the current instance of Form1
                    this.Close();


                }
                    else
                    {
                        MessageBox.Show("Invalid username/password");
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 g = new Form4();
            this.Hide();

            // Show Form2
            g.ShowDialog();

            // Close the current instance of Form1
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form2 f = new Form2();
            this.Hide();

            // Show Form2
            f.ShowDialog();

            // Close the current instance of Form1
            this.Close();
        }
    }
    }

