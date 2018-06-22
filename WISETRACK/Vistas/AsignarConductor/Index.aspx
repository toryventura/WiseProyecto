<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.AsignarConductor.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="container-fluid">

            <h3><b>LISTADO DE VEHICULOS ASIGNADOS</b></h3>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" href="~/Vistas/AsignarConductor/Create">Asignar Conductor</a> |
                    <input type="button" class="btn-link" value="Exportar a Excel" id="pruexcel" onclick="onBtExport();" />
                </div>
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="listaConductor" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
        </div>
    </div>


    <!-- Modal esta para eliminar el id button -->
    <div id="myEliminar" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Eliminar IDButton</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="idmensaje"></label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="ok" type="button" class="btn btn-default" data-dismiss="modal" onclick="Actions();">Aceptar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    
    <script type="text/javascript">

        var columnDefs = [
        { headerName: "ID", field: "ID", width: 150, hide: true },
        { headerName: "Nombre Conductor", field: "nombreConductor", width: 300 },
        { headerName: "Vehiculo", field: "nroplaca", width: 200 },
        { headerName: "IDButton", field: "keys", width: 200 },
        { headerName: "Fecha Registro", field: "fecha", width: 250 },
        { headerName: "Finalizar", width: 150, cellRenderer: FinalizarRecord }

        ];

        function FinalizarRecord(params) {
            var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Finalizar(' + params.data.ID + ')" >Finalizar</button>';
            return html;
        }
        var ids = 0;
        function Finalizar(cod) {
            ids = cod;
            var mensaje = "Esta seguro de finalizar operacion ?.."
            $('#idmensaje').text(mensaje);
            $('#myEliminar').modal('show');

        }
        //NroTelefono
        var gridOptions = {
            enableSorting: true,
            enableFilter: true,

            enableColResize: true,
            columnDefs: columnDefs,
            showToolPanel: false,
            rowSelection: 'multiple',
            rowModelType: 'pagination'

        };

        function Actions() {
           
            var action = "{'id': '"+ids+"'}";
            $.ajax({
                url: "/Vistas/AsignarConductor/Index.aspx/Finalizar",
                data: action,
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : JSON.parse(response.d);
                if (models[0].Key=="OK") {
                    var ms = models[0].Value;
                    success(ms);
                    destroy();
                    getDatos();
                } else {
                    if (models[0].Key=="ERROR") {
                        error(models[0].Value);
                    }
                }

                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                alert("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    alert("Administrador -> " + jqXHR.statusText);
                } else {
                    //gridOptions.api.setRowData(myData);

                }
                //DesbloquearPantalla();
            });
        }
        function setDataSource(allOfTheData) {
            var dataSource = {
                //rowCount: ???, - not setting the row count, infinite paging will be used
                pageSize: 500,
                overflowSize: 500,
                getRows: function (params) {
                    // this code should contact the server for rows. however for the purposes of the demo,
                    // the data is generated locally, and a timer is used to give the expereince of
                    // an asynchronous call
                    console.log('asking for ' + params.startRow + ' to ' + params.endRow);
                    setTimeout(function () {
                        // take a chunk of the array, matching the start and finish times
                        var rowsThisPage = allOfTheData.slice(params.startRow, params.endRow);
                        var lastRow = -1;
                        // see if we have come to the last page, and if so, return it
                        if (allOfTheData.length <= params.endRow) {
                            lastRow = allOfTheData.length;
                        }
                        params.successCallback(rowsThisPage, lastRow);
                    }, 500);
                }
            };
            gridOptions.api.setDatasource(dataSource);

        }
        function create() {
            var gridDiv = document.querySelector('#listaConductor');
            new agGrid.Grid(gridDiv, gridOptions);
        }



        //**************************---------********************************

        function destroy() {
            gridOptions.api.destroy();
        }

        function getDatos() {
            create();
            var action = "{'data': 'GM'}";
            $.ajax({
                url: "/Vistas/AsignarConductor/Index.aspx/getDatos",
                data: action,
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                myData = JSON.parse(data.d);

                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                alert("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    alert("Administrador -> " + jqXHR.statusText);
                } else {
                    //gridOptions.api.setRowData(myData);
                    setDataSource(myData);
                }
                //DesbloquearPantalla();
            });
        }

        window.onload = getDatos();

    </script>
</asp:Content>
