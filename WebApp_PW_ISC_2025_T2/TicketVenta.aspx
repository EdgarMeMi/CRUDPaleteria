<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TicketVenta.aspx.cs" Inherits="WebApp_PW_ISC_2025_T2.TicketVenta" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketVenta.aspx.cs" Inherits="WebApp_PW_ISC_2025_T2.TicketVenta" %>
<!DOCTYPE html>
<html>
<head>
    <title>Ticket de Compra</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background: #fdfdfd;
            color: #333;
            padding: 40px;
        }
        .ticket {
            background: #fffaf2;
            border-radius: 12px;
            padding: 30px;
            max-width: 600px;
            margin: auto;
            box-shadow: 0 4px 20px rgba(0,0,0,0.1);
        }
        .ticket h2 {
            text-align: center;
            color: #e57c23;
        }
        .ticket table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .ticket table th, .ticket table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }
        .ticket-total {
            text-align: right;
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
        }
        .btn-print {
            display: block;
            margin: 30px auto 0;
            background: #57cc99;
            color: white;
            border: none;
            padding: 12px 25px;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
        }
        @media print {
            .btn-print { display: none; }
        }
    </style>
</head>
<body>
    <div class="ticket">
        <h2>Ticket de Compra</h2>
        <asp:Repeater ID="rptTicket" runat="server">
            <HeaderTemplate>
                <table>
                    <tr><th>Producto</th><th>Cantidad</th><th>Subtotal</th></tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Nombre") %></td>
                    <td><%# Eval("Cantidad") %></td>
                    <td>$<%# Eval("Subtotal", "{0:F2}") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="ticket-total">
            Total: $<asp:Label ID="lblTotal" runat="server" />
        </div>

        <button class="btn-print" onclick="window.print()">Descargar / Imprimir Ticket</button>
    </div>
</body>
</html>


