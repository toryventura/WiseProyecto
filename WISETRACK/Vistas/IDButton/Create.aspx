<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Vistas.IDButton.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Registrar IDBUTON</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" Text="ID"></asp:Label>
                        <div class="col-sm-3">
                            <input id="txtKeys" class="form-control" type="text" placeholder="1234532335456744" title="ID es requerido. Longitud 16-20 digitos" maxlength="20" required="required" pattern="[A-Za-z0-9 ]{3,20}" onkeypress="return SoloNumeros(event)" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblid" runat="server" CssClass="col-sm-2 control-label" Text="Nombre o Alias"></asp:Label>
                        <div class="col-sm-3">
                            <input id="txtAlias" class="form-control" type="text" placeholder="Nombre o Alias" title="ID es requerido. Longitud 3-20 digitos" required="required" pattern="[a-zA-Z0-9]{3,20}" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="label" class="col-sm-2 control-label">lista GPS:</label>

                        <div class="table-responsive">
                            <div id="listaGps" style="height: 300px; padding-top: 3%; padding-left: 5%; width: 60%" class="ag-fresh"></div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <input type="button" name="name" value="Guardar " class="btn btn-default" onclick="guardarDatos();" />

                        </div>
                    </div>
                    <div>
                        <a href="/Vistas/IDButton/Index">Volver átras</a>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript" src="../../Content/black/ag-grid-enterprise.js"></script>
    <script type ="text/javascript">
        function SoloNumeros(e) {
            var key = (document.all) ? e.keyCode : e.which;

            
            return true;
        }

        
        function ValidarCampos() {
            var txtkyes = $('#txtKeys').val();
            if (txtkyes.length < 16) {
                error("key es requerido. Longitud 16-20 digitos");
                $('#txtKeys').focus();
                return false;
            }
            return true;
        }
        var columnDefs = [
            {
                field: 'RowSelect',
                headerName: ' ',
                width: 30,
                checkboxSelection: true,
                suppressMenu: true,
                suppressSorting: true,
                headerCellRenderer: selectAllRenderer
            },

        { headerName: "IMEI", field: "IMEI", width: 150 },
        { headerName: "ID", field: "ID", width: 100 },
        { headerName: "Modelo", field: "Modelo", width: 100 },
        { headerName: "Placa", field: "Placa", width: 100 }

        ];

        //NroTelefono
        var gridOptions = {
            enableSorting: true,
            enableFilter: true,

            enableColResize: true,
            columnDefs: columnDefs,
            showToolPanel: false,
            rowSelection: 'multiple',
            rowModelType: 'pagination'

        };

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
            var gridDiv = document.querySelector('#listaGps');
            new agGrid.Grid(gridDiv, gridOptions);
        }

        //function selectAllAmerican() {
        //    gridOptions.api.forEachNode(function (node) {

        //        if (node.data.razon_social === 'Avicola SOFIA') {
        //            node.setSelected(true);
        //        }
        //    });
        //}
        //function SumaSelect() {
        //    var cke=0
        //    gridOptions.api.forEachNode(function (node) {

        //        if (node.isSelected()) {
        //            cke++;
        //        }
        //    });

        //}
        function getImeis() {
            var lista = [];
            var i = 0
            gridOptions.api.forEachNode(function (node) {
                if (node.isSelected()) {
                    lista[i] = node.data.IMEI;
                    i++;
                }
            });
            return lista;
        }
        //**************************---------********************************
        function guardarDatos() {
            var list = getImeis();

            var key = $('#txtKeys').val();
            var alias = $('#txtAlias').val();


            if (ValidarCampos()) {
                var action = "{'list':'" + list + "', 'key': '" + key + "','alias': '" + alias + "'}";
                $.ajax({
                    url: "/Vistas/IDButton/Create.aspx/setguardar",
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
                        Redireccionar("/Vistas/IDButton/Index");
                    } else {
                        if (result.Key == "ERROR") {
                            error(result.Value);
                        }
                    }

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
                url: "/Vistas/IDButton/Create.aspx/getDatos",
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
                    setDataSource(myData);
                }
                //DesbloquearPantalla();
            });
        }

        window.onload = getDatos();

    </script>
</asp:Content>
