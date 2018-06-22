<%@ Page Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmSeguimientoant.aspx.cs" Inherits="WISETRACK.FrmSeguimiento" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        #map {
            width: auto;
            height: 620px;
        }

        body {
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }

        @media (min-width: 1024px) and (max-width: 1366px) {
            .navbar-text.pull-right {
                float: none;
                padding-left: 5px;
                padding-right: 5px;
            }
        }
    </style>
    <style type="text/css">
        #overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #ffffff;
            width: 100%;
            height: 100%;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        #theprogress {
            /*background-color: #D3BB9C;*/
            width: 110px;
            height: 24px;
            text-align: center;
            filter: alpha(opacity=80);
            opacity: 1;
        }

        #modalprogress {
            position: absolute;
            top: 50%;
            left: 50%;
            margin: -11px 0 0 -55px;
            color: white;
        }

        body > #modalprogress {
            position: fixed;
        }
    </style>
    <style type="text/css">
        #styles, #add-tab {
            float: left;
            margin-top: 10px;
            width: 400px;
        }

            #styles label,
            #add-tab label {
                display: inline-block;
                width: 130px;
            }

        .phoney {
            background: -webkit-gradient(linear,left top,left bottom,color-stop(0, rgb(112,112,112)),color-stop(0.51, rgb(94,94,94)),color-stop(0.52, rgb(57,57,57)));
            background: -moz-linear-gradient(center top,rgb(112,112,112) 0%,rgb(94,94,94) 51%,rgb(57,57,57) 52%);
        }

        .phoneytext {
            text-shadow: 0 -1px 0 #000;
            color: #fff;
            font-family: Helvetica Neue, Helvetica, arial;
            font-size: 18px;
            line-height: 25px;
            padding: 4px 45px 4px 15px;
            font-weight: bold;
            background: url(../images/arrow.png) 95% 50% no-repeat;
        }

        .phoneytab {
            text-shadow: 0 -1px 0 #000;
            color: #fff;
            font-family: Helvetica Neue, Helvetica, arial;
            font-size: 18px;
            background: rgb(112,112,112) !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu1").click(function () {
                $("#campo1").slideToggle("slow");
            });
        });
        $(document).ready(function () {
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

    <div class="container-fluid" style="padding-top: 33px">
        <div class="row-fluid">
            <div class="span3">
                <div class="panel panel-primary">
                    <div class="panel-heading">Monitoreo</div>
                    <div class="panel-title label-info" style="font-size: small" id="menu1"><b>Ultima Posicion Moviles</b></div>
                    <div class="panel-body" id="campo1">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lblfiltros" runat="server" Text="Filtros" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:UpdatePanel ID="upcboplaca" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox CssClass="dropdown" ID="cboplaca" EnableTextSelection="false" runat="server"
                                        Font-Size="Small" DropDownCssClass="dropdown" AutoPostBack="true" Filter="Contains" EmptyMessage="Seleccione"
                                        OnItemDataBound="cboplaca_ItemDataBound" OnSelectedIndexChanged="cboplaca_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:Label ID="lbltrespuesta" runat="server" Text="Tiempo Respuesta" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <select id="cbotrespuesta" class="form-control" style="width: 150px" onchange="javascript:myFunction(this.value);">
                                <option value="30000">Tiempo</option>
                                <option value="30000">30 Segundos</option>
                                <option value="60000">01 Minuto</option>
                                <option value="180000">03 Minuto</option>
                                <option value="500000">05 Minutos</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <input id="btnprueba" runat="server" class="btn btn-primary btn-sm" type="button" value="Buscar" onclick="buscarVehiculo()" />

                            <asp:Button ID="btnExportar" runat="server" CssClass=" btn btn-default btn-sm" Text="Exportar" OnClick="btnExportar_Click" />
                        </div>
                    </div>

                    <div class="panel-title label-info" id="menu2" style="font-size: small"><b>+Detalle</b></div>
                    <div class="panel-body" id="campo2">
                        <div class="table-responsive table-hover">
                            <asp:UpdatePanel ID="upseguimiento" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="table-responsive" runat="server" id="divScrollTA" style="width: 100%; height: 270px; overflow-y: scroll">
                                        <asp:GridView ID="gdvSeguimiento" runat="server"
                                            CssClass="table table-striped table-bordered table-hover"
                                            Font-Size="Smaller" AutoGenerateColumns="False" DataKeyNames="ID"
                                            EmptyDataText="Seleccione un vehiculo" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableViewState="true" OnRowDataBound="gdvSeguimiento_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ckbsegclick" runat="server" Font-Size="Smaller" OnCheckedChanged="ckbsegclick_CheckedChanged" AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbseguimiento" runat="server" Font-Size="Smaller" Visible="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Image ID="image" runat="server" />
                                                        <asp:ImageButton ID="btnimage" runat="server" ImageUrl="~/Content/img/tools/verde.jpg" OnClientClick="javascript: CentrearMapa(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" SortExpression="ID" Visible="false" />
                                                <asp:BoundField DataField="EstadoMotor" HeaderText="EstadoMotor" SortExpression="EstMotor"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm"
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NroPlaca" HeaderText="Nro Placa" SortExpression="Placa"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Velocidad" HeaderText="Velocidad" SortExpression="Vel"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Latitud" HeaderText="Latitud" SortExpression="Lat"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm"
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Longitud" HeaderText="Longitud" SortExpression="Long"
                                                    HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm"
                                                    ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm">
                                                    <HeaderStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="hidden-lg hidden-md hidden-xs hidden-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaGPS" HeaderText="Fecha" SortExpression="Fecha"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoGPS" HeaderText="EstadoGPS" SortExpression="EstGPS"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Temperatura" HeaderText="Temperatura" SortExpression="Temp"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoPuerta" HeaderText="EstadoPuerta" SortExpression="EstPuerta"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VoltajeBateria" HeaderText="VoltajeBateria" SortExpression="VBateria"
                                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm"
                                                    ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm">
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs visible-sm"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs visible-sm"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Conductor" SortExpression="Conductor"
                                                    HeaderStyle-CssClass="visible-lg hidden-md hidden-xs"
                                                    ItemStyle-CssClass="visible-lg hidden-md hidden-xs">
                                                    <HeaderStyle CssClass="visible-lg hidden-md hidden-xs"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg hidden-md hidden-xs"></ItemStyle>
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="panel-title label-info" id="menu3" style="font-size: small"><b>+Filtros</b></div>
                    <div class="panel-body" id="campo3">
                    </div>
                </div>
            </div>
            <div class="span9">
                <div id="map"></div>
            </div>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upcboplaca">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress">
                    <div id="theprogress">
                        <img src="Content/img/tools/load.gif" alt="indicador" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div>
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000">
        </asp:Timer>
    </div>

    <script type="text/javascript" src="Scripts/jsinfobubble/src/infobubble-compiled.js">
    </script>

    <script type="text/javascript">
        function tramaTemp() {
            this.nro;
            this.ID;
            this.EstadoGPS;
            this.EstadoMotor;
            this.Velocidad;
            this.Latitud;
            this.Longitud;
            this.FechaGPS;
            this.NroPlaca;
            this.Nombre;
            this.Asimut;
            this.NIT;
            this.IDButton;
            this.Kilometraje;
            this.Temperatura;
            this.VoltajeBateria;
            this.EstadoPuerta;
            this.tipov;
        }

        var url;
        var EstadoMotorValue;
        var idLoad;
        var idfunction;
        var map;
        var markers = [];
        var markersU = [];
        var markersString = [];
        var markersStringSensores = [];
        var infowindow;
        var geocoder;
        var dir = '';
        var cbonroplaca2;
        var infoBubble;

        //funcion que inicia el mapa
        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -17.783288, lng: -63.1817407 },
                zoom: 6,
                mapTypeId: google.maps.MapTypeId.NORMAL
            });

            geocoder = new google.maps.Geocoder();

            google.maps.event.addListener(map, 'click', function (event) {
                addMarker(event.latLng, map);
            });

            infoBubble = new InfoBubble({
                maxWidth: 250
            });

            infoBubble.addTab('Datos-Vehiculo', 'Cargando...');
            infoBubble.addTab('Sensores', 'Cargando...');

            infowindow = new google.maps.InfoWindow();
        }

        function addMarker(location, map) {
            deleteMarkers();
            var image = 'Imagenes/icon/ic_action.png';
            var marker = new google.maps.Marker({
                position: location,
                title: '' + location,
                map: map,
                animation: google.maps.Animation.DROP,
                icon: image,
                draggable: true
            });
            markers.push(marker);
        }

        function setMapOnAll(map) {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(map);
            }
        }

        function deleteMarkers() {
            setMapOnAll(null);
            markers = [];
        }

        function setMapOnAllU(map) {
            for (var i = 0; i < markersU.length; i++) {
                markersU[i].setMap(map);
            }
            markersU = [];
            markersString = [];
        }

        function buscarVehiculo() {
            var _time = $('#cbotrespuesta').val();
            clearInterval(idLoad);
            clearInterval(idfunction);
            obtenerUltimaPosicion();
            idfunction = setInterval(function () { obtenerUltimaPosicion(); }, _time);
        }

        //llamar a la webservice
        function obtenerUltimaPosicion() {
            var cbonroplaca = $find('<%=cboplaca.ClientID %>');
            cbonroplaca2 = cbonroplaca.get_selectedItem().get_value();
            WISETRACK.WebServices.WisetrackServices.obtenerUltimaPosicion(cbonroplaca2, OnSuccess, OnFailed);
        }

        function cargarUltimaPosicionLoad() {
            var placaload = "todas";
            WISETRACK.WebServices.WisetrackServices.obtenerUltimaPosicion(placaload, OnSuccess, OnFailed);
        }

        function OnSuccess(response) {
            setMapOnAllU(null);
            infoBubble.close();
            var myobject = JSON.parse(response);
            var cadena = { tramaTemp: myobject };
            if (response.length > 0) {
                deleteMarkers();
                if (cadena.tramaTemp.length > 1) {
                    for (var i = 0; i < cadena.tramaTemp.length; i++) {
                        var ultubi = { lat: parseFloat(cadena.tramaTemp[i].Latitud), lng: parseFloat(cadena.tramaTemp[i].Longitud) };
                        var plac = cadena.tramaTemp[i].NroPlaca;
                        var EstadoGpsValue;
                        if (cadena.tramaTemp[i].EstadoGPS == 0) {
                            EstadoGpsValue = "Apagado";
                            obtenerFecha(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                        }

                        if (cadena.tramaTemp[i].EstadoGPS == 1) {
                            EstadoGpsValue = "Encendido";
                            obtenerFecha(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                        }

                        if (cadena.tramaTemp[i].EstadoPuerta) {
                            var epuerta = "Cerrado";
                        } else {
                            var epuerta = "Abierto";
                        }

                        var temperaturaValue;
                        if (cadena.tramaTemp[i].Temperatura == -999) {
                            temperaturaValue = "No Disponible";
                        }
                        else {
                            temperaturaValue = cadena.tramaTemp[i].Temperatura + " ºC";
                        }

                        var voltajeValue;
                        if (cadena.tramaTemp[i].VoltajeBateria == -999) {
                            voltajeValue = "No Disponible";
                        }
                        else {
                            voltajeValue = cadena.tramaTemp[i].VoltajeBateria + ' V';
                        }

                        var conductor;

                        if (cadena.tramaTemp[i].Nombre == null) {
                            conductor = "No Asignado";
                        } else {
                            conductor = cadena.tramaTemp[i].Nombre + ' ';
                        }

                        var contentString = '<div id="content">' +
                            //'<div id="infobox">' +
                            '<p><b>NroPlaca: </b>' + cadena.tramaTemp[i].NroPlaca + '</p>' +
                            //'<p><b>ID: </b>' + cadena.tramaTemp[i].ID + '</p>' +
                            '<p><b>EstadoGPS: </b>' + EstadoGpsValue + '</p>' +
                            '<p><b>EstadoMotor: </b>' + EstadoMotorValue + '</p>' +
                            '<p><b>Velocidad: </b>' + cadena.tramaTemp[i].Velocidad + '</p>' +
                            //'<p><b>IDButton: </b>' + cadena.tramaTemp[i].IDButton + '</p>' +
                            '<p><b>Fecha: </b>' + cadena.tramaTemp[i].FechaGPS + '</p>' +
                            '<p><b>Conductor: </b>' + conductor + '</p>' +
                            //'<p><b>Kilometraje: </b>' + cadena.tramaTemp[i].Kilometraje + '</p>' +
                            '<p><b>VoltajeBateria: </b>' + voltajeValue + '</p>' +
                            //'<p><b>EstadoPuerta: </b>' + cadena.tramaTemp[i].tipov + '</p>' +
                            '</div>';

                        var contentStringSensores = '<div id="content">' +
                            '<p><b>Temperatura: </b>' + temperaturaValue + '</p>' +
                            '<p><b>EstadoPuerta: </b>' + epuerta + '</p>' +
                            '<p><b>IDButton: </b>' + 0 + '</p>' +
                            '<p><b>Gasolina: </b>' + 0 + '</p>' +
                            '<p><b>Garmin: </b>' + 0 + '</p>' +
                            '<p><b>Camara: </b>' + 0 + '</p>' +
                            '</div>';

                        orientacion(cadena.tramaTemp[i].Asimut, url);

                        var marker = new google.maps.Marker({
                            position: ultubi,
                            title: '#' + plac,
                            icon: icon,
                            map: map
                        });

                        markersU.push(marker);
                        markersString.push(contentString);
                        markersStringSensores.push(contentStringSensores);

                        google.maps.event.addListener(marker, 'click', function (event) {
                            for (var i = 0; i < markersU.length; i++) {
                                var mresult = markersU[i];
                                if (mresult.position == event.latLng) {
                                    var mcad = markersString[i];
                                    var mcadsensores = markersStringSensores[i];
                                    buscarDireccion(event.latLng);
                                    var mensajito = "<div id='infobox'><p><b>Direccion: </b>" + dir + "</p></div>";
                                    infoBubble.updateTab(0, 'Datos-Vehiculo', mcad + mensajito);
                                    infoBubble.updateTab(1, 'Sensores', mcadsensores);
                                    infoBubble.open(map, mresult);
                                }
                            }
                        });
                    }

                } else {
                    for (var i = 0; i < cadena.tramaTemp.length; i++) {
                        var ultubi = { lat: parseFloat(cadena.tramaTemp[i].Latitud), lng: parseFloat(cadena.tramaTemp[i].Longitud) };
                        var plac = cadena.tramaTemp[i].NroPlaca;
                        var EstadoGpsValue;
                        if (cadena.tramaTemp[i].EstadoGPS == 0) {
                            EstadoGpsValue = "Apagado";
                            obtenerFecha(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                        }

                        if (cadena.tramaTemp[i].EstadoGPS == 1) {
                            EstadoGpsValue = "Encendido";
                            obtenerFecha(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                        }

                        if (cadena.tramaTemp[i].EstadoPuerta) {
                            var epuerta = "Cerrado";
                        } else {
                            var epuerta = "Abierto";
                        }

                        var temperaturaValue;
                        if (cadena.tramaTemp[i].Temperatura == -999) {
                            temperaturaValue = "No Disponible";
                        }
                        else {
                            temperaturaValue = cadena.tramaTemp[i].Temperatura + " ºC";
                        }

                        var voltajeValue;
                        if (cadena.tramaTemp[i].VoltajeBateria == -999) {
                            voltajeValue = "No Disponible";
                        }
                        else {
                            voltajeValue = cadena.tramaTemp[i].VoltajeBateria + ' V';
                        }

                        var conductor;

                        if (cadena.tramaTemp[i].Nombre == null) {
                            conductor = "No Asignado";
                        } else {
                            conductor = cadena.tramaTemp[i].Nombre + ' ';
                        }

                        var contentString = '<div id="content">' +
                            //'<div id="infobox">' +
                            '<p><b>NroPlaca: </b>' + cadena.tramaTemp[i].NroPlaca + '</p>' +
                            //'<p><b>ID: </b>' + cadena.tramaTemp[i].ID + '</p>' +
                            '<p><b>EstadoGPS: </b>' + EstadoGpsValue + '</p>' +
                            '<p><b>EstadoMotor: </b>' + EstadoMotorValue + '</p>' +
                            '<p><b>Velocidad: </b>' + cadena.tramaTemp[i].Velocidad + '</p>' +
                            //'<p><b>IDButton: </b>' + cadena.tramaTemp[i].IDButton + '</p>' +
                            '<p><b>Fecha: </b>' + cadena.tramaTemp[i].FechaGPS + '</p>' +
                            '<p><b>Conductor: </b>' + conductor + '</p>' +
                            //'<p><b>Kilometraje: </b>' + cadena.tramaTemp[i].Kilometraje + '</p>' +
                            '<p><b>VoltajeBateria: </b>' + voltajeValue + '</p>' +
                            //'<p><b>EstadoPuerta: </b>' + cadena.tramaTemp[i].tipov + '</p>' +
                            '</div>';

                        var contentStringSensores = '<div id="content">' +
                            '<p><b>Temperatura: </b>' + temperaturaValue + '</p>' +
                            '<p><b>EstadoPuerta: </b>' + epuerta + '</p>' +
                            '<p><b>IDButton: </b>' + 0 + '</p>' +
                            '<p><b>Gasolina: </b>' + 0 + '</p>' +
                            '<p><b>Garmin: </b>' + 0 + '</p>' +
                            '<p><b>Camara: </b>' + 0 + '</p>' +
                            '</div>';

                        orientacion(cadena.tramaTemp[i].Asimut, url);

                        //alert("Aqui " + icon + " - " + cadena.tramaTemp[i].NroPlaca);
                        var marker = new google.maps.Marker({
                            position: ultubi,
                            title: '#' + plac,
                            icon: icon,
                            map: map
                        });

                        markersU.push(marker);
                        markersString.push(contentString);
                        markersStringSensores.push(contentStringSensores);

                        google.maps.event.addListener(marker, 'click', function (event) {
                            for (var i = 0; i < markersU.length; i++) {
                                var mresult = markersU[i];
                                if (mresult.position == event.latLng) {
                                    var mcad = markersString[i];
                                    var mcadsensores = markersStringSensores[i];
                                    buscarDireccion(event.latLng);
                                    var mensajito = "<div id='infobox'><p><b>Direccion: </b>" + dir + "</p></div>";
                                    infoBubble.updateTab(0, 'Datos-Vehiculo', mcad + mensajito);
                                    infoBubble.updateTab(1, 'Sensores', mcadsensores);
                                    infoBubble.open(map, mresult);
                                }
                            }
                        });
                    }
                    map.setCenter(marker.getPosition());
                }

            } else {
                alert("No existen datos");
            }
        }

        function obtenerFecha(_fecha, _emotor, _tipov) {
            var factual = new Date();
            var f2 = new Date(_fecha);
            var r = ((factual - f2) / 1000);
            r = r - 14440;
            if (r < 1800) { //x < 30 min
                tipoVehiculo(_tipov);
                obtenerEstadoMotor(_emotor);
            } else {
                if ((r >= 1800) && (r <= 3600)) { // x >= 30 min and x < 1hr
                    obtenerEstadoMotorByGPS(_emotor);
                    tipoVehiculo(_tipov);
                    url = url + "/amarillo/";
                } else {
                    if (r > 3600) { // x > 1hr
                        obtenerEstadoMotorByGPS(_emotor);
                        tipoVehiculo(_tipov);
                        url = url + "/rojo/";
                    }
                }
            }
        }

        function tipoVehiculo(_tipove) {
            var nombretipov = "";
            switch (_tipove) {
                case 1:
                    nombretipov = "auto";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 2:
                    nombretipov = "minibus";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 3:
                    nombretipov = "vagoneta";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 4:
                    nombretipov = "moto";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 5:
                    nombretipov = "camioneta";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 6:
                    nombretipov = "camion";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 7:
                    nombretipov = "cisterna";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 8:
                    nombretipov = "persona";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 9:
                    nombretipov = "compactador";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 10:
                    nombretipov = "volqueta";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 11:
                    nombretipov = "tractor";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 12:
                    nombretipov = "grua";
                    url = "Content/img/movil/" + nombretipov;
                    break;
                case 13:
                    nombretipov = "excavadora";
                    url = "Content/img/movil/" + nombretipov;
                    break;
            }
        }

        function obtenerEstadoMotor(_motor) {
            switch (_motor) {
                case 11:
                    EstadoMotorValue = "Ignicion en Descanso";
                    url = url + "/azul/";
                    break;
                case 12:
                    EstadoMotorValue = "Ignicion en Movimiento";
                    url = url + "/verde/";
                    break;
                case 21:
                    EstadoMotorValue = "Designicion en Descanso";
                    url = url + "/azul/";
                    break;
                case 22:
                    EstadoMotorValue = "Designicion en Movimiento";
                    url = url + "/celeste/";
                    break;
                case 41:
                    EstadoMotorValue = "Descanso con motor Encendido";
                    url = url + "/verde/";
                    break;
                case 42:
                    EstadoMotorValue = "Movimiento con Motor Encendido";
                    url = url + "/verde/";
                    break;
                case 1:
                    EstadoMotorValue = "Con ignicion";
                    url = url + "/verde/";
                    break;
                case 0:
                    EstadoMotorValue = "Sin ignicion";
                    url = url + "/azul/";
                    break;
                default:
                    EstadoMotorValue = "Revisar";
                    url = url + "/coral/";
                    break;
            }
        }

        function obtenerEstadoMotorByGPS(_motor) {
            switch (_motor) {
                case 11:
                    EstadoMotorValue = "Ignicion en Descanso";
                    break;
                case 12:
                    EstadoMotorValue = "Ignicion en Movimiento";
                    break;
                case 21:
                    EstadoMotorValue = "Designicion en Descanso";
                    break;
                case 22:
                    EstadoMotorValue = "Designicion en Movimiento";
                    break;
                case 41:
                    EstadoMotorValue = "Descanso con motor Encendido";
                    break;
                case 42:
                    EstadoMotorValue = "Movimiento con Motor Encendido";
                    break;
                case 1:
                    EstadoMotorValue = "Con Ignicion";
                    break;
                case 0:
                    EstadoMotorValue = "Sin ignicion";
                    break;
                default:
                    EstadoMotorValue = "Revisar";
                    break;
            }
        }

        function buscarDireccion(slatlng) {
            var direcion = "";
            geocoder.geocode({ 'location': slatlng }, function (results, status) {

                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        direcion = results[0].formatted_address;
                        dir = '' + direcion;
                    } else {
                        direcion = 'No results found';
                        dir = direcion;
                    }
                } else {
                    direcion = 'Geocoder failed due to: ' + status;
                    dir = direcion;
                }
            });
        }

        function orientacion(asimut, _url) {
            if (asimut >= 338) {
                resultado = "norte";
                icon = _url + resultado + ".png";
            }
            if (asimut <= 23) {
                resultado = "norte";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 23 && asimut < 68) {
                resultado = "noreste";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 68 && asimut < 113) {
                resultado = "este";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 113 && asimut < 158) {
                resultado = "sureste";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 158 && asimut < 203) {
                resultado = "sur";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 203 && asimut < 248) {
                resultado = "suroeste";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 248 && asimut < 293) {
                resultado = "oeste";
                icon = _url + resultado + ".png";
            }
            if (asimut >= 293 && asimut < 338) {
                resultado = "noroeste";
                icon = _url + resultado + ".png";
            }
        }

        function OnFailed(response) {
            alert("ERROR 500: BAD INTERNAL SERVER");
        }

        function myFunction(_time) {
            clearInterval(idLoad);
            clearInterval(idfunction);
            obtenerUltimaPosicion();
            idfunction = setInterval(function ()
            {
                obtenerUltimaPosicion();
            }, _time);
        }

        //Carga al iniciar el formulario
        function myPosicionLoad() {
            cargarUltimaPosicionLoad();
            idLoad = setInterval(function ()
            {
                cargarUltimaPosicionLoad();
            }, 30000);
        }

        function CentrearMapa(data) {
            var row = data.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            ////Latitud
            var uno = row.cells[5].innerHTML;
            //Longitud
            var dos = row.cells[6].innerHTML;
            var union = { lat: uno, lng: dos };
            map.setCenter(new google.maps.LatLng(uno, dos));
        }

        window.onload = myPosicionLoad();

    </script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8FXziUDgzJajbYyYAWXVRKqoKv3g6hFs&signed_in=true&callback=initMap">
    </script>
</asp:Content>

