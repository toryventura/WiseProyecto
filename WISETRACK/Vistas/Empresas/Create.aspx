<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Registrar Empresa</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblNit" runat="server" Text="NIT" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtNit" runat="server" CssClass="form-control" placeholder="Nit" type="text" title="Nit es requerido" required="true" pattern="[A-Za-z0-9]{7,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblRazon_social" runat="server" CssClass="col-sm-2 control-label" Text="Razon Social"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtRazon_social" runat="server" CssClass="form-control" required="true" pattern="[A-Za-z0-9 -ñÑ]{5,150}" placeholder="Nombre Empresa" title="Nombre es requerido"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" CssClass="col-sm-2 control-label" Text="Email"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeHolder="info@example.com" required="true" pattern="[a-z0-9ñÑ._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" title="Email es requerido"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblContacto" runat="server" CssClass="col-sm-2 control-label" Text="Contacto"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control" placeHolder="Nombre Contacto" required="true" pattern="[A-Za-z0-9 -ñÑ]{5,150}" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblemailcontacto" runat="server" CssClass="col-sm-2 control-label" Text="Email Contacto"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEmailcontact" runat="server" CssClass="form-control" placeHolder="info@example.com" required="true" pattern="[a-z0-9ñÑ._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click" />

                        </div>
                    </div>
                    <div>
                        <a href="/Vistas/Empresas/Details">Volver átras</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
