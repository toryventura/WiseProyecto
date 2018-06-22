<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WISETRACK.Vistas.Geocercas.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group">
                <h2><b>Eliminar Geocerca</b></h2>
                <h4><b>¿Estás seguro que quieres eliminar esto?</b></h4>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>ID
                    </dt>

                    <dd>
                        <asp:Label ID="lblcodigoGeo" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Descripcion
                    </dt>

                    <dd>
                        <asp:Label ID="lbldescripcion" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Color Limite
                    </dt>

                    <dd>
                        <asp:Label ID="lblcolorlim" runat="server" Text=""></asp:Label>
                    </dd>
                    
                    <dt>Color Relleno
                    </dt>

                    <dd>
                        <asp:Label ID="lblcolorrelleno" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Zona
                    </dt>

                    <dd>
                        <asp:Label ID="lblzona" runat="server" Text=""></asp:Label>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar" OnClick="btnEliminar_Click"/>
                    | 
                    <a href="/Vistas/Geocercas/Index">Volver átras</a>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
