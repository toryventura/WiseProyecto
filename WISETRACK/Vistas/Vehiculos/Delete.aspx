<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WISETRACK.Vistas.Vehiculos.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Eliminar Movil</b></h2>
                <h4><b>¿Estás seguro que quieres eliminar esto?</b></h4>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>Placa</dt>
                    <dd>
                        <asp:Label ID="lblplaca" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Chasis</dt>
                    <dd>
                        <asp:Label ID="lblchasis" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Motor</dt>
                    <dd>
                        <asp:Label ID="lblmotor" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Modelo</dt>
                    <dd>
                        <asp:Label ID="lblmodelo" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Año</dt>
                    <dd>
                        <asp:Label ID="lblanio" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Usuario</dt>
                    <dd>
                        <asp:Label ID="lblusuarioreg" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Registro</dt>
                    <dd>
                        <asp:Label ID="lblfechareg" runat="server" Text=""></asp:Label>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar" OnClick="btnEliminar_Click" />
                    | 
                    <a href="/Vistas/Vehiculos/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
