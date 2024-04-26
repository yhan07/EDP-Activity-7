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
using System.Xml.Linq;

namespace librarysystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login_form g = new Login_form();
            this.Hide();

            // Show Form2
            g.ShowDialog();

            // Close the current instance of Form1
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string username = this.textBox2.Text;
            string password = this.textBox1.Text;
            string rec_answer = this.textBox3.Text;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=1234;database=librarysystem";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                // Check if the account with the given name already exists
                string checkIfExistsQuery = "SELECT COUNT(*) FROM accounts WHERE name = @name";
                MySqlCommand checkIfExistsCmd = new MySqlCommand(checkIfExistsQuery, conn);
                checkIfExistsCmd.Parameters.AddWithValue("@name", username);

                int existingAccountCount = Convert.ToInt32(checkIfExistsCmd.ExecuteScalar());

                if (existingAccountCount > 0)
                {
                    MessageBox.Show("Account with the given name already exists. Please choose a different name.");
                    return;
                }

                // Insert new account
                string insertQuery = "INSERT INTO accounts (name, password, rec_answer) VALUES (@name, @password, @rec_answer)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@name", username);
                insertCmd.Parameters.AddWithValue("@password", password);
                insertCmd.Parameters.AddWithValue("@rec_answer", rec_answer);

                int rowsAffected = insertCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Account created successfully");
                }
                else
                {
                    MessageBox.Show("Failed to create account");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
