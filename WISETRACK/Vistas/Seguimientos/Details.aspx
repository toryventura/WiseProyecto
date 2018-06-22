<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WISETRACK.Vistas.Seguimientos.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Detalle Asignacion Seguimiento</b></h2>
            </div>
            <div class="table-responsive table-condensed table-hover">
                <dl class="dl-horizontal">
                    <dt>ID</dt>
                    <dd>
                        <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Inicio</dt>
                    <dd>
                        <asp:Label ID="lblfechai" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Fin</dt>
                    <dd>
                        <asp:Label ID="lblfechaf" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Usuario Registro</dt>
                    <dd>
                        <asp:Label ID="lblusuareg" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha Registro</dt>
                    <dd>
                        <asp:Label ID="lblfechareg" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Estado</dt>
                    <dd>
                        <asp:Label ID="lblestado" runat="server" Text=""></asp:Label>
                    </dd>

                </dl>
            </div>
            <div class="table table-condensed table-responsive table-hover" style="font-size:smaller">
                <h5><b>Empresa</b></h5>
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
                                <asp:Label ID="lblnite" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblrazonse" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblemaile" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
                <h5><b>Vehiculo</b></h5>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Placa</th>
                            <th>Modelo</th>
                            <th>Año</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblplacav" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblmodelov" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblaniov" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
                <h5><b>GPS</b></h5>
                <table class="table">
                    <thead>
                        <tr>
                            <th>IMEI</th>
                            <th>ID</th>
                            <th>Modelo</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblimeig" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblidg" runat="server" Text=""></asp:Label></td>
                            <td>
                                <asp:Label ID="lblmodelog" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </tbody>
                </table>

                <div class="form-actions no-color">
                    <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Editar" OnClick="btnEditar_Click" />
                    | 
                    <a href="/Vistas/Seguimientos/Index">Volver átras</a>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
