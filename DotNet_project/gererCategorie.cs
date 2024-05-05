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
    public partial class gererCategorie : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NAWAL;Initial Catalog=DotNet;Integrated Security=True");

        public gererCategorie()
        {
            InitializeComponent();
            this.Load += gererCategorie_Load;
            dataGridView1.CellPainting += dataGridView1_CellPainting; // Attacher l'événement CellPainting
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nom = nomLabel.Text;
            if (button1.Text == "Ajouter") { AjouterCategorie(nom); }
            else if(button1.Text == "Modifier") { ModifierCategorie(nom); }
                
        }
        private void ModifierCategorie(string nom)
        {
            try
            {
                connection.Open();

                // Récupérer l'ID de la catégorie sélectionnée
                string categoryId = dataGridView1.SelectedRows[0].Cells["CategoryID"].Value.ToString();

                // Mettre à jour le nom de la catégorie dans la base de données
                SqlCommand cmd = new SqlCommand("UPDATE Category SET CategoryName = @Nom WHERE CategoryID = @CategoryID", connection);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                cmd.ExecuteNonQuery();
                connection.Close();

                // Mettre à jour l'interface utilisateur
                nomLabel.Text = "";
                button1.Text = "Ajouter"; // Réinitialiser le libellé du bouton

                MessageBox.Show("La catégorie a été modifiée avec succès.");

                // Mettre à jour automatiquement la GridView après la modification de la catégorie
                ChargerDonneesCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification de la catégorie : " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        private void AjouterCategorie(string nom)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@Nom)", connection);
                cmd.Parameters.AddWithValue("@Nom", nom);

                cmd.ExecuteNonQuery();
                connection.Close();
                nomLabel.Text = "";
                MessageBox.Show("La categorie est bien ajoutée");

                // Mettre à jour automatiquement la GridView après l'ajout d'une nouvelle catégorie
                ChargerDonneesCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du produit : " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Fermer la fenêtre
            this.Close();
            // Arrêter l'application
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }
        private void gererCategorie_Load(object sender, EventArgs e)
        {
            // Chargement des données dans le DataGridView au chargement du formulaire
            ChargerDonneesCategories();
        }

        private void ChargerDonneesCategories()
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM Category";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                // Liaison des données au DataGridView
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["CategoryID"].Visible = false;
                dataGridView1.Columns["CategoryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vérifie si la cellule cliquée est une cellule de contenu
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Affiche une fenêtre modale pour demander à l'utilisateur de supprimer ou modifier la ligne sélectionnée
                CustomMessageBox customDialog = new CustomMessageBox();
                DialogResult result = customDialog.ShowDialog();
                // Vérifie le choix de l'utilisateur
                if (result == DialogResult.Yes) // Modifier
                {
                    // Récupérer les informations de la ligne sélectionnée
                    string categoryId = dataGridView1.Rows[e.RowIndex].Cells["CategoryID"].Value.ToString();
                    string categoryName = dataGridView1.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
                    // Récupérez d'autres informations nécessaires ici

                    nomLabel.Text = categoryName;

                    // Mettre à jour le libellé du bouton d'action pour indiquer la modification
                    button1.Text = "Modifier";
                    button1.BackColor = Color.Blue;

                }
                else if (result == DialogResult.No) // Supprimer
                {
                    DialogResult confirmResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette categorie  ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Supprimer la ligne sélectionnée
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        string categoryId = dataGridView1.Rows[e.RowIndex].Cells["CategoryID"].Value.ToString();
                        SupprimerCategorie(categoryId);
                    }
                }
            }
        }

        private void SupprimerCategorie(string categoryId)
        {
            try
            {
                connection.Open();

                string query = "DELETE FROM Category WHERE CategoryID = @CategoryID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryID", categoryId);
                command.ExecuteNonQuery();

                MessageBox.Show("Enregistrement supprimé avec succès.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression de l'enregistrement : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                using (Brush brush = new SolidBrush(Color.Orange)) // Définir la couleur de fond
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds); // Remplir le rectangle avec la couleur de fond
                    e.PaintContent(e.ClipBounds); // Peindre le contenu de la cellule
                    e.Handled = true; // Indiquer que le dessin est terminé
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void nomLabel_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            (new gererProduit()).Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            (new gererCommande()).Show();
        }
    }
}
