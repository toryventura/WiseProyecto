<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WISETRACK.Vistas.GPSs.Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Eliminar GPS</b></h2>
                <h4><b>¿Estás seguro que quieres eliminar esto?</b></h4>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>IMEI</dt>
                    <dd>
                        <asp:Label ID="lblimei" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>ID</dt>
                    <dd>
                        <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Telefono</dt>
                    <dd>
                        <asp:Label ID="lbltelefono" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Modelo</dt>
                    <dd>
                        <asp:Label ID="lblmodelo" runat="server" Text=""></asp:Label>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar" OnClick="btnEliminar_Click" />
                    | 
                    <a href="/Vistas/GPSs/Index">Volver átras</a>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
