<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WISETRACK.Vistas.Seguimientos.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Finalizar Asignacion Seguimiento</b></h2>
                <h4><b>¿Estás seguro que quieres Finalizar esto?</b></h4>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>ID</dt>
                    <dd>
                        <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Inicio</dt>
                    <dd>
                        <asp:Label ID="lblfechai" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Fin</dt>
                    <dd>
                        <asp:Label ID="lblfechaf" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Vehiculo</dt>
                    <dd>
                        <asp:Label ID="lblplaca" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Gps</dt>
                    <dd>
                        <asp:Label ID="lblgps" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Estado</dt>
                    <dd>
                        <asp:Label ID="lblestado" runat="server" Text=""></asp:Label>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar" OnClick="btnEliminar_Click" />
                    | 
                    <a href="/Vistas/Seguimientos/Index">Volver átras</a>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
