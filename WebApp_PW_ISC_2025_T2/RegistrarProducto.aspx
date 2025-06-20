<%@ Page Title="Registrar Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarProducto.aspx.cs" Inherits="WebApp_PW_ISC_2025_T2.RegistrarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
     <style>
    body {
        background-image: url('../images/chispas.jpg'); /* Ajusta la ruta */
        background-size: cover;
        background-position: center;
        background-repeat: no-repeat;
    }
</style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
   <script type="text/javascript">
       function soloNumerosEnteros(event) {
           var tecla = event.key;

           // Permitir solo dígitos (0-9) y borrar (Backspace)
           if (!/^\d$/.test(tecla) && tecla !== "Backspace") {
               event.preventDefault();
               return false;
           }
       }

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

    <div class="container" style="display: flex; flex-direction: column; justify-content: center; align-items: center; height: 80vh;">
         <h1 class="text-center" style="margin-bottom: 10px; font-family: 'a Abstract Groovy'; color: #000000;">Registrar producto</h1>
        <table class="table-bordered" style="padding: 25px; border-radius: 10px; box-shadow: 3px 3px 15px gray;">
            <tr>
                <td style="background-color: #FFFFFF;"><strong>ID Producto:</strong></td>
                <td><asp:TextBox ID="txtIdProducto" runat="server" CssClass="form-control" Width="300px" Height="35px" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF;"><strong>Nombre:</strong></td>
                <td><asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="300px" Height="35px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF;"><strong>Descripción:</strong></td>
                <td><asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Width="300px" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF;"><strong>Costo:</strong></td>
                <td><asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" Width="300px" Height="35px" 
    ReadOnly="false" onkeypress="return soloNumerosDecimales(event, this)"></asp:TextBox>
</td>
            </tr>
            <tr>
    <td style="background-color: #FFFFFF;"><strong>Cantidad:</strong></td>
    <td><asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Width="300px" Height="35px" onkeypress="return soloNumerosEnteros(event)"></asp:TextBox>
</td>
</tr>
            <tr>
                <td style="background-color: #FFFFFF;"><strong>Fecha de Ingreso:</strong></td>
                <td><asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" Width="300px" Height="35px"  Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #FFFFFF;">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" Width="140px" Height="40px" OnClick="btnRegistrar_Click"/>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" Width="140px" Height="40px"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
