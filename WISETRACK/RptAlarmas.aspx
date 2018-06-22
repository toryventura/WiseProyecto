<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptAlarmas.aspx.cs" Inherits="WISETRACK.RptAlarmas2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                <telerik:RadComboBox ID="cbohorai" runat="server" DropDownCssClass="dropdown" AllowCustomText="true"
                                    EmptyMessage="Hora" Width="70px" CssClass="dropdown">
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
                                    EmptyMessage="Hora" Width="70px" CssClass="dropdown">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblplaca" runat="server" Text="Vehiculo (Placa)" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div id="myGridVehiculos" style="height: 150px; width: 100%" class="ag-theme-balham"></div>
                        </div>

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblTiposAlarma" runat="server" Text="Seleccione uno o varios Tipos de Alarma" Font-Size="Small" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div id="myGridTipoAlarma" style="height: 150px; width: 100%" class="ag-theme-balham"></div>
                        </div>

                        <div class="form-inline">
                            <div class="form-group">
                                <input type="button" name="Buscar" value="Buscar" class="btn btn-primary btn-sm" onclick="CargarReporte();" />
                            </div>
                            <div class="form-group">
                                <input type="button" name="Exportar" value="Exportar" class="btn btn-primary btn-sm" onclick="onBtExport();" />

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
                    <div class="panel-title label-info" style="font-size: small"><b>+ Filtros</b></div>
                    <div class="panel-body">

                        <div class="form-group">
                            <div class="table-responsive" style="width: 100%; height: 500px;">
                                <%--resultado de la reporte --%>
                                <div id="myGridReporte" class="ag-theme-balham" style="height: 500px; width: 100%" ></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script>
        var columnDefsTipoAlarma = [

            {
                headerName: 'Tipo',
                width: 100,
                field: 'CodTipoAlarma',
                headerCheckboxSelection: true,
                headerCheckboxSelectionFilteredOnly: true,
                checkboxSelection: true
            },
        //{ headerName: '', width: 34, checkboxSelection: true, suppressSorting: true, suppressMenu: true },
        //{ headerName: "Codigo Cliente", field: "CodCliente", width: 150 },
        { headerName: "Nombre", field: "Descripcion", width: 200 }


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

        var columnDefsAlertas = [

        { headerName: "Codigo", field: "Codigo", width: 150, hide:true },
        { headerName: "Nombre", field: "Nombre", width: 150 },
        { headerName: "Tipo", field: "Tipo", width: 150 },
        { headerName: "Categoria", field: "Categoria", width: 150 },
        { headerName: "Vehiculo", field: "Vehiculo", width: 150 },
        { headerName: "FechaInicio", field: "FechaInicio", width: 150 },
        { headerName: "FechaFin", field: "FechaFin", width: 150 },
        { headerName: "ValorPerm", field: "ValorPerm", width: 150 },
        { headerName: "ValorReg", field: "ValorReg", width: 150 },
        { headerName: "Ubicacion", field: "Ubicacion", width: 150 },
        { headerName: "Action", width: 80, cellRenderer: verRecord }



        ];
        var gridOptionsAlertas = {


            

            

            enableSorting: true,
            enableFilter: true,
        
        debug: true,
        rowSelection: 'multiple',
        enableColResize: true,
        
        enableRangeSelection: true,
        columnDefs: columnDefsAlertas,
        pagination: true

        };
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
        var gridOptionsTipoAlarma = {


            columnDefs: columnDefsTipoAlarma,

            enableFilter: true,
           
            rowSelection: 'multiple',
            enableSorting: true,
            showToolPanel: false,
           
        };
        function setDataSource(allOfTheData, gridOption) {
            
            gridOption.api.setRowData(allOfTheData);

        }
        function createGlobal(gridDiv, gridOptions) {

            new agGrid.Grid(gridDiv, gridOptions);
        }
        function destroy(grid) {
            grid.api.destroy();
        }
        function onBtExport() {
            var hoy = new Date();
            var params = {
                skipHeader: false,
                skipFooters: true,
                skipGroups: true,
                fileName: 'ReporteDetenciones' + hoy.format("mdyyhhmmss") + '.xls'
            };

            gridOptionsAlertas.api.exportDataAsCsv(params);
        }
        var sw = 0;
        function CargarReporte() {
            var listplacas = onSelectionListVehiculos();
            var listtipos = onSelectionListTiposAlertas();
            var datepicker1 = $("#datepicker1").data("kendoDatePicker").value();
            var datepicker2 = $("#datepicker2").data("kendoDatePicker").value();
            var datestring1 = kendo.toString(datepicker1, "dd/MM/yyyy");
            var datestring2 = kendo.toString(datepicker2, "dd/MM/yyyy");

            var hora1 = $find("<%=cbohorai.ClientID%>");
            var hora2 = $find("<%=cbohoraf.ClientID  %>");
            var fechaI = datestring1 + ' ' + hora1.get_text();
            var fechaF = datestring2 + ' ' + hora2.get_text();

            var gridDiv = document.querySelector("#myGridReporte");
            if (sw == 0) {
                createGlobal(gridDiv, gridOptionsAlertas)
                sw = 1;
            } else {
                destroy(gridOptionsAlertas);
                createGlobal(gridDiv, gridOptionsAlertas);
            }

            var action = { tipos: listtipos, placa: listplacas, fechaI: fechaI, fechaF: fechaF };
            var j = JSON.stringify(action);
            //string[] lista, string fechaI, string fechaF, int tipoRel, int tiempoDet
            $.ajax({
                url: "/RptAlarmas.aspx/CargarDataAlarma",
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
                    //gridOptions.api.setRowData(myData);
                    setDataSource(myData, gridOptionsAlertas);
                }
                //DesbloquearPantalla();
            });
        }
        function onSelectionListVehiculos() {
            var selectedRows = gridOptionVehiculos.api.getSelectedRows();
            var selectedRowsString = [];
            selectedRows.forEach(function (selectedRow, index) {
                selectedRowsString.push(selectedRow.NroPlaca);
            });
            return selectedRowsString;
        }
        function onSelectionListTiposAlertas() {
            var selectedRows = gridOptionsTipoAlarma.api.getSelectedRows();
            var selectedRowsString = [];
            selectedRows.forEach(function (selectedRow, index) {
                selectedRowsString.push(selectedRow.CodTipoAlarma);
            });
            return selectedRowsString;
        }
        function CargarVehiculos() {


            //string[] lista, string fechaI, string fechaF, int tipoRel, int tiempoDet
            var gridDiv = document.querySelector('#myGridVehiculos');
            createGlobal(gridDiv, gridOptionVehiculos);
            $.ajax({
                url: "/RptAlarmas.aspx/cargarVehiculo",
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
                    //gridOptions.api.setRowData(myData);
                    setDataSource(myData, gridOptionVehiculos);
                }
                //DesbloquearPantalla();
            });
        }
        function CargarTipoAlarma() {



            //var action = { lista: list, fechaI: fechaI, fechaF: fechaF, tipoRel: tiporel, tiempoDet: tiempoDet };
            //var j = JSON.stringify(action);
            //string[] lista, string fechaI, string fechaF, int tipoRel, int tiempoDet
            var gridDiv = document.querySelector('#myGridTipoAlarma');
            createGlobal(gridDiv, gridOptionsTipoAlarma);
            $.ajax({
                url: "/RptAlarmas.aspx/CargarTiposAlarma",

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
                    //gridOptions.api.setRowData(myData);
                    setDataSource(myData, gridOptionsTipoAlarma);
                }
                //DesbloquearPantalla();
            });
        }
        

        $(document).ready(function () {

            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
            CargarTipoAlarma();
            CargarVehiculos();

        });
    </script>
</asp:Content>
