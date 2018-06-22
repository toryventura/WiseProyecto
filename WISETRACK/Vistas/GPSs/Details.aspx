<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WISETRACK.Vistas.GPSs.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Detalle GPS</b></h2>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>IMEI</dt>
                    <dd>
                        <asp:Label ID="lblimei" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>ID</dt>
                    <dd>
                        <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Telefono</dt>
                    <dd>
                        <asp:Label ID="lbltelefono" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Modelo</dt>
                    <dd>
                        <asp:Label ID="lblmodelo" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Usuario Registro</dt>
                    <dd>
                        <asp:Label ID="lblusuarioreg" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Registro</dt>
                    <dd>
                        <asp:Label ID="lblfechareg" runat="server" Text=""></asp:Label>
                    </dd>
                </dl>
            </div>
            <div class="table table-condensed table-responsive table-hover">
                <table class="table">
                    <thead>
                        <tr>
                            <th>NIT</th>
                            <th>Razon Social</th>
                            <th>Email Empresa</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblnit" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblrazons" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblemail" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </tbody>
                </table>

                <div class="form-actions no-color">
                    <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Editar" OnClick="btnEditar_Click"  />
                    | 
                    <a href="/Vistas/GPSs/Index">Volver átras</a>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
