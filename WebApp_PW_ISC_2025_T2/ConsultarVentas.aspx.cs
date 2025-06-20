using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApp_PW_ISC_2025_T2
{
    public partial class ConsultarVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            ConexionChinacatown obj = new ConexionChinacatown();
            using (SqlConnection conn = new SqlConnection(obj.CNX))
            {
                string query = "SELECT IdVenta, Nombre, Cantidad, Total, FechaVenta FROM Ventas ORDER BY FechaVenta DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvVentas.DataSource = dt;
                gvVentas.DataBind();
            }
        }
    }
}
