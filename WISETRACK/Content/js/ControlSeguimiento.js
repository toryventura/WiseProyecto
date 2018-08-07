

var milisengundos = 60000;
$(document).ready(function () {
    main();
    
})
var interval;
function main() {
    interval = setInterval("runActualizar()", milisengundos);
}

var nroplaca = "";
function runActualizar() {

    ListarSeguimiento1(nroplaca);
}

function combo(e) {
    var valor = $("#cbotrespuesta option:selected").val();
    milisengundos = parseInt(valor);
    clearInterval(interval);
    main();
}
///////////////////////////MAPS//////////////////////////////////////////////////
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

    //infoBubble = new InfoBubble({
    //    maxWidth: 250
    //});

    //infoBubble.addTab('Datos-Vehiculo', 'Cargando...');
    //infoBubble.addTab('Sensores', 'Cargando...');

    infowindow = new google.maps.InfoWindow();
}

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
    this.IMEI;
    this.direcciones;
    this.Patente;
}

///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{
    headerName: "E", field: "E", width: 25,
    cellRenderer: function (params) {
        //var hoy = new Date();
        //var fecha = new Date(params.data.FechaGPS);
        //var r = ((hoy - fecha) / 1000);
        //r = r - 14440;
        var r = getMinutos(params.data.FechaGPS);
        var resultElement = document.createElement("span");
        var starImageElement = document.createElement("img");
        if (r < 30) { //x < 30 min
            switch (params.data.EstadoMotor) {
                case 11:
                    starImageElement.src = "/Content/img/tools/azul.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 12:
                    starImageElement.src = "/Content/img/tools/celeste.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 21:
                    starImageElement.src = "/Content/img/tools/verde.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 22:
                    starImageElement.src = "/Content/img/tools/verde.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 41:
                    starImageElement.src = "/Content/img/tools/azul.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 42:
                    starImageElement.src = "/Content/img/tools/celeste.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 1:
                    starImageElement.src = "/Content/img/tools/verde.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                case 0:
                    starImageElement.src = "/Content/img/tools/azul.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
                default:
                    starImageElement.src = "/Content/img/tools/coral.jpg";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                    break;
            }

        } else {
            if ((r >= 30) && (r <= 60)) { // x >= 30 min and x < 1hr
                starImageElement.src = "/Content/img/tools/amarillo.jpg";
                resultElement.appendChild(starImageElement);
                return resultElement;
            } else {
                if (r > 60) { // x > 1hr
                    starImageElement.src = "/Content/img/tools/rojo.png";
                    resultElement.appendChild(starImageElement);
                    return resultElement;
                }
            }
        }
    }
},
{ headerName: "ID", field: "ID", width: 100, hide: true },
{ headerName: "Patente", field: "Patente", width: 100 },
{ headerName: "NroPlaca", field: "NroPlaca", width: 100 },
{ headerName: "FechaGPS", field: "FechaGPS", width: 150, enableValue: true },
{ headerName: "Temperatura", field: "Temperatura", width: 120 },
{ headerName: "Velocidad", field: "Velocidad", width: 100 },
{ headerName: "Kilometraje", field: "Kilometraje", width: 100, hide: true },
{ headerName: "VoltajeBateria", field: "VoltajeBateria", width: 130 },
{ headerName: "EstadoPuerta", field: "EstadoPuerta", width: 130 },
{
    headerName: "EstadoMotor", field: "EstadoMotor", width: 100, hide: true,

    cellStyle: function (params) {
        //var hoy = new Date();
        //var fecha = new Date(params.data.FechaGPS);
        //var r = ((hoy - fecha) / 1000);
        //r = r - 14440;

        //var timeStart = new Date().getTime();
        //var timeEnd = new Date(_fecha).getTime();
        //var hourDiff = timeStart - timeEnd; //in ms
        //var secDiff = hourDiff / 1000; //in s
        //var minDiff = hourDiff / 60 / 1000; //in minutes
        var r = getMinutos(params.data.FechaGPS);

        if (r < 30) { //x < 30 min
            switch (params.value) {
                case 11:
                    return { color: 'white', backgroundColor: 'blue' };
                    break;
                case 12:
                    return { color: 'white', backgroundColor: 'lightblue' };
                    break;
                case 16:
                    return { color: 'white', backgroundColor: 'lightblue' };
                    break;
                case 21:
                    return { color: 'white', backgroundColor: 'green' };
                    break;
                case 22:
                    return { color: 'white', backgroundColor: 'green' };
                    break;
                case 41:
                    return { color: 'white', backgroundColor: 'blue' };
                    break;
                case 42:
                    return { color: 'white', backgroundColor: 'lightblue' };
                    break;
                case 1:
                    return { color: 'white', backgroundColor: 'green' };
                    break;
                case 0:
                    return { color: 'white', backgroundColor: 'blue' };
                    break;
                default:
                    return { color: 'white', backgroundColor: 'orange' };
                    break;
            }

        } else {
            if ((r >= 30) && (r <= 60)) { // x >= 30 min and x < 1hr
                return { color: 'white', backgroundColor: 'yellow' };
            } else {
                if (r > 60) { // x > 1hr
                    return { color: 'white', backgroundColor: 'red' };
                }
            }
        }
    }
},
{ headerName: "tipov", field: "tipov", width: 100, hide: true },
{ headerName: "EstadoGPS", field: "EstadoGPS", width: 120 },
{ headerName: "Latitud", field: "Latitud", width: 130, hide: true },
{ headerName: "Longitud", field: "Longitud", width: 100, hide: true },
{ headerName: "Nombre", field: "Nombre", width: 130, hide: true },
{ headerName: "Asimut", field: "Asimut", width: 200, hide: true },
{ headerName: "NIT", field: "NIT", width: 50, hide: true, hide: true },
{ headerName: "IDButton", field: "IDButton", width: 100, hide: true },
{ headerName: "IMEI", field: "IMEI", width: 100, hide: true },
{ headerName: "Direccion", field: "direcciones", width: 200 }
];

var gridOptions = {
    columnDefs: columnDefs,
    //enableFilter: true,
    //enableSorting: true,
    rowSelection: 'multiple',
    enableFilter: true,
    enableSorting: true,
    onSelectionChanged: onSelectionChanged
};

function getMinutos(_fecha) {
    var timeStart = new Date().getTime();
    var timeEnd = new Date(_fecha).getTime();
    var hourDiff = timeStart - timeEnd; //in ms
    var secDiff = hourDiff / 1000; //in s
    var minDiff = hourDiff / 60 / 1000; //in minutes
    return minDiff;
}

function onSelectionChanged() {
    var selectedRows = gridOptions.api.getSelectedRows();
    var selectedRowsStringLa = '';
    var selectedRowsStringLo = '';
    var selectedRowsStringNroPlaca = '';
    var selectedRowsStringDireccion = '';
    selectedRows.forEach(function (selectedRow, index) {
        if (index != 0) {
            selectedRowsStringLa += ', ';
            selectedRowsStringLo += ', ';
            selectedRowsStringNroPlaca += ', ';
            selectedRowsStringDireccion += ', ';
        }
        selectedRowsStringLa += selectedRow.Latitud;
        selectedRowsStringLo += selectedRow.Longitud;
        selectedRowsStringNroPlaca += selectedRow.NroPlaca;
        selectedRowsStringDireccion += selectedRow.direcciones;
    });
    map.setCenter(new google.maps.LatLng(selectedRowsStringLa, selectedRowsStringLo));
    map.setZoom(20);
    var os = new google.maps.LatLng(selectedRowsStringLa, selectedRowsStringLo);

    for (var i = 0; i < markersU.length; i++) {
        var mresult = markersU[i];
        if (mresult.position.toString() == os.toString()) {
            var mcad = markersString[i];
            var mensajito = mcad + "<div id='infobox'><b>Direccion: </b>" + selectedRowsStringDireccion + "</div>";
            infowindow.setContent(mensajito);
            infowindow.open(map, mresult);
        }
    }
}

function onBtExport() {
    var hoy = new Date();
    var params = {
        skipHeader: false,
        skipFooters: true,
        skipGroups: true,
        fileName: 'ReporteSeguimiento' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}

//idLoad = setInterval(document.addEventListener('DOMContentLoaded', function () {
//function Init() {
//    ListarSeguimientoInit1();
//    idLoad = setInterval(function () {
//        destroy();
//        ListarSeguimientoInit1();
//    }, 60000);
//}

//function ListarSeguimientoInit() {
//    create();
//    // do http request to get our sample data - not using any framework to keep the example self contained.
//    // you will probably use a framework like JQuery, Angular or something else to do your HTTP calls.
//    var httpRequest = new XMLHttpRequest();
//    httpRequest.open('POST', '/WebServices/WisetrackServices.asmx/ListarSeguimiento', true);
//    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    httpRequest.send("placa=todas");
//    httpRequest.onreadystatechange = function () {
//        if (httpRequest.readyState == 4 && httpRequest.status == 200) {
//            var resultado = httpRequest.responseText;
//            resultado = resultado.substring(76);
//            resultado = resultado.replace('</string>', '');
//            var myobject = JSON.parse(resultado);
//            gridOptions.api.setRowData(myobject);
//            //setRowData(myobject);
//            PintarMapa(myobject);
//        }
//    };
//}

function ListarSeguimientoInit1() {
    BloquearPantalla();

    var action = "{'placa': 'todas'}";
    $.ajax({
        url: "/FrmSeguimiento.aspx/ListarSeguimiento",
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

        } else {
            if (myData.length > 0) {

                create();

                setDataSource(myData,gridOptions);
                PintarMapa(myData);
            }

        }
        DesbloquearPantalla();
    });
}
function setDataSource(allOfTheData, gridOption) {
            
    gridOption.api.setRowData(allOfTheData);

}
//function AfterLoad(placa, _time) {

//        destroy();
//        ListarSeguimiento1(placa);

//}
function ObtenerPlacafinal(nro) {
    nroplaca = nro
    ListarSeguimiento1(nro);
}


function ListarSeguimiento1(nro) {
    BloquearPantalla();
    var action = "{'placa':'" + nro + "' }";
    $.ajax({
        url: "/FrmSeguimiento.aspx/ListarSeguimiento",
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
            //  alert("Administrador -> " + jqXHR.statusText);
        } else {
            //gridOptions.api.setRowData(myData);
            if (myData.length > 0) {

                destroy();
                create();
                setDataSource(myData,gridOptions);
                PintarMapa(myData);
            }
        }
        DesbloquearPantalla();
    });
}

function create() {
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);

}

function destroy() {
    gridOptions.api.destroy();
}

function PintarMapa(lista) {
    setMapOnAllU(null);
    //infoBubble.close();
    if (lista.length > 0) {
        var cadena = { tramaTemp: lista };
        if (lista.length > 1) {
            for (var i = 0; i < cadena.tramaTemp.length; i++) {
                var ultubi = { lat: parseFloat(cadena.tramaTemp[i].Latitud), lng: parseFloat(cadena.tramaTemp[i].Longitud) };
                var plac = cadena.tramaTemp[i].NroPlaca;
                //var EstadoGpsValue = " ";
                if (cadena.tramaTemp[i].EstadoGPS == 'Apagado') {
                    //EstadoGpsValue = "Apagado";
                    obtenerFecha1(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                }

                if (cadena.tramaTemp[i].EstadoGPS == 'Encendido') {
                    //EstadoGpsValue = "Encendido";
                    obtenerFecha1(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                }

                var temperaturaValue = " ";
                if (cadena.tramaTemp[i].Temperatura == -999) {
                    temperaturaValue = "No Disponible";
                }
                else {
                    temperaturaValue = cadena.tramaTemp[i].Temperatura + " ºC";
                }

                var voltajeValue = " ";
                if (cadena.tramaTemp[i].VoltajeBateria == -999) {
                    voltajeValue = "No Disponible";
                }
                else {
                    voltajeValue = cadena.tramaTemp[i].VoltajeBateria + ' V';
                }

                var conductor = " ";

                if (cadena.tramaTemp[i].Nombre == null) {
                    conductor = "No Asignado";
                } else {
                    conductor = cadena.tramaTemp[i].Nombre + ' ';
                }

                var contentString =
                    //'<li class="active"><a href="#datos-vehiculo" data-toggle="tab">Datos-Vehiculo</a></li>' +
                    //'<li class="active"><a href="#sensores" data-toggle="tab">Sensores</a></li></ul>' +
                    '<div class="tab-content">' +
                    //'<div class="tab-pane active" id="datos-vehiculo">' +
                    //'<div id="infobox">' +
                    '<p><b>NroPlaca: </b>' + cadena.tramaTemp[i].NroPlaca + '<br/>' +
                    '<b>Patente: </b>' + cadena.tramaTemp[i].Patente + '<br/>' +
                    //'<p><b>ID: </b>' + cadena.tramaTemp[i].ID + '</p>' +
                    '<b>EstadoGPS: </b>' + getMensajegps(cadena.tramaTemp[i].FechaGPS) + '<br/>' +
                    '<b>EstadoMotor: </b>' + EstadoMotorValue + '<br/>' +
                    '<b>Velocidad: </b>' + cadena.tramaTemp[i].Velocidad + '<br/>' +
                    //'<p><b>IDButton: </b>' + cadena.tramaTemp[i].IDButton + '</p>' +
                    '<b>Fecha: </b>' + cadena.tramaTemp[i].FechaGPS + '<br/>' +
                    '<b>Conductor: </b>' + conductor + '<br/>' +
                    //'<p><b>Kilometraje: </b>' + cadena.tramaTemp[i].Kilometraje + '</p>' +
                    '<b>VoltajeBateria: </b>' + voltajeValue + '<br/>' +
                    //'<p><b>EstadoPuerta: </b>' + cadena.tramaTemp[i].tipov + '</p>' +
                    //'</div>' +
                    //'<div class="tab-pane active" id="sensores">' +
                    '<b>Temperatura: </b>' + temperaturaValue + '<br/>' +
                    '<b>EstadoPuerta: </b>' + cadena.tramaTemp[i].EstadoPuerta + '<br/>' +
                    '<b>IDButton: </b>' + 0 + '<br/>' +
                    '<b>Gasolina: </b>' + 0 + '<br/>' +
                    '<b>Garmin: </b>' + 0 + '<br/>' +
                    '<b>Camara: </b>' + 0 + '<br/>' + '</p>' +
                    '<div>';

                //var contentStringSensores = '<div id="content">' +
                //    '<p><b>Temperatura: </b>' + temperaturaValue + '</p>' +
                //    '<p><b>EstadoPuerta: </b>' + cadena.tramaTemp[i].EstadoPuerta + '</p>' +
                //    '<p><b>IDButton: </b>' + 0 + '</p>' +
                //    '<p><b>Gasolina: </b>' + 0 + '</p>' +
                //    '<p><b>Garmin: </b>' + 0 + '</p>' +
                //    '<p><b>Camara: </b>' + 0 + '</p>' +
                //    '</div>';

                orientacion(cadena.tramaTemp[i].Asimut, url);
                console.log(icon + '=> ' + cadena.tramaTemp[i].NroPlaca);


                var marker = new google.maps.Marker({
                    position: ultubi,
                    title: '#' + plac,
                    icon: icon,
                    map: map
                });

                markersU.push(marker);
                markersString.push(contentString);
                //markersStringSensores.push(contentStringSensores);

                google.maps.event.addListener(marker, 'click', function (event) {
                    for (var i = 0; i < markersU.length; i++) {
                        var mresult = markersU[i];
                        if (mresult.position == event.latLng) {
                            var mcad = markersString[i];
                            var mcadsensores = markersStringSensores[i];
                            buscarDireccion(event.latLng);
                            var mensajito = mcad + "<div id='infobox'><b>Direccion: </b>" + dir + "</div>";
                            infowindow.setContent(mensajito);
                            infowindow.open(map, mresult);
                            //infoBubble.updateTab(0, 'Datos-Vehiculo', mcad + mensajito);
                            //infoBubble.updateTab(1, 'Sensores', mcadsensores);
                            //infoBubble.open(map, mresult);
                        }
                    }
                });
            }
        } else {
            for (var i = 0; i < cadena.tramaTemp.length; i++) {
                var ultubi = { lat: parseFloat(cadena.tramaTemp[i].Latitud), lng: parseFloat(cadena.tramaTemp[i].Longitud) };
                var plac = cadena.tramaTemp[i].NroPlaca;
                //var EstadoGpsValue;
                if (cadena.tramaTemp[i].EstadoGPS == 'Apagado') {
                    //EstadoGpsValue = "Apagado";
                    obtenerFecha1(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                }

                if (cadena.tramaTemp[i].EstadoGPS == 'Encendido') {
                    //EstadoGpsValue = "Encendido";
                    obtenerFecha1(cadena.tramaTemp[i].FechaGPS, cadena.tramaTemp[i].EstadoMotor, cadena.tramaTemp[i].tipov);
                }

                var temperaturaValue = '';
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

                var prueba = '<ul class="nav nav-tabs" id="myTab">' +
                    '<li class="active"><a href="#map" data-toggle="tab">Datos-Vehiculo</a></li>' +
                    '<li class="active"><a href="#map" data-toggle="tab">Sensores</a></li></ul>';


                var contentString =
                    //'<li class="active"><a href="#datos-vehiculo" data-toggle="tab">Datos-Vehiculo</a></li>' +
                    //'<li class="active"><a href="#sensores" data-toggle="tab">Sensores</a></li></ul>' +
                    '<div class="tab-content">' +
                    //'<div class="tab-pane active" id="datos-vehiculo">' +
                    //'<div id="infobox">' +
                    '<p><b>NroPlaca: </b>' + cadena.tramaTemp[i].NroPlaca + '<br/>' +
                    '<b>Patente: </b>' + cadena.tramaTemp[i].Patente + '<br/>' +
                    //'<p><b>ID: </b>' + cadena.tramaTemp[i].ID + '</p>' +
                    '<b>EstadoGPS: </b>' + getMensajegps(cadena.tramaTemp[i].FechaGPS) + '<br/>' +
                    '<b>EstadoMotor: </b>' + EstadoMotorValue + '<br/>' +
                    '<b>Velocidad: </b>' + cadena.tramaTemp[i].Velocidad + '<br/>' +
                    //'<p><b>IDButton: </b>' + cadena.tramaTemp[i].IDButton + '</p>' +
                    '<b>Fecha: </b>' + cadena.tramaTemp[i].FechaGPS + '<br/>' +
                    '<b>Conductor: </b>' + conductor + '<br/>' +
                    //'<p><b>Kilometraje: </b>' + cadena.tramaTemp[i].Kilometraje + '</p>' +
                    '<b>VoltajeBateria: </b>' + voltajeValue + '<br/>' +
                    //'<p><b>EstadoPuerta: </b>' + cadena.tramaTemp[i].tipov + '</p>' +
                    //'</div>' +
                    //'<div class="tab-pane active" id="sensores">' +
                    '<b>Temperatura: </b>' + temperaturaValue + '<br/>' +
                    '<b>EstadoPuerta: </b>' + cadena.tramaTemp[i].EstadoPuerta + '<br/>' +
                    '<b>IDButton: </b>' + 0 + '<br/>' +
                    '<b>Gasolina: </b>' + 0 + '<br/>' +
                    '<b>Garmin: </b>' + 0 + '<br/>' +
                    '<b>Camara: </b>' + 0 + '<br/>' + '</p>' +
                    '<div>';


                //var contentStringSensores = '<div id="content">' +
                //    '</div>';

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
                //markersStringSensores.push(contentStringSensores);

                google.maps.event.addListener(marker, 'click', function (event) {
                    for (var i = 0; i < markersU.length; i++) {
                        var mresult = markersU[i];
                        if (mresult.position == event.latLng) {
                            var mcad = markersString[i];
                            var mcadsensores = markersStringSensores[i];
                            buscarDireccion(event.latLng);
                            var mensajito = mcad + "<div id='datos-vehiculo'><b>Direccion: </b>" + dir + "</div>";
                            infowindow.setContent(mensajito);
                            infowindow.open(map, mresult);
                        }
                    }
                });
            }
            map.setCenter(marker.getPosition());
        }
    } else {
        alert("No hay datos");
    }
}
function getMensajegps(_fecha) {
    var mindiff = getMinutos(_fecha);
    if (mindiff<30) {
        return "EN LINEA";
    } else {
        if (mindiff>=30 && mindiff<=60) {
            return "FUERA DE LINEA > 30 MIN."
        } else {
            return "FUERA DE LINEA > 1 HRS."
        }
    }
}
function obtenerFecha1(_fecha, _emotor, _tipov) {
    //var timeStart = new Date().getTime();
    //var timeEnd = new Date(_fecha).getTime();
    //var hourDiff = timeStart - timeEnd; //in ms
    //var secDiff = hourDiff / 1000; //in s
    //var minDiff = hourDiff / 60 / 1000; //in minutes
    var minDiff = getMinutos(_fecha);
    if (minDiff < 30) { //x < 30 min
        tipoVehiculo(_tipov);
        obtenerEstadoMotor(_emotor); 
    } else {
        if ((minDiff >= 30) && (minDiff <= 60)) { // x >= 30 min and x < 1hr
            obtenerEstadoMotorByGPS(_emotor);
            tipoVehiculo(_tipov);
            url = url + "/amarillo/";
        } else {
            if (minDiff > 60) { // x > 1hr
                obtenerEstadoMotorByGPS(_emotor);
                tipoVehiculo(_tipov);
                url = url + "/rojo/";
            }
        }
    }

}
//function obtenerFecha(_fecha, _emotor, _tipov) {
//    var factual = new Date();
//    var f2 = new Date(_fecha);
//    var r = Math.abs(((factual - f2) / 1000));
//    r = r - 14440;
//    if (r < 1800) { //x < 30 min
//        tipoVehiculo(_tipov);
//        obtenerEstadoMotor(_emotor);
//    } else {
//        if ((r >= 1800) && (r <= 3600)) { // x >= 30 min and x < 1hr
//            obtenerEstadoMotorByGPS(_emotor);
//            tipoVehiculo(_tipov);
//            url = url + "/amarillo/";
//        } else {
//            if (r > 3600) { // x > 1hr
//                obtenerEstadoMotorByGPS(_emotor);
//                tipoVehiculo(_tipov);
//                url = url + "/rojo/";
//            }
//        }
//    }
//}

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
            nombretipov = "excavadora";
            url = "Content/img/movil/" + nombretipov;
            break;
        default:
            nombretipov = "auto";
            url = "Content/img/movil/" + nombretipov;
            break
            //case 11:
            //    nombretipov = "tractor";
            //    url = "Content/img/movil/" + nombretipov;
            //    break;
            //case 12:
            //    nombretipov = "grua";
            //    url = "Content/img/movil/" + nombretipov;
            //    break;
            //case 13:
            //    nombretipov = "excavadora";
            //    url = "Content/img/movil/" + nombretipov;
            //    break;
    }
}

function obtenerEstadoMotor(_motor) {
    switch (_motor) {
        case 11:
            EstadoMotorValue = "APAGADO";
            url = url + "/azul/";
            break;
        case 12:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            url = url + "/celeste/";
            break;
        case 16:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            url = url + "/celeste/";
            break;
        case 21:
            EstadoMotorValue = "ENCENDIDO";//MOVIL EN LINEA
            url = url + "/verde/";
            break;
        case 22:
            EstadoMotorValue = "ENCENDIDO";//MOVIL EN LINEA
            url = url + "/verde/";
            break;
        case 41:
            EstadoMotorValue = "APAGADO";
            url = url + "/azul/";
            break;
        case 42:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            url = url + "/celeste/";
            break;
        case 1:
            EstadoMotorValue = "ENCENDIDO";//MOVIL EN LINEA
            url = url + "/verde/";
            break;
        case 0:
            EstadoMotorValue = "APAGADO";
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
            EstadoMotorValue = "APAGADO";
            break;
        case 12:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            break;
        case 16:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            break;
        case 21:
            EstadoMotorValue = "ENCENDIDO";//MOVIL EN LINEA
            break;
        case 22:
            EstadoMotorValue = "ENCENDIDO";//MOVIL EN LINEA
            break;
        case 41:
            EstadoMotorValue = "APAGADO";
            break;
        case 42:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            break;
        case 1:
            EstadoMotorValue = "ENCENDIDO";//MOVIL EN LINEA
            break;
        case 0:
            EstadoMotorValue = "APAGADO";
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

function setMapOnAllU(map) {
    for (var i = 0; i < markersU.length; i++) {
        markersU[i].setMap(map);
    }
    markersU = [];
    markersString = [];
}

window.onload = ListarSeguimientoInit1();


