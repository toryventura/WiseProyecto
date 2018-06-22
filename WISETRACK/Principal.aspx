<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="WISETRACK.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Carousel
    ================================================== -->
    <div id="myCarousel" class="carousel slide">
        <div class="carousel-inner">
            <div class="item active">
                <img src="Imagenes/ima4.jpg" alt="" />
                <div class="container">
                    <%--<div class="carousel-caption">
                        <h1>Example headline.</h1>
                        <p class="lead">Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                        <a class="btn btn-large btn-primary" href="#">Sign up today</a>
                    </div>--%>
                </div>
            </div>
            <div class="item">
                <img src="Imagenes/ima2.jpg" alt="" />
                <div class="container">
                    <%--<div class="carousel-caption">
                        <h1>Another example headline.</h1>
                        <p class="lead">Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                        <a class="btn btn-large btn-primary" href="#">Learn more</a>
                    </div>--%>
                </div>
            </div>
            <div class="item">
                <img src="Imagenes/ima8.jpg" alt="" />
                <div class="container">
                    <%--<div class="carousel-caption">
                        <h1>One more for good measure.</h1>
                        <p class="lead">Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                        <a class="btn btn-large btn-primary" href="#">Browse gallery</a>
                    </div>--%>
                </div>
            </div>
        </div>
        <a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>
    </div>
    <!-- /.carousel -->

    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    <div class="container marketing">

        <!-- Three columns of text below the carousel -->
        <div class="row">
            <div class="span7">
                <img src="Imagenes/icon1.png" alt="" />
                <h2>Porque WISETRACK</h2>
                <p>Wisetrack® incorpora una inteligencia adicional a su negocio a través de soluciones robustas y flexibles que potencian las redes de entrega de valor.</p>
                <p><a class="btn" href="www.wisetrack.bo">Ver detalles &raquo;</a></p>
            </div>
            <!-- /.span4 -->
            <div class="span7">
                <img src="Imagenes/icon2.png">
                <h2>Como Funciona</h2>
                <p>El sistema Wisetrack® se basa en un dispositivo GPS instalado en cada uno de los móviles, el cual posee la capacidad de recibir la información de los satélites de posicionamiento global y establecer así su posición, velocidad, altura sobre el nivel del mar y dirección, información que es transmitida por un módulo de comunicaciones (que forma parte del equipo) hacia una base de datos ubicada en las oficinas, a través de la red de telefonía celular (GPRS).</p>
                <p><a class="btn" href="www.wisetrack.bo">Ver detalles &raquo;</a></p>
            </div>
            <!-- /.span4 -->
            <div class="span7">
                <img src="Imagenes/icon3.png">
                <h2>Wisetrack</h2>
                <p>Wisetrack Bolivia se dirige a un mercado corporativo en el cual tiene clientes en diferentes sectores como Retail, Minería, Cementeras y Hormigoneras, Operadores Logísticos, Transporte de Cargas Peligrosas, Seguridad Ciudadana, Transporte de Valores, Gas y Combustible, Transporte de Personal, Transporte de Carga en General, Consumo Masivo, entre otros. A todos ellos, les ofrecemos un servicio a la medida y necesidades de su negocio.</p>
                <p><a class="btn" href="www.wisetrack.bo">Ver detalles &raquo;</a></p>
            </div>
            <!-- /.span4 -->
        </div>
        <!-- /.row -->
    </div>


</asp:Content>
