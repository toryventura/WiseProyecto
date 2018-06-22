<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.GPSs.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>LISTADO DE GPS</h3>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" href="~/Vistas/GPSs/Create">Crear Nuevo</a> |
                </div>
                <div class="form-group">
                    <a runat="server" href="~/Vistas/Seguimientos/Index">Ver Seguimiento</a> |
                </div>
                
                <input type="button" class="btn-link" value="Exportar a Excel" id="pruexcel" onclick="onBtExport();"/>
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
            
        </div>
    </div>
    
    <script type="text/javascript" src="../../Content/black/ag-grid-enterprise.js"></script>
    <script src="../../Content/js/GestionGps.js"></script>
</asp:Content>
