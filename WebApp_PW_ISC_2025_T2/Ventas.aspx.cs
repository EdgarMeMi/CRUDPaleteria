using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebApp_PW_ISC_2025_T2
{
    public partial class Ventas : System.Web.UI.Page
    {
        public class ItemVenta
        {
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal => Cantidad * PrecioUnitario;
        }

        protected List<ItemVenta> Ticket
        {
            get
            {
                var ticket = Session["Ticket"] as List<ItemVenta>;
                if (ticket == null)
                {
                    ticket = new List<ItemVenta>();
                    Session["Ticket"] = ticket;
                }
                return ticket;
            }
            set
            {
                Session["Ticket"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                MostrarTicket();
            }
        }

        private void CargarProductos()
        {
            ddlProductos.Items.Clear();
            ConexionChinacatown con = new ConexionChinacatown();
            using (SqlConnection sql = new SqlConnection(con.CNX))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdProducto, Nombre, Descripcion FROM Productos", sql);
                sql.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string nombre = dr["Nombre"].ToString();
                    string desc = dr["Descripcion"].ToString();
                    ddlProductos.Items.Add(new System.Web.UI.WebControls.ListItem(nombre + " - " + desc, dr["IdProducto"].ToString()));
                }
                sql.Close();
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConexionChinacatown con = new ConexionChinacatown();
            using (SqlConnection sql = new SqlConnection(con.CNX))
            {
                SqlCommand cmd = new SqlCommand("SELECT Costo, Cantidad FROM Productos WHERE IdProducto=@id", sql);
                cmd.Parameters.AddWithValue("@id", ddlProductos.SelectedValue);
                sql.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtPrecio.Text = dr["Costo"].ToString();
                    txtStock.Text = dr["Cantidad"].ToString();
                }
                sql.Close();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCantidadComprar.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarMensaje("Error", "Cantidad inválida.", "error");
                return;
            }

            int stock = int.Parse(txtStock.Text);
            if (cantidad > stock)
            {
                MostrarMensaje("Error", "La cantidad solicitada excede el stock.", "error");
                return;
            }

            string nombre = ddlProductos.SelectedItem.Text.Split('-')[0].Trim();
            decimal precio = decimal.Parse(txtPrecio.Text);

            Ticket.Add(new ItemVenta
            {
                Nombre = nombre,
                Cantidad = cantidad,
                PrecioUnitario = precio
            });

            MostrarTicket();

            // Limpiar
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCantidadComprar.Text = "";
        }

        private void MostrarTicket()
        {
            GridView1.DataSource = Ticket;
            GridView1.DataBind();

            decimal total = 0;
            foreach (var item in Ticket)
                total += item.Subtotal;

            lblTotal.Text = "Total: $" + total.ToString("F2");
        }

        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Ticket.RemoveAt(index);
                MostrarTicket();
            }
        }

        protected void btnCompraFinal_Click(object sender, EventArgs e)
        {
            if (Ticket.Count == 0)
            {
                MostrarMensaje("Error", "No hay productos en el ticket.", "warning");
                return;
            }

            string idVenta = new Random().Next(10000, 99999).ToString();
            decimal total = 0;
            int cantidadTotal = 0;
            List<string> nombres = new List<string>();

            foreach (var item in Ticket)
            {
                ConexionChinacatown conCheck = new ConexionChinacatown();
                using (SqlConnection sql = new SqlConnection(conCheck.CNX))
                {
                    sql.Open();
                    SqlCommand check = new SqlCommand("SELECT Cantidad FROM Productos WHERE Nombre = @nombre", sql);
                    check.Parameters.AddWithValue("@nombre", item.Nombre);
                    int stockActual = (int)check.ExecuteScalar();

                    if (item.Cantidad > stockActual)
                    {
                        Ticket.Clear();
                        MostrarTicket();
                        MostrarMensaje("Error", "Stock insuficiente en al menos un producto. Ticket cancelado.", "error");
                        // Guardar ticket en sesión y redirigir a página del ticket
                        Session["TicketVentaFinal"] = Ticket;
                        Session["TotalVentaFinal"] = total;
                        Session["IdVentaFinal"] = idVenta;
                        Response.Redirect("TicketVenta.aspx");

                        return;
                    }
                }

                total += item.Subtotal;
                cantidadTotal += item.Cantidad;
                nombres.Add(item.Nombre);
            }

            string nombreFinal = string.Join(", ", nombres);

            ConexionChinacatown conInsert = new ConexionChinacatown();
            using (SqlConnection sql = new SqlConnection(conInsert.CNX))
            {
                sql.Open();
                SqlCommand insert = new SqlCommand("INSERT INTO Ventas (IdVenta, Nombre, Cantidad, Total, FechaVenta) VALUES (@id, @nombre, @cant, @total, @fecha)", sql);
                insert.Parameters.AddWithValue("@id", idVenta);
                insert.Parameters.AddWithValue("@nombre", nombreFinal);
                insert.Parameters.AddWithValue("@cant", cantidadTotal);
                insert.Parameters.AddWithValue("@total", total);
                insert.Parameters.AddWithValue("@fecha", DateTime.Now);
                insert.ExecuteNonQuery();

                foreach (var item in Ticket)
                {
                    SqlCommand update = new SqlCommand("UPDATE Productos SET Cantidad = Cantidad - @cant WHERE Nombre = @nombre", sql);
                    update.Parameters.AddWithValue("@cant", item.Cantidad);
                    update.Parameters.AddWithValue("@nombre", item.Nombre);
                    update.ExecuteNonQuery();
                }
            }

            Ticket.Clear();
            MostrarTicket();
            MostrarMensaje("Venta Exitosa", "Venta registrada correctamente.", "success");
        }

        private void MostrarMensaje(string titulo, string mensaje, string tipo)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"Swal.fire('{titulo}', '{mensaje}', '{tipo}');", true);
        }
    }
}
