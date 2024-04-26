using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace librarysystem
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Set license context to non-commercial to avoid license check
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Path to the template Excel file
                string templatePath = @"C:\Users\User\Desktop\template.xlsx";
                string savePath = @"C:\Users\User\Desktop\BorrowedBooks.xlsx";

                // Check if the template file exists
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("Template file not found: " + templatePath);
                    return;
                }

                // Load the template Excel file using EPPlus
                using (var templatePackage = new ExcelPackage(new FileInfo(templatePath)))
                {
                    // Get the worksheet from the template by name
                    ExcelWorksheet templateWorksheet = templatePackage.Workbook.Worksheets["Sheet1"];

                    // Create a new Excel package to work with
                    using (var excelPackage = new ExcelPackage())
                    {
                        // Add a new worksheet to the package
                        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                        // Insert data into the worksheet starting from cell A8
                        DataTable dataTable = (DataTable)datagrid_bbooks.DataSource;
                        worksheet.Cells["A8"].LoadFromDataTable(dataTable, true);

                        // Copy content from template worksheet to the new worksheet
                        int rowCount = templateWorksheet.Dimension.Rows;
                        int colCount = templateWorksheet.Dimension.Columns;
                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                var templateCell = templateWorksheet.Cells[row, col];
                                var cell = worksheet.Cells[row, col];
                                cell.Value = templateCell.Value;

                                // Copy font style
                                cell.Style.Font.Bold = templateCell.Style.Font.Bold;
                                cell.Style.Font.Italic = templateCell.Style.Font.Italic;
                                cell.Style.Font.UnderLine = templateCell.Style.Font.UnderLine;
                                cell.Style.Font.Strike = templateCell.Style.Font.Strike;

                                // Convert ExcelColor to System.Drawing.Color for Fill color
                                if (templateCell.Style.Fill.PatternType != OfficeOpenXml.Style.ExcelFillStyle.None)
                                {
                                    var excelColor = templateCell.Style.Fill.BackgroundColor;
                                    System.Drawing.Color fillColor = System.Drawing.ColorTranslator.FromHtml(excelColor.Rgb);
                                    cell.Style.Fill.PatternType = templateCell.Style.Fill.PatternType;
                                    cell.Style.Fill.BackgroundColor.SetColor(fillColor);
                                }

                                // Copy other relevant style properties as needed
                                cell.Style.HorizontalAlignment = templateCell.Style.HorizontalAlignment;
                                cell.Style.VerticalAlignment = templateCell.Style.VerticalAlignment;
                                // Add more style properties as needed
                            }
                        }

                        // Save the Excel file as archive.xlsx
                        excelPackage.SaveAs(new FileInfo(savePath));

                        MessageBox.Show("Excel file generated successfully at: " + savePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Excel file: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button1.Visible = true;
            panel2.Visible = !panel2.Visible;
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
                string selectQuery = "SELECT * FROM borrowedbooks";

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
                    datagrid_bbooks.DataSource = dataTable;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            panel2.Visible = !panel2.Visible;
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
                string selectQuery = "SELECT * FROM books";

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
                    datagrid_bbooks.DataSource = dataTable;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button3.Visible = false;
            button2.Visible = true;
            panel2.Visible = !panel2.Visible;
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
                string selectQuery = "SELECT * FROM members";

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
                    datagrid_bbooks.DataSource = dataTable;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Set license context to non-commercial to avoid license check
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Path to the template Excel file
                string templatePath = @"C:\Users\User\Desktop\template.xlsx";
                string savePath = @"C:\Users\User\Desktop\AvailableBooks.xlsx";

                // Check if the template file exists
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("Template file not found: " + templatePath);
                    return;
                }

                // Load the template Excel file using EPPlus
                using (var templatePackage = new ExcelPackage(new FileInfo(templatePath)))
                {
                    // Get the worksheet from the template by name
                    ExcelWorksheet templateWorksheet = templatePackage.Workbook.Worksheets["Sheet1"];

                    // Create a new Excel package to work with
                    using (var excelPackage = new ExcelPackage())
                    {
                        // Add a new worksheet to the package
                        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                        // Insert data into the worksheet starting from cell A8
                        DataTable dataTable = (DataTable)datagrid_bbooks.DataSource;
                        worksheet.Cells["A8"].LoadFromDataTable(dataTable, true);

                        // Copy content from template worksheet to the new worksheet
                        int rowCount = templateWorksheet.Dimension.Rows;
                        int colCount = templateWorksheet.Dimension.Columns;
                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                var templateCell = templateWorksheet.Cells[row, col];
                                var cell = worksheet.Cells[row, col];
                                cell.Value = templateCell.Value;

                                // Copy font style
                                cell.Style.Font.Bold = templateCell.Style.Font.Bold;
                                cell.Style.Font.Italic = templateCell.Style.Font.Italic;
                                cell.Style.Font.UnderLine = templateCell.Style.Font.UnderLine;
                                cell.Style.Font.Strike = templateCell.Style.Font.Strike;

                                // Convert ExcelColor to System.Drawing.Color for Fill color
                                if (templateCell.Style.Fill.PatternType != OfficeOpenXml.Style.ExcelFillStyle.None)
                                {
                                    var excelColor = templateCell.Style.Fill.BackgroundColor;
                                    System.Drawing.Color fillColor = System.Drawing.ColorTranslator.FromHtml(excelColor.Rgb);
                                    cell.Style.Fill.PatternType = templateCell.Style.Fill.PatternType;
                                    cell.Style.Fill.BackgroundColor.SetColor(fillColor);
                                }

                                // Copy other relevant style properties as needed
                                cell.Style.HorizontalAlignment = templateCell.Style.HorizontalAlignment;
                                cell.Style.VerticalAlignment = templateCell.Style.VerticalAlignment;
                                // Add more style properties as needed
                            }
                        }

                        // Save the Excel file as archive.xlsx
                        excelPackage.SaveAs(new FileInfo(savePath));

                        MessageBox.Show("Excel file generated successfully at: " + savePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Excel file: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Set license context to non-commercial to avoid license check
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Path to the template Excel file
                string templatePath = @"C:\Users\User\Desktop\template.xlsx";
                string savePath = @"C:\Users\User\Desktop\LibraryMembers.xlsx";

                // Check if the template file exists
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("Template file not found: " + templatePath);
                    return;
                }

                // Load the template Excel file using EPPlus
                using (var templatePackage = new ExcelPackage(new FileInfo(templatePath)))
                {
                    // Get the worksheet from the template by name
                    ExcelWorksheet templateWorksheet = templatePackage.Workbook.Worksheets["Sheet1"];

                    // Create a new Excel package to work with
                    using (var excelPackage = new ExcelPackage())
                    {
                        // Add a new worksheet to the package
                        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                        // Insert data into the worksheet starting from cell A8
                        DataTable dataTable = (DataTable)datagrid_bbooks.DataSource;
                        worksheet.Cells["A8"].LoadFromDataTable(dataTable, true);

                        // Copy content from template worksheet to the new worksheet
                        int rowCount = templateWorksheet.Dimension.Rows;
                        int colCount = templateWorksheet.Dimension.Columns;
                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                var templateCell = templateWorksheet.Cells[row, col];
                                var cell = worksheet.Cells[row, col];
                                cell.Value = templateCell.Value;

                                // Copy font style
                                cell.Style.Font.Bold = templateCell.Style.Font.Bold;
                                cell.Style.Font.Italic = templateCell.Style.Font.Italic;
                                cell.Style.Font.UnderLine = templateCell.Style.Font.UnderLine;
                                cell.Style.Font.Strike = templateCell.Style.Font.Strike;

                                // Convert ExcelColor to System.Drawing.Color for Fill color
                                if (templateCell.Style.Fill.PatternType != OfficeOpenXml.Style.ExcelFillStyle.None)
                                {
                                    var excelColor = templateCell.Style.Fill.BackgroundColor;
                                    System.Drawing.Color fillColor = System.Drawing.ColorTranslator.FromHtml(excelColor.Rgb);
                                    cell.Style.Fill.PatternType = templateCell.Style.Fill.PatternType;
                                    cell.Style.Fill.BackgroundColor.SetColor(fillColor);
                                }

                                // Copy other relevant style properties as needed
                                cell.Style.HorizontalAlignment = templateCell.Style.HorizontalAlignment;
                                cell.Style.VerticalAlignment = templateCell.Style.VerticalAlignment;
                                // Add more style properties as needed
                            }
                        }

                        // Save the Excel file as archive.xlsx
                        excelPackage.SaveAs(new FileInfo(savePath));

                        MessageBox.Show("Excel file generated successfully at: " + savePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Excel file: " + ex.Message);
            }
        }
    }
    }

