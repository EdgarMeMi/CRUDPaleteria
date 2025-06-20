<%@ Page Title="Generar Venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="WebApp_PW_ISC_2025_T2.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        .venta-container {
            max-width: 600px;
            margin: 50px auto;
            padding: 30px;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0px 0px 15px rgba(0, 0, 0, 0.1);
        }

        .venta-container h1 {
            text-align: center;
            margin-bottom: 30px;
            font-size: 28px;
            color: #333333;
        }

        .venta-container .form-group {
            margin-bottom: 20px;
        }

        .venta-container label {
            font-weight: bold;
            color: #555555;
        }

        .venta-container .btn {
            width: 100%;
            font-size: 16px;
            padding: 10px;
            margin-top: 10px;
            border-radius: 8px;
        }

        .ticket-table {
            margin-top: 30px;
        }
    </style>

    <div class="venta-container">
        <h1>Generar Venta</h1>

        <div class="form-group">
            <label>Selecciona un Producto:</label>
            <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label>Precio Unitario:</label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Stock Disponible:</label>
            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Cantidad a Comprar:</label>
            <asp:TextBox ID="txtCantidadComprar" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Producto" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnCompraFinal" runat="server" Text="Finalizar Compra" CssClass="btn btn-success" OnClick="btnCompraFinal_Click" />

        <div class="ticket-table">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm" CommandName="EliminarItem" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblTotal" runat="server" CssClass="h5 text-right d-block" Text="Total: $0.00"></asp:Label>
        </div>
    </div>
</asp:Content>


