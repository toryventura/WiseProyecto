<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.Alarmas.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            
             <h3><b>LISTADO DE ALARMAS</b></h3>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" href="~/Vistas/Alarmas/Create">Crear Nuevo</a> |
                    <input type="button" class="btn-link" value="Exportar a Excel" id="pruexcel" onclick="onBtExport();"/>
                </div>                
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
        </div>
    </div>
    <script src="../../Content/js/GestionAlarma.js"></script>
</asp:Content>
