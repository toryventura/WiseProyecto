<%@ Page Title="Crear Rol" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Roles.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h2>Crear Rol</h2>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div class="form-horizontal">
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Nombre" CssClass="col-md-2 control-label">Nombre</asp:Label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="Nombre" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Nombre"
                                    CssClass="text-danger" ErrorMessage="El campo de nombre es obligatorio." />
                            </div>

                            <asp:Label runat="server" AssociatedControlID="dpdNiveles" CssClass="col-md-2 control-label" Text="<b>Nivel</b>"/>
                            <div class="col-sm-2">
                                <asp:DropDownList runat="server" ID="dpdNiveles" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="CrearRol_Click" Text="Crear" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                <div>
                    <a href="/Vistas/Roles/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
