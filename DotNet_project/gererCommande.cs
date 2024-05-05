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
    public partial class gererCommande : Form
    {
        public gererCommande()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            //deconnexion
        }

        private void label4_Click(object sender, EventArgs e)
        {
            (new gererProduit()).Show();
        }

        private void label5_Click(object sender, EventArgs e)
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
    }
}
