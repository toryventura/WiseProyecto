﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptEncendidoApagado.aspx.cs" Inherits="WISETRACK.RptEncendidoApagado" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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

        #overlay {
            position: fixed;
            z-index: 98;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
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
                    <div class="panel-heading">Reporte de Motor Encendido y Apagado</div>
                    <div class="panel-title label-info" style="font-size: small"><b>Filtros</b></div>
                    <div class="panel-body" style="height: 100%">
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
                        <div class="form-group"></div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblplaca" runat="server" Text="<b>Móvil (Placa)</b>" Font-Size="Small"></asp:Label>
                            </div>
                        </div>

                        <div class="form-inline">
                            <div class="form-group" style="width: 100%; height: 210px; overflow-y: auto">
                                <div class="col-sm-9 table-responsive" style="padding-left: 0; margin-left: 5%; width: 92%; margin-right: 3%; padding-right: 0px; right: 0%; top: 0px;">
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
                    <div class="panel-body">
                        <div class="table-responsive" style="width: 100%; height: 400px;">
                            <asp:UpdatePanel ID="upresultado" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <rsweb:ReportViewer ID="rptViewer" runat="server" Width="96%" SizeToReportContent="false"></rsweb:ReportViewer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upcargar">
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modalprogress">
                        <div id="theprogress">
                            <img src="Content/img/cargando5.gif" alt="indicador" />
                        </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <script>
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
            $(document).ready(function () {

                var fechaActual = new Date();

                $("#datepicker1").data("kendoDatePicker").value(fechaActual);
                $("#datepicker2").data("kendoDatePicker").value(fechaActual);
            });
        </script>
    </div>
</asp:Content>
