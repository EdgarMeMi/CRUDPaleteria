<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="WebApp_PW_ISC_2025_T2.EliminarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
             <style>
        body {
            background-image: url('../images/nieve2.jpg'); /* Ajusta la ruta */
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
        }
    </style>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script type="text/javascript">
        function soloNumerosDecimales(event, input) {
            var tecla = event.key;
            var valor = input.value;

            // Permitir números (0-9), punto decimal (.), y borrar (Backspace)
            if (!/[\d.]/.test(tecla) && tecla !== "Backspace") {
                event.preventDefault();
                return false;
            }

            // Evitar múltiples puntos decimales
            if (tecla === "." && valor.includes(".")) {
                event.preventDefault();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function validarEntrada(input) {
            input.value = input.value.replace(/[^a-zA-Z0-9 ]/g, '');
        }
    </script>
   <div class="container" style="display: flex; flex-direction: column; justify-content: center; align-items: center; height: 80vh;">
    <h1 class="text-center" style="margin-bottom: 10px; font-family: 'a Abstract Groovy'; color: #FF0000;">Eliminar producto</h1>
    <table class="table-bordered" style="padding: 25px; border-radius: 10px; box-shadow: 2px 2px 10px gray; width: 450px; text-align: left;">
    <tr>
        <td style="background-color: #FFFFFF; width: 200px;"><strong style="color: #FF00FF">ID o Nombre de Producto a Buscar:</strong></td>
        <td style="background-color: #FFFFFF;">
            <asp:TextBox ID="txt_nombre_id" runat="server" CssClass="form-control" Width="100%" Height="35px" oninput="validarEntrada(this)"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="background-color: #FFFFFF;"><strong>ID Producto:</strong></td>
        <td><asp:TextBox ID="txt_id" runat="server" CssClass="form-control" Width="100%" Height="35px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="background-color: #FFFFFF;"><strong>Nombre:</strong></td>
        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="100%" Height="35px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="background-color: #FFFFFF;"><strong>Descripción:</strong></td>
        <td><asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Width="100%" Height="60px" TextMode="MultiLine" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="background-color: #FFFFFF;"><strong>Costo:</strong></td>
        <td><asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" Width="100%" Height="35px" ReadOnly="false" onkeypress="return soloNumerosDecimales(event, this)" Enabled="False"></asp:TextBox></td>
    </tr>
         <tr>
     <td style="background-color: #FFFFFF;"><strong>Cantidad:</strong></td>
     <td><asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Width="100%" Height="35px" ReadOnly="false" onkeypress="return soloNumerosDecimales(event, this)" Enabled="False"></asp:TextBox></td>
 </tr>
    <tr>
        <td style="background-color: #FFFFFF;"><strong>Fecha de Ingreso:</strong></td>
        <td><asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" Width="100%" Height="35px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; background-color: #FFFFFF;">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" Width="140px" Height="40px" OnClick="btnBuscar_Click1" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" Width="140px" Height="40px" OnClick="btnLimpiar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" Width="140px" Height="40px" OnClick="btnEliminar_Click1" />
        </td>
    </tr>
</table>

</div>

</asp:Content>
