<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Usuarios.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-inline">
                <div class="form-group">
                    <h3><b>LISTADO DE CUENTAS</b></h3>
                    <div class="form-group">
                        <a runat="server" href="~/Account/Register">Crear Nuevo</a> |
                    </div>
                    <input type="button" class="btn-link" value="Exportar a Excel" id="pruexcel" onclick="onBtExport();"/>
                </div>
            </div>
            <div class="form-group"></div>
             <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
            
        </div>
    </div>
    <div class="form-group"></div>
    
    <script src="../../Content/js/GestionCuenta.js"></script>
</asp:Content>
