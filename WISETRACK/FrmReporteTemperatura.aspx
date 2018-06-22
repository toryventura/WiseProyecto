<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmReporteTemperatura.aspx.cs" Inherits="WISETRACK.FrmReporteTemperatura" %>

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

    <div class="container-fluid" style="padding-top: 33px;">
        <div class="row-fluid">
            <div class="span3">
                <div class="panel panel-primary">
                    <div class="panel-heading">Temperatura</div>
                    <div class="panel-title label-info" style="font-size: small" id="menu1"><b>Filtros</b></div>
                    <div class="panel-body" id="campo1">
                        <div class="form-inline">
                            <div class="form-group">
                                <label id="lblfechaini" for="exampleInputEmail3">Fecha Inicio</label>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <input id="txtfechaini" type="text" name="txtfechaini" class="form-control"
                                    pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                    title="Favor de rellenar fecha valida" />
                                <asp:DropDownList ID="cbohorai" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="00:00"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="00:30"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="01:00"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="01:30"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="02:00"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="02:30"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="03:00"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="03:30"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="04:00"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="04:30"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="05:00"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="05:30"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="06:00"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="06:30"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="07:00"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="07:30"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="08:00"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="08:30"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="09:00"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="09:30"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="10:00"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="10:30"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="11:00"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="11:30"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="12:00"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="12:30"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="13:00"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="13:30"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="14:00"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="14:30"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="15:00"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="15:30"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="16:00"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="16:30"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="17:00"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="17:30"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="18:00"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="18:30"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="19:00"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="19:30"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="20:00"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="20:30"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="21:00"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="21:30"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="22:00"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="22:30"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="23:00"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="23:30"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="23:59"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group"></div>

                        <div class="form-inline">
                            <div class="form-group">
                                <label id="lblfechafin" for="exampleInputEmail3">Fecha Final&nbsp</label>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <input id="txtfechafin" type="text" name="txtfechafin"
                                    pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                    title="Favor de rellenar fecha valida" />
                                <asp:DropDownList ID="cbohoraf" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="00:00"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="00:30"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="01:00"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="01:30"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="02:00"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="02:30"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="03:00"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="03:30"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="04:00"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="04:30"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="05:00"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="05:30"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="06:00"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="06:30"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="07:00"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="07:30"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="08:00"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="08:30"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="09:00"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="09:30"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="10:00"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="10:30"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="11:00"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="11:30"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="12:00"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="12:30"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="13:00"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="13:30"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="14:00"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="14:30"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="15:00"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="15:30"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="16:00"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="16:30"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="17:00"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="17:30"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="18:00"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="18:30"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="19:00"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="19:30"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="20:00"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="20:30"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="21:00"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="21:30"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="22:00"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="22:30"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="23:00"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="23:30"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="23:59"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="form-inline">
                            <div class="form-group">
                                <label id="lblplaca" for="exampleInputEmail3">Nro Placa&nbsp;&nbsp;&nbsp;&nbsp&nbsp</label>
                            </div>
                            <div class="form-group">
                                <asp:UpdatePanel ID="upcboplaca" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboplaca" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdatePanel ID="upcargar" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-primary btn-sm" Text="Filtrar" Style="font-size: smaller" OnClick="btnCargar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnExportar" runat="server" CssClass="btn btn-success btn-sm" Text="Exportar" Style="font-size: smaller" OnClick="btnExportar_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-title label-info" style="font-size: small"><b>Grupo</b></div>
                    <div class="panel-body">
                        <div class="form-group">
                        </div>
                    </div>
                </div>
            </div>
            <div class="span9" style="background-color: transparent">
                <div class="container-fluid">
                    <div class="row-fluid">
                        <div class="table" style="background-color: coral">
                            <div id="container12" style="height: 400px;">
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="table">
                            <div id="myGrid" style="height: 470px; width: 100%" class="ag-fresh"></div>
                        </div>
                        <input type="button" id="btnexportar" value="exportar" onclick="onBtExport()" />
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

        <script src="https://code.highcharts.com/highcharts.js"></script>
        <script src="https://code.highcharts.com/modules/exporting.js"></script>

        <script type="text/javascript" src="../../Content/black/ag-grid-enterprise.js"></script>
        <script type="text/javascript" src="../../Content/js/ControlTemperatura.js"></script>
        <script type="text/javascript">
            $(function () {
                https://dojo.telerik.com/

                    $("#datepicker1").kendoDatePicker({
                        format: "dd/MM/yyyy"
                    });

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
            function errorlog(ms) {
                error(ms);
            }
            function successlog(ms) {
                success(ms);
            }
            function bloquear() {
                BloquearPantalla();
            }
            function desbloquear() {
                DesbloquearPantalla();
            }

            $(function () {
                $('#container12').highcharts({
                    title: {
                        text: 'Monthly Average Temperature',
                        x: -20 //center
                    },
                    subtitle: {
                        text: 'Source: WorldClimate.com',
                        x: -20
                    },
                    xAxis: {
                        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                            'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: 'Temperature (°C)'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },
                    tooltip: {
                        valueSuffix: '°C'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        name: 'Tokyo',
                        data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
                    }, {
                        name: 'New York',
                        data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
                    }, {
                        name: 'Berlin',
                        data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
                    }, {
                        name: 'London',
                        data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                    }]
                });
            });
        </script>
    </div>

</asp:Content>

