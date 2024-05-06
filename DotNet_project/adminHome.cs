using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet_project
{
    public partial class adminHome : Form
    {
        public adminHome()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void adminHome_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            (new gererProduit()).Show();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            (new gererCategorie()).Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            (new gererClients()).Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            (new gererCommande()).Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            (new login()).Show();
        }
    }
}
