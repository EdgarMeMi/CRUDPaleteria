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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txt_nombre_id.Text = "";
            txt_id_nombre.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCosto.Text = "";
            txtCantidad.Text = "";
            txtFechaIngreso.Text = "";
            txt_nombre_id.Focus();
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
                txt_id_nombre.Text = reader["IdProducto"].ToString();
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
    }
}