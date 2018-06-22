///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{ headerName: "Usuario", field: "UserName", width: 150 },
{ headerName: "Email", field: "Email", width: 150 },
{ headerName: "Rol", field: "UserRole", width: 200 },
{ headerName: "Asignado a", field: "Persona", width: 200 },
{ headerName: "", width: 100, cellRenderer: editRecord },
 { headerName: "", width: 100, cellRenderer: deleteRecord }

];


//function detailRecord(params) {
//    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
//    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick= "Details(' + params.rowIndex + ')" >Ver Detalles</button>'
//    return html;
//}
function editRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Edit(\'' + params.data.UserName + '\')" >Editar</button>'
    return html;
}
function deleteRecord(params) {
    //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
    var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn-link btn-xs" onclick="Delete(\'' + params.data.UserName + '\')" >Eliminar</button>'
    return html;
}

function Edit(user) {
    
   
 
    window.location = '/Vistas/Usuarios/Edit?user=' + user + '';
   

}

function Delete(user) {

  
    window.location = '/Vistas/Usuarios/Delete?user=' + user + '';

   

}

var gridOptions = {
    // set rowData to null or undefined to show loading panel by default
    // rowData: rowData,
    enableSorting: true,
    enableFilter: true,

    enableColResize: true,
    columnDefs: columnDefs,
    showToolPanel: false,
    pagination:true,
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
        fileName: 'ReporteCuenta' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//**************************---------********************************




function getDatos() {
    create();
    var action = "{'data': 'GM'}";
    $.ajax({
        url: "/Vistas/Usuarios/Index.aspx/getDatos",
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