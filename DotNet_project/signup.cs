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
    public partial class signup : Form
    {
        string connectionString = "data source=LAPTOP-ROHL39L4;initial catalog=projetDotnet;integrated security=true";

        public signup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if all text boxes are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                label5.Text = "Please fill in all fields.";
                return;
            }

            // Validate email format
            if (!IsValidEmail(textBox2.Text))
            {
                label5.Text = "Invalid email format.";
                return;
            }

            // Check if the username or email already exists in the database
            if (UserNameOrEmailExists(textBox1.Text, textBox2.Text))
            {
                label5.Text = "Username or email already exists.";
                return;
            }

            // Add the user to the database
            if (AddUserToDatabase(textBox1.Text, textBox2.Text, textBox3.Text))
            {
                // Show success message
                MessageBox.Show("Account created successfully.");

                // Close the form or navigate to another form
                
            }
            else
            {
                // Show error message if adding user to the database fails
                label5.Text = "Failed to create account. Please try again.";
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool UserNameOrEmailExists(string username, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username OR Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private bool AddUserToDatabase(string username, string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [User] (Username, Email, Password, Role) VALUES (@Username, @Email, @Password, @Role)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Role", "User"); // Assuming default role is "User"

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login loginForm = new login();

            loginForm.Show();
            this.Hide();
        }
    }
}
