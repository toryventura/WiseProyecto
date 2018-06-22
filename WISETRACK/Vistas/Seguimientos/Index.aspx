<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.Seguimientos.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>ASIGNACION DE SEGUIMIENTO</h3>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" class="form-control btn-primary" href="~/Vistas/Seguimientos/Create">Crear Nuevo</a>
                </div>
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>

        </div>
    </div>

   
    <script src="../../Content/js/GestionAsignacionSeguimiento.js"></script>

</asp:Content>
