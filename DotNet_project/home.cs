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
            logoIcon.Click -= logoIcon_Click;

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

        private void logoIcon_Click(object sender, EventArgs e)
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

      


        /* Define a method to create PictureBoxes dynamically
        private void CreateDynamicPictureBoxes()
        {
            // PictureBox 1
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = DotNet_project.Properties.Resources.orderIcon; // Set image
            pictureBox1.Location = new Point(725, 20); // Set location
            pictureBox1.Size = new Size(43, 41); // Set size
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Set size mode
            pictureBox1.Click += PictureBox_Click; // Attach click event handler
            menupregroupBox.Controls.Add(pictureBox1); // Add to form's Controls collection

            // Label 6
            Label label6 = new Label();
            label6.AutoSize = true;
            label6.Size = new Size(148, 20);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(658, 64);
            label6.Text = "mes commandes";
            menupregroupBox.Controls.Add(label6);

            // PictureBox 3
            PictureBox pictureBox2 = new PictureBox();
            pictureBox2.Image = DotNet_project.Properties.Resources.accountIcon; // Set image
            pictureBox2.Location = new Point(846, 20); // Set location
            pictureBox2.Size = new Size(43, 41); // Set size
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; // Set size mode
            pictureBox2.Click += PictureBox_Click; // Attach click event handler
            menupregroupBox.Controls.Add(pictureBox2); // Add to form's Controls collection

            // Label 5
            Label label5 = new Label();
            label5.AutoSize = true;
            label5.ForeColor = Color.Black;
            label5.Size = new Size(107, 20);
            label5.Location = new Point(812, 64);
            label5.Text = "mon compte";
            menupregroupBox.Controls.Add(label5);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    */
    }
}
