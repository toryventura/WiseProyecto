﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.TipoGeocerca.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Editar Tipo de Zona</h3>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div class="form-horizontal">
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtID" CssClass="col-md-2 control-label">ID</asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="txtID" CssClass="form-control" TextMode="SingleLine" Enabled="false"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtID"
                                CssClass="text-danger" ErrorMessage="El campo de Descripción es obligatorio." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Descripcion" CssClass="col-md-2 control-label">Descripción</asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="Descripcion" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Descripcion"
                                CssClass="text-danger" ErrorMessage="El campo de Descripción es obligatorio." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/TipoGeocerca/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
