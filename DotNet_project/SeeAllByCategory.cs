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
using WindowsFormsApp;

namespace DotNet_project
{
    public partial class SeeAllByCategory : Form
    {
        string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";

        public SeeAllByCategory(Category category)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            LoadProducts(category);
            DisplayWelcomeMessage();
        }

        private void SeeAllByCategory_Load(object sender, EventArgs e)
        {

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




        // Method to load products for a specific category
        private void LoadProducts(Category category)
        {
            string query = "SELECT ProductID, ProductName, Price, ImageURL FROM Product WHERE CategoryID = @CategoryID";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            int pictureBoxWidth = 100;
            int pictureBoxHeight = 100;
            int panelWidth = pictureBoxWidth + 40; // Assuming 20 pixels spacing between panels
            int panelHeight = 190;
            int panelCount = 0;
            int maxColumns = 4; // Maximum number of columns per row

            GroupBox groupBox = new GroupBox();
            groupBox.Text = category.Name; // Set the group box title
            groupBox.Width = 650; // Adjust the width as needed
            groupBox.Padding = new Padding(30); // Add padding inside the group box
            this.Controls.Add(groupBox);
            // Calculate the vertical position of the group box
            int groupBoxY = 100;

            // Set the position of the group box
            groupBox.Location = new Point((this.ClientSize.Width - groupBox.Width) / 2, groupBoxY);

            while (reader.Read())
            {
                Panel panel = new Panel();
                panel.Width = panelWidth;
                panel.Height = panelHeight;
                panel.BorderStyle = BorderStyle.FixedSingle; // Add border to the panel

                int columnIndex = panelCount % maxColumns;
                int x = columnIndex * (panelWidth + 10) +30 ; // Adjusting X position based on the column index
                int rowIndex = panelCount / maxColumns;
                int y = rowIndex * (panelHeight + 30)+20; // Adjusting Y position based on the row index

                panel.Location = new Point(x, y);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = pictureBoxWidth;
                pictureBox.Height = pictureBoxHeight;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.ImageLocation = reader["ImageURL"].ToString();
                pictureBox.Location = new Point(10, 10);
                panel.Controls.Add(pictureBox);

                Label nameLabel = new Label();
                nameLabel.Text = reader["ProductName"].ToString();
                nameLabel.AutoSize = true;
                nameLabel.Location = new Point(10, pictureBoxHeight + 20);
                panel.Controls.Add(nameLabel);

                Label priceLabel = new Label();
                priceLabel.Text = "$" + reader["Price"].ToString(); // Assuming Price is stored as decimal in the database
                priceLabel.AutoSize = true;
                priceLabel.Location = new Point(10, nameLabel.Bottom + 5);
                panel.Controls.Add(priceLabel);

                Button addButton = new Button();
                addButton.Text = "Ajouter au panier";
                addButton.Location = new Point(10, priceLabel.Bottom + 5);
                addButton.AutoSize = true;

                int productId = Convert.ToInt32(reader["ProductID"]);
                addButton.Click += (sender, e) => AddToCartButton_Click(sender, e, productId);
                panel.Controls.Add(addButton);

                groupBox.Controls.Add(panel);

                panelCount++;
            }

            // Calculate the height of the group box based on the number of rows
            int rowCount = (panelCount + maxColumns - 1) / maxColumns; // Round up division to get the total number of rows
            groupBox.Height = rowCount * (panelHeight + 20);
        

        reader.Close(); // Close the reader after processing
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

        private void logoIcon_Click_1(object sender, EventArgs e)
        {
            // Handle click event for logo
            Form1 home = new Form1();
            home.Show();
            this.FindForm().Hide();
        }

        private void sinscrirelinkLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the sign-up form
            signup signUpForm = new signup();

            signUpForm.Show();
            this.Hide();
        }

       

        private void seconnecterlinkLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the sign-up form
            login loginForm = new login();

            loginForm.Show();
            this.Hide();
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
    }
}
