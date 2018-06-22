<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.TipoGeocerca.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h3><b>LISTADO TIPO DE ZONAS</b></h3>
            <div class="form-group">
                <a runat="server" href="~/Vistas/TipoGeocerca/Create">Crear Nuevo..</a> |
                <a runat="server" href="#" onclick="onBtExport();">Exportar Excel</a>
            </div>
            
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
            <div class="form-group">
            </div>
        </div>
    </div>

    
    <script src="../../Content/js/GestionTipoZona.js"></script>
</asp:Content>
