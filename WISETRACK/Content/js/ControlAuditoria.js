$(document).ready(function () {
    $("#menu1").click(function () {
        $("#campo1").slideToggle("slow");
    });
});
$(document).ready(function () {
    $("#menu01").click(function () {
        $("#campo01").slideToggle("slow");
    });
});
$(document).ready(function () {
    $("#menu2").click(function () {
        $("#campo2").slideToggle("slow");
    });
});

///////////////////////////////////////AG-GRID/////////////////////////////////////////////////

var columnDefs = [
{ headerName: "Patente", field: "Patente", width: 100 },
{ headerName: "NroPlaca", field: "NroPlaca", width: 100 },
{ headerName: "FechaGPS", field: "FechaGPS", width: 150 },
{ headerName: "Temperatura", field: "Temperatura", width: 130 },
{ headerName: "EstadoPuerta", field: "EstadoPuerta", width: 130, hide: true },
{ headerName: "Velocidad", field: "Velocidad", width: 100 },
{ headerName: "EstadoMotor", field: "EstadoMotor", width: 130 },
{ headerName: "Kilometraje", field: "Kilometraje", width: 100 },
{ headerName: "VoltajeBateria", field: "VoltajeBateria", width: 130 },
{ headerName: "direcciones", field: "direcciones", width: 250 },
{ headerName: "ID", field: "ID", width: 50, hide: true },
{ headerName: "IMEI", field: "IMEI", width: 100, hide: true },
{ headerName: "EstadoGPS", field: "EstadoGPS", width: 100, hide: true },
{ headerName: "Asimut", field: "Asimut", width: 100, hide: true },
{ headerName: "Longitud", field: "Longitud", width: 100, hide: true },
{ headerName: "Latitud", field: "Latitud", width: 100, hide: true },
{ headerName: "Altitud", field: "Altitud", width: 100, hide: true },
{ headerName: "TipoMensaje", field: "TipoMensaje", width: 100, hide: true },
{ headerName: "TipoRespuesta", field: "TipoRespuesta", width: 100, hide: true },
{ headerName: "IDButton", field: "IDButton", width: 100, hide: true }
];

//var gridOptions = {
//    enableColResize: true,
//    virtualPaging: true, // this is important, if not set, normal paging will be done
//    debug: true,
//    rowSelection: 'multiple',
//    rowDeselection: true,
//    columnDefs: columnDefs,
//    rowModelType: 'virtual'
//};

var gridOptions = {
    columnDefs: columnDefs,
    enableFilter: true,
    enableSorting: true,
    rowSelection: 'multiple',
    onSelectionChanged: onSelectionChanged
};

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

    for (var i = 0; i < markersA.length; i++) {
        var mresult = markersA[i];
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
        fileName: 'ReporteAuditoria' + hoy + '.xls'
    };

    gridOptions.api.exportDataAsCsv(params);
}


///////////////////////////////////////MAPS/////////////////////////////////////////////////
//Variables inicialidas
var map;
var markers = [];
var markersA = [];
var markersString = [];
var puntosArray = [];
//var trama = new tramaSerial();
var linea;
var dir = '';
var icon;
var url;
var resultado;
var geocoder;
//funcion que inicia el mapa
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -17.783288, lng: -63.1817407 },
        zoom: 7,
        mapTypeId: google.maps.MapTypeId.NORMAL
    });

    geocoder = new google.maps.Geocoder();
    google.maps.event.addListener(map, 'click', function (event) {
        //addMarker(event.latLng, map);
    });
    infowindow = new google.maps.InfoWindow();
}

function tramaSerial() {
    this.ID;
    this.IMEI;
    this.EstadoGPS;
    this.Velocidad;
    this.Asimut;
    this.Longitud;
    this.Latitud;
    this.Altitud;
    this.TipoMensaje;
    this.TipoRespuesta;
    this.EstadoMotor;
    this.FechaGPS;
    this.NroPlaca;
    this.IDButton;
    this.Kilometraje;
    this.Temperatura;
    this.VoltajeBateria;
    this.EstadoPuerta;
    this.Patente;
    this.direcciones;
}
function AuditoriaOpmizado(fini, ffin, placa, count) {
    /// aqui temenos que haces la logica para eliminar el IDbutton
    BloquearPantalla();
    create();
    var values;

    var dataValue = "{ _fini: '" + fini + "', _ffin: '" + ffin + "', placa: '" + placa + "' }";

    $.ajax({
        url: "/FrmAuditoria.aspx/ObtenerAuditoria",
        data: dataValue,
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        values = JSON.parse(data.d);

        //DesbloquearPantalla();
    }).fail(function (jqXHR, textStatus) {
        //alert("Administrador Error 500 -> " + textStatus);
        //DesbloquearPantalla();
    }).always(function (jqXHR, textStatus) {
        if (textStatus != "success") {
            //alert("Administrador -> " + jqXHR.statusText);
        } else {
            gridOptions.api.setRowData(values);
            if (values != null) {
                if (values.length > 0) {

                    PintarAuditoria(values, count);
                    success("Operacion Corretamente..");
                } else {
                    error("No hay Datos para mostrar, no se puede realizar la auditoria de este Vehiculo.");
                }
            } else {
                if (value == null) {
                    error("No hay Datos para mostrar, no se puede realizar la auditoria de este Vehiculo.")
                }
            }

        }
        DesbloquearPantalla();
    });

}
function verAuditoria(fini, hini, ffin, hfin, placa, cod, velocidad, count) {
    BloquearPantalla();
    var data = "fini=" + fini + "&hini=" + hini + "&ffin=" + ffin + "&hfin=" + hfin + "&placa=" + placa;
    create();
    // do http request to get our sample data - not using any framework to keep the example self contained.
    // you will probably use a framework like JQuery, Angular or something else to do your HTTP calls.
    var httpRequest = new XMLHttpRequest();
    //httpRequest.open('POST', 'http://localhost:53953/Services/WebService1.asmx/mostrarBanco');
    httpRequest.open('POST', '/WebServices/WisetrackServices.asmx/obtenerAuditoriaOptimizado', true);
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    //"fini=" + fini + "&hini=" + hini + "&ffin=" + ffin + "&hfin=" + hfin + "&placa=" + placa
    httpRequest.send("fini=" + fini + "&hini=" + hini + "&ffin=" + ffin + "&hfin=" + hfin + "&placa=" + placa + "&cod=" + cod + "&velocidad=" + velocidad);
    httpRequest.onreadystatechange = function () {
        if (httpRequest.readyState == 4 && httpRequest.status == 200) {
            var resultado = httpRequest.responseText;
            resultado = resultado.substring(76);
            resultado = resultado.replace('</string>', '');
            var myobject = JSON.parse(resultado);
            gridOptions.api.setRowData(myobject);
            //setRowData(myobject);
            //setDataSource(myobject);
            PintarAuditoria(myobject, count);

            DesbloquearPantalla();
        }
    };
}


function setDataSource(allOfTheData, gridOption) {
            
    gridOption.api.setRowData(allOfTheData);

}

function create() {
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

function PintarAuditoria(myobject, count) {
    var stringestadogps = "";
    var stringestadomotor = "";

    var deserializa = { tramaSerial: myobject }
    //alert(deserializa);
    if (myobject.length > 0) {
        var size = deserializa.tramaSerial.length - 1;
        var iniciar = 0;
        for (var i = 0; i < deserializa.tramaSerial.length; i++) {
            dir = '';
            var pLatLng = new google.maps.LatLng(parseFloat(deserializa.tramaSerial[i].Latitud), parseFloat(deserializa.tramaSerial[i].Longitud));
            var slatlng = { lat: parseFloat(deserializa.tramaSerial[i].Latitud), lng: parseFloat(deserializa.tramaSerial[i].Longitud) };

            //obtener la cantidad de auditorias para los colores de las flechas

            if (deserializa.tramaSerial[i].Velocidad >= 1) {
                switch (count) {
                    case 2:
                        url = "/Content/img/flechas/verde/";
                        break;
                    case 3:
                        url = "/Content/img/flechas/morado/";
                        break;
                    case 4:
                        url = "/Content/img/flechas/naranja/";
                        break;
                    default:
                        url = "/Content/img/flechas/rojo/";
                        break;
                }
                orientacion(deserializa.tramaSerial[i].Asimut, i, url, count);
            } else {
                url = "/Content/img/detenciones/circulo.png";
                icon = url;
            }

            if (deserializa.tramaSerial[i].EstadoGPS == 1) {
                stringestadogps = "Encendido";
            } else {
                stringestadogps = "Apagado";
            }

            stringestadomotor = obtenerEstadoMotor(deserializa.tramaSerial[i].EstadoMotor);

            var contentString =
                '<div class="tab-content">' +
                //'<div class="tab-pane active" id="datos-vehiculo">' +
                //'<div id="infobox">' +
                    '<p><b>NroPlaca: </b>' + deserializa.tramaSerial[i].NroPlaca + '<br/>' +
                    '<b>Patente: </b>' + deserializa.tramaSerial[i].Patente + '<br/>' +
                //'<p><b>ID: </b>' + cadena.tramaTemp[i].ID + '</p>' +
                    '<b>EstadoGPS: </b>' + stringestadogps + '<br/>' +
                    '<b>EstadoMotor: </b>' + stringestadomotor + '<br/>' +
                    '<b>Velocidad: </b>' + deserializa.tramaSerial[i].Velocidad + '<br/>' +
                //'<p><b>IDButton: </b>' + cadena.tramaTemp[i].IDButton + '</p>' +
                    '<b>Fecha: </b>' + deserializa.tramaSerial[i].FechaGPS + '<br/>' +
                    '<b>Conductor: </b>' + 'No definido' + '<br/>' +
                //'<p><b>Kilometraje: </b>' + cadena.tramaTemp[i].Kilometraje + '</p>' +
                    '<b>VoltajeBateria: </b>' + deserializa.tramaSerial[i].VoltajeBateria + '<br/>' +
                //'<p><b>EstadoPuerta: </b>' + cadena.tramaTemp[i].tipov + '</p>' +
                //'</div>' +
                //'<div class="tab-pane active" id="sensores">' +
                    '<b>Temperatura: </b>' + deserializa.tramaSerial[i].Temperatura + '<br/>' +
                    '<b>EstadoPuerta: </b>' + deserializa.tramaSerial[i].EstadoPuerta + '<br/>' +
                    '<b>IDButton: </b>' + 0 + '<br/>' +
                    '<b>Gasolina: </b>' + 0 + '<br/>' +
                    '<b>Garmin: </b>' + 0 + '<br/>' +
                    '<b>Camara: </b>' + 0 + '<br/>' + '</p>' +
                    '<div>';

            //'<div id="content">' +
            //'<div id="bodyContent">' +
            //'<p><b>NroPlaca: </b>' + deserializa.tramaSerial[i].NroPlaca + '</p>' +
            ////'<p><b>ID: </b>' + deserializa.tramaSerial[i].IMEI + '</p>' +
            //'<p><b>EstadoGPS: </b>' + deserializa.tramaSerial[i].EstadoGPS + '</p>' +
            ////'<p><b>EstadoMotor: </b>' + deserializa.tramaSerial[i].EstadoMotor + '</p>' +
            //'<p><b>Velocidad: </b>' + deserializa.tramaSerial[i].Velocidad + '</p>' +
            //'<p><b>Temperatura: </b>' + deserializa.tramaSerial[i].Temperatura + ' °C </p>' +
            ////'<p><b>IDButton: </b>' + deserializa.tramaSerial[i].IDButton + '</p>' +
            //'<p><b>Fecha: </b>' + deserializa.tramaSerial[i].FechaGPS + '</p>' +

            ////'<p><b>Kilometraje: </b>' + deserializa.tramaSerial[i].Kilometraje + '</p>' +
            //'<p><b>VoltajeBateria: </b>' + deserializa.tramaSerial[i].VoltajeBateria + '</p>' +
            //'<p><b>EstadoPuerta: </b>' + deserializa.tramaSerial[i].EstadoPuerta + '</p>' +
            //'</div> </div>';

            switch (i) {
                case iniciar:
                    var marker = new google.maps.Marker({
                        position: slatlng,
                        title: '#' + i,
                        label: "A",
                        map: map
                    });
                    break;

                case size:
                    var marker = new google.maps.Marker({
                        position: slatlng,
                        title: '#' + i,
                        label: "B",
                        map: map
                    });
                    break;

                default:
                    var marker = new google.maps.Marker({
                        position: slatlng,
                        title: '#' + i,
                        icon: icon,
                        map: map
                    });
                    break;
            }
            markersA.push(marker);
            puntosArray.push(slatlng);
            markersString.push(contentString);

            google.maps.event.addListener(marker, 'click', function (event) {
                for (var i = 0; i < markersA.length; i++) {
                    var mresult = markersA[i];
                    if (mresult.position == event.latLng) {
                        var mcad = markersString[i];
                        buscarDireccion(event.latLng);
                        var mensajito = mcad + "<div id='bodyContent2'><p><b>Direccion: </b>" + dir + "</p></div>";
                        infowindow.setContent(mensajito);
                        infowindow.open(map, mresult);
                    }
                }
            });
        }
        map.setCenter(marker.getPosition());
    } else {
        alert("No Disponible");
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

function orientacion(asimut, i, url, count) {
    if (asimut >= 338) {
        resultado = "norte";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut <= 23) {
        resultado = "norte";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 23 && asimut < 68) {
        resultado = "noreste";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 68 && asimut < 113) {
        resultado = "este";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 113 && asimut < 158) {
        resultado = "sureste";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 158 && asimut < 203) {
        resultado = "sur";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 203 && asimut < 248) {
        resultado = "suroeste";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 248 && asimut < 293) {
        resultado = "oeste";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
    if (asimut >= 293 && asimut < 338) {
        resultado = "noroeste";
        if (count == 2 || count == 3) {
            icon = url + resultado + ".png";
        } else {
            icon = url + resultado + ".ico";
        }
    }
}

function LimpiarAuditoria() {
    var httpRequest = new XMLHttpRequest();
    //httpRequest.open('POST', '/WebServices/WisetrackServices.asmx/obtenerAuditoriaOptimizado', true);
    httpRequest.open('POST', '/FrmAuditoria.aspx/LimpiarTodo', true);
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send();
    httpRequest.onreadystatechange = function () {
        if (httpRequest.readyState == 4 && httpRequest.status == 200) {
            var resultado = httpRequest.responseText;
            deleteMarkersA();
        }
    };
}

function destroy() {
    gridOptions.api.destroy();
}

function setMapOnAll(map) {
    for (var i = 0; i < markersA.length; i++) {
        markersA[i].setMap(map);
    }
    markersA = [];
}

function deleteMarkersA() {
    setMapOnAll(null);
    markers = [];
    markersA = [];
    markersString = [];
    puntosArray = [];
}


function obtenerEstadoMotor(_motor) {
    var EstadoMotorValue = "";
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
            EstadoMotorValue = "MOVIL EN LINEA";
            break;
        case 22:
            EstadoMotorValue = "MOVIL EN LINEA";
            break;
        case 41:
            EstadoMotorValue = "APAGADO";
            break;
        case 42:
            EstadoMotorValue = "APAGADO CON MOVIMIENTO";
            break;
        case 1:
            EstadoMotorValue = "MOVIL EN LINEA";
            break;
        case 0:
            EstadoMotorValue = "APAGADO";
            break;
        default:
            EstadoMotorValue = "Revisar";
            break;
    }

    return EstadoMotorValue;
}
