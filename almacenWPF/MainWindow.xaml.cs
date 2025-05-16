using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace AlmacenAppWPF
{
    public partial class MainWindow : Window
    {
        string connStr = "server=localhost;user id=root;password=;database=almacen;";
        MySqlConnection conn;

        public MainWindow()
        {
            InitializeComponent();
            conn = new MySqlConnection(connStr);
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string nombreInput = textBoxNombre.Text.Trim();
            string contraInput = textBoxContra.Password.Trim();

            if (string.IsNullOrEmpty(nombreInput) || string.IsNullOrEmpty(contraInput))
            {
                MessageBox.Show("Llena ambos campos.");
                return;
            }

            try
            {
                conn.Open();

                string query = @"
                    SELECT 'almacenista' AS rol FROM almacenista 
                    WHERE nombre = @nombre AND contra = @contra
                    UNION
                    SELECT 'tecnico' AS rol FROM tecnico 
                    WHERE nombre = @nombre AND contra = @contra";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreInput);
                    cmd.Parameters.AddWithValue("@contra", contraInput);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string rol = reader.GetString("rol");
                            MessageBox.Show("Bienvenido");

                            if (rol == "almacenista")
                            {
                                textBlockRoleMessage.Text = "Bienvenido, Almacenista";
                                textBlockRoleMessage.Visibility = Visibility.Visible;

                                nombreTag.Visibility = Visibility.Collapsed;
                                contraTag.Visibility = Visibility.Collapsed;
                                textBoxNombre.Visibility = Visibility.Collapsed;
                                textBoxContra.Visibility = Visibility.Collapsed;
                                buttonLogin.Visibility = Visibility.Collapsed;
                                buttonAgregar.Visibility = Visibility.Visible;

                                reader.Close();

                                string piezasQuery = "SELECT nombre, estatus FROM piezas";

                                using (MySqlCommand piezasCmd = new MySqlCommand(piezasQuery, conn))
                                using (MySqlDataReader piezasReader = piezasCmd.ExecuteReader())
                                {
                                    string piezasInfo = "";

                                    while (piezasReader.Read())
                                    {
                                        string nombrePieza = piezasReader.GetString("nombre");
                                        string estatusPieza = piezasReader.GetString("estatus");

                                        piezasInfo += $"- {nombrePieza}: {estatusPieza}\n";
                                    }

                                    textBlockPiezas.Text = piezasInfo;
                                    textBlockPiezas.Visibility = Visibility.Visible;
                                }
                            }
                            if (rol == "tecnico")
                            {
                                textBlockRoleMessage.Text = "Bienvenido, Técnico";

                                textBlockRoleMessage.Visibility = Visibility.Visible;
                                nombreTag.Visibility = Visibility.Collapsed;
                                contraTag.Visibility = Visibility.Collapsed;
                                textBoxNombre.Visibility = Visibility.Collapsed;
                                textBoxContra.Visibility = Visibility.Collapsed;
                                buttonLogin.Visibility = Visibility.Collapsed;
                                buttonRentar.Visibility = Visibility.Visible; 

                                reader.Close();

                                string piezasQuery = "SELECT nombre, estatus FROM piezas";

                                using (MySqlCommand piezasCmd = new MySqlCommand(piezasQuery, conn))
                                using (MySqlDataReader piezasReader = piezasCmd.ExecuteReader())
                                {
                                    string piezasInfo = "";

                                    while (piezasReader.Read())
                                    {
                                        string nombrePieza = piezasReader.GetString("nombre");
                                        string estatusPieza = piezasReader.GetString("estatus");

                                        piezasInfo += $"- {nombrePieza}: {estatusPieza}\n";
                                    }

                                    textBlockPiezas.Text = piezasInfo;
                                    textBlockPiezas.Visibility = Visibility.Visible;
                                }
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void buttonReturnFromAdd_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();

            newToolTag.Visibility = Visibility.Collapsed;
            textBoxNewToolName.Visibility = Visibility.Collapsed;
            buttonConfirmar.Visibility = Visibility.Collapsed;
            buttonReturnFromAdd.Visibility = Visibility.Collapsed;

            textBlockRoleMessage.Visibility = Visibility.Visible;
            try
            {
                string piezasQuery = "SELECT nombre, estatus FROM piezas";

                using (MySqlCommand piezasCmd = new MySqlCommand(piezasQuery, conn))
                using (MySqlDataReader piezasReader = piezasCmd.ExecuteReader())
                {
                    string piezasInfo = "";

                    while (piezasReader.Read())
                    {
                        string nombrePieza = piezasReader.GetString("nombre");
                        string estatusPieza = piezasReader.GetString("estatus");

                        piezasInfo += $"- {nombrePieza}: {estatusPieza}\n";
                    }

                    textBlockPiezas.Text = piezasInfo;
                    textBlockPiezas.Visibility = Visibility.Visible;
                }

                buttonAgregar.Visibility = Visibility.Visible;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void buttonAgregar_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Well, I am working");
            //for debuggin because this son of a button wasn't responding!!!

            newToolTag.Visibility = Visibility.Visible;
            textBoxNewToolName.Visibility = Visibility.Visible;
            buttonConfirmar.Visibility = Visibility.Visible;
            buttonReturnFromAdd.Visibility = Visibility.Visible;
            buttonAgregar.Visibility = Visibility.Visible;

            textBlockRoleMessage.Visibility = Visibility.Collapsed;
            textBlockPiezas.Visibility = Visibility.Collapsed;
            buttonAgregar.Visibility = Visibility.Collapsed;
        }

        private void buttonConfirmar_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string newToolName = textBoxNewToolName.Text.Trim();

            if (string.IsNullOrEmpty(newToolName))
            {
                MessageBox.Show("Ingresa el nombre de la herramienta.");
                return;
            }

            try
            {
                string insertQuery = "INSERT INTO piezas (nombre, estatus) VALUES (@nombre, 'EN ALMACÉN')";

                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", newToolName);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Herramienta agregada exitosamente.");

                        textBoxNewToolName.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar herramienta.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void buttonRentar_Click(object sender, RoutedEventArgs e)
        {
            textBlockRoleMessage.Text = "¿Qué piezas quieres pedir?";

            buttonRentar.Visibility = Visibility.Collapsed;
            buttonRequest1.Visibility = Visibility.Visible;       
            materialesAPedir.Visibility = Visibility.Visible;
            buttonReturnFromRequest.Visibility = Visibility.Visible;
        }

        private void buttonRequest_Click(object sender, RoutedEventArgs e)
        {
            string toolName = materialesAPedir.Text.Trim();

            if (string.IsNullOrEmpty(toolName))
            {
                MessageBox.Show("Por favor, escribe el nombre de la herramienta a solicitar.");
                return;
            }

            try
            {
                conn.Open();

                // First check if the tool exists
                string checkQuery = "SELECT COUNT(*) FROM piezas WHERE nombre = @nombre";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@nombre", toolName);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("Not found.");
                        return;
                    }
                }

                // If it exists, update the estatus to 'PRESTADO'
                string updateQuery = "UPDATE piezas SET estatus = 'PRESTADO' WHERE nombre = @nombre";
                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@nombre", toolName);
                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Herramienta solicitada exitosamente.");
                        materialesAPedir.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al actualizar el estatus.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void buttonReturnFromRequest_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();

            materialesAPedir.Visibility = Visibility.Collapsed;
            buttonRequest1.Visibility = Visibility.Collapsed;
            buttonReturnFromRequest.Visibility = Visibility.Collapsed;
            
            buttonRentar.Visibility = Visibility.Visible;
            textBlockRoleMessage.Text = ("Bienvenido, Técnico");

            try
            {
                string piezasQuery = "SELECT nombre, estatus FROM piezas";

                using (MySqlCommand piezasCmd = new MySqlCommand(piezasQuery, conn))
                using (MySqlDataReader piezasReader = piezasCmd.ExecuteReader())
                {
                    string piezasInfo = "";

                    while (piezasReader.Read())
                    {
                        string nombrePieza = piezasReader.GetString("nombre");
                        string estatusPieza = piezasReader.GetString("estatus");

                        piezasInfo += $"- {nombrePieza}: {estatusPieza}\n";
                    }

                    textBlockPiezas.Text = piezasInfo;
                    textBlockPiezas.Visibility = Visibility.Visible;
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}