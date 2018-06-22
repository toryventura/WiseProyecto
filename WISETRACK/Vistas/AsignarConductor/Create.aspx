<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Vistas.AsignarConductor.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>ASIGNAR CONDUCTOR</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" Text="Conductores"></asp:Label>
                        <div class="col-sm-3">
                            <select style="text-align: left; width: 100%" name="pais" id="idCondcutores" class="btn btn-default">
                                <option value="0">Selecciona...</option>
                            </select>

                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" CssClass="col-sm-2 control-label" Text="Vehiculos"></asp:Label>
                        <div class="col-sm-3">
                            <select style="text-align: left; width: 100%" name="pais" id="idVehiculos" class="btn btn-default" onchange="getKeys(this);">
                                <option value="0">Selecciona...</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblid" runat="server" CssClass="col-sm-2 control-label" Text="Gps vinculado"></asp:Label>
                        <div class="col-sm-3">
                            <input id="txtAlias" style="width: 100%" class="form-control" type="text" placeholder="Gps asociado" required="required" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="label" class="col-sm-2 control-label">lista GPS:</label>

                        <div class="table-responsive">
                            <div id="listaIDButton" style="height: 300px; padding-top: 3%; padding-left: 5%; width: 60%" class="ag-fresh"></div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <input type="button" name="name" value="Guardar " class="btn btn-default" onclick="guardar();" />

                        </div>
                    </div>
                    <div>
                        <a href="/Vistas/IDButton/Index">Volver átras</a>
                    </div>
                </div>
            </div>

        </div>

        <!-- Modal esta para eliminar el id button -->
        <div id="myGuardar" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Asignacion Conductor</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label id="idmensaje"></label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="ok" type="button" class="btn btn-default" data-dismiss="modal" onclick="guardarDatos();">Aceptar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Canselar</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script type="text/javascript" src="../../Content/black/ag-grid-enterprise.js"></script>
    <script type="text/javascript">

        var sw = 0;

        ///////*****cargar Conductores ***////////////////////////////

        function cargarCboConductores(data) {
            var lsConductores = $('#idCondcutores');
            lsConductores.find('option').remove().end().append('<option value="0">Seleccione...</option>').val('');
            for (i in data) {
                lsConductores.append('<option value="' + data[i].CI + '">' + data[i].NombreCompleto + '</option>');
            };
            // selector.find('option').remove().end().append('selecionA...').val('');

            //$.each(data, function (id, value) {
            //    $("#exampleInputState select").append('<option value="' + value.CI + '">' + value.NombreCompleto + '</option>');
            //});
        }

        function getConductores() {
            $.ajax({
                url: "/Vistas/AsignarConductor/Create.aspx/CargarConductores",
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (response) {
                //myData = JSON.parse(response.d);
                //rowData = myData;
                //DesbloquearPantalla();
                var modelss = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : JSON.parse(response.d);
                if (modelss.length > 0) {
                    cargarCboConductores(modelss);
                } else {
                    // no existe conductores disponibles.

                    error("No Existe Conductores disponibles, debera registrar conductores")
                }
                //   valor = models;
                /// mostrar mendaje si no esxite conductores libre

            }).fail(function (jqXHR, textStatus) {

                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {

                } else {
                    //gridOptions.api.setRowData(myData);

                }

                //DesbloquearPantalla();
            });

        }

        function getKeys(sel) {
            var vla = sel.value;
            if (vla != "0") {

                var action = "{'nroplaca': '" + vla + "'}";
                $.ajax({
                    url: "/Vistas/AsignarConductor/Create.aspx/getKeys",
                    data: action,
                    type: 'POST',
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (response) {
                    var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : JSON.parse(response.d);
                    if (models != "undefined") {
                        $('#txtAlias').val(models.IMEI);
                        if (models.IdButtuns != null) {

                            if (models.IdButtuns.length > 0) {
                                if (sw != 0) {
                                    destroy();
                                }
                                create();
                                myData = models.IdButtuns;
                                sw = 1;
                            } else {
                                if (sw != 0) {
                                    destroy();
                                }
                                sw = 0;

                                myData = models.IdButtuns;
                                error("No hay IDButtons asociados a este GPS...");
                            }

                        } else {
                            if (sw != 0) {
                                destroy();
                            }
                            sw = 0;

                            myData = [];
                            error("No hay IDButtons asociados a este GPS...");
                        }

                        // cargarCboVehiculos(models);
                    } else {
                        // no existe conductores disponibles.
                    }

                    //DesbloquearPantalla();
                }).fail(function (jqXHR, textStatus) {
                    alert("Administrador Error 500 -> " + textStatus);
                    //DesbloquearPantalla();
                }).always(function (jqXHR, textStatus) {
                    if (textStatus != "success") {
                        alert("Administrador -> " + jqXHR.statusText);
                    } else {
                        //gridOptions.api.setRowData(myData);
                        if (myData.length > 0) {

                            setDataSource(myData);
                        }
                    }

                    //DesbloquearPantalla();
                });
            }

        }
        //////***cargar Vehiculos no asignados ***/////////////////////

        function asignarEdit(nroplaca) {
            // $('#myModal').show();
            //alert("" + nroplaca);
            GetCondcutores();
            $('#txtnroplaca').val(nroplaca);
        }
        function Asignar() {
            var ci = parse.int($("#idCondcutores option:selected").text());
            if (ci != "0") {

            } else {
                //seleciones un condcutor de las lista
            }
        }


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

        { headerName: "ID", field: "ID", width: 100, hide: true },
        { headerName: "Keys", field: "Keys", width: 150 },
        { headerName: "Alias", field: "Alias", width: 100 },
        { headerName: "FechaReg", field: "FechaReg", width: 100 }

        ];
        var arr = [];
        var colObject = function (ID, Keys, Alias, FechaReg) {
            this.ID = ID;
            this.keys = keys;
            this.Alias = Alias;
            this.FechaReg = FechaReg;
        };

        function converttoArray(models) {
            var arr = [];
            for (var i = 0; i < len; i++) {
                arr.push((new colObject(23, 323, 23, 3)));
            }
        }
        //for (var i = 0; i < len; i++) {
        //    arr.push((new columnDefs(oFullResponse.results[i].label, true, true)));
        //}
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
            var gridDiv = document.querySelector('#listaIDButton');
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
        function destroy() {

            gridOptions.api.destroy();


        }

        function getImeis() {
            var lista = [];
            var i = 0
            if (sw != 0) {
                gridOptions.api.forEachNode(function (node) {
                    if (node.isSelected()) {
                        lista[i] = node.data.Keys;
                        i++;
                    }
                });
            }

            return lista;
        }
        function guardar() {
            var lista = getImeis();

            var ci = $("#idCondcutores option:selected").val();
            var nroplaca = $("#idVehiculos option:selected").val();
            if (ci != "0") {
                if (nroplaca != "0") {

                    if (lista.length > 0) {
                        if (lista.length == 1) {
                            var mensaje = "Esta seguro de Guardar?"
                            $('#idmensaje').text(mensaje);
                            $('#myGuardar').modal('show');
                        } else {
                            error("Solo debe seleccionar un IDButon");
                        }
                    } else {
                        var mensaje = "Esta seguro de guardar sin asignar un IDButton ?..."
                        $('#idmensaje').text(mensaje);
                        $('#myGuardar').modal('show');
                    }
                } else {
                    error("Seleccone un Vehiculo....");
                }
            } else {
                error("Selecione un Conductor....");
            }


        }
        //**************************---------********************************
        function guardarDatos() {
            var list = getImeis();
            var ci = $("#idCondcutores option:selected").val();
            var nroplaca = $("#idVehiculos option:selected").val();
            if (ci != null || ci != "0") {
                if (nroplaca != null || nroplca != "0") {

                    var action = "{'list':'" + list + "', 'ci': '" + ci + "','nroplaca': '" + nroplaca + "'}";
                    $.ajax({
                        url: "/Vistas/AsignarConductor/Create.aspx/setguardar",
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
                            Redireccionar("/Vistas/AsignarConductor/Index");
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
                else {
                    error("Seleccone un Vehiculo....");
                }
            } else {
                error("Selecione un Conductor....");
            }


        }


        ///////////////////************************getCargarVehiculos***********************/////////////////////
        function getVehiculos() {

            $.ajax({
                url: "/Vistas/AsignarConductor/Create.aspx/CargarVehiculos",

                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : JSON.parse(response.d);
                if (models.length > 0) {
                    cargarCboVehiculos(models);
                } else {
                    // no existe conductores disponibles.
                }

                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                alert("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    alert("Administrador -> " + jqXHR.statusText);
                } else {
                    //gridOptions.api.setRowData(myData);
                }
                //DesbloquearPantalla();
            });
        }
        function cargarCboVehiculos(data) {
            var lsVehiculos = $('#idVehiculos');
            lsVehiculos.find('option').remove().end().append('<option value="0">Seleccione...</option>').val('');
            for (i in data) {
                lsVehiculos.append('<option value="' + data[i].Id + '">' + data[i].NroPlaca + '</option>');
            };

        }
        function cargarDatos() {
            getConductores();
            getVehiculos();
        }
        window.onload = cargarDatos();

    </script>
</asp:Content>
