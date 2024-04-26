using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace librarysystem
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


       


        private void button1_Click_1(object sender, EventArgs e)
        {

            string name = this.textBox2.Text;
            string newPassword = this.textBox1.Text;

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=1234;database=librarysystem";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                // Update password for the specified username
                string updateQuery = "UPDATE accounts SET password = @newPassword WHERE name = @username";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@newPassword", newPassword);
                updateCmd.Parameters.AddWithValue("@username", name);

                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully");
                }
                else
                {
                    MessageBox.Show("Failed to update password. Username not found.");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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
    }
}




