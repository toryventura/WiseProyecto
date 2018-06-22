<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="AsignConductor.aspx.cs" Inherits="WISETRACK.Vistas.Vehiculos.AsignConductor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Asignar Conductor</b></h2>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>Placa</dt>
                    <dd>
                        <asp:Label ID="lblplaca" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Nro Chasis</dt>
                    <dd>
                        <asp:Label ID="lblchasis" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Nro Motor</dt>
                    <dd>
                        <asp:Label ID="lblmotor" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Modelo</dt>
                    <dd>
                        <asp:Label ID="lblmodelo" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Año</dt>
                    <dd>
                        <asp:Label ID="lblanio" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt><br /></dt>
                    <dt>Conductor</dt>
                    <dd>
                        <br />
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="dpdConductores" CssClass="form-control" />
                        </div>
                    </dd>
                    <dt><br /></dt>
                    <dt>Fecha</dt>
                    <dd>
                        <br />
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Font-Size="Small" placeholder="25/12/2015" 
                                type="text" title="Fecha es requerido" required="true" pattern="(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[012])/[0-9]{4}"></asp:TextBox>
                        </div>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click"  />
                    | 
                    <a href="/Vistas/Vehiculos/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
