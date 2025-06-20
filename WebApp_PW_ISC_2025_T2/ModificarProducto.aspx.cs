using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp_PW_ISC_2025_T2
{
    public partial class ModificarProducto : System.Web.UI.Page
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
            txt_nombre_id.Focus();

            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtCosto.Enabled = false;
            txtCantidad.Enabled = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
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
                txtNombre.Enabled = true;
                txtDescripcion.Enabled = true;
                txtCosto.Enabled = true;
                txtCantidad.Enabled = true;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Sin resultados', 'No se encontró ningún producto con ese ID o nombre.', 'info');", true);
            }

            reader.Close();
            sqlc1.Close();
        }

        protected void btnActu_Click(object sender, EventArgs e)
        {
            string id = txt_id.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string costo = txtCosto.Text.Trim();
            string cantidad = txtCantidad.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Error', 'Primero busca un producto para modificar.', 'warning');", true);
                return;
            }

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(costo) || string.IsNullOrEmpty(cantidad))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Error', 'Nombre, descripción, costo y cantidad no pueden estar vacíos.', 'warning');", true);
                return;
            }

            ConexionChinacatown obj = new ConexionChinacatown();
            SqlConnection sqlc1 = new SqlConnection(obj.CNX);
            SqlCommand cmd = sqlc1.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"UPDATE Productos 
                        SET Nombre = @nombre, 
                            Descripcion = @descripcion, 
                            Costo = @costo, 
                            Cantidad=@cantidad
                        WHERE IdProducto = @id";

            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);
            cmd.Parameters.AddWithValue("@costo", costo);
            cmd.Parameters.AddWithValue("@cantidad", cantidad);
            cmd.Parameters.AddWithValue("@id", id);

            sqlc1.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            sqlc1.Close();

            if (filasAfectadas > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Actualizado', 'Producto actualizado correctamente.', 'success');", true);
                txt_nombre_id.Text = "";
                txt_id.Text = "";
                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtCosto.Text = "";
                txtCantidad.Text = "";
                txtFechaIngreso.Text = "";
                txt_nombre_id.Focus();

                txtNombre.Enabled = false;
                txtDescripcion.Enabled = false;
                txtCosto.Enabled = false;
                txtCantidad.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Error', 'No se pudo actualizar el producto.', 'error');", true);
            }
        }
    }
}