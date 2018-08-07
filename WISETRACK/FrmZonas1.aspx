<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmZonas1.aspx.cs" Inherits="WISETRACK.FrmZonas1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        #map {
            width: auto;
            height: 550px;
        }

        #map1 {
            width: auto;
            height: 450px;
        }

        .delete-menu {
            position: absolute;
            background: white;
            padding: 3px;
            color: #666;
            font-weight: bold;
            border: 1px solid #999;
            font-family: sans-serif;
            font-size: 12px;
            box-shadow: 1px 3px 3px rgba(0, 0, 0, .3);
            margin-top: -10px;
            margin-left: 10px;
            cursor: pointer;
        }

            .delete-menu:hover {
                background: #eee;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu1").click(function () {
                $("#campo1").slideToggle("slow");
            });
        });
        $(document).ready(function () {
            //$("#campo2").hide();
            $("#menu2").click(function () {
                $("#campo2").slideToggle("slow");
            });
        });
        $(document).ready(function () {
            $("#menu3").click(function () {
                $("#campo3").slideToggle("slow");
            });
        });
    </script>

    <div class="container-fluid" style="padding-top: 5px">
        <div class="row-fluid">
            <div class="span3">
                <div class="panel panel-primary">
                    <div class="panel-heading"><b>Configurador Zonas</b></div>
                    <div class="panel-title label-info" id="menu1" style="font-size: small"><b>Geocercas</b></div>
                    <div class="panel-body" id="campo1">

                        <div class="form-group">
                            <div id="myGridGeocerca" style="height: 150px; width: 100%" class="ag-theme-balham"></div>
                        </div>

                    </div>
                    <%--PARTE 2 --%>
                    <div class="panel-title label-info" style="font-size: small" id="menu2"><b>+ Filtros</b></div>
                    <div class="panel-body" id="campo2" style="display: none">
                        <div class="form-group">
                            <label id="lblnombre" font-size="Smaller">Nombre</label>
                            <input type="text" id="txtnombre" value="" placeholder="Introducir Nombre Geocerca" class="form-control" title="Nombre es requerido. Longitud 4-20 caracteres" required="required" pattern="[0-9][a-zA-Z]{4,20}" />
                        </div>
                        <div class="form-group">
                            <label id="Label1" font-size="Smaller">Tipo Zona</label>

                            <telerik:RadComboBox
                                CssClass="dropdown"
                                ID="cbozonas"
                                AllowCustomText="true"
                                runat="server"
                                EmptyMessage="Zonas"
                                Font-Size="Smaller"
                                DropDownCssClass="dropdown">
                            </telerik:RadComboBox>


                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <label id="lblcolorR" font-size="Smaller">Color Relleno</label>
                                <div class="input-group my-colorpicker2">
                                    <input type="text" value="#FF0000" id="txtcolor" class="form-control" />
                                    <div class="input-group-addon">
                                        <i></i>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="panel-title label-info" style="font-size: small" id="menu3"><b>+ Nuevo Registro</b></div>
                    <div class="panel-body" id="campo3">

                        <div class="form-inline">

                            <%--<input type="button" id="btnNuevo" value="Nuevo" class="btn btn-primary btn-xs" onclick="nuevoPoligono()" />--%>
                            <button id="btnNew" type="button" title="Nueva Geocerca" class="btn btn-primary btn-xs" onclick="nuevoPoligono();">
                                <i class="material-icons" style="font-size: 48px;">add_location</i>
                            </button>

                            <%--<input type="button" id="btnGuardar" value="Guardar" class="add_location" onclick="enviarGeocerca()" />--%>
                            <button id="btnEdit" type="button" title="Visualizar Geocercas" class="btn btn-primary btn-xs" onclick="editPoigono();" data-togle="Editar">
                                <i class="material-icons" style="font-size: 48px;">satellite</i>
                            </button>

                            <%--<input type="button" id="btnLimpiar" value="Cancelar" class="btn btn-primary btn-xs " onclick="cancelPoligono()" />--%>
                            <button id="btnAdd" type="button" title="Guardar Geocercas" class="btn btn-primary btn-xs" onclick="GetPuntosLong();" data-togle="Editar">
                                <i class="material-icons" style="font-size: 48px;">edit_location</i>
                            </button>

                            <button id="btnView" type="button" class="btn btn-primary btn-xs" onclick="Mensaje();">
                                <i class="material-icons" style="font-size: 48px;">add_location</i>
                            </button>
                            <%--<input type="button" id="btnLimpiar" value="Cancelar" class="btn btn-primary btn-xs " onclick="cancelPoligono()" />--%>
                            <button id="btnClean" type="button" title="Limpiar" class="btn btn-primary btn-xs" onclick="cancelPoligono();" data-togle="Editar">
                                <i class="material-icons" style="font-size: 48px">delete_sweep</i>
                            </button>

                        </div>

                    </div>
                </div>
            </div>
            <div class="span9">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div id="map"></div>

                    </div>
                </div>

            </div>

        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Visualizacion de Geocercas </h4>
                </div>
                <div class="modal-body">
                    <div id="map1"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="info" style="width: 10px; height: 10px"></div>

    <div class="modal fade bd-example-modal-sm" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel1">Visualizacion de Geocercas </h4>
                </div>
                <div class="modal-body">
                    <p>¿Esta seguro que desea guardar la geocerca editada?</p>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Guardar" id="btneditargeo" class="btn btn-default" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        var map;
        var listener;
        var poligono;

        var poligono_aux;
        var array_puntos = [];
        // Action 0= guardar ,1=Actualizar 
        var accion;
        var SelectionGeocerca = 0
        //Clases Serializadas
        function geocercaSerial() {
            this.Descripcion;
            this.ColorLimite;
            this.ColorRelleno;
            this.CodTipoGEO;
        }

        function puntosgeoSerial() {
            this.Latitud;
            this.Longitud;
        }

        function paintGeocerca() {
            this.CodigoGEO;
            this.Descripcion;
            this.ColorLimite;
            this.ColorRelleno;
            this.Zona;
            this.NIT;
            this.Nro;
            this.Latitud;
            this.Longitud;
        }
        var $mapDiv = $('#map');
        var markers = [];
        var mapDim = {
            height: $mapDiv.height(),
            width: $mapDiv.width()
        }
        var bounds = (array_puntos.length > 0) ? createBoundsForMarkers(array_puntos) : null;
        function createMarkerForPoint(point) {
            return new google.maps.Marker({
                position: new google.maps.LatLng(point.lat, point.lng)
            });
        }
        function createBoundsForMarkers(polyArray) {
            var bounds = new google.maps.LatLngBounds();
            var path, paths;

            paths = polyArray.getPaths();
            for (var i = 0; i < paths.getLength(); i++) {
                path = paths.getAt(i);
                for (var ii = 0; ii < path.getLength(); ii++) {
                    bounds.extend(path.getAt(ii));
                }
            }

            return bounds;
        }
        function getArrayBounds(polyArray) {
            var bounds = new google.maps.LatLngBounds();
            var path, paths;
            for (var polys = 0; polys < polyArray.length; polys++) {
                paths = polyArray[polys].getPaths();
                for (var i = 0; i < paths.getLength(); i++) {
                    path = paths.getAt(i);
                    for (var ii = 0; ii < path.getLength(); ii++) {
                        bounds.extend(path.getAt(ii));
                    }
                }
            }
            return bounds;
        }
        function getBoundsZoomLevel(bounds, mapDim) {
            var WORLD_DIM = { height: 256, width: 256 };
            var ZOOM_MAX = 21;

            function latRad(lat) {
                var sin = Math.sin(lat * Math.PI / 180);
                var radX2 = Math.log((1 + sin) / (1 - sin)) / 2;
                return Math.max(Math.min(radX2, Math.PI), -Math.PI) / 2;
            }

            function zoom(mapPx, worldPx, fraction) {
                return Math.floor(Math.log(mapPx / worldPx / fraction) / Math.LN2);
            }

            var ne = bounds.getNorthEast();
            var sw = bounds.getSouthWest();

            var latFraction = (latRad(ne.lat()) - latRad(sw.lat())) / Math.PI;

            var lngDiff = ne.lng() - sw.lng();
            var lngFraction = ((lngDiff < 0) ? (lngDiff + 360) : lngDiff) / 360;

            var latZoom = zoom(mapDim.height, WORLD_DIM.height, latFraction);
            var lngZoom = zoom(mapDim.width, WORLD_DIM.width, lngFraction);

            return Math.min(latZoom, lngZoom, ZOOM_MAX);
        }
        function initMap() {
            map = new google.maps.Map($mapDiv[0], {
                center: { lat: -17.783288, lng: -63.1817407 },
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

        }


        function GetPuntosLong() {
            var vertices = poligono_aux.getPath();// este es el que saca todas la puntos del mapas 
            // Iterate over the vertices.
            var mlistpuntosgeo = [];
            for (var i = 0; i < vertices.getLength(); i++) {
                var xy = vertices.getAt(i);
                var mpuntosgeocerca = new puntosgeoSerial();
                mpuntosgeocerca.Latitud = xy.lat();
                mpuntosgeocerca.Longitud = xy.lng();
                mlistpuntosgeo.push(mpuntosgeocerca);
            }
            return mlistpuntosgeo;
        }

        function cancelPoligono() {
            // limpiarTodo();
            limpiarTodo2();
            enableSubmit('#btnNew');
            enableSubmit('#btnEdit');
            enableSubmit('#btnView');
            enableSubmit('#btnDelete');
            google.maps.event.removeListener(listener);
        }

        function nuevoPoligono() {
            $("#campo2").slideToggle("slow");
            //limpiarTodo();
            limpiarTodo2();
            accion = 0;
            SelectionGeocerca = 0;
           // poligono.setPath(marker_vector);
            poligono_aux.setPath(array_puntos);
            listener = new google.maps.event.addListener(map, 'click', function (event) {
                //addPoli(event.latLng, map);
                addPuntos(event.latLng, map);
            });
        }

        function limpiarTodo() {
            poligono.setMap(null);
        }

        function limpiarTodo2() {
            poligono_aux.setMap(null);
        }

        function addPoli(location, map) {
            marker_vector.push(location);

            var marker = new google.maps.Marker({
                position: location,
                map: map,
                title: '#' + ' - ' + markers.length + ' - ' + location,
                draggable: true
            });

            markers.push(marker);
            poligono.setPath(marker_vector);
            dragstart(markers, poligono, map);
        }

        function dragstart(markers, poligono, map) {
            for (var j = 1; j <= markers.length; j++) {
                var este = markers[j];

                google.maps.event.addListener(este, 'dragend', function (event) {
                    dragEnd(j, event.latLng, map);
                });
            }
        }

        function dragEnd(pos, puntoLatLng, map) {
            hideMarkers();

            markers_aux = [];
            marker_vector_aux = [];

            for (var b = markers.length; b >= pos; b--) {
                if (b != pos) {
                    var maux = markers.pop();
                    markers_aux.push(maux);

                    var paux = marker_vector.pop();
                    marker_vector_aux.push(paux);
                } else {
                    markers.pop();
                    marker_vector.pop();

                    if (markers.length == 0) {
                        markers = [];
                        marker_vector = [];
                        var marker = new google.maps.Marker({
                            position: puntoLatLng,
                            map: map,
                            title: '#' + ' - ' + markers.length + ' ? ' + puntoLatLng,
                            draggable: true
                        });

                        marker_vector.push(puntoLatLng);
                        markers.push(marker);
                    } else {
                        var marker = new google.maps.Marker({
                            position: puntoLatLng,
                            map: map,
                            title: '#' + ' - ' + markers.length + ' - ' + puntoLatLng,
                            draggable: true
                        });

                        marker_vector.push(puntoLatLng);
                        markers.push(marker);
                    }
                }
            }

            for (var z = markers_aux.length; z > 0; z--) {
                var bc = markers_aux.pop();
                markers.push(bc);
                var bd = marker_vector_aux.pop();
                marker_vector.push(bd);
            }

            poligono.setPath(marker_vector);
            showMarkers(map);
            dragstart(markers, poligono, map);
        }
        function addPuntos(location, map) {
            var marker = new google.maps.Marker({
                position: location,
                //icon: icon,
                title: '#',
                map: map,
                visible: false
            });
            markersp.push(marker);
            array_puntos.push(location);
            var ct = array_puntos.length;
            if (ct == 3) {
                google.maps.event.removeListener(listener);
            }
            //map.setCenter(marker.getPosition());
            poligono_aux.setPath(array_puntos);

        }

        function PintarGeocerca(myobject, map1, color) {
            array_puntos = latitudes(myobject);
            poligono_aux = new google.maps.Polygon({
                paths: array_puntos,
                editable: true,
                strokeColor: color,
                fillColor: color,
                strokeOpacity: 1.0,
                strokeWeight: 2
            });
            bounds = (array_puntos.length > 0) ? createBoundsForMarkers(poligono_aux) : null;
            poligono_aux.setMap(map1);
            map1.setCenter(getLatLngCenter(myobject));

            map1.setZoom(getBoundsZoomLevel(bounds, mapDim));

        }
        function rad2degr(rad) { return rad * 180 / Math.PI; }
        function degr2rad(degr) { return degr * Math.PI / 180; }
        /*
        Deg array of arrays with latitude and longtitude
         *   pairs in degrees. e.g. [[latitude1, longtitude1], [latitude2
         *   [longtitude2] ...]
         *
         * @return array with the center latitude longtitude pairs in 
         *   degrees.
         */
        function getLatLngCenter(latLngInDegr) {
            var LATIDX = 0;
            var LNGIDX = 1;
            var sumX = 0;
            var sumY = 0;
            var sumZ = 0;

            for (var i = 0; i < latLngInDegr.length; i++) {
                var lat = degr2rad(latLngInDegr[i].Latitud);
                var lng = degr2rad(latLngInDegr[i].Longitud);
                // sum of cartesian coordinates
                sumX += Math.cos(lat) * Math.cos(lng);
                sumY += Math.cos(lat) * Math.sin(lng);
                sumZ += Math.sin(lat);
            }

            var avgX = sumX / latLngInDegr.length;
            var avgY = sumY / latLngInDegr.length;
            var avgZ = sumZ / latLngInDegr.length;

            // convert average x, y, z coordinate to latitude and longtitude
            var lng = Math.atan2(avgY, avgX);
            var hyp = Math.sqrt(avgX * avgX + avgY * avgY);
            var lat = Math.atan2(avgZ, hyp);

            return new google.maps.LatLng(rad2degr(lat), rad2degr(lng));
        }
        function latitudes(arrays) {
            var ld = [];
            for (var i = 0; i < arrays.length; i++) {
                ld.push(new google.maps.LatLng(arrays[i].Latitud, arrays[i].Longitud))
            }
            return ld;
        }
        //      public int CodigoGEO { get; set; }
        //public string Descripcion { get; set; }
        //public string ColorLimite { get; set; }
        //public string ColorRelleno { get; set; }
        //public int CodTipoGEO { get; set; }
        //TipoDescripcion
        //public string UsuaReg { get; set; }
        //public System.DateTime FechaReg { get; set; }
        //public string UsuaModif { get; set; }
        //public System.DateTime FechaModif { get; set; }
        //public string NIT { get; set; }
        var columnDefsGeocerca = [
            {
                headerName: "ID", field: "CodigoGEO", width: 75, filterParams: { newRowsAction: 'keep' },
                checkboxSelection: function (params) {
                    // we put checkbox on the name if we are not doing grouping
                    return params.columnApi.getRowGroupColumns().length === 0;
                },
                headerCheckboxSelection: function (params) {
                    // we put checkbox on the name if we are not doing grouping
                    return params.columnApi.getRowGroupColumns().length === 0;
                }
            },
            //{ headerName: '', width: 34, checkboxSelection: true, suppressSorting: true, suppressMenu: true },
            //{ headerName: "Codigo Cliente", field: "CodCliente", width: 150 },
            { headerName: "Geocerca", field: "Descripcion", width: 150 },
            { headerName: "Zona", field: "TipoDescripcion", width: 150 },
            { headerName: "Nit", field: "NIT", width: 150 },
            { headerName: "ColorLimite", field: "ColorLimite", width: 150, hide: true },
            { headerName: "ColorRelleno", field: "ColorRelleno", width: 150, hide: true },
            { headerName: "CodTipoGEO", field: "CodTipoGEO", width: 150, hide: true },
            {
                headerName: "FechaReg", field: "FechaReg", width: 150, hide: true
            },
            { headerName: "", pinned: 'right', width: 40, cellRenderer: eliminar }

        ];
        var gridOptionGeocerca = {
            enableSorting: true,
            enableFilter: true,
            enableColResize: true,
            columnDefs: columnDefsGeocerca,
            rowSelection: 'single',

        };
        function eliminar(params) {
            //var html = '<a title="Remove" href="javascript:;" class="align-center btn-link btn-sm" ng-click="RemoveRecord(' + params.rowIndex + ')">Delete</a>';
            var html = '<button type="button" id="idbutton" data-action-type="view1" class="btn btn-xs btn-danger" onclick="bntEliminar(' + params.data.CodigoGEO + ');" ><span class="glyphicon glyphicon-erase"></span> </button>'
            return html;
        }
        function bntEliminar(cod) {
            // the action delete or function call modal where quetion.
            alert('pruebaas');
        }
        function setDataSource(allOfTheData, gridOption) {
            gridOption.api.setRowData(allOfTheData);
        }
        function createGlobal(gridDiv, gridOptions) {
            new agGrid.Grid(gridDiv, gridOptions);
        }

        function getDataGeo() {
            var cd = SelectionGeocerca;
            var color = $('#tipocolor').val();
            var nombre = $('#txtnombre').val();
            var codtipozona = $find('<%=cbozonas.ClientID%>');
            var cod = codtipozona.get_selectedItem().get_value();
            if (cod > 0 && nombre != "") {
                var geocerca = new geocercaSerial();
                geocerca.CodigoGEO = cd;
                geocerca.Descripcion = nombre;
                geocerca.ColorLimite = "#FF0000";
                geocerca.ColorRelleno = color;
                geocerca.CodTipoGEO = cod;
                return geocerca;
            }
            return "";
        }
        function onSelectionGeocerca() {
            var selectedRows = gridOptionGeocerca.api.getSelectedRows();
            var selectedRowsString = 0;
            selectedRows.forEach(function (selectedRow, index) {
                if (index != 0) {
                    selectedRowsString = 0;
                }
                selectedRowsString = selectedRow.CodigoGEO;
            });
            return selectedRowsString;
        }
        function editPoigono() {
            BloquearPantalla();
            accion = 1;
            SelectionGeocerca = onSelectionGeocerca();

            var j = { idgeocerca: SelectionGeocerca };
            var c = JSON.stringify(j);

            if (SelectionGeocerca != 0) {
                disableSubmit("#btnNew");
                disableSubmit("#btnView");
                $.ajax({
                    url: "FrmZonas1.aspx/GetPuntos",
                    type: "POST",
                    data: c,
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    cache: false
                }).done(function (data) {
                    var myobject = JSON.parse(data.d);
                    var dat = myobject.Coordenadas;
                    if (dat.length > 0) {
                        $("#campo2").slideToggle("slow");
                        $('#txtcolor').val(myobject.ColorRelleno);
                        $('#txtnombre').val(myobject.Descripcion);
                        changecombo(myobject.CodTipoGEO);
                        disableSubmit("#btnEdit");
                        PintarGeocerca(dat, map, myobject.ColorRelleno);
                    } else {
                        error("No existe Puntos para dibujar");
                    }

                }).fail(function (jqXHR, textStatus) {
                    error("Fallo intente" + textStatus);
                    enableSubmit("#btnNew");
                    enableSubmit("#btnView");

                }).always(function (jqXHR, textStatus) {
                    DesbloquearPantalla();

                });
            } else {
                error("Seleccione alguna Geocerca de la grilla");
                DesbloquearPantalla();
            }

        }
        function changecombo(myItemValue) {
            var combo = $find("<%= cbozonas.ClientID %>");
            var itm = combo.findItemByValue(myItemValue);
            itm.select();
        }
        function AddGeocerca() {
            BloquearPantalla();
            checkDatos();
            var r = getDataGeo();
            if (r != "") {
                var lis = GetPuntosLong();
                if (lis.length > 0) {
                    var dat = { geocerca: r, puntosGeocercas: lis, accion: accion };
                    var jx = JSON.stringify(dat);
                    $.ajax({
                        url: "FrmZonas1.aspx/AddUpdate",
                        data: jx,
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        dataType: "JSON"
                    }).done(function (data) {
                        //SI HA GUARDO EXITOSAMENTE TEMENOS QUE AVISAR AL USAARIO LIMPIAR LOS CAMPOS PRA UN NUEVO RESGISTRO, Y ACTUALIZAR EL DATAGRID.PARA  QUE SE NUESTRE EL  DATO QUE SE ADICIONO.
                        enableSubmit("#btnNew");
                        enableSubmit("#btnView");
                    }).fail(function (jqXHR, textStatus) {
                        // EN CASO QUE ALGO FALLO,
                        error(textStatus);
                    }).always(function (jqXHR, textStatus) {
                        // ES SIEMPRE SE EJECUTA, DONE O FIAL 
                        DesbloquearPantalla();
                    });
                } else {
                    DesbloquearPantalla();
                    error("No tiene ninguna coordena capturada");
                }

            } else {
                DesbloquearPantalla();
                error("falta Datos Rellemar");
            }

        }
        function CargarGeocerca() {
            //string[] lista, string fechaI, string fechaF, int tipoRel, int tiempoDet
            var gridDiv = document.querySelector('#myGridGeocerca');
            createGlobal(gridDiv, gridOptionGeocerca);
            $.ajax({
                url: "/FrmZonas1.aspx/getGeocercaAll",
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (data) {
                myData = JSON.parse(data.d);
                rowData = myData;
                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                error("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    error("Administrador -> " + jqXHR.statusText);
                } else {
                    //gridOptions.api.setRowData(myData);
                    setDataSource(myData, gridOptionGeocerca);
                }
                //DesbloquearPantalla();
            });
        }
        $(document).ready(function () {
            CargarGeocerca();
            $('.my-colorpicker2').colorpicker()
        });
    </script>

    <%--<script src="Content/js/ControlZona.js"></script>--%>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8FXziUDgzJajbYyYAWXVRKqoKv3g6hFs&signed_in=true&callback=initMap">
    </script>

</asp:Content>
