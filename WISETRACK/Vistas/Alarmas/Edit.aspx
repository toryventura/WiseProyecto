<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.Alarmas.Edit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function errorlog(ms) {
            alert(ms);
        }
        function alertlog(ms) {
            success(ms);
        }

        function deshabilitarGeorcercas() {
            $('#divGeocercas').hide();
            $('#divDataGeocercas').hide();
        }
        function HabilitarGeocercas() {
            $('#divGeocercas').show()
            $('#divDataGeocercas').show();
        }
        function CheckAllGeocercas(Checkbox) {
            var gdvGeocercas = document.getElementById("<%= gdvGeocercas.ClientID %>");
            var count = gdvGeocercas.rows.length;

            for (i = 1; i <= count; i++) {
                var gdvRow = gdvGeocercas.rows[i];
                var ckbGeocerca = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                ckbGeocerca.checked = Checkbox.checked;
            }
        }

        function UncheckAllGeo(Checkbox) {
            var gdvGeocercas = document.getElementById("<%= gdvGeocercas.ClientID %>");
            var ckbAllGeo = gdvGeocercas.rows[0].cells[0].getElementsByTagName("INPUT")[0];

            if (!Checkbox.checked)
                ckbAllGeo.checked = false;
        }

        function CheckAllVehiculos(Checkbox) {
            var gdvVehiculos = document.getElementById("<%= gdvVehiculos.ClientID %>");
            var count = gdvVehiculos.rows.length;

            for (i = 1; i <= count; i++) {
                var gdvRow = gdvVehiculos.rows[i];
                var ckbVehiculo = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                ckbVehiculo.checked = Checkbox.checked;
            }
        }

        function UncheckAllVeh(Checkbox) {
            var gdvVehiculos = document.getElementById("<%= gdvVehiculos.ClientID %>");
            var ckbAllVeh = gdvVehiculos.rows[0].cells[0].getElementsByTagName("INPUT")[0];

            if (!Checkbox.checked)
                ckbAllVeh.checked = false;
        }

        function CheckAllDestinatarios(Checkbox) {
            var gdvDestinatarios = document.getElementById("<%= gdvDestinatarios.ClientID %>");
            var count = gdvDestinatarios.rows.length;

            for (i = 1; i <= count; i++) {
                var gdvRow = gdvDestinatarios.rows[i];
                var ckbDestinatario = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                ckbDestinatario.checked = Checkbox.checked;
            }
        }

        function UncheckAllDest(Checkbox) {
            var gdvDestinatarios = document.getElementById("<%= gdvDestinatarios.ClientID %>");
            var ckbAllDest = gdvDestinatarios.rows[0].cells[0].getElementsByTagName("INPUT")[0];

            if (!Checkbox.checked)
                ckbAllDest.checked = false;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Editar Alarma</h3>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div id="divCodigo" class="form-horizontal">
                    <div class="form-group">
                        <asp:Label AssociatedControlID="txtCodigo" runat="server" Text="<b>Código</b>" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <div id="divNombre" class="form-group">
                        <asp:Label ID="lblNombre" runat="server" CssClass="col-sm-2 control-label" Text="<b>Nombre*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                                title="Nombre es requerido. Mayor a 3 caracteres" required="true" pattern="[A-Za-z0-9 ]{3,20}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="divTipo" class="form-group">
                        <asp:Label ID="lblTipoA" runat="server" CssClass="col-sm-2 control-label" Text="<b>Tipo</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="upTipoA" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox CssClass="dropdown" ID="cboTipoA" AllowCustomText="false"
                                        AutoPostBack="false" runat="server" Font-Size="Small" Width="253px"
                                        DropDownCssClass="dropdown" Enabled="false">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div id="divCategoria" class="form-group">
                        <asp:Label ID="lblCategoriaA" runat="server" CssClass="col-sm-2 control-label" Text="<b>Categoría</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox CssClass="dropdown" ID="cboCategoriaA" AllowCustomText="false"
                                        AutoPostBack="false" runat="server" Font-Size="Small" Width="253px"
                                        DropDownCssClass="dropdown" Enabled="false">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div id="divGeocerca" runat="server" class="form-group">
                        <asp:Label runat="server" ID="lblGeocerca" CssClass="col-sm-2 control-label"><b>Geocerca de Inicio</b></asp:Label>
                        <div class="col-sm-4">
                            <asp:DropDownList runat="server" ID="dpdGeocercas" CssClass="form-control" />
                        </div>
                    </div>
                    <div id="divVelocidad" runat="server" class="form-group">
                        <asp:Label ID="lblVelocidad" runat="server" CssClass="col-sm-2 control-label" Text="<b>Velocidad Máxima (Km/h)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtVelocidad" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Velocidad Máxima" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divVelocidad2" runat="server" class="form-group">
                        <asp:Label ID="lblVelocidad2" runat="server" CssClass="col-sm-2 control-label" Text="<b>Velocidad Mínima (Km/h)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtVelocidad2" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Velocidad Mínima" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divTiempo" runat="server" class="form-group">
                        <asp:Label ID="lblTiempo" runat="server" CssClass="col-sm-2 control-label" Text="<b>Tiempo Máximo (min)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTiempo" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Tiempo Máximo" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divTiempo2" runat="server" class="form-group">
                        <asp:Label ID="lblTiempo2" runat="server" CssClass="col-sm-2 control-label" Text="<b>Tiempo Mínimo (min)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTiempo2" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Tiempo Mínimo" pattern="[0-9]{1,10} "></asp:TextBox>
                        </div>
                    </div>
                    <div id="divDistancia" runat="server" class="form-group">
                        <asp:Label ID="lblDistancia" runat="server" CssClass="col-sm-2 control-label" Text="<b>Distancia Máxima (Km)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtDistancia" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Distancia Máxima" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divDistancia2" runat="server" class="form-group">
                        <asp:Label ID="lblDistancia2" runat="server" CssClass="col-sm-2 control-label" Text="<b>Distancia Mínina (Km)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtDistancia2" runat="server" CssClass="form-control" placeHolder="Distancia Mínima" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divTemperatura" runat="server" class="form-group">
                        <asp:Label ID="lblTemperatura" runat="server" CssClass="col-sm-2 control-label" Text="<b>Temperatura Máxima (ºC)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTemperatura" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Temperatura Máxima" pattern="^-?[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divTemperatura2" runat="server" class="form-group">
                        <asp:Label ID="lblTemperatura2" runat="server" CssClass="col-sm-2 control-label" Text="<b>Temperatura Mínima (ºC)*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTemperatura2" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" placeHolder="Temperatura Mínima" pattern="^-?[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divFechaHora" runat="server" class="form-group">
                        <asp:Label ID="lblFechaHora" runat="server" CssClass="col-sm-2 control-label" Text="<b>Hora Inicio</b>"></asp:Label>
                        <div class="col-sm-9">
                            <telerik:RadComboBox ID="cboFechaHora" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" EmptyMessage="Hora Inicio" Width="85px" CssClass="dropdown" OnItemDataBound="cboFechaHora_ItemDataBound">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="00:00" />
                                    <telerik:RadComboBoxItem Value="1" Text="01:00" />
                                    <telerik:RadComboBoxItem Value="2" Text="02:00" />
                                    <telerik:RadComboBoxItem Value="3" Text="03:00" />
                                    <telerik:RadComboBoxItem Value="4" Text="04:00" />
                                    <telerik:RadComboBoxItem Value="5" Text="05:00" />
                                    <telerik:RadComboBoxItem Value="6" Text="06:00" />
                                    <telerik:RadComboBoxItem Value="7" Text="07:00" />
                                    <telerik:RadComboBoxItem Value="8" Text="08:00" />
                                    <telerik:RadComboBoxItem Value="9" Text="09:00" />
                                    <telerik:RadComboBoxItem Value="10" Text="10:00" />
                                    <telerik:RadComboBoxItem Value="11" Text="11:00" />
                                    <telerik:RadComboBoxItem Value="12" Text="12:00" />
                                    <telerik:RadComboBoxItem Value="13" Text="13:00" />
                                    <telerik:RadComboBoxItem Value="14" Text="14:00" />
                                    <telerik:RadComboBoxItem Value="15" Text="15:00" />
                                    <telerik:RadComboBoxItem Value="16" Text="16:00" />
                                    <telerik:RadComboBoxItem Value="17" Text="17:00" />
                                    <telerik:RadComboBoxItem Value="18" Text="18:00" />
                                    <telerik:RadComboBoxItem Value="19" Text="19:00" />
                                    <telerik:RadComboBoxItem Value="20" Text="20:00" />
                                    <telerik:RadComboBoxItem Value="21" Text="21:00" />
                                    <telerik:RadComboBoxItem Value="22" Text="22:00" />
                                    <telerik:RadComboBoxItem Value="23" Text="23:00" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div id="divFechaHora2" runat="server" class="form-group">
                        <asp:Label ID="lblFechaHora2" runat="server" CssClass="col-sm-2 control-label" Text="<b>Hora Fin</b>"></asp:Label>
                        <div class="col-sm-9">
                            <telerik:RadComboBox ID="cboFechaHora2" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" EmptyMessage="Hora Fin" Width="85px" CssClass="dropdown" OnItemDataBound="cboFechaHora2_ItemDataBound">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="00:00" />
                                    <telerik:RadComboBoxItem Value="1" Text="01:00" />
                                    <telerik:RadComboBoxItem Value="2" Text="02:00" />
                                    <telerik:RadComboBoxItem Value="3" Text="03:00" />
                                    <telerik:RadComboBoxItem Value="4" Text="04:00" />
                                    <telerik:RadComboBoxItem Value="5" Text="05:00" />
                                    <telerik:RadComboBoxItem Value="6" Text="06:00" />
                                    <telerik:RadComboBoxItem Value="7" Text="07:00" />
                                    <telerik:RadComboBoxItem Value="8" Text="08:00" />
                                    <telerik:RadComboBoxItem Value="9" Text="09:00" />
                                    <telerik:RadComboBoxItem Value="10" Text="10:00" />
                                    <telerik:RadComboBoxItem Value="11" Text="11:00" />
                                    <telerik:RadComboBoxItem Value="12" Text="12:00" />
                                    <telerik:RadComboBoxItem Value="13" Text="13:00" />
                                    <telerik:RadComboBoxItem Value="14" Text="14:00" />
                                    <telerik:RadComboBoxItem Value="15" Text="15:00" />
                                    <telerik:RadComboBoxItem Value="16" Text="16:00" />
                                    <telerik:RadComboBoxItem Value="17" Text="17:00" />
                                    <telerik:RadComboBoxItem Value="18" Text="18:00" />
                                    <telerik:RadComboBoxItem Value="19" Text="19:00" />
                                    <telerik:RadComboBoxItem Value="20" Text="20:00" />
                                    <telerik:RadComboBoxItem Value="21" Text="21:00" />
                                    <telerik:RadComboBoxItem Value="22" Text="22:00" />
                                    <telerik:RadComboBoxItem Value="23" Text="23:00" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div id="divTiempoEnvio" runat="server"  class="form-group">
                        <asp:Label ID="lblTiempoEnvio" runat="server" CssClass="col-sm-2 control-label" Text="<b>Tiempo de Envio (Alerta continua) (min)</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTiempoEnvio" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divEstado" class="form-group">
                        <asp:Label ID="lblEstado" runat="server" CssClass="col-sm-2 control-label" Text="Activa"></asp:Label>
                        <div class="col-sm-1">
                            <asp:CheckBox ID="ckbEstado" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div id="divEmail" class="form-group">
                        <asp:Label ID="lblEmail" runat="server" CssClass="col-sm-2 control-label" Text="Email"></asp:Label>
                        <div class="col-sm-1">
                            <asp:CheckBox ID="ckbEmail" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div id="divIntervaloEnvio" class="form-group">
                        <asp:Label ID="lblIntervaloEnvio" runat="server" CssClass="col-sm-2 control-label" Text="<b>Intervalo de Envio por Email (min)</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtIntervaloEnvio" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" pattern="[0-9]{1,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divCantidadEnvio"  class="form-group">
                        <asp:Label ID="lblCantidadEnvio" runat="server" CssClass="col-sm-2 control-label" Text="<b>Cantidad de Emails para Envio</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtCantidadEnvio" runat="server" Onkeydown="return jsDecimals(event);" CssClass="form-control" pattern="[0-9]{1,10}" OnTextChanged="txtCantidadEnvio_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divGeocercas" class="form-group">
                        <asp:Label runat="server" ID="lblGeocercas" CssClass="col-sm-2 control-label"><b>Geocercas</b></asp:Label>
                        <%--<div class="col-sm-1">
                            <input type="checkbox" id="ckbGeocercas" class="form-control" />
                        </div>--%>
                    </div>

                    <div id="divDataGeocercas" class="form-group" style="width: 100%; height: 155px; overflow-y: auto">
                        <asp:Label runat="server" ID="Label1" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-9 table-responsive">
                            <asp:GridView ID="gdvGeocercas" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="CodigoGEO"
                                EmptyDataText="Sin Geocercas Disponibles"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="SelecAllGeo" runat="server" Font-Size="Smaller" onclick="CheckAllGeocercas(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="SelecGeo" runat="server" Font-Size="Smaller" Visible="true" onclick="UncheckAllGeo(this);" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                        <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CodigoGEO" HeaderText="Codigo" ReadOnly="true" SortExpression="CodigoGEO"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ReadOnly="true" SortExpression="Descripcion"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="true" SortExpression="Tipo"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle CssClass="success" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="divVehiculos" class="form-group">
                        <asp:Label runat="server" ID="lblVehiculos" CssClass="col-sm-2 control-label"><b>Vehiculos</b></asp:Label>
                        <%--<div class="col-sm-1">
                            <input type="checkbox" id="ckbVehiculos" class="form-control" onclick="setVehiculos()"/>
                        </div>--%>
                    </div>
                    <div id="divDataVehiculos" runat="server" class="form-group" style="width: 100%; height: 155px; overflow-y: auto">
                        <asp:Label runat="server" ID="Label2" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-9 table-responsive">
                            <asp:GridView ID="gdvVehiculos" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="NroPlaca"
                                EmptyDataText="Sin Vehiculos Disponibles"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="SelecAllVeh" runat="server" Font-Size="Smaller" onclick="CheckAllVehiculos(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="SelecVeh" runat="server" Font-Size="Smaller" Visible="true" onclick="UncheckAllVeh(this);" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                        <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NroPlaca" HeaderText="Placa" ReadOnly="true" SortExpression="NroPlaca"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="true" SortExpression="Tipo"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="true" SortExpression="Marca"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Conductor" HeaderText="Conductor" ReadOnly="true" SortExpression="Conductor"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle CssClass="success" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="divDestinatarios" class="form-group">
                        <asp:Label runat="server" ID="lblDestinatarios" CssClass="col-sm-2 control-label"><b>Destinatarios</b></asp:Label>
                        <%--<div class="col-sm-1">
                            <input type="checkbox" id="ckbDestinatarios" class="form-control" onclick="setDestinatarios()"/>
                        </div>--%>
                    </div>
                    <div id="divDataDestinatarios" runat="server" class="form-group" style="width: 100%; height: 155px; overflow-y: auto">
                        <asp:Label runat="server" ID="Label3" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-9 table-responsive">
                            <asp:GridView ID="gdvDestinatarios" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="CI"
                                EmptyDataText="Sin Destinatarios Disponibles"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gdvDestinatarios_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="SelecAllDest" runat="server" Font-Size="Smaller" onclick="CheckAllDestinatarios(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="SelecDest" runat="server" Font-Size="Smaller" Visible="true" onclick="UncheckAllDest(this);" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                        <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CI" HeaderText="CI" ReadOnly="true" SortExpression="CI"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Apellido(s)" ReadOnly="true" SortExpression="NombreCompleto"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" SortExpression="Email"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" ReadOnly="true" SortExpression="Telefono"
                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle CssClass="success" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-group">
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/Alarmas/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var cboTipoA = $find("<%=cboTipoA.ClientID %>");
            var value1 = cboTipoA.get_value();
            var cboCategoriaA = $find("<%=cboCategoriaA.ClientID %>");
            var value2 = cboCategoriaA.get_value();



            if (value1 != 6) {
                if (value2 == 1 && value1 != 12 && value1 != 13) {
                    deshabilitarGeorcercas();
                } else {
                    HabilitarGeocercas();
                }
            } else {
                HabilitarGeocercas();
            }


        })
    </script>
</asp:Content>
