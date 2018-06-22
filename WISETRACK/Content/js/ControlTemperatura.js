
var gridOptions = {
    columnDefs: [
        { headerName: 'Name', field: 'name' },
        { headerName: 'Role', field: 'role' }
    ],
    rowData: [
        { name: 'Niall', role: 'http://www.google.com.bo' },
        { name: 'Eamon', role: 'Manager' },
        { name: 'Brian', role: 'Musician' },
        { name: 'Kevin', role: 'Manager' }
    ]
};

function onBtExport() {
    var hoy = new Date();
    var params = {
        skipHeader: false,
        skipFooters: true,
        skipGroups: true,
        fileName: 'ReporteAuditoria' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}


(function () {

    document.addEventListener('DOMContentLoaded', function () {

        var gridDiv = document.querySelector('#myGrid');

        new agGrid.Grid(gridDiv, gridOptions);
    });

})();