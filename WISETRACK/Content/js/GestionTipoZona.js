///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [

{ headerName: "Razon Social", field: "RazonSocial", width: 150 },
{ headerName: "ID", field: "CodTipoGEO", width: 100 },
{ headerName: "Descripcion", field: "Descripcion", width: 100 },
{ headerName: "Action", width: 80, cellRenderer: editRecord }
];

var rowData = [];

function selectAllRenderer(params) {
    var cb = document.createElement('input');
    cb.setAttribute('type', 'checkbox');

    var eHeader = document.createElement('label');
    var eTitle = document.createTextNode(params.colDef.headerName);
    eHeader.appendChild(cb);
    eHeader.appendChild(eTitle);

    cb.addEventListener('change', function (e) {
        if ($(this)[0].checked) {
            params.api.selectAll();
        } else {
            params.api.deselectAll();
        }
    });
    return eHeader;
}
function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "rowEdit(' + params.data.CodTipoGEO + ')" >Editar</button>'
    return html;
}

function rowEdit(codigo) {
    window.location = '/Vistas/TipoGeocerca/Edit?CodTipoGEO=' + codigo + '';

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
    pagination: true
    //onRowSelected: rowSelectedFunc,
    //onSelectionChanged: onSelectionChanged,
    //rowClicked: onRowClicked

    // custom loading template. the class ag-overlay-loading-center is part of the grid,
    // it gives a white background and rounded border
    //overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>',
    //overlayNoRowsTemplate: '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">This is a custom \'no rows\' overlay</span>'
};

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
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

function onBtExport() {
    var hoy = new Date();
    var params = {
        skipHeader: false,
        skipFooters: true,
        skipGroups: true,
        fileName: 'ReporteTipoGeocerca' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************

function LISTAR() {
    create();
    //var action = "{'data': 'GM'}";

    $.ajax({
        url: "/Vistas/TipoGeocerca/Index.aspx/CargarGestionTipoZona",
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
            //setDataSource(myData);
        }
        //DesbloquearPantalla();
    });
}

window.onload = LISTAR();

