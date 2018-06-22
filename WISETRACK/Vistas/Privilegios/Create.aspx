<%@ Page Title="Crear Privilegio" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Privilegios.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h2>Crear Privilegio</h2>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div class="form-horizontal">
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Descripcion" CssClass="col-md-2 control-label">Descripción</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="Descripcion" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Descripcion"
                                    CssClass="text-danger" ErrorMessage="El campo de descripción es obligatorio." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="DirPagina" CssClass="col-md-2 control-label">Dirección de Página</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="DirPagina" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="DirPagina"
                                    CssClass="text-danger" ErrorMessage="El campo de dirección de página es obligatorio." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="CrearPrivilegio_Click" Text="Crear" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            <div>
                <a href="/Vistas/Privilegios/Index">Volver átras</a>
            </div>
        </div>
    </div>
</asp:Content>
