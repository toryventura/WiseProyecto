///////////////////////////////////////AG-GRID/////////////////////////////////////////////////
var cods = 0;
var action = 0;//0:finalizar ,1:Activar

var columnDefs = [

{ headerName: "ID", field: "ID", width: 150, hide: true },
{ headerName: "Razon Social", field: "Rsocial", width: 200 },
{ headerName: "Key", field: "Keys", width: 150 },
{ headerName: "Alias IdButton", field: "Alias", width: 150 },
{ headerName: "Fecha", field: "FechaReg", width: 200 },
{ headerName: "Estado", field: "Estado", width: 100 },
{ headerName: "", width: 100, cellRenderer: editRecord },
{ headerName: "", width: 100, cellRenderer: deleteRecord }
];

function GestorVehiculo() {
    this.Id;
    this.alias;
    this.Rsocial;
    this.FechaReg;
    this.Estado;
}


//function detailRecord(params) {
//    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
//    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "Details(' + params.data.ID + ',\'' + params.data.Estado + '\')" >Ver Detalles</button>'
//    return html;
//}
function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Edit(' + params.data.ID + ',\'' + params.data.Estado + '\')" >Editar</button>'
    return html;
}
function deleteRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html;
    if (params.data.Estado == "Activo") {
        html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Delete(' + params.data.ID + ',' + params.data.Keys + ',\'' + params.data.Estado + '\')" >Dar de Baja</button>';
    } else {
        html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Activars(' + params.data.ID + ',' + params.data.Keys + ',\'' + params.data.Estado + '\')" >Activar</button>'
    }

    return html;
}

//var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn btn-default" onclick= "rowindex(' + params.rowIndex + ')' + '">Eliminar</button>';
//function Details(cod, estado) {
//    window.location = '/Vistas/IDButton/Details?cod=' + cod + '';

//}
function Edit(cod, estado) {
    if (estado == "Activo") {

        window.location = '/Vistas/IDButton/Edit?cod=' + cod + '';
    } else {
        error("NO SE PUEDE EDITAR, SI ESTA DE BAJA");
    }
}

function Delete(cod, keys, estado) {
    cods = cod;
    action = 0;
    var mensaje = "Esta seguro que desea Finalizar el IDButton  " + keys + " ?";
    $('#idmensaje').text(mensaje);
    $('#myEliminar').modal('show');
}
function Activars(cod, keys, estado) {
    cods = cod;
    action = 1;
    var mensaje = "Esta seguro que desea Habilitar e  " + keys + " ?";
    $('#idmensaje').text(mensaje);
    $('#myEliminar').modal('show');
}
var gridOptions = {

    enableSorting: true,
    enableFilter: true,

    enableColResize: true,
    columnDefs: columnDefs,
    showToolPanel: false,
    rowSelection: 'multiple',

    pagination: true,
    paginationAutoPageSize:true

};


function create() {
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}
function destroy() {
    gridOptions.api.destroy();
}


function onBtExport() {
    var hoy = new Date();
    var params = {
        skipHeader: false,
        skipFooters: true,
        skipGroups: true,
        fileName: 'ReporteIDButton' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************
function Actions() {
    if (action == 0) {
        Eliminar();
    } else {
        Activar();
    }

}
function Eliminar() {
    /// aqui temenos que haces la logica para eliminar el IDbutton
    if (cods != 0) {
        var action = "{'id': '" + cods + "'}";
        $.ajax({
            url: "/Vistas/IDButton/Index.aspx/Eliminar",
            data: action,
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            var values = JSON.parse(data.d);
            var result = values[0];

            if (result.Key == "OK") {
                success(result.Value);
                destroy();
                getDatos();
            } else {
                if (result.Key == "ERROR") {
                    error(result.Value);
                }

            }
            cods = 0;
            //DesbloquearPantalla();
        }).fail(function (jqXHR, textStatus) {
            //alert("Administrador Error 500 -> " + textStatus);
            //DesbloquearPantalla();
        }).always(function (jqXHR, textStatus) {
            if (textStatus != "success") {
                //alert("Administrador -> " + jqXHR.statusText);
            } else {

            }
            //DesbloquearPantalla();
        });
    }

}
function Activar() {
    /// aqui temenos que haces la logica para eliminar el IDbutton
    if (cods != 0) {
        var action = "{'id': '" + cods + "'}";
        $.ajax({
            url: "/Vistas/IDButton/Index.aspx/Activar",
            data: action,
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            var values = JSON.parse(data.d);
            var result = values[0];

            if (result.Key == "OK") {
                success(result.Value);
                destroy();
                getDatos();
            } else {
                if (result.Key == "ERROR") {
                    error(result.Value);
                }

            }
            cods = 0;
            //DesbloquearPantalla();
        }).fail(function (jqXHR, textStatus) {
            //alert("Administrador Error 500 -> " + textStatus);
            //DesbloquearPantalla();
        }).always(function (jqXHR, textStatus) {
            if (textStatus != "success") {
                //alert("Administrador -> " + jqXHR.statusText);
            } else {

            }
            //DesbloquearPantalla();
        });
    }

}

function getDatos() {
    create();
    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/IDButton/Index.aspx/getDatos",
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
          
        }
        //DesbloquearPantalla();
    });
}

window.onload = getDatos();