<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.GPSs.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Editar GPS</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblimei" runat="server" Text="<b>IMEI</b>" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtimei" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblid" runat="server" CssClass="col-sm-2 control-label" Text="<b>ID</b>"></asp:Label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control" type="text" placeHolder="e.g. 7765" title="ID es requerido. Longitud 3-20 digitos" required="true" pattern="[a-zA-Z0-9]{3,20}" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbltelefono" runat="server" CssClass="col-sm-2 control-label" Text="<b>Telefono</b>"></asp:Label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txttelefono" runat="server" CssClass="form-control" placeHolder="e.g. 76859592" type="text" title="Telefono es requerido. Longitud 7-8 digitos" required="true" pattern="[0-9]{8,10}" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblModelo" runat="server" CssClass="col-sm-2 control-label" Text="<b>Modelo</b>"></asp:Label>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cbomodelo" runat="server" CssClass="form-control">
                                <asp:ListItem Enabled="true" Value="GV200" Text="GV200"></asp:ListItem>
                                <asp:ListItem Value="GV300" Text="GV300"></asp:ListItem>
                                <asp:ListItem Value="GV500" Text="GV500"></asp:ListItem>
                                <asp:ListItem Value="GV55" Text="GV55"></asp:ListItem>
                                <asp:ListItem Value="TRAXS" Text="TRAXS"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnEditar_Click" />
                        </div>
                    </div>

                </div>
                <div>
                    <a href="/Vistas/GPSs/Index">Volver átras</a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
