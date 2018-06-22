<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WISETRACK.Vistas.Vehiculos.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="span8">
                <div>
                    <div class="col-sm-9 col-xs-9 col-md-9 col-lg-5">
                        <div class="form-group">
                            <h2><b>Detalle Vehiculo</b></h2>
                        </div>
                        <div class="table">
                            <dl class="dl-horizontal">
                                <dt>Placa</dt>
                                <dd>
                                    <asp:Label ID="lblplaca" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Nro Chasis</dt>
                                <dd>
                                    <asp:Label ID="lblchasis" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Patente</dt>
                                <dd>
                                    <asp:Label ID="lblpatente" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Nro Motor</dt>
                                <dd>
                                    <asp:Label ID="lblmotor" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Modelo</dt>
                                <dd>
                                    <asp:Label ID="lblmodelo" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Año</dt>
                                <dd>
                                    <asp:Label ID="lblanio" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Usuario Registro</dt>
                                <dd>
                                    <asp:Label ID="lblusuarioreg" runat="server" Text=""></asp:Label>
                                </dd>
                                <dt>Fecha Registro</dt>
                                <dd>
                                    <asp:Label ID="lblfechareg" runat="server" Text=""></asp:Label>
                                </dd>
                            </dl>
                        </div>
                    </div>
                    <div  class="col-sm-9 col-xs-9 col-md-9 col-lg-6">
                        <img src="" class="img-responsive" alt="No Datos" id="imageve" onclick="verfoto()" />
                    </div>
                </div>
            </div>
            <div class="span9">
                <div class="table table-condensed table-responsive table-hover">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>NIT</th>
                                <th>Razon Social</th>
                                <th>Email Empresa</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblnite" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrazonse" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblemaile" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>

                    <div class="form-actions no-color">
                        <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Editar" OnClick="btnEditar_Click" />
                        | 
                    <a href="/Vistas/Vehiculos/Index">Volver átras</a>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script>

        function vehiculo() {
            this.NroPlaca;
            this.NroChasis;
            this.NroMotor;
            this.Modelo;
            this.CodTipoV;
            this.CodMarca;
            this.UsuaReg;
            this.FechaReg;
            this.UsuaModif;
            this.FechaModif;
            this.Año;
            this.idempresa;
            this.Foto;
        }

        function verfoto() {
            var plac = $('#<%=lblplaca.ClientID%>').text();
            WISETRACK.WebServices.WisetrackServices.verFoto(plac, onSuccess, onFailed);
            
        }

        function onSuccess(response) {
            var myobject = JSON.parse(response);
            var cadena = { vehiculo: myobject };
            if (response.length > 0) {
                var veh = cadena.vehiculo[0].Foto;
                document.getElementById("imageve").src = "data:image/*;base64," + veh;
            }
        }

        function onFailed(response) {
            alert("Error al conectar el servidor");
        }

        window.onload = verfoto;

    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
    <script src="../../Scripts/jquery-2.1.4.js"></script>
</asp:Content>
