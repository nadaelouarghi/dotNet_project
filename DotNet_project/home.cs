using DotNet_project;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
<<<<<<< HEAD
        string connectionString = "data source=DESKTOP-NAWAL;initial catalog=Dotnet;integrated security=true";

        // Define a class to represent a category
        public class Category
        {
            public int CategoryID { get; set; }
            public string Name { get; set; }
        }

=======
        string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";
        
>>>>>>> d7842f7dbb6d940c3ae2def3a7948148db1a6cc4
        // List to store categories
        List<Category> Categories = new List<Category>();

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            DisplayWelcomeMessage();
            LoadCategories();
            DisplayImages();
        }

        // Method to load categories from the database
        private void LoadCategories()
        {
            string query = "SELECT CategoryID, CategoryName FROM Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    category.Name = reader["CategoryName"].ToString();
                    Categories.Add(category);
                }
                reader.Close();
            }
        }

        // Method to display images for each category
        private void DisplayImages()
        {
<<<<<<< HEAD
            string connectionString = "Data Source=DESKTOP-NAWAL;Initial Catalog=DotNet;Integrated Security=True";
=======
>>>>>>> d7842f7dbb6d940c3ae2def3a7948148db1a6cc4
            string query = "SELECT TOP 4 ProductID, ProductName, Price, ImageURL FROM Product WHERE CategoryID = @CategoryID";
            SqlConnection connection = new SqlConnection(connectionString);

            // Open connection to database
            connection.Open();

            // Calculate the width and height of each PictureBox and Panel
            int pictureBoxWidth = 100;
            int pictureBoxHeight = 100;
            int panelWidth = pictureBoxWidth + 40; // Assuming 20 pixels spacing between PictureBox controls
            int panelHeight = 180;

            // Iterate through each category
            foreach (var category in Categories)
            {
              // Query to count the number of products for the current category
                string countQuery = "SELECT COUNT(*) FROM Product WHERE CategoryID = @CategoryID";
                SqlCommand countCommand = new SqlCommand(countQuery, connection);
                countCommand.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                int productCount = (int)countCommand.ExecuteScalar();

                if (productCount > 0)
                {
                    // Create a group box for each category
                    GroupBox groupBox = new GroupBox();
                    groupBox.Text = category.Name;
                    groupBox.Width = 610;
                    groupBox.Height = 220;
                    groupBox.Padding = new Padding(20); // Add padding inside the group box

                    this.Controls.Add(groupBox);

                    // Calculate the vertical position of the group box
                    int groupBoxY = 100 + (groupBox.Height + 20) * Categories.IndexOf(category);

                    // Set the position of the group box
                    groupBox.Location = new Point((this.ClientSize.Width - groupBox.Width) / 2, groupBoxY);

                    // Calculate the horizontal position of the first panel to center the panels
                    int initialPanelX = (groupBox.Width - (4 * panelWidth + 3 * 10)) / 2; // Assuming 10 pixels spacing between panels
                                                                                          // Query the database to get the top 4 products for the current category
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    SqlDataReader reader = command.ExecuteReader();

                    // Iterate through the top 4 products for the current category
                    for (int i = 0; i < 4 && reader.Read(); i++)
                    {
                        // Create a panel to hold picture box, label, and button
                        Panel panel = new Panel();
                        panel.Width = panelWidth;
                        panel.Height = panelHeight;
                        panel.Location = new Point(initialPanelX + i * (panelWidth + 10), 20);
                        panel.BorderStyle = BorderStyle.FixedSingle; // Add border to the panel


                        // Create PictureBox
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Width = pictureBoxWidth;
                        pictureBox.Height = pictureBoxHeight;
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox.ImageLocation = reader["ImageURL"].ToString();
                        pictureBox.Location = new Point((panel.Width - pictureBox.Width) / 2, 0);
                        panel.Controls.Add(pictureBox);

                        // Create label for product name and price
                        Label nameLabel = new Label();
                        nameLabel.Text = reader["ProductName"].ToString();
                        nameLabel.AutoSize = true;
                        nameLabel.Location = new Point((panel.Width - nameLabel.Width) / 2, pictureBoxHeight + 5);
                        panel.Controls.Add(nameLabel);

                        Label priceLabel = new Label();
                        priceLabel.Text = "$" + reader["Price"].ToString(); // Assuming Price is stored as decimal in the database
                        priceLabel.AutoSize = true;
                        priceLabel.Location = new Point((panel.Width - priceLabel.Width) / 2, nameLabel.Bottom + 5);
                        panel.Controls.Add(priceLabel);

                        // Create button to add to cart
                        Button addButton = new Button();
                        addButton.Text = "Ajouter au panier";
                        addButton.Location = new Point((panel.Width - addButton.Width) / 2, priceLabel.Bottom + 5);
                        addButton.AutoSize = true;

                        int productId = Convert.ToInt32(reader["ProductID"]);
                        // Attach the click event handler, capturing the productId for the current product
                        addButton.Click += (sender, e) => AddToCartButton_Click(sender, e, productId);
                        panel.Controls.Add(addButton); panel.Controls.Add(addButton);
                        groupBox.Controls.Add(panel);
                    }

                    // Create a linked label
                    LinkLabel linkedLabel = new LinkLabel();
                    linkedLabel.Text = "Voir plus...";
                    linkedLabel.LinkClicked += (sender, e) => SeeMore(sender, e, category);
                    linkedLabel.AutoSize = true;
                    linkedLabel.Location = new Point(groupBox.Width - linkedLabel.Width - 5, groupBox.Height - linkedLabel.Height + 10); // Position the link label at the bottom right corner
                    linkedLabel.LinkColor = Color.Black;
                    // Add the linked label to the group box
                    groupBox.Controls.Add(linkedLabel);
                    reader.Close(); // Close the reader after processing
                }
            }
        }

        private void SeeMore(object sender, LinkLabelLinkClickedEventArgs e, Category category)
        {
            SeeAllByCategory seeAllByCategoryForm = new SeeAllByCategory(category);
            seeAllByCategoryForm.Show();
            this.Hide();
        }

        // Event handler for Add to Cart button click
        private void AddToCartButton_Click(object sender, EventArgs e, int productId)
        {
            if (string.IsNullOrEmpty(SessionManager.Username))
            {
                // Create an instance of the login form
                login loginForm = new login();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                AddProductToCart(productId);
            }
        }

        // Method to add the product to the cart
        private void AddProductToCart(int productId)
        {
            string query = "INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                // Assuming you have a way to get the UserID from the session
                command.Parameters.AddWithValue("@UserID", SessionManager.UserId);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.Parameters.AddWithValue("@Quantity", 1); // Assuming you always add one quantity at a time
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Product added to cart successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add product to cart. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void DisplayWelcomeMessage()
        {
            if (!string.IsNullOrEmpty(SessionManager.Username))
            {
                welcomemsglabel.Text = "Welcome, " + SessionManager.Username + "!";
                sinscrirelinkLabel.Visible = false;
                seconnecterlinkLabel.Visible = false;
            }
            else
            {
                accountlabel.Visible = false;
                accountpictureBox.Visible = false;
                orderlabel.Visible = false;
                orderpictureBox.Visible = false;
            }
        }

        private void cartIcon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SessionManager.Username))
            {
                // Create an instance of the sign-up form
                login loginForm = new login();

                loginForm.Show();
                this.Hide();

            }
            else
            {
                // Create an instance of the cart form
                cart cartForm = new cart();

                cartForm.Show();
                this.Hide();
            }
        }

        private void seconnecterlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the sign-up form
            login loginForm = new login();

            loginForm.Show();
            this.Hide();
        }

        private void sinscrirelinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the sign-up form
            signup signUpForm = new signup();

            signUpForm.Show();
            this.Hide();
        }

        private void logoIcon_Click(object sender, EventArgs e)
        {

        }
    }
}
