<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptDetenciones.aspx.cs" Inherits="WISETRACK.RptDetenciones2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid" style="padding-top: 10px">
        <div class="row-fluid">
            <div class="span3">
                <div class="panel panel-primary">
                    <div class="panel-heading">Reporte de Detenciones</div>
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
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblTiempoDet" runat="server" Text="<b>Tiempo (Min)</b>" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <telerik:RadComboBox ID="rcbTipoRel" runat="server" DropDownCssClass="dropdown" AllowCustomText="false" EmptyMessage="" Width="160px" CssClass="dropdown">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Igual A" />
                                    <telerik:RadComboBoxItem Value="1" Text="Mayor A" />
                                    <telerik:RadComboBoxItem Value="2" Text="Menor A" />
                                    <telerik:RadComboBoxItem Value="3" Selected="true" Text="Mayor o Igual A" />
                                    <telerik:RadComboBoxItem Value="4" Text="Menor o igual A" />
                                </Items>
                            </telerik:RadComboBox>
                            <%--<asp:TextBox ID="txbTiempoDet" runat="server" Font-Size="Small" Width="70px" CssClass="input-small" Text="1" TextMode="Number"></asp:TextBox>--%>
                            <input id="txtTiempo" class="input-small" type="number" value="1" style="font-size: small; width: 70px;" />
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <label style="font-size: small; margin-right: 10px">Radio Cobertura</label>
                                <input type="number" style="width: 100px" name="txtcobertura" id="txtcobertura" value="10" />
                            </div>

                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblplaca" runat="server" Text="<b>Móvil (Placa)</b>" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group" style="width: 100%; height: 190px; overflow-y: auto">

                                <div class="col-sm-9 table-responsive" style="padding-left: 0; margin-left: 1%; width: 98%; margin-right: 1%; padding-right: 0px; right: 0%; top: 0px;">
                                    <div id="myGridVehiculos" style="height: 180px; width: 100%" class="ag-theme-balham"></div>
                                </div>
                            </div>

                        </div>

                        <div class="form-inline">
                            <div class="form-group">
                                <input type="button" class="btn btn-primary btn-sm" name="Buscar" value="Buscar" title="Buscar" onclick="CargarReporte();" />
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnExportar" Text="Exportar" CssClass="btn btn-primary btn-sm" OnClick="btnExportar_Click" />
                                <%--<input type="button" class="btn btn-primary btn-sm" name="Exportar" value="Exportar Excel" title="Exportar" onclick="onBtExport();" />--%>
                            </div>

                        </div>
                    </div>

                </div>
                <%--</div>--%>
                <!--/.well -->
            </div>

            <script>
                $(function () {


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
                        <div class="table-responsive" style="width: 100%; height: 436px; overflow-y: auto">

                            <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script>
        var columnDefs = [
            { headerName: "IDReporte", field: "IDReporte", width: 100, hide: true },
            { headerName: "Vehiculo", field: "Vehiculo", width: 150 },
            { headerName: "Conductor", field: "Conductor", width: 100, hide: true },
            { headerName: "Geocerca", field: "Geocerca", width: 100, hide: true },
            { headerName: "Inicio Detencion", field: "FechaInicio", width: 200 },
            { headerName: "Fin Detencion", field: "FechaFin", width: 200 },
            { headerName: "Tiempo(min)", field: "Tiempo", width: 150 },
            { headerName: "Ubicacion", field: "Ubicacion", width: 250 },
            { headerName: "Latitud", field: "Latitud", width: 100, hide: true },
            { headerName: "Longitud", field: "Longitud", width: 100, hide: true },
            { headerName: "Action", width: 80, cellRenderer: verRecord }
        ];


        var columnDefsVehiculo = [
            {
                headerName: "Nro Placa", field: "NroPlaca", width: 100, filterParams: { newRowsAction: 'keep' },
                checkboxSelection: function (params) {
                    // we put checkbox on the name if we are not doing grouping
                    return params.columnApi.getRowGroupColumns().length === 0;
                },
                headerCheckboxSelection: function (params) {
                    // we put checkbox on the name if we are not doing grouping
                    return params.columnApi.getRowGroupColumns().length === 0;
                }
            },
            //{ headerName: '', width: 34, checkboxSelection: true, suppressSorting: true, suppressMenu: true },
            //{ headerName: "Codigo Cliente", field: "CodCliente", width: 150 },
            { headerName: "Patente", field: "Patente", width: 150 },
            { headerName: "Modelo", field: "Modelo", width: 150 }

        ];

        var gridOptionVehiculos = {

            columnDefs: columnDefsVehiculo,

            enableFilter: true,

            rowSelection: 'multiple',
            enableSorting: true,
            showToolPanel: false


        };

        function verRecord(params) {
            //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
            var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "rowVer(\'' + params.data.Longitud + '\',\'' + params.data.Latitud + '\')" >Var Mapa</button>'
            return html;
        }

        function rowVer(longitud, latitud) {
            var ruta = "https://wisetrack.e-tech.com.bo/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud;
            window.open(ruta, '_blank');
        }
        function onSelectionListVehiculos() {
            var selectedRows = gridOptionVehiculos.api.getSelectedRows();
            var selectedRowsString = [];
            selectedRows.forEach(function (selectedRow, index) {
                selectedRowsString.push(selectedRow.NroPlaca);
            });
            return selectedRowsString;
        }
        var gridOptions = {
            // set rowData to null or undefined to show loading panel by default
            // rowData: rowData,
            enableSorting: true,
            enableFilter: true,

            enableColResize: true,
            columnDefs: columnDefs,
            showToolPanel: false,
            rowSelection: 'multiple',
            //enableStatusBar: true,
            //enableRangeSelection: true,
            pagination: true,
            paginationAutoPageSize: true
            //onRowSelected: rowSelectedFunc    
            //onSelectionChanged: onSelectionChanged,
            //rowClicked: onRowClicked

            // custom loading template. the class ag-overlay-loading-center is part of the grid,
            // it gives a white background and rounded border
            //overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>',
            //overlayNoRowsTemplate: '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">This is a custom \'no rows\' overlay</span>'
        };

        function create() {
            var gridDiv = document.querySelector('#myGrid ');
            new agGrid.Grid(gridDiv, gridOptions);
        }

        function onBtExport() {
            //var hoy = new Date();
            //var params = {
            //    skipHeader: false,
            //    skipFooters: true,
            //    skipGroups: true,
            //    fileName: 'ReporteDetenciones' + hoy.format("mdyyhhmmss") + '.xls'
            //};

            //gridOptions.api.exportDataAsCsv(params);
            Exprt();
        }
        var sw = 0;
        function CargarReporte() {
            var list = onSelectionListVehiculos();
            var datepicker1 = $("#datepicker1").data("kendoDatePicker").value();
            var datepicker2 = $("#datepicker2").data("kendoDatePicker").value();
            var datestring1 = kendo.toString(datepicker1, "dd/MM/yyyy");
            var datestring2 = kendo.toString(datepicker2, "dd/MM/yyyy");

            var hora1 = $find("<%=cbohorai.ClientID%>");
            var hora2 = $find("<%=cbohoraf.ClientID  %>");
            var fechaI = datestring1 + ' ' + hora1.get_text();
            var fechaF = datestring2 + ' ' + hora2.get_text();
            var cboCondicion1 = $find("<%=rcbTipoRel.ClientID%>");
            var tiporel = parseInt(cboCondicion1.get_value());
            var tiempoDet = parseInt($('#txtTiempo').val());
            var date1 = new Date(fechaI);
            var date2 = new Date(fechaF);
            var radio = $("#txtcobertura").val();

            //var dataValue = "{ _fini: '" + fini + "', _ffin: '" + ffin + "', placa: '" + placa + "' }";
            if (sw == 0) {
                create();
                sw = 1;
            } else {
                destroy(gridOptions);
                create();
            }

            var action = { lista: list, fechaI: fechaI, fechaF: fechaF, tipoRel: tiporel, tiempoDet: tiempoDet, radio: radio };
            var j = JSON.stringify(action);
            //string[] lista, string fechaI, string fechaF, int tipoRel, int tiempoDet
            $.ajax({
                url: "/RptDetenciones.aspx/CargarReporte",
                data: j,
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                myData = JSON.parse(data.d);
                rowData = myData;
                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                alert("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    alert("Administrador -> " + jqXHR.statusText);
                } else {
                    gridOptions.api.setRowData(myData);

                }
                //DesbloquearPantalla();
            });
        }
        function createGlobal(gridDiv, gridOptions) {

            new agGrid.Grid(gridDiv, gridOptions);
        }
        function destroy(grid) {
            grid.api.destroy();
        }
        function Exprt() {
            var antion = { formato: "0", fechaI: '17/08/2018', fechaF: '18/04/2018' };
            var j = JSON.stringify(antion);
            $.ajax({
                url: "/RptDetenciones.aspx/Exportar",
                type: 'POST',
                cache: false,
                data: j,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                alert("data");
            }).fail(function (jqXHR, textStatus) {
                alert("fail");
            }).always(function (jqXHR, textStatus) {
                alert("always");
            });
        }
        function CargarVehiculos() {


            //string[] lista, string fechaI, string fechaF, int tipoRel, int tiempoDet
            var gridDiv = document.querySelector('#myGridVehiculos');
            createGlobal(gridDiv, gridOptionVehiculos);
            $.ajax({
                url: "/RptDetenciones.aspx/cargarVehiculo",
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                myData = JSON.parse(data.d);
                rowData = myData;
                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                alert("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    alert("Administrador -> " + jqXHR.statusText);
                } else {
                    gridOptionVehiculos.api.setRowData(myData);
                    //setDataSource(myData, gridOptionVehiculos);
                }
                //DesbloquearPantalla();
            });
        }

        $(document).ready(function () {

            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
            CargarVehiculos();
        });
    </script>

</asp:Content>
