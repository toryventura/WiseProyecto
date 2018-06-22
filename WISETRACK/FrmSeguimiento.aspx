<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmSeguimiento.aspx.cs" Inherits="WISETRACK.FrmSeguimiento2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/js/StyleAuditoria.css" rel="stylesheet" />
    <style>
        #example .demo-container .submit {
            margin-left: 5px;
        }

        .demo-container .results {
            margin: 20px 0 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                    <telerik:RadComboBox 
                                        RenderMode="Lightweight" 
                                        ID="cboplaca" 
                                        runat="server"
                                        EnableLoadOnDemand="true" 
                                        Filter="Contains" 
                                        AllowCustomText="true"
                                        EmptyMessage="Movil"  
                                        ItemsPerRequest="4" 
                                        onchange="javascript:ObtenerPlaca(this.value);">
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
                            <select id="cbotrespuesta" class="form-control" style="width: 150px" onchange="combo(this);" >
                                <option value="60000">Tiempo</option>
                                <option value="60000">01 Minuto</option>
                                <option value="180000">03 Minuto</option>
                                <option value="300000">05 Minutos</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <input id="btnprueba" runat="server" class="btn btn-primary btn-xs" type="button" value="Buscar" onclick="BuscarTodo()" />
                            <input id="btnExportar" runat="server" class="btn btn-success btn-xs" type="button" value="Exportar" onclick="onBtExport()" />
                        </div>
                    </div>

                    <div class="panel-title label-info" id="menu2" style="font-size: small"><b>+Detalle</b></div>
                    <div class="panel-body" id="campo2">
                        <div class="table">
                            <div id="myGrid" style="height: 200px; width: 100%" class="ag-theme-balham"></div>
                        </div>
                    </div>

                    <div class="panel-title label-info" id="menu3" style="font-size: small"><b>+Filtros</b></div>
                </div>
            </div>
            <div class="span9">
                <div id="map" style=""></div>
            </div>
        </div>
    </div>
    

    <script type="text/javascript">
        function ObtenerPlaca(nro) {
           // var _time = $('#cbotrespuesta').val();
            //destroy();
            //AfterLoad(mensaje, _time);
            ObtenerPlacafinal(nro);
        }

        function BuscarTodo() {
            //var _time = $('#cbotrespuesta').val();
            var cbonroplaca = $find('<%=cboplaca.ClientID %>');
             var nro = cbonroplaca.get_selectedItem().get_value();
          //  destroy();
            //AfterLoad(cbonroplaca2, _time);
             ObtenerPlacafinal(nro);
        }

    </script>



    <script type="text/javascript" src="Content/js/ControlSeguimiento.js"></script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8FXziUDgzJajbYyYAWXVRKqoKv3g6hFs&signed_in=true&callback=initMap">
    </script>
</asp:Content>
