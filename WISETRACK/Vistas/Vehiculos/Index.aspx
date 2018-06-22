<%@ Page Title="Vehiculos" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.Vehiculos.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <h3><b>LISTADO DE VEHICULOS</b></h3>
            <div class="form-inline">
                <div class="form-group">
                    <a runat="server" href="~/Vistas/Vehiculos/Create">Crear Nuevo</a> |
                </div>
                <div class="form-group">
                    <a runat="server" href="~/Vistas/Seguimientos/Index">Ver Seguimiento</a> |
                </div>
                <div class="form-group">
                    <a runat="server" href="~/Vistas/AsignarConductor/Index">Asignar Conductor</a> |
                </div>
                <input type="button" value="Exportar Excel" class="btn-link" id="pruexcel" onclick="onBtExport();" />
            </div>
            <div class="form-group"></div>
            <div class="table-responsive">
                <div id="myGrid" style="height: 430px; width: 100%" class="ag-theme-balham"></div>
            </div>
        </div>

        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Asignar Conductor</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <input type="text" name="" value="23234 " id="txtnroplaca" class="btn btn-default" disabled="disabled" />
                        </div>
                        <div class="form-group">
                            <select style="text-align: left" name="pais" id="idCondcutores" class="btn btn-default">
                                <option value="">Selecciona...</option>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="ok" type="button" class="btn btn-default" data-dismiss="modal" onclick="Asignar();">Aceptar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Canselar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    
    <script src="../../Content/js/GestionVehiculo.js"></script>
    <script type="text/javascript">
        function asignarEdit(nroplaca) {
            // $('#myModal').show();
            //alert("" + nroplaca);
            GetCondcutores();
            $('#txtnroplaca').val(nroplaca);
        }
        function Asignar() {
            var ci = parse.int($("#idCondcutores option:selected").text());
            if (ci != "0") {

            } else {
                //seleciones un condcutor de las lista
            }
        }
        function cargarCombo(data) {
            var lsConductores = $('#idCondcutores');
            lsConductores.find('option').remove().end().append('<option value="0">Seleccione...</option>').val('');
            for (i in data) {
                lsConductores.append('<option value="' + data[i].CI + '">' + data[i].NombreCompleto + '</option>');
            };
            // selector.find('option').remove().end().append('selecionA...').val('');

            //$.each(data, function (id, value) {
            //    $("#exampleInputState select").append('<option value="' + value.CI + '">' + value.NombreCompleto + '</option>');
            //});
        }

        function GetCondcutores() {

            //var action = "{'data': 'GM'}";

            $.ajax({
                url: "/Vistas/Vehiculos/Index.aspx/CargarConductores",
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (response) {
                //myData = JSON.parse(response.d);
                //rowData = myData;
                //DesbloquearPantalla();
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : JSON.parse(response.d);
                if (models.length > 0) {
                    cargarCombo(models);
                    $('#myModal').modal('show');
                } else {
                    // no existe conductores disponibles.
                }
                //   valor = models;
                /// mostrar mendaje si no esxite conductores libre

            }).fail(function (jqXHR, textStatus) {

                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {

                } else {
                    //gridOptions.api.setRowData(myData);

                }

                //DesbloquearPantalla();
            });

        }



    </script>

</asp:Content>

