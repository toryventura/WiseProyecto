<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WISETRACK.Vistas.Personas.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Eliminar Persona</b></h2>
                <h4><b>¿Estás seguro que quieres eliminar esto?</b></h4>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>CI
                    </dt>

                    <dd>
                        <asp:Label ID="lblCi" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Nombre
                    </dt>

                    <dd>
                        <asp:Label ID="lblnombre" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Apellido Paterno
                    </dt>

                    <dd>
                        <asp:Label ID="lblapellidop" runat="server" Text=""></asp:Label>
                    </dd>
                    
                    <dt>Apellido Materno
                    </dt>

                    <dd>
                        <asp:Label ID="lblapellidom" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Direccion
                    </dt>

                    <dd>
                        <asp:Label ID="lbldireccion" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Telefono
                    </dt>

                    <dd>
                        <asp:Label ID="lbltelefono" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Email
                    </dt>

                    <dd>
                        <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Contacto
                    </dt>

                    <dd>
                        <asp:Label ID="lblcontacto" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Telefono Contacto
                    </dt>

                    <dd>
                        <asp:Label ID="lbltelefonoc" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Licencia Conducir
                    </dt>

                    <dd>
                        <asp:Label ID="lbllicencia" runat="server" Text=""></asp:Label>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar" OnClick="btnEliminar_Click"/>
                    | 
                    <a href="/Vistas/Personas/Index">Volver átras</a>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
