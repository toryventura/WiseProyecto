///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{ headerName: "CodSeguimiento", field: "CodSeguimiento", width: 100, hide: true },
{ headerName: "RazonSocial", field: "RazonSocial", width: 150 },
{ headerName: "IMEI", field: "IMEI", width: 150 },
{ headerName: "NroPlaca", field: "NroPlaca", width: 100 },
{ headerName: "Modelo", field: "Modelo", width: 100 },
{ headerName: "FechaInicio", field: "FechaInicio", width: 120 },
{ headerName: "FechaFin", field: "FechaFin", width: 200 },
{ headerName: "Estado", field: "estado", width: 100 },
{ headerName: "", width: 100, cellRenderer: detailRecord },
{ headerName: "", width: 150, cellRenderer: deleteRecord }
];

function detailRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Detail(\'' + params.data.CodSeguimiento + '\',\'' + params.data.estado + '\')" >Ver Detalle</button>'
    return html;
}

function deleteRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Delete(\'' + params.data.CodSeguimiento + '\',\'' + params.data.estado + '\')" >Finalizar Seguimiento</button>'
    return html;
}

function Delete(cod, estado) {
    if (estado != "Finalizado") {
        window.location = '/Vistas/Seguimientos/Delete?id=' + cod + '';
    } else {
        alert("Sistemas: El seguimiento ya ha sido finalizado");
    }
}

function Detail(cod, estado) {
    if (estado != "Finalizado") {
        window.location = '/Vistas/Seguimientos/Details?id=' + cod + '';
    } else {
        alert("Sistemas: El seguimiento ya ha sido finalizado");
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
    pagination: true,
    paginationAutoPageSize:true
    
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
        fileName: 'ReporteAsignacionSeguimiento' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************

function getDatos() {
    create();
    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/Seguimientos/Index.aspx/getDatos",
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