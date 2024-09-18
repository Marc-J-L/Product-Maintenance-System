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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MaintainProducts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = @"Server=(localdb)\ProjectModels; Database=My_VS_DB;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Products_Loaded;
        }

        private void Products_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ProductCode, Name, Version, ReleaseDate FROM [dbo].[Product]";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ProductCode = reader["ProductCode"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Version = reader["Version"].ToString(),
                                    ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"])

                                };
                                products.Add(product);
                            }
                        }
                    }
                    con.Close();
                }

                ListProduct.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data:{ex.Message}");
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to modify.");
                return;
            }

            var selectedProduct = (Product)ListProduct.SelectedItem;

            ModifyProduct modifyProduct = new ModifyProduct(selectedProduct);

            modifyProduct.Show();

            this.Close();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (ListProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            var selectedProduct = (Product)ListProduct.SelectedItem;

            var result = MessageBox.Show($"Are you sure you want to delete the Product '{selectedProduct.Name}'?", "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Server=(localdb)\ProjectModels; Database=My_VS_DB;Integrated Security=True"))
                    {
                        con.Open();
                        string deleteQuery = "DELETE FROM [dbo].[Product] WHERE ProductCode = @ProductCode";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductCode", selectedProduct.ProductCode);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product deleted successfully!");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the product.");
                                LoadData();
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
        }
    }
}
