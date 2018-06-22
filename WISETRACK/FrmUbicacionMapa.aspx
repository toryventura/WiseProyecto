<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmUbicacionMapa.aspx.cs" Inherits="WISETRACK.FrmUbicacionMapa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        #map {
            width: auto;
            height: 530px;
        }

        body {
            padding-top: 30px;
            padding-bottom: 30px;
        }

        @media (min-width: 1024px) and (max-width: 1366px) {
            .navbar-text.pull-right {
                float: none;
                padding-left: 5px;
                padding-right: 5px;
            }
        }
    </style>

    <div class="container-fluid" style="padding-top: 33px">
        <div class="row-fluid">
            <div class="span12">
                <div id="map">

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function initMap() {
            var latitud = parseFloat(<% =Request.QueryString["latitud"] %>);
            var longitud = parseFloat(<% =Request.QueryString["longitud"] %>);

            var latLng = { lat: latitud, lng: longitud };

            var map = new google.maps.Map(document.getElementById('map'), {
                center: latLng,
                zoom: 13,
                mapTypeId: google.maps.MapTypeId.NORMAL
            });

            var marker = new google.maps.Marker({
                position: latLng,
                map: map,
                title: '' + latLng
            });
        }
    </script>

    <script async defer 
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8FXziUDgzJajbYyYAWXVRKqoKv3g6hFs&signed_in=true&callback=initMap">
    </script>
</asp:Content>
