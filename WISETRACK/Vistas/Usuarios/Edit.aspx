<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.Usuarios.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Editar Usuario</h3>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label AssociatedControlID="txbUserName" runat="server" Text="<b>Nombre de Usuario</b>" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-9">
                            <asp:TextBox ID="txbUserName" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label AssociatedControlID="txbEmail" runat="server" CssClass="col-sm-2 control-label" Text="<b>Email</b>" />
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="txbEmail" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txbEmail"
                                CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="dpdPersonas" CssClass="col-md-2 control-label" Text="<b>Asignado a</b>"/>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList runat="server" ID="dpdPersonas" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="dpdRoles" CssClass="col-md-2 control-label" Text="<b>Rol</b>"/>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList runat="server" ID="dpdRoles" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="prueba" class="col-md-2 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnEditar_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/Usuarios/Index">Volver átras</a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
