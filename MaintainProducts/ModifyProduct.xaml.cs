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
    /// Interaction logic for ModifyProduct.xaml
    /// </summary>
    public partial class ModifyProduct : Window
    {
        private Product _product;

        private string connectionString = @"Server=(localdb)\ProjectModels; Database=My_VS_DB;Integrated Security=True";


        public ModifyProduct(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadProductDetails();

        }

        private void LoadProductDetails()
        {
            code.Content = _product.ProductCode;
            name.Text = _product.Name;
            version.Text = _product.Version;
            date.Text = _product.ReleaseDate.ToString("yyyy-MM-dd");

        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            // Update book information
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE [dbo].[Product] SET  Name = @Name, Version = @Version, ReleaseDate = @ReleaseDate WHERE ProductCode = @ProductCode";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        //cmd.Parameters.AddWithValue("@ProductCode ", code.Content);
                        cmd.Parameters.AddWithValue("@Name", name.Text);
                        cmd.Parameters.AddWithValue("@Version", version.Text);
                        cmd.Parameters.AddWithValue("@ReleaseDate", DateTime.TryParse(date.Text, out var mydate) ? (object)mydate : DBNull.Value);

                        cmd.ExecuteNonQuery();

                    }
                    con.Close();
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the product: {ex.Message}");
            }

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE [dbo].[Product] SET  Name = @Name, Version = @Version, ReleaseDate = @ReleaseDate WHERE ProductCode = @ProductCode";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductCode ", code.Content);
                        cmd.Parameters.AddWithValue("@Name", name.Text);
                        cmd.Parameters.AddWithValue("@Version", version.Text);
                        cmd.Parameters.AddWithValue("@ReleaseDate", DateTime.TryParse(date.Text, out var mydate) ? (object)mydate : DBNull.Value);

                        cmd.ExecuteNonQuery();

                    }
                    con.Close();
                }
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the product: {ex.Message}");
            }
        }
    }
}
