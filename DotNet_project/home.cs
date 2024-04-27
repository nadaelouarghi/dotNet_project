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
        string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";

        // Define a class to represent a category
        public class Category
        {
            public int CategoryID { get; set; }
            public string Name { get; set; }
        }

        // List to store categories
        List<Category> Categories = new List<Category>();

        public Form1()
        {
            InitializeComponent();
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
            string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";
            string query = "SELECT TOP 4 ProductID, ProductName, Price, ImageURL FROM Product WHERE CategoryID = @CategoryID";
            SqlConnection connection = new SqlConnection(connectionString);

            // Open connection to database
            connection.Open();

            // Calculate the width and height of each PictureBox and Panel
            int pictureBoxWidth = 100;
            int pictureBoxHeight = 100;
            int panelWidth = pictureBoxWidth + 30; // Assuming 20 pixels spacing between PictureBox controls
            int panelHeight = 200;

            // Iterate through each category
            foreach (var category in Categories)
            {
                // Create a group box for each category
                GroupBox groupBox = new GroupBox();
                groupBox.Text = category.Name;
                groupBox.Width = 600;
                groupBox.Height = 200;

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
                    addButton.Text = "Add to Cart";
                    addButton.Location = new Point((panel.Width - addButton.Width) / 2, priceLabel.Bottom + 5);

                    int productId = Convert.ToInt32(reader["ProductID"]);
                    // Attach the click event handler, capturing the productId for the current product
                    addButton.Click += (sender, e) => AddToCartButton_Click(sender, e, productId);
                    panel.Controls.Add(addButton); panel.Controls.Add(addButton);

                    groupBox.Controls.Add(panel);
                }

                reader.Close(); // Close the reader after processing
            }
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
                // Logic to add the product to the cart
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the sign-up form
            signup signUpForm = new signup();

            signUpForm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create an instance of the sign-up form
            login loginForm = new login();

            loginForm.Show();
            this.Hide();
        }

        private void DisplayWelcomeMessage()
        {
            if (!string.IsNullOrEmpty(SessionManager.Username))
            {
                label1.Text = "Welcome, " + SessionManager.Username + "!";
                button1.Visible = false;
                button2.Visible = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SessionManager.Username))
            {
                // Create an instance of the sign-up form
                login loginForm = new login();

                loginForm.Show();
                this.Hide();

            }
        }
    }

}
