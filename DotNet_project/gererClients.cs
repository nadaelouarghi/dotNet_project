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
    public partial class gererClients : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NAWAL;Initial Catalog=DotNet;Integrated Security=True");
        private readonly string[] roles = { "Admin", "User" };
        public gererClients()
        {
            InitializeComponent();
            // Ajouter les rôles à la ComboBox lors du chargement du formulaire
            Role.Items.AddRange(roles);
            this.Load += GererClients_Load;
            //dataGridView1.CellPainting += DataGridView1_CellPainting;
         }


        private void GererClients_Load(object sender, EventArgs e)
        {
            ChargerDonneesClients();
        }

        private void ChargerDonneesClients()
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM [User]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                // Masquer les colonnes que vous ne voulez pas afficher, par exemple l'ID du client
                dataGridView1.Columns["UserID"].Visible = false;
                dataGridView1.Columns["Password"].Visible = false; // Masquer le mot de passe pour des raisons de sécurité

                // Définir la propriété AutoSizeMode de chaque colonne sur Fill
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name != "UserID" && col.Name != "Password") // Ne pas modifier la colonne ProductID
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

        private void button1_Click(object sender, EventArgs e)
        {
            string nom = Nom.Text;
            string email = Email.Text;
            string password = Password.Text;
            string role = Role.SelectedItem.ToString();

            if (button1.Text == "Ajouter")
            {
                AjouterClient(nom, email, password, role);
            }
            else if (button1.Text == "Modifier")
            {
                ModifierClient(nom, email, password, role);
            }
        }

        private void AjouterClient(string nom, string email, string password, string role)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [User] (UserName, Email, Password, Role) VALUES (@Nom, @Email, @Password, @Role)", connection);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);

                cmd.ExecuteNonQuery();
                connection.Close();

                Nom.Text = "";
                Email.Text = "";
                Password.Text = "";
                Role.SelectedIndex = -1;

                MessageBox.Show("Le client a été ajouté avec succès.");

                ChargerDonneesClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du client : " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void ModifierClient(string nom, string email, string password, string role)
        {
            try
            {
                connection.Open();

                // Récupérer l'ID du client sélectionné
                string userId = dataGridView1.SelectedRows[0].Cells["UserID"].Value.ToString();

                // Mettre à jour les informations du client dans la base de données
                SqlCommand cmd = new SqlCommand("UPDATE [User] SET UserName = @Nom, Email = @Email, Password = @Password, Role = @Role WHERE UserID = @UserID", connection);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@UserID", userId);

                cmd.ExecuteNonQuery();
                connection.Close();

                // Mettre à jour l'interface utilisateur
                ClearFields();
                button1.Text = "Ajouter"; // Réinitialiser le libellé du bouton
                button1.BackColor = Color.MediumSeaGreen;
                MessageBox.Show("Le client a été modifié avec succès.");

                // Mettre à jour automatiquement la GridView après la modification du client
                ChargerDonneesClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification du client : " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }


        private void SupprimerProduit(int userId)
        {
            try
            {
                connection.Open();

                string query = "DELETE FROM [User] WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
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
            Nom.Text = "";
            Email.Text = "";
            Password.Text = "";
            Role.SelectedIndex = -1;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            (new gererCommande()).Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            (new gererClients()).Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            (new gererCategorie()).Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            (new gererProduit()).Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    string userId = dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value.ToString();
                    string userName = dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
                    string email = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                    string password = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                    string role = dataGridView1.Rows[e.RowIndex].Cells["Role"].Value.ToString();

                    // Récupérez d'autres informations nécessaires ici
                    Nom.Text = userName;
                    Email.Text = email;
                    Password.Text = password;
                    //Role.SelectedIndex = -1;
                    Role.Text = role;
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
                        string productId = dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value.ToString();
                        SupprimerProduit(int.Parse(productId));
                    }
                }
            }
        }
    }
}
