using System;
using System.Collections.Generic;

namespace WebApp_PW_ISC_2025_T2
{
    public partial class TicketVenta : System.Web.UI.Page
    {
        public class ItemVenta
        {
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["DatosVenta"] != null)
            {
                var ticket = Session["DatosVenta"] as List<Ventas.ItemVenta>;
                var total = Session["TotalVenta"] != null ? (decimal)Session["TotalVenta"] : 0;
                lblTotal.Text = total.ToString("F2");

                List<ItemVenta> datos = new List<ItemVenta>();
                foreach (var item in ticket)
                {
                    datos.Add(new ItemVenta
                    {
                        Nombre = item.Nombre,
                        Cantidad = item.Cantidad,
                        Subtotal = item.Subtotal
                    });
                }

                rptTicket.DataSource = datos;
                rptTicket.DataBind();
            }
        }
    }
}
