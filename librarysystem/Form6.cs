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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using DocumentFormat.OpenXml.Bibliography;


namespace librarysystem
{
    public partial class Dashboard : Form
    {
        private object loggedInAccounts;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void report_btn_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=1234;database=librarysystem";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                // Query to retrieve data from the view account_details
                string selectQuery = "SELECT * FROM account_details";

                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                {
                    // Create a DataTable to store the results
                    DataTable dataTable = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd))
                    {
                        // Fill the DataTable with the results from the view
                        adapter.Fill(dataTable);
                    }

                    // Set the DataTable as the DataSource for the DataGridView
                    dgv1.DataSource = dataTable;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void admin_btn_Click(object sender, EventArgs e)
        {
            Form7 g = new Form7();
            this.Hide();

            // Show Form2
            g.ShowDialog();

            // Close the current instance of Form1
            this.Close();
        }
    }
    }


       
