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

    public partial class login : Form
    {
        string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";

        public login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if all text boxes are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                label5.Text = "Please fill in all fields.";
                return;
            }
            // Get username and password from text boxes
            string username = textBox1.Text;
            string password = textBox3.Text;

            // Validate username and password
            bool isValidUser = ValidateUser(username, password);

            if (isValidUser)
            {
                // If valid user, redirect to home or another form
                // For example:
                SessionManager.Username = username;
                SessionManager.UserId = GetUserID(username);
                Form1 homeForm = new Form1();
                homeForm.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                // If invalid user, display error message
                label5.Text = "Invalid username or password.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }



        // Method to get the UserID from the database based on the username
        private int GetUserID(string username)
        {
            string query = "SELECT UserID FROM [User] WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Username", username);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Return -1 if user not found (handle this case appropriately)
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signup signForm = new signup();

                signForm.Show();
                this.Hide();
            
        }
    }
}