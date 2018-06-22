<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmHerramienta.aspx.cs" Inherits="WISETRACK.FrmHerramienta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="Scripts/jquery-2.1.4.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
    <script src="Scripts/jquery-2.2.0.js"></script>

    <asp:TextBox ID="txtnro" runat="server" Text="mensaje"></asp:TextBox>
    <select id="cbocargar">
        <option value="1">Resultado 1</option>
        <option value="2">Resultado 2</option>
    </select> 

    <input type="text" id="txtmensaje" value="mensaje" />

    <input type="button" id="btnresultado" value="Resultado" onclick="obtener()" />


    <script type="text/javascript">

        function obtener() {
            var nro = $("#txtnro").val();
            var mensaje = $("#txtmensaje").val();
            var cbo = $("#cbocargar").val();

            alert("Nro " + nro + " Mensaje " + mensaje + " Combo " + cbo);
        }
    </script>

</asp:Content>


