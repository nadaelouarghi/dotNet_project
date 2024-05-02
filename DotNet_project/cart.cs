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

namespace DotNet_project
{
    public partial class cart : Form
    {
        string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";

        public cart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            cart_menuUserControl.cartIcon.Click -= cart_menuUserControl.cartIcon_Click;
        }

        private void cart_Load(object sender, EventArgs e)
        {
            LoadCartItems();

        }

        private List<CartItem> GetCartItemsForUser(string username)
        {
            List<CartItem> cartItems = new List<CartItem>();

            string query = "SELECT p.ProductName, c.CategoryName, p.Price, ci.Quantity, p.ImageURL " +
                           "FROM Cart ci " +
                           "JOIN Product p ON ci.ProductID = p.ProductID " +
                           "JOIN Category c ON p.CategoryID = c.CategoryID " +
                           "JOIN [User] u ON ci.UserID = u.UserID " +
                           "WHERE u.Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CartItem cartItem = new CartItem
                    {
                        ProductName = reader["ProductName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        ImageURL = reader["ImageURL"].ToString()
                    };
                    cartItems.Add(cartItem);
                }
            }

            return cartItems;
        }
        private void ClearCartLayout()
        {
            // Remove existing PictureBox and Label controls from the form
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox || control is Label)
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }
        }
        private void LoadCartItems()
        {
            // Fetch cart items from the database
            List<CartItem> cartItems = GetCartItemsForUser(SessionManager.Username);

            // Clear existing items in the layout
            ClearCartLayout();

            // Display cart items in the custom layout
            int yPos = 100; // Initial Y position for the first item
            foreach (CartItem item in cartItems)
            {
                // Create PictureBox for product image
                PictureBox pictureBox = new PictureBox();
                pictureBox.ImageLocation = item.ImageURL;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(100, 100); // Set appropriate size
                pictureBox.Location = new Point(100, yPos); // Adjust X and Y position as needed
                this.Controls.Add(pictureBox);

                // Create Label for product name
                Label nameLabel = new Label();
                nameLabel.Text = "Product: " + item.ProductName;
                nameLabel.AutoSize = true;
                nameLabel.Location = new Point(250, yPos); // Adjust X position as needed
                this.Controls.Add(nameLabel);

                // Create Label for category
                Label categoryLabel = new Label();
                categoryLabel.Text = "Category: " + item.CategoryName;
                categoryLabel.AutoSize = true;
                categoryLabel.Location = new Point(250, yPos + 30); // Adjust X and Y position as needed
                this.Controls.Add(categoryLabel);

                // Create Label for price
                Label priceLabel = new Label();
                priceLabel.Text = "Price: DH" + item.Price.ToString("0.00");
                priceLabel.AutoSize = true;
                priceLabel.Location = new Point(250, yPos + 60); // Adjust X and Y position as needed
                this.Controls.Add(priceLabel);

                // Create Label for quantity
                Label quantityLabel = new Label();
                quantityLabel.Text = "Quantity: " + item.Quantity.ToString();
                quantityLabel.AutoSize = true;
                quantityLabel.Location = new Point(250, yPos + 90); // Adjust X and Y position as needed
                this.Controls.Add(quantityLabel);

                // Create Label for total price
                Label totalPriceLabel = new Label();
                totalPriceLabel.Text = "Total Price: $" + item.TotalPrice.ToString("0.00");
                totalPriceLabel.AutoSize = true;
                totalPriceLabel.Location = new Point(250, yPos + 120); // Adjust X and Y position as needed
                this.Controls.Add(totalPriceLabel);

                yPos += 150; // Adjust Y position for the next item
            }
        }


    }
}
