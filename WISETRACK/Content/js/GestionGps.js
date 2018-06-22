///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{ headerName: "Empresa", field: "razon_social", width: 150 },
{ headerName: "IMEI", field: "IMEI", width: 150 },
{ headerName: "ID", field: "ID", width: 100 },
{ headerName: "Modelo", field: "Modelo", width: 100 },
{ headerName: "Nro Telefono", field: "NroTelefono", width: 200 },
{ headerName: "Estado", field: "estado", width: 100 },
{ headerName: "", width: 100, cellRenderer: editRecord },
 { headerName: "", width: 100, cellRenderer: deleteRecord }

];

//NroTelefono

function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Edit(\'' + params.data.IMEI + '\',\'' + params.data.estado + '\')" >Editar</button>'
    return html;
}
function deleteRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Delete(\'' + params.data.IMEI + '\',\'' + params.data.estado + '\')" >Eliminar</button>'
    return html;
}

function Edit(imei,estado) {
    
        window.location = '/Vistas/GPSs/Edit?imei=' + imei + '';
   


}

function Delete(imei,estado) {

   
        window.location = '/Vistas/GPSs/Delete?imei=' + imei + '';

    

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
    paginationAutoPageSize:true
    //onRowSelected: rowSelectedFunc,
    //onSelectionChanged: onSelectionChanged,
    //rowClicked: onRowClicked

    // custom loading template. the class ag-overlay-loading-center is part of the grid,
    // it gives a white background and rounded border
    //overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>',
    //overlayNoRowsTemplate: '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">This is a custom \'no rows\' overlay</span>'
};

function create() {
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}


function onBtExport() {
    var hoy = new Date();
    var params = {
        skipHeader: false,
        skipFooters: true,
        skipGroups: true,
        fileName: 'ReporteGps' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************




function getDatos() {
    create();
    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/GPSs/Index.aspx/getDatos",
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
         gridOptions.api.setRowData(myData);
            //setDataSource(myData);
        }
        //DesbloquearPantalla();
    });
}

window.onload = getDatos();