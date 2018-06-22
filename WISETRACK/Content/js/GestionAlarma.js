///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{ headerName: "Codigo", field: "CodAlarma", width: 150 },
{ headerName: "Razon Social", field: "razonSocial", width: 150 },
{ headerName: "Nombre", field: "NombreAlarma", width: 200 },
{ headerName: "Tipo", field: "TipoAlarma", width: 200 },
{ headerName: "Estado", field: "Estado", width: 100 },

{ headerName: "", width: 100, cellRenderer: detailRecord },

{ headerName: "", width: 100, cellRenderer: editRecord },
 { headerName: "", width: 100, cellRenderer: deleteRecord }

];

function GestorVehiculo() {
    this.razonSocial;
    this.CodAlarma;
    this.NombreAlarma;
    this.TipoAlarma;
    this.Estado;

}


var rowData = [];
function detailRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "Details(' + params.data.CodAlarma + ',\'' + params.data.Estado + '\')" >Ver Detalles</button>'
    return html;
}
function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Edit(' + params.data.CodAlarma + ',\'' + params.data.Estado + '\')" >Editar</button>'
    return html;
}
function deleteRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Delete(' + params.data.CodAlarma + ',\'' + params.data.Estado + '\')" >Eliminar</button>'
    return html;
}

//var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn btn-default" onclick= "rowindex(' + params.rowIndex + ')' + '">Eliminar</button>';
function Details(cod,estado) {
        window.location = '/Vistas/Alarmas/Details?cod=' + cod + '';

}
function Edit(cod,estado) {
    
        window.location = '/Vistas/Alarmas/Edit?cod=' + cod + '';
    


}

function Delete(cod, estado) {

    //var children = $('.ag-body-container').children()[item];
    //var chil = $(children).children()[0];
    //var text = $(chil).text();
    //var active = $(children).children()[4];
    //var textactive = $(active).text();
    //if (textactive != "NO ACTIVA") {
    //    window.location = '/Vistas/Alarmas/Delete?cod=' + text + '';

    //} else {
    //    alert("informacion Alarma no esta Activa");
    //}
    alert("Informacion: Opcion bloqueada por el administrador de sistemas");
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

    //onRowSelected: rowSelectedFunc,
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
        fileName: 'ReporteAlarmas' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************




function getDatos() {
    create();
    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/Alarmas/Index.aspx/getDatos",
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
            setDataSource(myData,gridOptions);
        }
        //DesbloquearPantalla();
    });
}

window.onload = getDatos();