///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [

{ headerName: "Nit", field: "nit", width: 100, hide: true },
{ headerName: "Razon Social", field: "razon_social", width: 150 },
{ headerName: "CI", field: "CI", width: 100 },
{ headerName: "Nombre", field: "Nombre", width: 100 },
{ headerName: "Paterno", field: "ApellidoP", width: 100 },
{ headerName: "Materno", field: "ApellidoM", width: 100 },
{ headerName: "Direccion", field: "Direccion", width: 100 },
{ headerName: "Telefono", field: "Telefono", width: 100 },
{ headerName: "Email", field: "Email", width: 130 },
{ headerName: "estado", field: "estado", width: 100 },
{ headerName: "Action", width: 80, cellRenderer: editRecord },
{ headerName: "Action", width: 80, cellRenderer: deleteRecord }
];

var rowData = [];

function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "rowEdit(\'' + params.data.CI + '\',\'' + params.data.estado + '\')" >Editar</button>'
    return html;
}

function deleteRecord(params) {
    var html = '<button type="button" id="idbutton2" data-action-type="view3" class="btn-link btn-xs" onclick= "rowDelete(\'' + params.data.CI + '\',\'' + params.data.estado + '\')" >Eliminar</button>'
    return html;
}

function rowEdit(ci,estado) {
   
    if (estado != "De Baja") {
        window.location = '/Vistas/Personas/Edit?ci=' + ci + '';
    } else {
        alert("Informacion: El Personal ya ha sido dado de baja");
    }
}

function rowDelete(ci,estado) {
   
    if (estado != "De Baja") {
        window.location = '/Vistas/Personas/Delete?ci=' + ci + '';
    } else {
        alert("Informacion: El Personal ya ha sido dado de baja");
    }

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
    //onRowSelected: rowSelectedFunc    
    //onSelectionChanged: onSelectionChanged,
    //rowClicked: onRowClicked

    // custom loading template. the class ag-overlay-loading-center is part of the grid,
    // it gives a white background and rounded border
    //overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>',
    //overlayNoRowsTemplate: '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">This is a custom \'no rows\' overlay</span>'
};

function setDataSource(allOfTheData, gridOption) {
            
    gridOption.api.setRowData(allOfTheData);

}
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
        fileName: 'ReportePersonas' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************

function GestorPersona() {
    this.nit;
    this.razon_social;
    this.CI;
    this.Nombre;
    this.ApellidoP;
    this.ApellidoM;
    this.Direccion;
    this.Telefono;
    this.Email;
    this.estado;
}

function LISTAR() {
    create();
    //var action = "{'data': 'GM'}";

    $.ajax({
        url: "/Vistas/Personas/Index.aspx/CargarGestionPersona",
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
            setDataSource(myData,gridOptions);
        }
        //DesbloquearPantalla();
    });
}

window.onload = LISTAR();

