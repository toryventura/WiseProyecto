<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptAlarmas2.aspx.cs" Inherits="WISETRACK.RptAlarmas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CheckItemFltColumnas(sender, eventArgs) {
            sender.set_text("Seleccionar Columnas");
        }

        function CheckItemFltTipos(sender, eventArgs) {
            sender.set_text("Seleccionar Tipos");
        }

        function CheckItemFltCategorias(sender, eventArgs) {
            sender.set_text("Seleccionar Categorias");
        }

        function CheckItemFltVehiculos(sender, eventArgs) {
            sender.set_text("Seleccionar Vehículos");
        }

        function CheckAllTiposAlarma(Checkbox) {
            var gdvTiposAlarma = document.getElementById("<%= gdvTiposAlarma.ClientID %>");
            var count = gdvTiposAlarma.rows.length;

            for (i = 1; i <= count; i++) {
                var gdvRow = gdvTiposAlarma.rows[i];
                var ckbTipoAlarma = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                ckbTipoAlarma.checked = Checkbox.checked;
            }
        }

        function UncheckAllTiposAlarma(Checkbox) {
            var gdvTiposAlarma = document.getElementById("<%= gdvTiposAlarma.ClientID %>");
            var ckbAllTipoAlarma = gdvTiposAlarma.rows[0].cells[0].getElementsByTagName("INPUT")[0];

            if (!Checkbox.checked)
                ckbAllTipoAlarma.checked = false;
        }

        window.onload = function () {
            var div = document.getElementById("divScrollTA");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%= Request.Form["div_position"] %>');

            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;

            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
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
                <div class="panel panel-primary">
                    <div class="panel-heading">Reporte de Alarmas</div>
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
                                <telerik:RadComboBox ID="cbohoraf" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" EmptyMessage="Hora" Width="70px" 
                                    CssClass="dropdown" OnItemDataBound="cbohoraf_ItemDataBound">
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
                                <asp:Label ID="lblplaca" runat="server" Text="Vehiculo (Placa)" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <%--<telerik:RadComboBox ID="cboplaca" runat="server" DropDownCssClass="dropdown"
                                AllowCustomText="true" EmptyMessage="Placa" CssClass="dropdown"
                                OnItemDataBound="cboplaca_ItemDataBound">
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

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblTiposAlarma" runat="server" Text="Seleccione uno o varios Tipos de Alarma" Font-Size="Small" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:UpdatePanel ID="udpTiposAlarma" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="table-responsive" id="divScrollTA" style="width: 100%; height: 222px; overflow-y: auto">
                                        <asp:GridView ID="gdvTiposAlarma" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered table-hover" Font-Size="9px" DataKeyNames="CodTipoAlarma"
                                            EmptyDataText="Sin Tipo de Alarmas Disponibles"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="SelecAllTiposAlarma" runat="server" Font-Size="Smaller" onclick="CheckAllTiposAlarma(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="SelecTipoAlarma" runat="server" Font-Size="Smaller" Visible="true" onclick="UncheckAllTiposAlarma(this);" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CodTipoAlarma" HeaderText="Codigo" ReadOnly="true" SortExpression="CodTipoAlarma"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ReadOnly="true" SortExpression="Descripcion"
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
                                    <hr />
                                    <input type="hidden" id="div_position" name="div_position" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdatePanel ID="upcargar" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" Text="Buscar" CssClass="btn btn-primary btn-sm" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnExportar" OnClick="btnExportar_Click" Text="Exportar" Visible="false" CssClass="btn btn-primary btn-sm" />
                            </div>
                        </div>

                    </div>
                </div>
                <%--</div>--%>
                <!--/.well -->
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
                    <%--<asp:UpdatePanel ID="upresultado" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                    <div class="panel-body">
                        <div class="form-group">
                            <div style="display: inline">
                                <telerik:RadComboBox ID="cboFltTipos" runat="server" CheckBoxes="true" Width="217px"
                                    EnableCheckAllItemsCheckBox="false" OnClientItemChecked="CheckItemFltTipos">
                                    <Localization CheckAllString="Seleccionar Tipos" AllItemsCheckedString="Seleccionar Tipos" />
                                </telerik:RadComboBox>
                            </div>
                            <div style="display: inline">
                                <telerik:RadComboBox ID="cboFltCategorias" runat="server" CheckBoxes="true"
                                    EnableCheckAllItemsCheckBox="false" OnClientItemChecked="CheckItemFltCategorias">
                                    <Localization CheckAllString="Seleccionar Categorias" AllItemsCheckedString="Seleccionar Categorias" />
                                </telerik:RadComboBox>
                            </div>
                            <div style="display: inline">
                                <telerik:RadComboBox ID="cboFltVehiculos" runat="server" CheckBoxes="true"
                                    EnableCheckAllItemsCheckBox="false" OnClientItemChecked="CheckItemFltVehiculos">
                                    <Localization CheckAllString="Seleccionar Vehículos" AllItemsCheckedString="Seleccionar Vehículos" />

                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div style="display: inline">
                                <telerik:RadComboBox ID="cboFltColumnas" runat="server" CheckBoxes="true"
                                    EnableCheckAllItemsCheckBox="false" OnClientItemChecked="CheckItemFltColumnas">
                                    <Localization CheckAllString="Seleccionar Columnas" AllItemsCheckedString="Seleccionar Columnas" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="1" Text="Nombre" />
                                        <telerik:RadComboBoxItem Value="2" Text="Tipo" />
                                        <telerik:RadComboBoxItem Value="3" Text="Categoria" />
                                        <telerik:RadComboBoxItem Value="4" Text="Vehículo" />
                                        <telerik:RadComboBoxItem Value="5" Text="Fecha" />
                                        <telerik:RadComboBoxItem Value="6" Text="Valor Permitido" />
                                        <telerik:RadComboBoxItem Value="7" Text="Valor Registrado" />
                                        <%--<telerik:RadComboBoxItem Value="8" Text="Ubicación" />--%>
                                        <telerik:RadComboBoxItem Value="8" Text="Longitud" />
                                        <telerik:RadComboBoxItem Value="9" Text="Latitud" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                            <div style="display: inline">
                                <asp:Button runat="server" ID="btnFiltrar" OnClick="btnFiltrar_Click" Text="Filtrar" CssClass="btn btn-primary btn-sm" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="table-responsive" style="width: 100%; height: 398px; overflow-y: auto">
                                <asp:UpdatePanel ID="upresultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gdvAlarmas" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvAlarmas_RowCommand"
                                            CssClass="table table-striped table-bordered table-hover" Font-Size="10px" DataKeyNames="Codigo"
                                            AllowSorting="true" OnSorting="gdvAlarmas_Sorting"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="true" SortExpression="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categoria" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="Vehiculo" HeaderText="Vehículo" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="ValorPerm" HeaderText="Valor Permitido" SortExpression="ValorPerm" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="ValorReg" HeaderText="Valor Registrado" SortExpression="ValorReg" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <%--<asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" SortExpression="Ubicacion" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />--%>
                                                <asp:BoundField DataField="Longitud" HeaderText="Longitud" ReadOnly="true" SortExpression="Longitud" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                <asp:BoundField DataField="Latitud" HeaderText="Latitud" ReadOnly="true" SortExpression="Latitud" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
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
            var cboFltCols = $find("<%= cboFltColumnas.ClientID %>");
            cboFltCols.set_text("Seleccionar Columnas");

            var cboFltTipos = $find("<%= cboFltTipos.ClientID %>");
            cboFltTipos.set_text("Seleccionar Tipos");

            var cboFltCats = $find("<%= cboFltCategorias.ClientID %>");
            cboFltCats.set_text("Seleccionar Categorias");

            var cboFltVehs = $find("<%= cboFltVehiculos.ClientID %>");
            cboFltVehs.set_text("Seleccionar Vehículos");

            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
        });
    </script>
</asp:Content>
