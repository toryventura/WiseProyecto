<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.Privilegios.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Editar Privilegio</h3>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label AssociatedControlID="txbCodigo" runat="server" Text="<b>Código</b>" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-9">
                            <asp:TextBox ID="txbCodigo" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label AssociatedControlID="txbDescripcion" runat="server" CssClass="col-sm-2 control-label" Text="<b>Descripción</b>" />
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txbDescripcion" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txbDescripcion"
                                CssClass="text-danger" ErrorMessage="El campo de descripcion es obligatorio." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label AssociatedControlID="txbDirPagina" runat="server" CssClass="col-sm-2 control-label" Text="<b>Dirección de Página</b>" />
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txbDirPagina" CssClass="form-control" TextMode="SingleLine" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnEditar_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/Privilegios/Index">Volver átras</a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
