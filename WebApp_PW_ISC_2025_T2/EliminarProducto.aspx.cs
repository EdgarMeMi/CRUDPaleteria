using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp_PW_ISC_2025_T2
{
    public partial class EliminarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txt_nombre_id.Text = "";
            txt_id.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCosto.Text = "";
            txtCantidad.Text = "";
            txtFechaIngreso.Text = "";

            // Establecer foco en la primera caja de texto
            txt_nombre_id.Focus();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(txt_id.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtCosto.Text) ||
                 string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtFechaIngreso.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Error', 'Todos los campos deben estar llenos para eliminar.', 'error');", true);
                return;
            }

            // Conexión a la base de datos (ajusta la cadena de conexión cuando tengas la BD)
            string connectionString = "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Productos WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", txt_id.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Eliminado', 'Producto eliminado con éxito.', 'success');", true);

                            // Limpiar campos después de eliminar
                            txt_id.Text = "";
                            txtNombre.Text = "";
                            txtDescripcion.Text = "";
                            txtCosto.Text = "";
                            txtCantidad.Text = "";
                            txtFechaIngreso.Text = "";
                            txt_nombre_id.Text = "";
                            txt_nombre_id.Focus();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Error', 'No se encontró el producto.', 'error');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"Swal.fire('Error', 'Error al eliminar: {ex.Message}', 'error');", true);
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txt_nombre_id.Text.Trim();

            // 1️⃣ Validar que el campo no esté vacío
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Error', 'Ingrese un ID o Nombre para buscar.', 'error');", true);
                return;
            }

            // 2️⃣ Validar que no contenga caracteres especiales
            if (!Regex.IsMatch(busqueda, @"^[a-zA-Z0-9 ]+$"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Error', 'No se permiten caracteres especiales.', 'error');", true);
                return;
            }

            // 3️⃣ Conexión a la base de datos (Ajusta la cadena de conexión cuando tengas la BD)
            string connectionString = "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, nombre, descripcion, costo, cantidad, fecha_ingreso FROM Productos WHERE id = @busqueda OR nombre = @busqueda";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@busqueda", busqueda);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read()) // Si encuentra el producto
                        {
                            txt_id.Text = reader["id"].ToString();
                            txtNombre.Text = reader["nombre"].ToString();
                            txtDescripcion.Text = reader["descripcion"].ToString();
                            txtCosto.Text = reader["costo"].ToString();
                            txtCantidad.Text = reader["cantidad"].ToString();
                            txtFechaIngreso.Text = Convert.ToDateTime(reader["fecha_ingreso"]).ToString("dd-MM-yyyy");

                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Éxito', 'Producto encontrado.', 'success');", true);
                        }
                        else // Si no encuentra el producto
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire('Error', 'No se encontró el producto.', 'error');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"Swal.fire('Error', 'Error en la búsqueda: {ex.Message}', 'error');", true);
                }
            }
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {
            string buscar = txt_nombre_id.Text.Trim();

            if (string.IsNullOrEmpty(buscar))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Error', 'Por favor ingresa un ID o nombre de producto.', 'warning');", true);
                return;
            }

            ConexionChinacatown obj = new ConexionChinacatown();
            SqlConnection sqlc1 = new SqlConnection(obj.CNX);
            SqlCommand cmd = sqlc1.CreateCommand();
            cmd.CommandType = CommandType.Text;

            bool esNumero = int.TryParse(buscar, out int idProducto);

            if (esNumero)
            {
                cmd.CommandText = @"SELECT TOP 1 IdProducto, Nombre, Descripcion, Costo, Cantidad, Fecha 
                            FROM Productos 
                            WHERE IdProducto = @id";
                cmd.Parameters.AddWithValue("@id", idProducto);
            }
            else
            {
                cmd.CommandText = @"SELECT TOP 1 IdProducto, Nombre, Descripcion, Costo, Cantidad, Fecha 
                            FROM Productos 
                            WHERE Nombre = @nombre";
                cmd.Parameters.AddWithValue("@nombre", buscar);
            }

            sqlc1.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txt_id.Text = reader["IdProducto"].ToString();
                txtNombre.Text = reader["Nombre"].ToString();
                txtDescripcion.Text = reader["Descripcion"].ToString();
                txtCosto.Text = reader["Costo"].ToString();
                txtCantidad.Text = reader["Cantidad"].ToString();
                txtFechaIngreso.Text = Convert.ToDateTime(reader["Fecha"]).ToString("yyyy-MM-dd");

                // Habilitar campos
            
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Sin resultados', 'No se encontró ningún producto con ese ID o nombre.', 'info');", true);
            }

            reader.Close();
            sqlc1.Close();
        }
        private void LimpiarCampos()
        {
            txt_nombre_id.Text = "";
            txt_id.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCosto.Text = "";
            txtCantidad.Text = "";
            txtFechaIngreso.Text = "";

            txt_id.Enabled = false;
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtCosto.Enabled = false;
            txtCantidad.Enabled = false;
            txtFechaIngreso.Enabled = false;

            txt_nombre_id.Focus();
        }
        protected void btnEliminar_Click1(object sender, EventArgs e)
        {
            string id = txt_id.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Error', 'No hay producto cargado para eliminar.', 'warning');", true);
                return;
            }

            ConexionChinacatown obj = new ConexionChinacatown();
            SqlConnection sqlc1 = new SqlConnection(obj.CNX);
            SqlCommand cmd = sqlc1.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "DELETE FROM Productos WHERE IdProducto = @id";
            cmd.Parameters.AddWithValue("@id", id);

            sqlc1.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            sqlc1.Close();

            if (filasAfectadas > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Eliminado', 'Producto eliminado correctamente.', 'success');", true);
                LimpiarCampos(); // Llamar a tu método de limpiar
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Error', 'No se pudo eliminar el producto.', 'error');", true);
            }
        }
    }
}