<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptEstadoPuerta.aspx.cs" Inherits="WISETRACK.RptEstadoPuerta" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CheckItemVehiculo(sender, eventArgs) {
            var selec_item = eventArgs.get_item();
            var value = selec_item.get_value();

            if (value == '0') {
                var items = sender.get_items();

                for (var i = 0; i < items.get_count() ; i++) {
                    var item = items.getItem(i);

                    item.set_checked(selec_item.get_checked());
                }
            }
            else {
                sender.get_items().getItem(0).set_checked(false);
            }

            sender.set_text("Seleccionar Vehículos");
        }
    </script>
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
        }
    </style>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="container-fluid" style="padding-top: 33px">
        <div class="row-fluid">
            <div class="span3">
                <!--Sidebar content-->
                <%--<div class="well sidebar-nav">--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">Reporte de Apertura/Cierre</div>
                    <div class="panel-title label-info" style="font-size: small"><b>Filtros</b></div>
                    <div class="panel-body">
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
                                <telerik:RadComboBox ID="cbohoraf" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" EmptyMessage="Hora" Width="70px" CssClass="dropdown" OnItemDataBound="cbohoraf_ItemDataBound">
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
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblEstado" runat="server" Text="<b>Estado</b>" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <telerik:RadComboBox ID="rcbEstado" runat="server" DropDownCssClass="dropdown" AllowCustomText="false" EmptyMessage="" Width="160px" CssClass="dropdown">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="2" Text="APERTURA/CIERRE" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblplaca" runat="server" Text="<b>Móvil (Placa)</b>" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <%--<telerik:RadComboBox ID="cboplaca" runat="server" AllowCustomText="true" 
                                    OnItemDataBound="cboplaca_ItemDataBound" CheckBoxes="false"
                                    EnableCheckAllItemsCheckBox="false" OnClientItemChecked="CheckItemVehiculo">
                                    <Localization CheckAllString="Seleccionar Vehículos" 
                                        AllItemsCheckedString="Seleccionar Vehículos" />
                                </telerik:RadComboBox>--%>
                                <telerik:RadComboBox
                                    RenderMode="Lightweight"
                                    ID="cboplaca"
                                    runat="server"
                                    EnableLoadOnDemand="true"
                                    Filter="Contains"
                                    AllowCustomText="true"
                                    EmptyMessage="Movil"
                                    ItemsPerRequest="4">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdatePanel ID="upcargar" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" Text="Buscar" CssClass="btn btn-primary btn-sm" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnExportar" OnClick="btnExportar_Click" Text="Exportar" CssClass="btn btn-primary btn-sm" />
                            </div>
                            <div class="form-group">
                                <telerik:RadComboBox ID="rcbFormato" runat="server" DropDownCssClass="dropdown" AllowCustomText="false" EmptyMessage="" Width="100px" CssClass="dropdown">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="0" Text="EXCEL" Selected="true" />
                                        <telerik:RadComboBoxItem Value="1" Text="PDF" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
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
                <div class="panel panel-primary">
                    <div class="panel-heading">Detalle</div>
                    <div class="panel-title label-info" style="font-size: small"><b>Filtros</b></div>
                    <asp:UpdatePanel ID="uprespuesta" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="panel-body">
                                <div class="table-responsive" style="width: 100%; height: 434px; overflow-y: auto">
                                    <asp:GridView ID="gdvEstadosPuerta" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvEstadosPuerta_RowCommand"
                                        CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Vehiculo"
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" ReadOnly="true" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                            <asp:BoundField DataField="FechaApertura" HeaderText="Fecha Apertura" SortExpression="FechaApertura" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                            <asp:BoundField DataField="FechaCierre" HeaderText="Fecha Cierre" SortExpression="FechaCierre" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                            <asp:BoundField DataField="Tiempo" HeaderText="Tiempo (Min)" SortExpression="Tiempo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                            <asp:BoundField DataField="UbicacionA" HeaderText="Ubicacion" ReadOnly="true" SortExpression="UbicaciónA" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                            <asp:BoundField DataField="LongitudA" HeaderText="Longitud" ReadOnly="true" SortExpression="LongitudA" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                            <asp:BoundField DataField="LatitudA" HeaderText="Latitud" ReadOnly="true" SortExpression="LatitudA" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkVerMapa" runat="server" CssClass="alert-link" CommandName="VerMapa" Text="Ver Ubicación" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upcargar">
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

    <script>
        $(document).ready(function () {
            var cboVehiculo = $find("<%= cboplaca.ClientID %>");

            var vehItems = cboVehiculo.get_items();

            for (var i = 0; i < vehItems.get_count() ; i++) {
                var vehItem = vehItems.getItem(i);

                vehItem.set_checked(true);
            }

            cboVehiculo.set_text("Seleccionar Vehículos");

            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
        });
    </script>
</asp:Content>
