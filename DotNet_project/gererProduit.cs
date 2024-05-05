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
    public partial class gererProduit : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NAWAL;Initial Catalog=DotNet;Integrated Security=True");

        public gererProduit()
        {
            InitializeComponent();
            this.Load += gererProduit_Load;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void gererProduit_Load(object sender, EventArgs e)
        {
            // Chargement des données dans le DataGridView au chargement du formulaire
            ChargerCategories();
            ChargerDonneesProduits();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nom = nomTextBox.Text;
            float prix = float.Parse(prixTextBox.Text);
            string url = urlTextBox.Text;
            string description = descriptionTextBox.Text;
            int quantite = int.Parse(quantiteTextBox.Text);
            Category selectedCategory = (Category)categorieComboBox.SelectedItem;
            int categorieID = selectedCategory.ID;

            if (button1.Text == "Ajouter") { AjouterProduit(nom, prix, url, description, quantite, categorieID); }
            else if (button1.Text == "Modifier") { ModifierProduit(nom, prix, url, description, quantite, categorieID); }
        }

        private void ModifierProduit(string nom, float prix, string url, string description, int quantite, int categorie)
        {
            try
            {
                connection.Open();

                // Récupérer l'ID du produit sélectionné
                string productId = dataGridView1.SelectedRows[0].Cells["ProductID"].Value.ToString();

                // Mettre à jour les informations du produit dans la base de données
                SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductName = @Nom, Price = @Prix, ImageURL = @Url, Description = @Description, Quantity = @Quantite, CategoryID = @Categorie WHERE ProductID = @ProductID", connection);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prix", prix);
                cmd.Parameters.AddWithValue("@Url", url);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@Categorie", categorie);
                cmd.Parameters.AddWithValue("@ProductID", productId);

                cmd.ExecuteNonQuery();
                connection.Close();

                // Mettre à jour l'interface utilisateur
                ClearFields();
                button1.Text = "Ajouter"; // Réinitialiser le libellé du bouton

                MessageBox.Show("Le produit a été modifié avec succès.");

                // Mettre à jour automatiquement la GridView après la modification du produit
                ChargerDonneesProduits();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification du produit : " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        private void AjouterProduit(string nom, float prix, string url, string description, int quantite, int categorie)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductName, Description, Price, Quantity, CategoryID, ImageURL) VALUES (@Nom, @Description, @Prix, @Quantite, @Categorie, @Url)", connection);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prix", prix);
                cmd.Parameters.AddWithValue("@Url", url);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@Categorie", categorie);

                cmd.ExecuteNonQuery();
                connection.Close();
                ClearFields();
                MessageBox.Show("Le produit est bien ajouté");

                // Mettre à jour automatiquement la GridView après l'ajout d'un nouveau produit
                ChargerDonneesProduits();
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

        private void SupprimerProduit(int productId)
        {
            try
            {
                connection.Open();

                string query = "DELETE FROM Product WHERE ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", productId);
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

        private void ClearFields()
        {
            nomTextBox.Text = "";
            prixTextBox.Text = "";
            urlTextBox.Text = "";
            descriptionTextBox.Text = "";
            quantiteTextBox.Text = "";
            categorieComboBox.SelectedIndex = -1;
        }



        private List<Category> categories = new List<Category>();

        private void ChargerCategories()
        {
            try
            {
                connection.Open();

                string query = "SELECT CategoryID, CategoryName FROM Category";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                categories.Clear(); // Effacer la liste existante

                while (reader.Read())
                {
                    int categoryId = (int)reader["CategoryID"];
                    string categoryName = (string)reader["CategoryName"];
                    Category category = new Category(categoryId, categoryName);
                    categories.Add(category);
                }

                connection.Close();

                // Afficher les catégories dans le ComboBox
                categorieComboBox.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des catégories : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }





        private void ChargerDonneesProduits()
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM Product";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                

                // Liaison des données au DataGridView

                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["ProductID"].Visible = false;
                
                // Définir la propriété AutoSizeMode de chaque colonne sur Fill
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name != "ProductID") // Ne pas modifier la colonne ProductID
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                //MessageBox.Show("Nombre de lignes récupérées : " + dataTable.Rows.Count);

            }
            catch (Exception ex)
            {
                 MessageBox.Show("Erreur lors du chargement des données: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        

        private void label12_Click(object sender, EventArgs e)
        {
            // Fermer la fenêtre
            this.Close();
            // Arrêter l'application
            Application.Exit();
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
                    string productId = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                    string productName = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                    string price = dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                    string description = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                    string quantite = dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                    string url = dataGridView1.Rows[e.RowIndex].Cells["ImageURL"].Value.ToString();
                   // string categorie = dataGridView1.Rows[e.RowIndex].Cells["Categorie"].Value.ToString();


                    // Récupérez d'autres informations nécessaires ici
                    nomTextBox.Text = productName;
                    prixTextBox.Text = price;
                    urlTextBox.Text = url;
                    descriptionTextBox.Text = description;
                    quantiteTextBox.Text = quantite;
                    categorieComboBox.SelectedIndex = -1;

                    // Mettre à jour le libellé du bouton d'action pour indiquer la modification
                    button1.Text = "Modifier";
                    button1.BackColor = Color.Blue;
                }
                else if (result == DialogResult.No) // Supprimer
                {
                    DialogResult confirmResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce produit ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Supprimer la ligne sélectionnée
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        string productId = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                        SupprimerProduit(int.Parse(productId));
                    }
                }
            }
        }

    }
}
