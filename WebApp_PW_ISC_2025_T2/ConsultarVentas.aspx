<%@ Page Title="Consultar Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultarVentas.aspx.cs" Inherits="WebApp_PW_ISC_2025_T2.ConsultarVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .ventas-container {
            max-width: 900px;
            margin: 50px auto;
            background: #ffffff;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 0 15px rgba(0,0,0,0.1);
        }

        .ventas-container h1 {
            text-align: center;
            margin-bottom: 30px;
            font-size: 28px;
            color: #333333;
        }

        .grid-style {
            width: 100%;
            border-collapse: collapse;
            text-align: center;
        }

        .grid-style th {
            background-color: #4CAF50;
            color: white;
            padding: 10px;
        }

        .grid-style td {
            padding: 10px;
            border-bottom: 1px solid #ddd;
        }

        .grid-style tr:hover {
            background-color: #f5f5f5;
        }
    </style>

    <div class="ventas-container">
        <h1>Consulta de Ventas</h1>
        <asp:GridView ID="gvVentas" runat="server" CssClass="grid-style" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IdVenta" HeaderText="ID Venta" />
                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Total" HeaderText="Total ($)" DataFormatString="{0:C}" />
                <asp:BoundField DataField="FechaVenta" HeaderText="Fecha de Venta" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
