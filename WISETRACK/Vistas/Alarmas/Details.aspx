<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WISETRACK.Vistas.Alarmas.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="form-group">
                <h2><b>Detalle Alarma</b></h2>
            </div>
            <div class="table-responsive table-condensed table-hover table">
                <dl class="dl-horizontal">
                    <dt>Codigo</dt>
                    <dd>
                        <asp:Label ID="lblCodigo" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Nombre</dt>
                    <dd>
                        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Tipo</dt>
                    <dd>
                        <asp:Label ID="lblTipoAlarma" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Categoria</dt>
                    <dd>
                        <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtVelocidad" runat="server">Velocidad Máxima (Km/h)</dt>
                    <dd>
                        <asp:Label ID="lblVelocidad" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtTiempo" runat="server">Tiempo Máximo (Min)</dt>
                    <dd>
                        <asp:Label ID="lblTiempo" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtDistancia" runat="server">Distancia Máxima (Km)</dt>
                    <dd>
                        <asp:Label ID="lblDistancia" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtTemperatura" runat="server">Temperatura Máxima (ºC)</dt>
                    <dd>
                        <asp:Label ID="lblTemperatura" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtVelocidad2" runat="server">Velocidad Mínima (Km/h)</dt>
                    <dd>
                        <asp:Label ID="lblVelocidad2" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtTiempo2" runat="server">Tiempo Mínimo (Min)</dt>
                    <dd>
                        <asp:Label ID="lblTiempo2" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtDistancia2" runat="server">Distancia Mínima (Km)</dt>
                    <dd>
                        <asp:Label ID="lblDistancia2" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtTemperatura2" runat="server">Temperatura Mínima (ºC)</dt>
                    <dd>
                        <asp:Label ID="lblTemperatura2" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtFechaHora" runat="server">Hora Inicio</dt>
                    <dd>
                        <asp:Label ID="lblFechaHora" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtFechaHora2" runat="server">Hora Fin</dt>
                    <dd>
                        <asp:Label ID="lblFechaHora2" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Estado</dt>
                    <dd>
                        <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt id="dtTiempoEnvio" runat="server">Tiempo de Envio (Alerta continua)</dt>
                    <dd>
                        <asp:Label ID="lblTiempoEnvio" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Intervalo de Envio por Email</dt>
                    <dd>
                        <asp:Label ID="lblIntervaloEnvio" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Cantidad de Emails para Envio</dt>
                    <dd>
                        <asp:Label ID="lblCantidadEnvio" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Registrado por</dt>
                    <dd>
                        <asp:Label ID="lblUsuarioReg" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Fecha de Registro</dt>
                    <dd>
                        <asp:Label ID="lblFechaReg" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt><br /><br /></dt>
                    <dt id="dtGeocerca" runat="server" >Geocercas</dt>
                    <dd>
                        <br />
                        <asp:Repeater ID="rptGeocercas" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Codigo</th>
                                            <th>Descripción</th>
                                            <th>Tipo</th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCodGeo" runat="server" Text='<%#Eval("CodigoGeo") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescGeo" runat="server" Text='<%#Eval("Descripcion") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoGeo" runat="server" Text='<%#Eval("Tipo") %>' />
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </dd>
                    <dt><br /><br /></dt>
                    <dt>Vehículos</dt>
                    <dd>
                        <br />
                        <asp:Repeater ID="rptVehiculos" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Placa</th>
                                            <th>Tipo</th>
                                            <th>Marca</th>
                                            <th>Conductor</th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tboby>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNroPlaca" runat="server" Text='<%#Eval("NroPlaca") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipo" runat="server" Text='<%#Eval("Tipo") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarca" runat="server" Text='<%#Eval("Marca") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblConductor" runat="server" Text='<%#Eval("Conductor") %>' />
                                        </td>
                                    </tr>
                                </tboby>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </dd>
                    <dt><br /><br /></dt>
                    <dt>Destinatarios</dt>
                    <dd>
                        <br />
                        <asp:Repeater ID="rptDestinatarios" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>CI</th>
                                            <th>Nombre y Apellido(s)</th>
                                            <th>Email</th>
                                            <th>Telefono</th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tboby>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCI" runat="server" Text='<%#Eval("CI") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombreCompleto" runat="server" Text='<%#Eval("NombreCompleto") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTelefono" runat="server" Text='<%#Eval("Telefono") %>' />
                                        </td>
                                    </tr>
                                </tboby>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </dd>
                </dl>
                <div class="form-actions no-color">
                    <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Editar" OnClick="btnEditar_Click"  />
                    |
                     <input type="button" class="btn btn-default" value="Imprimir" onclick="javascript: window.print()" /> 
                    | 
                    <a href="/Vistas/Alarmas/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
