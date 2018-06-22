<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.IDButton.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="container-fluid">

            <h3><b>LISTADO DE IDBUTTON</b></h3>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" href="~/Vistas/IDButton/Create">Crear Nuevo</a> |
                    <input type="button" class="btn-link" value="Exportar a Excel" id="pruexcel" onclick="onBtExport();" />
                </div>
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
                
            </div>
        </div>
    </div>


    <!-- Modal esta para eliminar el id button -->
    <div id="myEliminar" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Eliminar IDButton</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="idmensaje"></label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="ok" type="button" class="btn btn-default" data-dismiss="modal" onclick="Actions();">Aceptar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Canselar</button>
                </div>
            </div>
        </div>
    </div>

   
    <script src="../../Content/js/GestionIDButton.js"></script>
</asp:Content>
