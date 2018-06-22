<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.Personas.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-inline">
                <h3><b>LISTADO DE PERSONAS</b></h3>
                <div class="form-group">
                    <a runat="server" href="~/Vistas/Personas/Create">Crear Nuevo</a> |
                </div>
                <div class="form-group">
                    <a runat="server" href="#" onclick="onBtExport();">Exportar Excel</a> 
                </div>
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
        </div>
    </div>
    <div class="form-group"></div>
    
    
    <script src="../../Content/js/GestionPersona.js"></script>
</asp:Content>
