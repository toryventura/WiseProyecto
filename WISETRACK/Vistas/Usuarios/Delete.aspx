<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WISETRACK.Vistas.Usuarios.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group">
                <h2><b>Eliminar Usuario</b></h2>
                <h4><b>¿Estás seguro que quieres eliminar esto?</b></h4>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>Usuario
                    </dt>

                    <dd>
                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Email
                    </dt>

                    <dd>
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Rol
                    </dt>

                    <dd>
                        <asp:Label ID="lblRol" runat="server" Text=""></asp:Label>
                    </dd>

                </dl>
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar" OnClick="btnEliminar_Click"/>
                    | 
                    <a href="/Vistas/Usuarios/Index">Volver átras</a>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
