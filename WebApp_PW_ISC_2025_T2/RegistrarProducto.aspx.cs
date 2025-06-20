using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace WebApp_PW_ISC_2025_T2
{
    public partial class RegistrarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarIdYFecha();
            }
        }

        private void GenerarIdYFecha()
        {
            Random aleatorio = new Random();
            int idProducto = aleatorio.Next(10000, 99999); // ID aleatorio de 5 dígitos
            txtIdProducto.Text = idProducto.ToString();

            string fechaCorta = DateTime.Now.ToString("dd/MM/yy"); // Fecha actual en formato corto
            txtFechaIngreso.Text = fechaCorta;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar si todos los campos están llenos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtCosto.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MostrarAlerta("Error", "Por favor, llene todos los campos antes de registrar.", "warning");
                return;
            }

            // Validar que el campo Costo sea un número decimal (double)
            double costo;
            if (!double.TryParse(txtCosto.Text, out costo))
            {
                MostrarAlerta("Error", "El costo debe ser un número válido (ejemplo: 123.45).", "warning");
                return;
            }

            // Validar que el campo Cantidad sea un número entero
            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MostrarAlerta("Error", "La cantidad debe ser un número entero válido.", "warning");
                return;
            }

            // Validar que la fecha sea una fecha válida
            DateTime fechaIngreso;
            if (!DateTime.TryParse(txtFechaIngreso.Text, out fechaIngreso))
            {
                MostrarAlerta("Error", "La fecha ingresada no es válida.", "warning");
                return;
            }

            // Proceder a registrar en la base de datos usando parámetros
            ConexionChinacatown obj = new ConexionChinacatown();
            using (SqlConnection sqlc1 = new SqlConnection(obj.CNX))
            {
                SqlCommand cmd = sqlc1.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Productos (IdProducto, Nombre, Descripcion, Costo, Cantidad, Fecha) VALUES (@IdProducto, @Nombre, @Descripcion, @Costo, @Cantidad, @Fecha)";
                cmd.Parameters.AddWithValue("@IdProducto", int.Parse(txtIdProducto.Text));
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@Costo", costo);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Fecha", fechaIngreso);

                sqlc1.Open();
                cmd.ExecuteNonQuery();
                sqlc1.Close();
            }

            // Mostrar mensaje de éxito
            MostrarAlerta("Éxito", "Producto registrado correctamente.", "success");

            // Limpiar campos
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCosto.Text = "";
            txtCantidad.Text = "";
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            string script = $@"
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje}',
                    icon: '{tipo}',
                    confirmButtonText: 'Aceptar'
                }});
            ";

            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }
    }
}
