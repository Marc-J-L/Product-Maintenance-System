using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaintainProducts
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string mycode = code.Text;
            string myname = name.Text;
            string myversion = version.Text;
            string mydate = date.Text;


            try
            {
                using (SqlConnection con = new SqlConnection(@"Server=(localdb)\ProjectModels; Database=My_VS_DB;Integrated Security=True"))
                {
                    con.Open();
                    string addDataQuery = "INSERT INTO [dbo].[Product] (ProductCode, Name, Version, ReleaseDate) VALUES (@ProductCode, @Name, @Version, @ReleaseDate)";
                    using (SqlCommand cmd = new SqlCommand(addDataQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductCode", mycode);
                        cmd.Parameters.AddWithValue("@Name", myname);
                        cmd.Parameters.AddWithValue("@Version", myversion);
                        cmd.Parameters.AddWithValue("@ReleaseDate", mydate);
                       

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product added successfully!");
                            con.Close();
                            code.Text = name.Text = version.Text = date.Text = "";

                            // Return to Books window
                            MainWindow mainWin = new MainWindow();
                            mainWin.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the product.");
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }
    }
}
