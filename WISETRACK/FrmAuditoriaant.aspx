<%@ Page Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmAuditoriaant.aspx.cs" Inherits="WISETRACK.FrmAuditoria" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        #overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #ffffff;
            width: 100%;
            height: 100%;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        #theprogress {
            /*background-color: #D3BB9C;*/
            width: 110px;
            height: 24px;
            text-align: center;
            filter: alpha(opacity=80);
            opacity: 1;
        }

        #modalprogress {
            position: absolute;
            top: 50%;
            left: 50%;
            margin: -11px 0 0 -55px;
            color: white;
        }

        body > #modalprogress {
            position: fixed;
        }
    </style>

    <style type="text/css">
        #map {
            width: auto;
            height: 550px;
        }

        body {
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }

        @media (min-width: 1024px) and (max-width: 1366px) {
            /* Enable use of floated navbar text */
            .navbar-text.pull-right {
                float: none;
                padding-left: 5px;
                padding-right: 5px;
            }

            .alto {
                max-height: 500px;
                min-height: 450px;
                height:450px;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu1").click(function () {
                $("#campo1").slideToggle("slow");
            });
        });
        $(document).ready(function () {
            $("#menu01").click(function () {
                $("#campo01").slideToggle("slow");
            });
        });
        $(document).ready(function () {
            $("#menu2").click(function () {
                $("#campo2").slideToggle("slow");
            });
        });
    </script>

    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.common.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.rtl.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.silver.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.mobile.all.min.css" />

    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2016.1.226/js/angular.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2016.1.226/js/kendo.all.min.js"></script>

    <div class="container-fluid alto" style="padding-top: 33px">
        <div class="row-fluid">
            <div class="span12">
                <div class="span3">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Auditoria</div>
                        <div class="panel-title label-info" style="font-size: small" id="menu1"><b>+ Filtros</b></div>
                        <div class="panel-body" id="campo1">
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblfechaini" runat="server" Text="<b>Fecha Inicio</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <input type="text" name="datepicker1" id="datepicker1" class="form-control"
                                        pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                        title="Favor de rellenar fecha valida" style="font-size: small; width: 160px" />
                                </div>
                                <div class="form-group">
                                    <telerik:RadComboBox ID="cbohorai" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" EmptyMessage="Hora" Width="70px" CssClass="dropdown" OnItemDataBound="cbohorai_ItemDataBound">
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
                                            <telerik:RadComboBoxItem Value="24" Text="23:59" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblfechafin" runat="server" Text="<b>Fecha Fin</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <input type="text" name="datepicker2" id="datepicker2" class="form-control"
                                        pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                        title="Favor de rellenar fecha valida" style="font-size: small; width: 160px" />
                                </div>
                                <div class="form-group">
                                    <telerik:RadComboBox ID="cbohoraf" runat="server" DropDownCssClass="dropdown" AllowCustomText="true"
                                        EmptyMessage="Hora" Width="70px" CssClass="dropdown" OnItemDataBound="cbohoraf_ItemDataBound">
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
                                            <telerik:RadComboBoxItem Value="24" Text="23:59" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblplaca" runat="server" Text="<b>Placa</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:UpdatePanel ID="upcboplaca" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadComboBox ID="cboplaca" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" AutoPostBack="true"
                                            EmptyMessage="Placa" CssClass="dropdown" OnItemDataBound="cboplaca_ItemDataBound"
                                            OnSelectedIndexChanged="cboplaca_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblkmh" runat="server" Text="<b>Km/h</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="dropdown-toggle">
                                        <asp:DropDownList ID="cbokm" CssClass="form-control" runat="server" Style="font-size: smaller">
                                            <asp:ListItem Text="Igual a" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Mayor a" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Mayor o igual a" Value="3" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtkmh" runat="server" Style="font-size: small; width:100px" CssClass="form-control" placeholder="0"
                                        type="number" title="KM es requerido" required="true" pattern="[0-9]{1,2}"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" style="padding-top: 10px"></div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="upbtncargar" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-primary btn-xs" Text="Cargar"
                                                Style="font-size: smaller" OnClick="btnCargar_Click" type="submit" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group">
                                    <asp:UpdatePanel ID="upbtnver" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button ID="btnVer" runat="server" CssClass="btn btn-primary btn-xs" Text="Ver"
                                                Style="font-size: smaller" OnClick="btnVer_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group">
                                    <input id="btnbuscar" type="button" runat="server" class="btn btn-primary btn-xs" value="Pintar"
                                        style="font-size: smaller" onclick="pintarPrueba()" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnExportar" runat="server" Font-Size="Smaller" CssClass="btn btn-primary btn-xs" Text="Exportar"
                                        Style="font-size: smaller" OnClick="btnExportar_Click" />
                                </div>
                                <div class="form-group">
                                    <input id="btnlimpiar" type="button" class="btn btn-primary btn-xs" value="Limpiar"
                                        style="font-size: smaller" onclick="LimpiarAuditoria()" />
                                </div>
                            </div>
                            <div class="form-group">
                            </div>
                            <div class="table-responsive table-hover">
                                <asp:UpdatePanel ID="upgrilla1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gdvListaAudtoria" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                            Font-Size="Smaller" AutoGenerateColumns="False" DataKeyNames="NroPlaca" EmptyDataText="Seleccione una Placa"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableViewState="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbauditoria" runat="server" Font-Size="Smaller" Visible="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NroPlaca" HeaderText="Placa" ReadOnly="true" SortExpression="Placa" />
                                                <asp:BoundField DataField="FechaIni" HeaderText="FechaInicio" SortExpression="FechaIni"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HoraIni" HeaderText="HoraInicio" SortExpression="HoraIni"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaFin" HeaderText="FechaFin" SortExpression="FechaFin"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HoraFin" HeaderText="HoraFin" SortExpression="HoraFin"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="Tipo"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor" HeaderText="Valor" SortExpression="Valor"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="panel-title label-info" style="font-size: small" id="menu01"><b>+ Detalle</b></div>
                        <div class="panel-body" id="campo01" style="display: none">
                            <div class="table-responsive" style="width: 100%; height: 222px; overflow-y: scroll">
                                <asp:UpdatePanel ID="upauditoria" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gdvAuditoria" runat="server" Width="100%"
                                            CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller"
                                            AutoGenerateColumns="False" DataKeyNames="ID" EmptyDataText="Seleccione una placa"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" EnableViewState="true" FooterStyle-Font-Size="Smaller" OnRowDataBound="gdvAuditoria_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" SortExpression="ID" Visible="false"/>
                                                <asp:BoundField DataField="IMEI" HeaderText="IMEI" SortExpression="IMEI" 
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm"
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NroPlaca" HeaderText="Placa" SortExpression="Placa"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" 
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaGPS" HeaderText="FechaGPS" SortExpression="FechaGPS"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" 
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Temperatura" HeaderText="Temperatura" SortExpression="°C"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs" 
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoGPS" HeaderText="EstadoGPS" SortExpression="EstadoGPS"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" 
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Velocidad" HeaderText="Velocidad" SortExpression="Vel"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Asimut" HeaderText="Asimut" SortExpression="Asimut"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" 
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Latitud" HeaderText="Latitud" SortExpression="Lat"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" 
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Longitud" HeaderText="Longitud" SortExpression="Long"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" 
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoMotor" HeaderText="EstadoMotor" SortExpression="EstadoMotor"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" 
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VoltajeBateria" HeaderText="Voltaje" SortExpression="PW"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoPuerta" HeaderText="Apertura" SortExpression="Apertura"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs" 
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs"></ItemStyle>
                                                </asp:BoundField>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%--PARTE 2 --%>
                        <div class="panel-title label-info" style="font-size: small" id="menu2"><b>+ Grupo</b></div>
                        <div class="panel-body" id="campo2">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                </div>
                <script>
                    $(function () {
                        http://dojo.telerik.com/

                            var datepicker1 = $("#datepicker1");

                        datepicker1.kendoMaskedTextBox({
                            mask: "00/00/0000"
                        });

                        datepicker1.kendoDatePicker({
                            format: "dd/MM/yyyy"
                        });

                        datepicker1.closest(".k-datepicker")
                        .add(datepicker1)
                        .removeClass("k-textbox");

                        //combine MaskedTextBox with DatePicker (officially unsupported)
                        var datepicker2 = $("#datepicker2");

                        datepicker2.kendoMaskedTextBox({
                            mask: "00/00/0000"
                        });

                        datepicker2.kendoDatePicker({
                            format: "dd/MM/yyyy"
                        });

                        datepicker2.closest(".k-datepicker")
                        .add(datepicker2)
                        .removeClass("k-textbox");
                    });
                </script>
                <div class="span9">
                    <div id="map"></div>
                    <div>
                        <asp:UpdatePanel ID="udpobtenercadena" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="pruebita1" runat="server" TextMode="MultiLine"
                                    Style="width: 500px; display: none; height: 500px"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upbtnver">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress">
                    <div id="theprogress">
                        <img src="Content/img/tools/load.gif" alt="indicador" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upbtncargar">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress">
                    <div id="theprogress">
                        <img src="Content/img/tools/load.gif" alt="indicador" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        //Variables inicialidas
        var map;
        var markers = [];
        var markersA = [];
        var markersString = [];
        var puntosArray = [];
        var trama = new tramaSerial();
        var linea;
        var dir = '';
        var icon;
        var url;
        var resultado;
        var geocoder;
        //funcion que inicia el mapa
        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -17.783288, lng: -63.1817407 },
                zoom: 10,
                mapTypeId: google.maps.MapTypeId.NORMAL
            });

            geocoder = new google.maps.Geocoder();

            google.maps.event.addListener(map, 'click', function (event) {
                addMarker(event.latLng, map);
            });

            infowindow = new google.maps.InfoWindow();
        }

        //agregar marcador al click
        function addMarker(location, map) {
            deleteMarkers();
            var image = 'Imagenes/icon/ic_action.png';
            var marker = new google.maps.Marker({
                position: location,
                title: '' + location,
                map: map,
                animation: google.maps.Animation.DROP,
                icon: image,
                draggable: true
            });
            markers.push(marker);
        }

        function setMapOnAll(map) {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(map);
                linea.setMap(map);
            }
            markers = [];
        }

        function deleteMarkers() {
            setMapOnAll(null);
        }

        function deleteMarkersA() {
            setMapOnAll(null);
            markersA = [];
            markersString = [];
            puntosArray = [];
        }

        function tramaSerial() {
            this.ID;
            this.IMEI;
            this.EstadoGPS;
            this.Velocidad;
            this.Asimut;
            this.Longitud;
            this.Latitud;
            this.Altitud;
            this.TipoMensaje;
            this.TipoRespuesta;
            this.EstadoMotor;
            this.FechaGPS;
            this.NroPlaca;
            this.IDButton;
            this.Kilometraje;
            this.Temperatura;
            this.VoltajeBateria;
            this.EstadoPuerta;
        }

        function pintarPrueba() {
            var res = document.getElementById('<%= pruebita1.ClientID %>').value;
            var cadena = JSON.parse(res);
            var deserializa = { tramaSerial: cadena }
            if (res.length > 0) {
                var size = deserializa.tramaSerial.length - 1;
                var iniciar = 0;
                for (var i = 0; i < deserializa.tramaSerial.length; i++) {
                    dir = '';
                    var pLatLng = new google.maps.LatLng(parseFloat(deserializa.tramaSerial[i].Latitud), parseFloat(deserializa.tramaSerial[i].Longitud));
                    var slatlng = { lat: parseFloat(deserializa.tramaSerial[i].Latitud), lng: parseFloat(deserializa.tramaSerial[i].Longitud) };

                    var gdvpgeo = document.getElementById("<%= gdvListaAudtoria.ClientID%>");
                    var count = gdvpgeo.rows.length;

                    if (deserializa.tramaSerial[i].Velocidad >= 1) {
                        switch (count) {
                            case 2:
                                url = "/Content/img/flechas/verde/";
                                break;
                            case 3:
                                url = "/Content/img/flechas/rojo/";
                                break;
                            case 4:
                                url = "/Content/img/flechas/naranja/";
                                break;
                            default:
                                url = "/Content/img/flechas/morado/";
                                break;
                        }

                        orientacion(deserializa.tramaSerial[i].Asimut, i, url, count);

                    } else {
                        url = "/Content/img/detenciones/circulo.png";
                        icon = url;
                    }

                    var contentString = '<div id="content">' +
                        '<div id="bodyContent">' +
                        '<p><b>NroPlaca: </b>' + deserializa.tramaSerial[i].NroPlaca + '</p>' +
                        //'<p><b>ID: </b>' + deserializa.tramaSerial[i].IMEI + '</p>' +
                        '<p><b>EstadoGPS: </b>' + deserializa.tramaSerial[i].EstadoGPS + '</p>' +
                        //'<p><b>EstadoMotor: </b>' + deserializa.tramaSerial[i].EstadoMotor + '</p>' +
                        '<p><b>Velocidad: </b>' + deserializa.tramaSerial[i].Velocidad + '</p>' +
                        '<p><b>Temperatura: </b>' + deserializa.tramaSerial[i].Temperatura + ' °C </p>' +
                        //'<p><b>IDButton: </b>' + deserializa.tramaSerial[i].IDButton + '</p>' +
                        '<p><b>Fecha: </b>' + deserializa.tramaSerial[i].FechaGPS + '</p>' +

                        //'<p><b>Kilometraje: </b>' + deserializa.tramaSerial[i].Kilometraje + '</p>' +
                        '<p><b>VoltajeBateria: </b>' + deserializa.tramaSerial[i].VoltajeBateria + '</p>' +
                        '<p><b>EstadoPuerta: </b>' + deserializa.tramaSerial[i].EstadoPuerta + '</p>' +
                        '</div> </div>';

                    switch (i) {
                        case iniciar:
                            var marker = new google.maps.Marker({
                                position: slatlng,
                                title: '#' + i,
                                label: "A",
                                map: map
                            });
                            break;

                        case size:
                            var marker = new google.maps.Marker({
                                position: slatlng,
                                title: '#' + i,
                                label: "B",
                                map: map
                            });
                            break;

                        default:
                            var marker = new google.maps.Marker({
                                position: slatlng,
                                title: '#' + i,
                                icon: icon,
                                map: map
                            });
                            break;
                    }

                    markersA.push(marker);
                    puntosArray.push(slatlng);
                    markersString.push(contentString);

                    google.maps.event.addListener(marker, 'click', function (event) {
                        for (var i = 0; i < markersA.length; i++) {
                            var mresult = markersA[i];
                            if (mresult.position == event.latLng) {
                                var mcad = markersString[i];
                                buscarDireccion(event.latLng);
                                var mensajito = mcad + "<div id='bodyContent2'><p><b>Direccion: </b>" + dir + "</p></div>";
                                infowindow.setContent(mensajito);
                                infowindow.open(map, mresult);
                            }
                        }
                    });

                }
            } else {
                alert("No Disponible");
            }

        }

        function buscarDireccion(slatlng) {
            var direcion = "";
            geocoder.geocode({ 'location': slatlng }, function (results, status) {

                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        direcion = results[0].formatted_address;
                        dir = '' + direcion;
                    } else {
                        direcion = 'No results found';
                        dir = direcion;
                    }
                } else {
                    direcion = 'Geocoder failed due to: ' + status;
                    dir = direcion;
                }
            });
        }

        function orientacion(asimut, i, url, count) {
            if (asimut >= 338) {
                resultado = "norte";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut <= 23) {
                resultado = "norte";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 23 && asimut < 68) {
                resultado = "noreste";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 68 && asimut < 113) {
                resultado = "este";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 113 && asimut < 158) {
                resultado = "sureste";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 158 && asimut < 203) {
                resultado = "sur";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 203 && asimut < 248) {
                resultado = "suroeste";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 248 && asimut < 293) {
                resultado = "oeste";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
            if (asimut >= 293 && asimut < 338) {
                resultado = "noroeste";
                if (count == 2 || count == 3) {
                    icon = url + resultado + ".png";
                } else {
                    icon = url + resultado + ".ico";
                }
            }
        }

        function LimpiarAuditoria() {
            WISETRACK.WebServices.WisetrackServices.LimpiarAuditoria(onSuccess, OnFailed);
        }

        function onSuccess(response) {
            var grillaPrincipal = document.getElementById("<%= gdvListaAudtoria.ClientID%>");
            grillaPrincipal.innerHTML = "";

            var grillaDetalle = document.getElementById("<%= gdvAuditoria.ClientID%>");
            grillaDetalle.innerHTML = "";
            initMap();
        }

        function OnFailed(response) {
            alert("" + response);
        }



    </script>
    <script>
        $(document).ready(function () {
            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
        });
    </script>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6goyEP05rA-H1u7YHb4CLGpBULn21kCY&signed_in=true&callback=initMap">
    </script>

</asp:Content>

