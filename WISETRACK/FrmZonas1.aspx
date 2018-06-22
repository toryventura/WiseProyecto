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
    </style>

    <script>
        $(document).ready(function () {

        });
    </script>

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

                            <input type="text" id="txtnombre" value="" class="form-control" title="Nombre es requerido. Longitud 4-20 caracteres" required="required" pattern="[0-9][a-zA-Z]{4,20}" />
                        </div>

                        <div class="form-group">
                            <label id="Label1" font-size="Smaller">Tipo Zona</label>
                            <asp:UpdatePanel ID="upcbozonas" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox CssClass="dropdown" ID="cbozonas" AllowCustomText="true"
                                        AutoPostBack="true" runat="server" EmptyMessage="Zonas" Font-Size="Smaller"
                                        DropDownCssClass="dropdown">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                            <a class="btn btn-app" onclick="nuevoPoligono()">
                                <i class="material-icons">add_location</i> Edit
                            </a>
                            <%--<input type="button" id="btnNuevo" value="Nuevo" class="btn btn-primary btn-xs" onclick="nuevoPoligono()" />--%>
                            <button type="button" title="Nueva Geocerca" class="btn btn-primary btn-xs" onclick="nuevoPoligono();">
                                <i class="material-icons" style="font-size: 48px;">add_location</i>
                            </button>

                            <%--<input type="button" id="btnGuardar" value="Guardar" class="add_location" onclick="enviarGeocerca()" />--%>
                            <button type="button" title="Visualizar Geocercas" class="btn btn-primary btn-xs" onclick="enviarGeocerca();" data-togle="Editar">
                                <i class="material-icons" style="font-size: 48px;">satellite</i>
                            </button>


                            <%--<input type="button" id="btnLimpiar" value="Cancelar" class="btn btn-primary btn-xs " onclick="cancelPoligono()" />--%>
                            <button type="button" title="Guardar Geocercas" class="btn btn-primary btn-xs" onclick="EditarPoligono();" data-togle="Editar">
                                <i class="material-icons" style="font-size: 48px;">edit_location</i>
                            </button>

                            <%--<input type="button" id="btnLimpiar" value="Cancelar" class="btn btn-primary btn-xs " onclick="cancelPoligono()" />--%>
                            <button type="button" title="Limpiar" class="btn btn-primary btn-xs" onclick="cancelPoligono();" data-togle="Editar">
                                <i class="material-icons" style="font-size: 48px">delete_sweep</i>
                            </button>

                            <button type="button" class="btn btn-primary btn-xs" onclick="Mensaje();">
                                <i class="material-icons" style="font-size: 48px;">add_location</i>
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
        var markers = [];
        var markers_aux = [];
        //para pintar
        var markersp = [];
        var array_puntos = [];
        var poligono_aux;

        //Dibujar poligono al hacer click
        var marker_vector = [];
        var marker_vector_aux = [];

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
        function Mensaje() {
            alert("hola");
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

        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -17.783288, lng: -63.1817407 },
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.NORMAL
            });


            //Dibujar poligono al hacer click
            poligono = new google.maps.Polygon({
                paths: marker_vector,
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35
            });

            poligono_aux = new google.maps.Polygon({
                paths: array_puntos,
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35,
                editable: true,
                draggable: true
            });

            poligono_aux.addListener('rightclick', showArrays);
            infoWindow = new google.maps.InfoWindow;
            poligono.setMap(map);
            poligono_aux.setMap(map);
        }

        function showArrays(event) {
            var vertices = this.getPath();
            var contentString = '<b>Bermuda Triangle polygon</b><br>' +
                'Clicked location: <br>' + event.latLng.lat() + ',' + event.latLng.lng() +
                '<br>';
            // Iterate over the vertices.
            var mlistpuntosgeo = [];
            for (var i = 0; i < vertices.getLength() ; i++) {
                var xy = vertices.getAt(i);
                contentString += '<br>' + 'Coordinate ' + i + ':<br>' + xy.lat() + ',' +
                    xy.lng();
                var mpuntosgeocerca = new puntosgeoSerial();
                mpuntosgeocerca.Latitud = xy.lat();
                mpuntosgeocerca.Longitud = xy.lng();
                mlistpuntosgeo.push(mpuntosgeocerca);
            }

            ModificarGeocerca(mlistpuntosgeo);
            // Replace the info window's content and position.
            //infoWindow.setContent(contentString);
            //infoWindow.setPosition(event.latLng);
            //infoWindow.open(map);
        }

        function ModificarGeocerca(lista) {
            $('#myModal1').modal('show');
            $('#btneditargeo').click(function () {
                if (lista.length > 0) {
                    var data = JSON.stringify(lista);
                    //  var mid = document.getElementById('lblidgeocerca.ClientID%>').innerHTML;
                    //  WISETRACK.WebServices.WisetrackServices.ActualizarGeocerca(mid, data, onSuccess, OnFailed);
                }

            });

        }


        function cancelPoligono() {
            limpiarTodo();
            limpiarTodo2();
            google.maps.event.removeListener(listener);
        }

        function nuevoPoligono() {
            $("#campo2").slideToggle("slow");
            limpiarTodo();
            limpiarTodo2();
            poligono.setPath(marker_vector);
            poligono_aux.setPath(array_puntos);
            listener = new google.maps.event.addListener(map, 'click', function (event) {
                //addPoli(event.latLng, map);
                addPuntos(event.latLng, map);
            });
        }

        function limpiarTodo() {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
            markers_aux = [];
            marker_vector_aux = [];
            marker_vector = [];

            //Limpiar el vector de la geocerca visualizada
            var marker_vectorM = [];
            var marker_vector_auxM = [];

            poligono.setPath(marker_vector);
        }

        function limpiarTodo2() {
            for (var i = 0; i < markersp.length; i++) {
                markersp[i].setMap(null);
            }

            markersp = [];
            array_puntos = [];
            poligono_aux.setPath(array_puntos);
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

        function showMarkers(map) {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(map);
            }
        }

        function hideMarkers() {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
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
        function enviarGeocerca() {
            var color = $('#tipocolor').val();
            var nombre = $('#txtnombre').val();
            var codtipozona = $find('<%=cbozonas.ClientID%>');
            var cod = codtipozona.get_selectedItem().get_value();
            var listgeo = [];
            var listpuntosgeo = [];

            if (cod > 0) {
                var geocerca = new geocercaSerial();
                geocerca.Descripcion = nombre;
                geocerca.ColorLimite = "#FF0000";
                geocerca.ColorRelleno = color;
                geocerca.CodTipoGEO = cod;
                listgeo.push(geocerca);

                for (var i = 0; i < markers.length; i++) {
                    var mlat = markers[i];
                    var puntos = new puntosgeoSerial();
                    puntos.Latitud = mlat.getPosition().lat();
                    puntos.Longitud = mlat.getPosition().lng();
                    //alert("puntos " + puntos.Latitud + " - " + puntos.Longitud);
                    listpuntosgeo.push(puntos);
                }
            }
            if (listgeo.length > 0) {
                var data = JSON.stringify(listgeo);
                var data2 = JSON.stringify(listpuntosgeo);
                WISETRACK.WebServices.WisetrackServices.enviarGeocerca(data, data2, onSuccess, OnFailed);
            }
        }

        function onSuccess(response) {
            alert(response);
            $('#myModal1').modal('hide');
        }

        function OnFailed(response) {
            alert("ERROR 500 BAD INTERNAL SERVER");
            $('#myModal1').modal('hide');
        }

        function pintarGeocercaPrueba() {
            cancelPoligono();
            limpiarTodo();
            //  var gdvpgeo = document.getElementById( gdvPuntosGeo.ClientID%>");
            var count = gdvpgeo.rows.length;
            if (count > 0) {
                for (var i = 1; i < count; i++) {
                    var grilla = gdvpgeo.rows[i];
                    var latlngp = new google.maps.LatLng(parseFloat(grilla.cells[1].innerText), parseFloat(grilla.cells[2].innerText));
                    //var latlngp = new google.maps.LatLng(parseFloat(grilla.cells[1].getElementsByTagName("INPUT")[0]), parseFloat(grilla.cells[1].getElementsByTagName("INPUT")[0]));
                    //alert("" + latlngp);
                    var marker = new google.maps.Marker({
                        position: latlngp,
                        //icon: icon,
                        title: '#' + i,
                        map: map,
                        visible: false
                    });
                    markersp.push(marker);
                    array_puntos.push(latlngp);
                }
                map.setCenter(marker.getPosition());
                poligono_aux.setPath(array_puntos);
            } else {
            }

        }
        var columnDefsGeocerca = [
            {
                headerName: "ID", field: "ID", width: 75, filterParams: { newRowsAction: 'keep' },
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
        { headerName: "Geocerca", field: "geocerca", width: 150 },
        { headerName: "Zona", field: "zona", width: 150 },
        { headerName: "Nit", field: "nit", width: 150 }

        ];
        var gridOptionGeocerca = {

            columnDefs: columnDefsGeocerca,

            enableFilter: true,

            rowSelection: 'multiple',
            enableSorting: true,
            showToolPanel: false


        };
        function setDataSource(allOfTheData, gridOption) {

            gridOption.api.setRowData(allOfTheData);

        }
        function createGlobal(gridDiv, gridOptions) {

            new agGrid.Grid(gridDiv, gridOptions);
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
                alert("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    alert("Administrador -> " + jqXHR.statusText);
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

    <script src="Content/js/ControlZona.js"></script>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8FXziUDgzJajbYyYAWXVRKqoKv3g6hFs&signed_in=true&callback=initMap">
    </script>

</asp:Content>
