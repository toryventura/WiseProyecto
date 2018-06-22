<%@ Page Title="Empresas" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Panel.aspx.cs" Inherits="WISETRACK.Vistas.Empresas.Panel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
             <h2><b>Empresas</b></h2>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" href="~/Vistas/Empresas/Create">Crear Nueva..</a>
                    <input type="button" class="btn-link" value="Exportar a Excel" id="pruexcel" onclick="onBtExport();"/>
                </div>
            </div>
        <div class="form-group"></div>
        <div class="table-responsive">
            <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
        </div>
    </div>
    </div>
   <!-- <script type="text/javascript" src="../../Content/black/ag-grid-enterprise.js"></script> -->
    <script src="../../Content/js/GestionPanel.js"></script>

</asp:Content>
