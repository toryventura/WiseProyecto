//var map;
//function initMap() {
//    map = new google.maps.Map(document.getElementById('map'), {
//        center: { lat: -17.783288, lng: -63.1817407 },
//        zoom: 6,
//        mapTypeId: google.maps.MapTypeId.NORMAL
//    });
//}

//$('#myModal').on('shown.bs.modal', function () {
//    initMap();
//});


var map1;
var infoWindow;
var lista_codgeo = [];
var lista_markers = [];
//var poligono_aux1;

function initMap1() {
    map1 = new google.maps.Map(document.getElementById('map1'), {
        center: { lat: -17.783288, lng: -63.1817407 },
        zoom: 9,
        mapTypeId: google.maps.MapTypeId.NORMAL
    });

    ListarGeocercaInit(map1);
}

$('#myModal').on('shown.bs.modal', function () {
    initMap1();
    if (lista_codgeo.length > 0) {
        lista_codgeo = [];
    }
});

function GeocercaTemp() {
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

function ListarGeocercaInit(map1) {
    // do http request to get our sample data - not using any framework to keep the example self contained.
    // you will probably use a framework like JQuery, Angular or something else to do your HTTP calls.
    var httpRequest = new XMLHttpRequest();
    httpRequest.open('POST', '/WebServices/WisetrackServices.asmx/VisualizarTodasGeocercas', true);
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send();
    httpRequest.onreadystatechange = function () {
        if (httpRequest.readyState == 4 && httpRequest.status == 200) {
            var resultado = httpRequest.responseText;
            resultado = resultado.substring(76);
            resultado = resultado.replace('</string>', '');
            var myobject = JSON.parse(resultado);
            PintarMapaGeocerca(myobject, map1);
        }
    };
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
        var lat = degr2rad(latLngInDegr[i].lat);
        var lng = degr2rad(latLngInDegr[i].lng);
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

    return ([rad2degr(lat), rad2degr(lng)]);
}

function PintarMapaGeocerca(myobject, map1) {
    var cad = '';
    //setMapOnAllU(null);   
    if (myobject.length > 0) {
        var cadena = { GeocercaTemp: myobject };
        for (var i = 0; i < cadena.GeocercaTemp.length; i++) {
            var puntero_ini = cadena.GeocercaTemp[i].CodigoGEO;
            var NombreGeo;
            if (cad == '') {
                cad = puntero_ini;
                NombreGeo = cadena.GeocercaTemp[i].Descripcion;
            }
            if (cad == puntero_ini) {
                var puntoslatlng = { lat: parseFloat(cadena.GeocercaTemp[i].Latitud), lng: parseFloat(cadena.GeocercaTemp[i].Longitud) };
                //var puntoslatlng = { lat: cadena.GeocercaTemp[i].Latitud, lng: cadena.GeocercaTemp[i].Longitud };
                lista_codgeo.push(puntoslatlng);
                var color_relleno = cadena.GeocercaTemp[i].ColorRelleno;
                var color_limite = cadena.GeocercaTemp[i].ColorLimite;
            } else {
                var poligono_aux1 = new google.maps.Polygon({
                    paths: lista_codgeo,
                    strokeColor: color_limite,
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: color_relleno,
                    fillOpacity: 0.35
                });
                

                poligono_aux1.setMap(map1);
                var centers = getLatLngCenter(lista_codgeo);
                showArrays(centers,NombreGeo);
                infoWindow = new google.maps.InfoWindow;
                cad = '';
                for (var j = 0; j < lista_codgeo.length; j++) {
                    var r = lista_codgeo.pop();
                }

                var puntoslatlng1 = { lat: cadena.GeocercaTemp[i].Latitud, lng: cadena.GeocercaTemp[i].Longitud };

                lista_codgeo = [];
                lista_codgeo.push(puntoslatlng1);
            }
        }
        var vertices = poligono_aux1.getPath();
        var erlatlng = { lat: vertices.getAt(1).lat(), lng: vertices.getAt(1).lng() };
        map1.setCenter(erlatlng);

    }
    function showArrays(centers, NombreGeo) {
        // Since this polygon has only one path, we can call getPath() to return the
        // MVCArray of LatLngs.
        //var vertices = this.getPath();

        var contentString = '<b>Nombre Geocerca:</b><br>'+NombreGeo+'<br>'+
                       '<br>';

        // Iterate over the vertices.
        //for (var i = 0; i < vertices.getLength() ; i++) {
        //    var xy = vertices.getAt(i);
        //    contentString += '<br>' + 'Coordinate ' + i + ':<br>' + xy.lat() + ',' +
        //        xy.lng();
        //}
        var myLatLng = { lat:centers[0], lng:centers[1] };

        // Replace the info window's content and position.
        infoWindow.setContent(contentString);
        infoWindow.setPosition(myLatLng);

        infoWindow.open(map1);
    }

}




