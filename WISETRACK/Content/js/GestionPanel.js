///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{ headerName: "Codigo", field: "NIT", width: 200 },
{ headerName: "Nombre", field: "RazonSocial", width: 350 },
{ headerName: "Email", field: "email", width: 300 },
{ headerName: "Fecha", field: "FechaReg", width: 210 },
//{ headerName: "operacion", field: "button", width: 100, cellRenderer: editRecord },
  { headerName: "Action", field: "button", width: 100, editable: false, cellRenderer: buttonCellRenderer }

];
var gridOptions = {
    // set rowData to null or undefined to show loading panel by default
    //// rowData: rowData,
    enableSorting: true,
    enableFilter: true,
    //   enableColResize: true,
    columnDefs: columnDefs,
    //rowData: rowData,

    //showToolPanel: false,
    //rowSelection: 'multiple',
    //enableStatusBar: true,
    //enableRangeSelection: true,
    pagination:true,
    paginationAutoPageSize:true
    //onRowSelected: rowSelectedFunc,
    //onSelectionChanged: onSelectionChanged,
    //rowClicked: onRowClicked

    // custom loading template. the class ag-overlay-loading-center is part of the grid,
    // it gives a white background and rounded border
    //overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>',
    //overlayNoRowsTemplate: '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">This is a custom \'no rows\' overlay</span>'
};

function buttonCellRenderer(params) {
    var link = document.createElement('a');
    link.setAttribute('href', '#');
    var i = 0;
    if (params.data.NIT == nit) {
        link.innerHTML = "SALIR";
        i = 1;
    } else {
        link.innerHTML = "INGRESAR";
        i = 0;
    }

    link.addEventListener('click', (function (e) {
        return function (e) {
            var nt = params.data.NIT;
            onClickLink(nt, i);
        }
    })(), false);
    //button.addEventListener('click', function () {
    //    console.log(params.data);
    //    document.getElementById('output').innerHTML = "You clicked on id " + params.data.NIT + " with name " + params.data.RazonSocial

    //});
    return link;
}
function onClickLink(nt, i) {
    ActivarEmpresa(nt, i);
    return false;
}
function ActivarEmpresa(nt, i) {

    var action = "{'nt':" + nt + ",'i':" + i + "}";
    $.ajax({
        url: "/Vistas/Empresas/Panel.aspx/ActivarEmpresa",
        data: action,
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        if (data.d != "") {
            window.location = data.d;
        }

        //DesbloquearPantalla();
    }).fail(function (jqXHR, textStatus) {
        //alert("Administrador Error 500 -> " + textStatus);
        //DesbloquearPantalla();
    }).always(function (jqXHR, textStatus) {
        if (textStatus != "success") {
            //alert("Administrador -> " + jqXHR.statusText);
        } else {
            //gridOptions.api.setRowData(myData);

        }
        //DesbloquearPantalla();
    });

}

//var buttonCellRenderer = function (params) {
//    var button = document.createElement('button');
//    button.innerHTML = 'Click me';
//    button.addEventListener('click', function () {
//        console.log(params.data);
//        document.getElementById('output').innerHTML = "You clicked on id " + params.data.id + " with name " + params.data.name

//        alert(para.data.NIT);
//    });
//    return button;
//};


function GestorVehiculo() {
    this.NIT;
    this.RazonSocial;
    this.NombreAlarma;
    this.FechaReg;
    this.ope;

}


var rowData = [];
//function detailRecord(params) {
//    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
//    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "Details(' + params.rowIndex + ')" >Ver Detalles</button>'
//    return html;
//}
function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Edit(' + params.data.NIT + ');" >INGRESAR</button>'
    return html;
}
//function deleteRecord(params) {
//    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
//    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Delete(' + params.rowIndex + ')" >Eliminar</button>'
//    return html;
//}

//var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn btn-default" onclick= "rowindex(' + params.rowIndex + ')' + '">Eliminar</button>';
//function Details(item) {
//    var children = $('.ag-body-container').children()[item];
//    var chil = $(children).children()[0];
//    var text = $(chil).text();
//    var active = $(children).children()[4];
//    var textactive = $(active).text();
//    if (textactive != "NO ACTIVA") {
//        window.location = '/Vistas/Alarmas/Details?cod=' + text + '';

//    } else {
//        alert("informacion Alarma no esta Activa");
//    }


//}
function Edit(item) {

    //    var children = $('.ag-body-container').children()[item];
    //    var hijos = $('.ag-body-container').size();
    //    var chil = $(children).children()[0];
    //    var text = $(chil).text();
    //    var active = $(children).children()[4];
    //    var textactive = $(active).text();
    //if (textactive != "NO ACTIVA") {
    //    window.location = '/Vistas/Alarmas/Edit?cod=' + text + '';
    //} else {
    //    alert("informacion Alarma no esta Activa");
    //}
    alert(item)

    onBtForEachNodeAfterFilter();

}





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
////-------------------------------------------
function onBtForEachNode() {

    console.log('### api.forEachNode() ###');

    gridOptions.api.forEachNode(printNode);

}



function onBtForEachNodeAfterFilter() {

    console.log('### api.forEachNodeAfterFilter() ###');

    gridOptions.api.forEachNodeAfterFilter(printNode);

}



function onBtForEachNodeAfterFilterAndSort() {

    console.log('### api.forEachNodeAfterFilterAndSort() ###');

    gridOptions.api.forEachNodeAfterFilterAndSort(printNode);

}



function printNode(node, index) {

    if (node.data) {
        var ele = node.data.button
        console.log(index + ' -> data: ' + node.data.NIT + ', ' + node.data.RazonSocial + ', ' + node.data.button);

    } else {

        console.log(index + ' -> group: ' + node.key);

    }

}

//**************************---------********************************
function Cargar() {

    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/Empresas/Panel.aspx/CargarEmpresas",
        data: action,
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        nit = data.d;
        //DesbloquearPantalla();
    }).fail(function (jqXHR, textStatus) {
        //alert("Administrador Error 500 -> " + textStatus);
        //DesbloquearPantalla();
    }).always(function (jqXHR, textStatus) {
        if (textStatus != "success") {
            //alert("Administrador -> " + jqXHR.statusText);
        } else {
            //gridOptions.api.setRowData(myData);

        }
        //DesbloquearPantalla();
    });
    getDatos();
}
var nit = "-1";
function getDatos() {
    create();
    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/Empresas/Panel.aspx/getDatos",
        data: action,
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {

        myData = JSON.parse(data.d);

        //DesbloquearPantalla();
    }).fail(function (jqXHR, textStatus) {
        //alert("Administrador Error 500 -> " + textStatus);
        //DesbloquearPantalla();
    }).always(function (jqXHR, textStatus) {
        if (textStatus != "success") {
            //alert("Administrador -> " + jqXHR.statusText);
        } else {
            //gridOptions.api.setRowData(myData);
            setDataSource(myData,gridOptions);
        }
        //DesbloquearPantalla();
    });
}

window.onload = Cargar();