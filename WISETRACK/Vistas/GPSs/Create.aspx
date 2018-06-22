<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Vistas.GPSs.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Registrar GPS</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblimei" runat="server" Text="IMEI" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtimei" runat="server" CssClass="form-control" placeHolder="811012839512" type="text" title="IMEI es requerido. Longitud 8-20 digitos" required="true" pattern="[0-9]{8,20}"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="glyphicon glyphicon-search btn btn-default" type="button" onclick="prueba()"></button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblid" runat="server" CssClass="col-sm-2 control-label" Text="ID"></asp:Label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control" type="text" placeHolder="7765" title="ID es requerido. Longitud 3-20 digitos" required="true" pattern="[a-zA-Z0-9]{3,20}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbltelefono" runat="server" CssClass="col-sm-2 control-label" Text="Telefono"></asp:Label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txttelefono" runat="server" CssClass="form-control" placeHolder="76859592" type="text" title="Telefono es requerido. Longitud 7-8 digitos" required="true" pattern="[0-9]{8,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblModelo" runat="server" CssClass="col-sm-2 control-label" Text="Modelo"></asp:Label>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cbomodelo" CssClass="form-control" runat="server">
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
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                    <div>
                        <a href="/Vistas/GPSs/Index">Volver átras</a>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script>
        function DataGPS() {
            this.IMEI;
            this.ID;
            this.NroTelefono;
            this.Modelo;
        }

        function prueba() {
            var imei = document.getElementById('<%=txtimei.ClientID%>').value;
            var httpRequest = new XMLHttpRequest();
            httpRequest.open('POST', '/WebServices/WisetrackServices.asmx/BuscarSiExisteGPS', true);
            httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            httpRequest.send("imei=" + imei);
            httpRequest.onreadystatechange = function () {
                if (httpRequest.readyState == 4 && httpRequest.status == 200) {
                    var resultado = httpRequest.responseText;
                    resultado = resultado.substring(76);
                    resultado = resultado.replace('</string>', '');
                    var myobject = JSON.parse(resultado);
                    RellenarCampos(myobject);
                }
            };
        }

        function RellenarCampos(lista) {
            if (lista == '0') {
                alert('La cuenta GPS esta disponible');
            } else {
                Limpiar();
                alert('La cuenta GPS esta en uso, Favor de elegir otro dispositivo GPS');
            }
        }

        function Limpiar() {
            document.getElementById('<%=txtimei.ClientID %>').value = "";
            document.getElementById('<%=txtid.ClientID %>').value = "";
            document.getElementById('<%=txttelefono.ClientID %>').value = "";
            document.getElementById('<%=cbomodelo.ClientID %>').value = "";
        }

    </script>
</asp:Content>
