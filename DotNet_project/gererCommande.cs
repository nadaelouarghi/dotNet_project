using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DotNet_project
{
    public partial class gererCommande : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NAWAL;Initial Catalog=DotNet;Integrated Security=True");
        public gererCommande()
        {
            InitializeComponent();
            this.Load += GererCommandes_Load;
        }

        private void GererCommandes_Load(object sender, EventArgs e)
        {
            ChargerDonneesClients();
        }

        private void ChargerDonneesClients()
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM [Order]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                // Masquer les colonnes que vous ne voulez pas afficher, par exemple l'ID du client
                dataGridView1.Columns["UserID"].Visible = false;
                dataGridView1.Columns["OrderID"].Visible = false; // Masquer le mot de passe pour des raisons de sécurité

                // Définir la propriété AutoSizeMode de chaque colonne sur Fill
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name != "UserID" && col.Name != "OrderID") // Ne pas modifier la colonne ProductID
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        private void ModifierStatus(string statut)
        {
            try
            {
                connection.Open();

                // Récupérer l'ID du client sélectionné
                string orderId = dataGridView1.SelectedRows[0].Cells["OrderID"].Value.ToString();

                // Mettre à jour les informations du client dans la base de données
                SqlCommand cmd = new SqlCommand("UPDATE [Order] SET  Status = @status WHERE OrderID = @orderId", connection);
                cmd.Parameters.AddWithValue("@status", statut);
                cmd.Parameters.AddWithValue("@UserID", orderId);

                cmd.ExecuteNonQuery();
                connection.Close();

                status.Text = "";
                MessageBox.Show("Le statut a été modifié avec succès.");

                // Mettre à jour automatiquement la GridView après la modification du client
                ChargerDonneesClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification du statut : " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string statut = status.Text;

            ModifierStatus(statut);


        }
        

        private void status_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vérifie si la cellule cliquée est une cellule de contenu
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string orderId = dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value.ToString();
                string statut = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                    // Récupérez d'autres informations nécessaires ici
                    status.Text = statut;
                    
            }
               
         
        }
    }
    
}
