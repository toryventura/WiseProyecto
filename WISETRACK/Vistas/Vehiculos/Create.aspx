<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Vistas.Vehiculos.Create" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // $('#btnBuscarNroPlaca').click(getContactos);
        });
        function getContactos() {
            //alert("numcaa olvidare"); 
            var nnn = (document.getElementById('<%=txtplaca.ClientID %>').value);
            var actionData = "{'NroPlaca':'" + $.trim(nnn) + "'}";
            $.ajax({
                type: "POST",
                url: "/Vistas/Vehiculos/Create.aspx/ObtenerDatos",
                data: actionData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var contactos = (typeof response.d) == 'string' ?
                               eval('(' + response.d + ')') :
                               response.d;
                    if (contactos != -1) {
                        switch (contactos) {
                            case 1:
                                Limpiar();
                                alert('La cuenta Vehiculo esta en uso, Favor de elegir otro dispositivo Vehiculo');
                               
                                break;
                            case 2:
                                alert('La cuenta Vehiculo esta desactivada disponible');
                                break;
                            case 3:
                                alert('La cuenta Vehiculo esta disponible');
                                break;

                            default:

                        }
                    }
                   
                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        function Limpiar() {
            document.getElementById('<%=txtanio.ClientID %>').value = "";
            document.getElementById('<%=txtchasis.ClientID %>').value = "";
            document.getElementById('<%=txtmotor.ClientID %>').value = "";
            document.getElementById('<%=txtpatente.ClientID %>').value = "";

            document.getElementById('<%=txtplaca.ClientID %>').value = "";
           
        }
    </script>
    <div class="container">
        <div class="row-fluid">
            <h3>REGISTRAR VEHICULO</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label id="lblplaca" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Nro Placa</label>
                        <%--<div class="col-sm-3">
                            <asp:TextBox ID="txtplaca" CssClass="form-control" runat="server" placeHolder="e.g. 2461LKX" type="text" title="Placa es requerido. Longitud 6-7 digitos" required="true" pattern="[0-9]{3,10}-[A-Z]{3,10}"></asp:TextBox>
                        </div>
                        <div class="col-sm-offset-2 col-sm-10">
                            <input type="button" id="btnBuscarNroPlaca" class="btn btn-default" value="buscar" onclick="getContactos();" />
                        </div>--%>
                        <div class="col-sm-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtplaca" CssClass="form-control" runat="server" placeHolder="2461LKX" type="text" 
                                    title="Placa es requerido. Longitud 6-7 digitos" required="true" pattern="[0-9]{3,10}-[A-Z]{3,10}"></asp:TextBox>
                                <span class="input-group-btn">
                                    <input type="button" id="btnBuscarNroPlaca" class="glyphicon glyphicon-search btn btn-default" value="Buscar" onclick="getContactos();" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblchasis" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Nro Chasis</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtchasis" CssClass="form-control" runat="server" placeHolder="e.g. 105231122015" type="text" title="Chasis es requerido. Longitud 6-15 digitos" required="true" pattern="[A-Za-z0-9]{5,15}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="Label1" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Patente</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtpatente" CssClass="form-control" runat="server" placeHolder="Nombre Patente" type="text" 
                                title="Patente del vehiculo que  esta siendo asociado" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblmotor" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Nro Motor</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtmotor" CssClass="form-control" runat="server" placeHolder="e.g. 4000" type="text" title="Motor es requerido. Longitud 3-10 digitos" required="true" pattern="[0-9]{3,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblmodelo" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Modelo</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtmodelo" CssClass="form-control" runat="server" placeHolder="e.g. XTERRA" type="text" title="Modelo es requerido. Longitud 3-10 digitos" required="true" pattern="[A-Za-z0-9 ]{3,30}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lbltipov" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Tipo Vehiculo</label>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList ID="cbotipov" CssClass="form-control" runat="server" placeholder="Tipo Vehiculo" required="true" title="Tipo Vehiculo es requerido"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblmarca" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Marca</label>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList ID="cbomarca" CssClass="form-control" runat="server" placeholder="Marca" required="true" title="Marca es requerido"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblfoto" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Foto</label>
                        <div class="col-sm-3 col-xs-11">
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon3">@</span>
                                <asp:FileUpload ID="fileupvehiculo" CssClass="form-control" text="Buscar" runat="server" aria-describedby="basic-addon3" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblanio" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Año</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtanio" CssClass="form-control" runat="server" placeholder="Año" required="false" title="Año es opcional" pattern="[0-9]{3,4}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                    <div>
                        <a href="/Vistas/Vehiculos/Index">Volver átras</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


