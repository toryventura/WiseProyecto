<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmZonas.aspx.cs" Inherits="WISETRACK.FrmZonas" %>

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
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblzona" runat="server" Text="Tipo Zona" Font-Size="Smaller"></asp:Label>
                            </div>
                            <div class="form-group">
                                &nbsp
                                <asp:LinkButton ID="lbleli" runat="server" Text="Eliminar Geocerca" Font-Size="Smaller" OnClick="lbleli_Click"></asp:LinkButton>
                                &nbsp
                                <a href="#" class="alert-link" data-toggle="modal" style="font-size: smaller" data-target="#myModal">Visualizar todas las Geocercas</a>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdatePanel ID="prueba" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadComboBox CssClass="dropdown" ID="cbotzona" AllowCustomText="true" AutoPostBack="true" runat="server" EmptyMessage="Zonas" Font-Size="Smaller" DropDownCssClass="dropdown" OnSelectedIndexChanged="cbotzona_SelectedIndexChanged"></telerik:RadComboBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <asp:UpdatePanel ID="upbtnbuscar" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary btn-xs" Font-Size="Smaller" Text="Buscar" OnClick="btnBuscar_Click" UseSubmitBehavior="false" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <input type="button" id="btnVer" value="Mostrar" font-size="Smaller" class="btn btn-primary btn-xs" onclick="pintarGeocercaPrueba()" />
                            </div>

                        </div>

                    </div>
                    <%--PARTE 2 --%>
                    <div class="panel-title label-info" style="font-size: small" id="menu2"><b>+ Filtros</b></div>
                    <div class="panel-body" id="campo2">
                        <div class="table-responsive" style="height: 150px; overflow-y: scroll; border-style: groove; border-color: #5bc0de">
                            <asp:UpdatePanel ID="prueba2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvGeocerca" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="Smaller" AutoGenerateColumns="False" DataKeyNames="CodigoGEO"
                                        EmptyDataText="Seleccione Tipo Geocerca" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gdvGeocerca_RowDataBound" EnableViewState="true">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckboxGeocerca" runat="server" Font-Size="Smaller" Visible="true" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                                <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodigoGEO" HeaderText="ID" ReadOnly="true" SortExpression="CodigoGEO" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Geocerca" SortExpression="Descripcion" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs">
                                                <HeaderStyle CssClass="visible-lg visible-md visible-xs"></HeaderStyle>
                                                <ItemStyle CssClass="visible-lg visible-md visible-xs"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Zona" HeaderText="Zona" SortExpression="Zona" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs">
                                                <HeaderStyle CssClass="visible-lg visible-md visible-xs"></HeaderStyle>

                                                <ItemStyle CssClass="visible-lg visible-md visible-xs"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NIT" HeaderText="NIT" SortExpression="NIT" HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle CssClass="success" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="form-group"></div>
                        <div class="table-responsive" style="height: 150px; overflow-y: scroll; border-style: groove; border-color: #5bc0de">
                            <asp:UpdatePanel ID="upgvpuntosgeo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvPuntosGeo" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="Smaller" AutoGenerateColumns="False" DataKeyNames="CodigoGEO" EmptyDataText="Seleccione una Geocerca!.">
                                        <Columns>
                                            <asp:BoundField DataField="CodigoGEO" HeaderText="ID" ReadOnly="True" SortExpression="CodigoGEO" />
                                            <asp:BoundField DataField="Latitud" HeaderText="Latitud" SortExpression="Latitud" HeaderStyle-CssClass="visible-lg visible-xs" ItemStyle-CssClass="visible-lg visible-xs" />
                                            <asp:BoundField DataField="Longitud" HeaderText="Longitud" SortExpression="Longitud" ItemStyle-CssClass="visible-xs visible-lg" HeaderStyle-CssClass="visible-xs visible-lg" />
                                            <asp:BoundField DataField="FechaReg" HeaderText="FechaReg" SortExpression="FechaReg" HeaderStyle-CssClass="hidden-xs visible-md" ItemStyle-CssClass="hidden-xs visible-md" />
                                            <asp:BoundField DataField="ColorLimite" HeaderText="Limite" SortExpression="Stroke" HeaderStyle-CssClass="hidden-xs visible-md" ItemStyle-CssClass="hidden-xs visible-md" />
                                            <asp:BoundField DataField="ColorRelleno" HeaderText="Relleno" SortExpression="Fill" HeaderStyle-CssClass="hidden-xs visible-md" ItemStyle-CssClass="hidden-xs visible-md" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle CssClass="success" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="panel-title label-info" style="font-size: small" id="menu3"><b>+ Nuevo Registro</b></div>
                    <div class="panel-body" id="campo3" style="display: none">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblnombre" runat="server" Text="Nombre" Font-Size="Smaller"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <%--<input type="text" id="txtnombre" name="txtnombre" class="form-control" style="font-size: smaller" value="" />--%>
                            <input type="text" id="txtnombre" value="" class="form-control" title="Nombre es requerido. Longitud 4-20 caracteres" required="required" pattern="[0-9][a-zA-Z]{4,20}" />
                        </div>

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lbltipoz" runat="server" Text="Tipo Zona" Font-Size="Smaller"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
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
                                <asp:Label ID="lblcolorR" runat="server" Text="Color Relleno" Font-Size="Smaller"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <select id="tipocolor" class="form-control" style="font-size: smaller">
                                <option id="1" value="#FF0000">Rojo</option>
                                <option id="2" value="#FFFF00">Amarillo</option>
                                <option id="3" value="#04B404">Verde</option>
                                <option id="4" value="#0101DF">Azul</option>
                            </select>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <input type="button" id="btnNuevo" value="Nuevo" class="btn btn-primary btn-xs" onclick="nuevoPoligono()" />
                            </div>
                            <div class="form-group">
                                <input type="button" id="btnGuardar" value="Guardar" class="btn btn-primary btn-xs" onclick="enviarGeocerca()" />
                            </div>
                            <div class="form-group">
                                <input type="button" id="btnLimpiar" value="Cancelar" class="btn btn-primary btn-xs" onclick="cancelPoligono()" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnExportar" runat="server" CssClass="btn btn-primary btn-xs" Font-Size="Smaller" UseSubmitBehavior="false" Text="Exportar" OnClick="btnExportar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span9">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div id="map"></div>
                        <asp:UpdatePanel ID="uplabelID" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Label ID="lblidgeocerca" runat="server" CssClass="label-primary" Text="0" Style="display: none"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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

            listener = new google.maps.event.addListener(map, 'click', function (event) {
                addPoli(event.latLng, map);
                
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
                    var mid = document.getElementById('<%=lblidgeocerca.ClientID%>').innerHTML;
                    WISETRACK.WebServices.WisetrackServices.ActualizarGeocerca(mid, data, onSuccess, OnFailed);
                }

            });

        }


        function cancelPoligono() {
            limpiarTodo();
            limpiarTodo2();
            google.maps.event.removeListener(listener);
        }

        function nuevoPoligono() {
            limpiarTodo();
            limpiarTodo2();
            poligono.setPath(marker_vector);
            poligono_aux.setPath(array_puntos);
            listener = new google.maps.event.addListener(map, 'click', function (event) {
                addPoli(event.latLng, map);
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
            var gdvpgeo = document.getElementById("<%= gdvPuntosGeo.ClientID%>");
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
                alert("Duerme, ya es tarde");
            }

        }
    </script>

    <script src="Content/js/ControlZona.js"></script>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8FXziUDgzJajbYyYAWXVRKqoKv3g6hFs&signed_in=true&callback=initMap">
    </script>

</asp:Content>
