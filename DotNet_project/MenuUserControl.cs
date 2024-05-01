using System;
using System.Windows.Forms;
using WindowsFormsApp;

namespace DotNet_project
{
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {

            InitializeComponent();
            DisplayWelcomeMessage(); // Display welcome message if applicable
        }

        public void orderIcon_Click(object sender, EventArgs e)
        {
            // Handle click event for orders
            MessageBox.Show("Orders clicked");
        }

        public void accountIcon_Click(object sender, EventArgs e)
        {
            // Handle click event for account
            MessageBox.Show("Account clicked");
        }

        public void cartIcon_Click(object sender, EventArgs e)
        {
            // Handle click event for cart
            cart cartForm = new cart();
            cartForm.Show();
            this.FindForm().Hide();
        }

        public void logoIcon_Click(object sender, EventArgs e)
        {
            // Handle click event for logo
            Form1 home = new Form1();
            home.Show();
            this.FindForm().Hide();
        }

        private void DisplayWelcomeMessage()
        {
            // Display welcome message if a username is available
            if (!string.IsNullOrEmpty(SessionManager.Username))
            {
                welcomemsglabel.Text = "Welcome, " + SessionManager.Username + "!";
            }
        }
    }
}
