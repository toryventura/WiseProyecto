///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [

{ headerName: "Razon Social", field: "RazonSocial", width: 150 },
{ headerName: "NroPlaca", field: "NroPlaca", width: 100 },
{ headerName: "NroChasis", field: "NroChasis", width: 100 },
{ headerName: "Modelo", field: "Modelo", width: 100 },
{ headerName: "Año", field: "Año", width: 150 },
{ headerName: "Estado", field: "Estado", width: 120 },
//{ headerName: "Conductor", field: "Conductor", width: 100 },
{ headerName: "Conductor", width:150,cellRenderer:conductorRecord },
{ headerName: "EstadoVehiculo", field: "EstadoVehiculo", width: 100, hide: true },
{ headerName: "EstadoEmpresaVehiculo", field: "EstadoEmpresaVehiculo", width: 130, hide: true },
{ headerName: "tipo", field: "tipo", width: 130, hide: true },
{ headerName: "marca", field: "marca", width: 100, hide: true },
{ headerName: "Action", width: 80, cellRenderer: editRecord },
{ headerName: "Action", width: 80, cellRenderer: detailRecord },
{ headerName: "Action", width: 80, cellRenderer: deleteRecord }
];

var rowData = [];

function conductorRecord(params) {
    var html = '<p>' + params.data.Conductor + '</p>';
    //if (params.data.Conductor=="(No Asignado)") {
    //    html = '<button type="button" id="idbutton"  data-action-type="view1" class="btn-link btn-xs" onclick= "asignarEdit(\'' + params.data.NroPlaca + '\')">Asignar</button>';
    //} else {
    
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    //var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "rowEdit(\'' + params.data.NroPlaca + '\',\'' + params.data.Estado + '\')">Editar</button>'
    return html;
}


function editRecord(params) {
    
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "rowEdit(\'' + params.data.NroPlaca + '\',\'' + params.data.Estado + '\')">Editar</button>'
    return html;
}

function detailRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton1" data-action-type="view2" class="btn-link btn-xs" onclick= "rowDetail(\'' + params.data.NroPlaca + '\',\'' + params.data.Estado + '\')" >Ver Detalle</button>'
    return html;
}

function deleteRecord(params) {
    var html = '<button type="button" id="idbutton2" data-action-type="view3" class="btn-link btn-xs" onclick= "rowDelete(\'' + params.data.NroPlaca + '\',\'' + params.data.Estado + '\')" >Eliminar</button>'
    return html;
}

//function asignarEdit(nroplaca) {
//    $('.modal fade').show();
//    //alert(""+nroplaca);
//}

function rowEdit(nroplaca, activo) {

    if (activo != "De baja") {
        window.location = '/Vistas/Vehiculos/Edit?placa=' + nroplaca + '';
    } else {
        alert("Informacion: El vehiculo ya ha sido dado de baja");
    }
}

function rowDetail(nroplaca, activo) {

    if (activo != "De baja") {
        window.location = '/Vistas/Vehiculos/Details?placa=' + nroplaca + '';
    } else {
        alert("Informacion: El vehiculo ya ha sido dado de baja");
    }
}

function rowDelete(nroplaca, activo) {

        window.location = '/Vistas/Vehiculos/Delete?placa=' + nroplaca + '';
    
}

var gridOptions = {
    // set rowData to null or undefined to show loading panel by default
    // rowData: rowData,
    enableSorting: true,
    enableFilter: true,

    enableColResize: true,
    columnDefs: columnDefs,
    showToolPanel: false,
   // rowSelection: 'multiple',
    //enableStatusBar: true,
    //enableRangeSelection: true,
    pagination: true,
    paginationAutoPageSize:true
    //onRowSelected: rowSelectedFunc,
    //onSelectionChanged: onSelectionChanged,
    //rowClicked: onRowClicked

    // custom loading template. the class ag-overlay-loading-center is part of the grid,
    // it gives a white background and rounded border
    //overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>',
    //overlayNoRowsTemplate: '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">This is a custom \'no rows\' overlay</span>'
};

//function onRowClicked(e) {
//    alert("hola");
//    if (e.event.target !== undefined) {
//        let data = e.data;
//        let actionType = e.event.target.getAttribute("data-action-type");

//        switch (actionType) {
//            case "view":
//                return this.onActionViewClick(data);
//            case "remove":
//                return this.onActionRemoveClick(data);
//        }
//    }
//}

//var gridOptions = {
//    enableSorting: true,
//    enableFilter: false,
//   // debug: true,
//    rowSelection: 'multiple',
//   // enableColResize: true,
//    paginationPageSize: 500,
//    columnDefs: columnDefs,
//    pagination: true,
//    suppressPaginationPanel: true,
//    suppressScrollOnNewData: true
// //   onPaginationChanged: onPaginationPageLoaded
//};

function create() {
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

function onPaginationPageLoaded() {
    console.log('onPaginationPageLoaded');

    setText('#lbLastPageFound', gridOptions.api.paginationIsLastPageFound());
    setText('#lbPageSize', gridOptions.api.paginationGetPageSize());
    // we +1 to current page, as pages are zero based
    setText('#lbCurrentPage', gridOptions.api.paginationGetCurrentPage() + 1);
    setText('#lbTotalPages', gridOptions.api.paginationGetTotalPages());

    setLastButtonDisabled(!gridOptions.api.paginationIsLastPageFound());
}

function onSelectionChanged() {
    var selectedRows = gridOptions.api.getSelectedNodesById();
    var selectedRowsStringchasis = '';
    var selectedRowsStringmodelo = '';
    var selectedRowsStringNroPlaca = '';
    var selectedRowsStringaño = '';
    selectedRows.forEach(function (selectedRow, index) {
        if (index != 0) {
            selectedRowsStringchasis += ', ';
            selectedRowsStringmodelo += ', ';
            selectedRowsStringNroPlaca += ', ';
            selectedRowsStringaño += ', ';
        }
        selectedRowsStringchasis += selectedRow.NroChasis;
        selectedRowsStringmodelo += selectedRow.Modelo;
        selectedRowsStringNroPlaca += selectedRow.NroPlaca;
        selectedRowsStringaño += selectedRow.Año;
        alert(selectedRowsStringaño + "" + index);
        window.location.href = "http://stackoverflow.com";
    });

}


function onBtExport() {
    var hoy = new Date();
    var params = {
        skipHeader: false,
        skipFooters: true,
        skipGroups: true,
        fileName: 'ReporteVehiculos' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************

function GestorVehiculo() {
    this.RazonSocial;
    this.NroPlaca;
    this.NroChasis;
    this.Modelo;
    this.Año;
    this.Estado;
    this.Conductor;
    this.EstadoVehiculo;
    this.EstadoEmpresaVehiculo;
    this.tipo;
    this.marca;
}

function pruebita() {
    create();
    //var action = "{'data': 'GM'}";

    $.ajax({
        url: "/Vistas/Vehiculos/Index.aspx/CargarGestionVehiculo",
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

window.onload = pruebita();

