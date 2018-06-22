<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmAuditoria.aspx.cs" Inherits="WISETRACK.FrmAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/js/StyleAuditoria.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="container-fluid alto" style="padding-top: 33px">
        <div class="row-fluid">
            <div class="span12">
                <div class="span3">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Auditoria</div>
                        <div class="panel-title label-info" style="font-size: small" id="menu1"><b>+ Filtros</b></div>
                        <div class="panel-body" id="campo1">
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblfechaini" runat="server" Text="<b>Fecha Inicio</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <input type="text" name="datepicker1" id="datepicker1" class="form-control"
                                        pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                        title="Favor de rellenar fecha valida" style="font-size: small; width: 160px" />
                                </div>
                                <div class="form-group">
                                    <telerik:RadComboBox ID="cbohorai" runat="server" DropDownCssClass="dropdown" AllowCustomText="true" EmptyMessage="Hora" Width="70px" CssClass="dropdown">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="00:00" />
                                            <telerik:RadComboBoxItem Value="0" Text="00:30" />
                                            <telerik:RadComboBoxItem Value="1" Text="01:00" />
                                            <telerik:RadComboBoxItem Value="1" Text="01:30" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:00" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:30" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:00" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:30" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:00" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:30" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:00" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:30" />
                                            <telerik:RadComboBoxItem Value="6" Text="06:00" />
                                            <telerik:RadComboBoxItem Value="6" Text="06:30" />
                                            <telerik:RadComboBoxItem Value="7" Text="07:00" />
                                            <telerik:RadComboBoxItem Value="7" Text="07:30" />
                                            <telerik:RadComboBoxItem Value="8" Text="08:00" />
                                            <telerik:RadComboBoxItem Value="8" Text="08:30" />
                                            <telerik:RadComboBoxItem Value="9" Text="09:00" />
                                            <telerik:RadComboBoxItem Value="9" Text="09:30" />
                                            <telerik:RadComboBoxItem Value="10" Text="10:00" />
                                            <telerik:RadComboBoxItem Value="10" Text="10:30" />
                                            <telerik:RadComboBoxItem Value="11" Text="11:00" />
                                            <telerik:RadComboBoxItem Value="11" Text="11:30" />
                                            <telerik:RadComboBoxItem Value="12" Text="12:00" />
                                            <telerik:RadComboBoxItem Value="12" Text="12:30" />
                                            <telerik:RadComboBoxItem Value="13" Text="13:00" />
                                            <telerik:RadComboBoxItem Value="13" Text="13:30" />
                                            <telerik:RadComboBoxItem Value="14" Text="14:00" />
                                            <telerik:RadComboBoxItem Value="14" Text="14:30" />
                                            <telerik:RadComboBoxItem Value="15" Text="15:00" />
                                            <telerik:RadComboBoxItem Value="15" Text="15:30" />
                                            <telerik:RadComboBoxItem Value="16" Text="16:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="16:30" />
                                            <telerik:RadComboBoxItem Value="17" Text="17:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="17:30" />
                                            <telerik:RadComboBoxItem Value="18" Text="18:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="18:30" />
                                            <telerik:RadComboBoxItem Value="19" Text="19:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="19:30" />
                                            <telerik:RadComboBoxItem Value="20" Text="20:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="20:30" />
                                            <telerik:RadComboBoxItem Value="21" Text="21:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="21:30" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:00" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:30" />
                                            <telerik:RadComboBoxItem Value="23" Text="23:00" />
                                            <telerik:RadComboBoxItem Value="22" Text="23:30" />
                                            <telerik:RadComboBoxItem Value="24" Text="23:59" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblfechafin" runat="server" Text="<b>Fecha Fin</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <input type="text" name="datepicker2" id="datepicker2" class="form-control"
                                        pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                        title="Favor de rellenar fecha valida" style="font-size: small; width: 160px" />
                                </div>
                                <div class="form-group">
                                    <telerik:RadComboBox ID="cbohoraf" runat="server" DropDownCssClass="dropdown" AllowCustomText="true"
                                        EmptyMessage="Hora" Width="70px" CssClass="dropdown">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="00:00" />
                                            <telerik:RadComboBoxItem Value="0" Text="00:30" />
                                            <telerik:RadComboBoxItem Value="1" Text="01:00" />
                                            <telerik:RadComboBoxItem Value="1" Text="01:30" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:00" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:30" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:00" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:30" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:00" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:30" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:00" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:30" />
                                            <telerik:RadComboBoxItem Value="6" Text="06:00" />
                                            <telerik:RadComboBoxItem Value="6" Text="06:30" />
                                            <telerik:RadComboBoxItem Value="7" Text="07:00" />
                                            <telerik:RadComboBoxItem Value="7" Text="07:30" />
                                            <telerik:RadComboBoxItem Value="8" Text="08:00" />
                                            <telerik:RadComboBoxItem Value="8" Text="08:30" />
                                            <telerik:RadComboBoxItem Value="9" Text="09:00" />
                                            <telerik:RadComboBoxItem Value="9" Text="09:30" />
                                            <telerik:RadComboBoxItem Value="10" Text="10:00" />
                                            <telerik:RadComboBoxItem Value="10" Text="10:30" />
                                            <telerik:RadComboBoxItem Value="11" Text="11:00" />
                                            <telerik:RadComboBoxItem Value="11" Text="11:30" />
                                            <telerik:RadComboBoxItem Value="12" Text="12:00" />
                                            <telerik:RadComboBoxItem Value="12" Text="12:30" />
                                            <telerik:RadComboBoxItem Value="13" Text="13:00" />
                                            <telerik:RadComboBoxItem Value="13" Text="13:30" />
                                            <telerik:RadComboBoxItem Value="14" Text="14:00" />
                                            <telerik:RadComboBoxItem Value="14" Text="14:30" />
                                            <telerik:RadComboBoxItem Value="15" Text="15:00" />
                                            <telerik:RadComboBoxItem Value="15" Text="15:30" />
                                            <telerik:RadComboBoxItem Value="16" Text="16:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="16:30" />
                                            <telerik:RadComboBoxItem Value="17" Text="17:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="17:30" />
                                            <telerik:RadComboBoxItem Value="18" Text="18:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="18:30" />
                                            <telerik:RadComboBoxItem Value="19" Text="19:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="19:30" />
                                            <telerik:RadComboBoxItem Value="20" Text="20:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="20:30" />
                                            <telerik:RadComboBoxItem Value="21" Text="21:00" />
                                            <telerik:RadComboBoxItem Value="16" Text="21:30" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:00" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:30" />
                                            <telerik:RadComboBoxItem Value="23" Text="23:00" />
                                            <telerik:RadComboBoxItem Value="22" Text="23:30" />
                                            <telerik:RadComboBoxItem Value="24" Text="23:59" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblplaca" runat="server" Text="<b>Placa</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:UpdatePanel ID="upcboplaca" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadComboBox
                                            ID="cboplaca"
                                            runat="server"
                                            RenderMode="Lightweight"
                                            EnableLoadOnDemand="true"
                                            AllowCustomText="true"
                                            Filter="Contains"
                                            EmptyMessage="Movil"
                                            ItemsPerRequest="4"
                                            CssClass="dropdown">
                                        </telerik:RadComboBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblkmh" runat="server" Text="<b>Km/h</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="dropdown-toggle">
                                        <asp:DropDownList ID="cbokm" CssClass="form-control" runat="server" Style="font-size: smaller">
                                            <asp:ListItem Text="Igual a" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Mayor a" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Mayor o igual a" Value="3" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtkmh" runat="server" Style="font-size: small; width: 100px" CssClass="form-control" placeholder="0"
                                        type="number" title="KM es requerido" required="true" pattern="[0-9]{1,2}"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" style="padding-top: 10px"></div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="upbtncargar" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-primary btn-xs" Text="Cargar"
                                                Style="font-size: smaller" type="submit" OnClick="btnCargar_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group">
                                    <input id="btnbuscar" type="button" runat="server" class="btn btn-primary btn-xs" value="Pintar"
                                        style="font-size: smaller" onclick="CargarDatosAuditoria()" />
                                </div>
                                <div class="form-group">
                                    <input type="button" id="btnExportar" runat="server" value="Exportar"
                                        class="btn btn-success btn-xs" onclick="exportar()" />

                                </div>
                                <div class="form-group">
                                    <input type="button" id="btnLimpiarOptimizado" runat="server" value="Limpiar"
                                        class="btn btn-danger btn-xs" onclick="LimpiarAuditoria2()" />
                                </div>
                            </div>
                            <div class="form-group">
                            </div>
                            <div class="table-responsive table-hover">
                                <asp:UpdatePanel ID="upgrilla1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gdvListaAudtoria" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                            Font-Size="Smaller" AutoGenerateColumns="False" DataKeyNames="NroPlaca" EmptyDataText="Seleccione una Placa"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableViewState="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbauditoria" runat="server" Font-Size="Smaller" Visible="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                                    <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Patente" HeaderText="Patente" ReadOnly="true" SortExpression="Patente" />
                                                <asp:BoundField DataField="NroPlaca" HeaderText="Placa" ReadOnly="true" SortExpression="Placa" />
                                                <asp:BoundField DataField="FechaIni" HeaderText="FechaInicio" SortExpression="FechaIni"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HoraIni" HeaderText="HoraInicio" SortExpression="HoraIni"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FechaFin" HeaderText="FechaFin" SortExpression="FechaFin"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HoraFin" HeaderText="HoraFin" SortExpression="HoraFin"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="Tipo"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor" HeaderText="Valor" SortExpression="Valor"
                                                    HeaderStyle-CssClass="visible-lg visible-md" ItemStyle-CssClass="visible-lg visible-md">
                                                    <HeaderStyle CssClass="visible-lg"></HeaderStyle>
                                                    <ItemStyle CssClass="visible-lg visible-md"></ItemStyle>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="panel-title label-info" style="font-size: small" id="menu01"><b>+ Detalle</b></div>
                        <div class="panel-body" id="campo01" style="display: none">
                            <div class="table">
                                <div id="myGrid" style="height: 350px; width: 100%" class="ag-theme-balham"></div>
                            </div>
                        </div>
                        <div class="panel-title label-info" style="font-size: small" id="menu2"><b>+ Grupo</b></div>
                        <div class="panel-body" id="campo2">
                        </div>
                    </div>
                </div>
                <script>
                    $(function () {
                        

                            var datepicker1 = $("#datepicker1");

                        datepicker1.kendoMaskedTextBox({
                            mask: "00/00/0000"
                        });

                        datepicker1.kendoDatePicker({
                            format: "dd/MM/yyyy"
                        });

                        datepicker1.closest(".k-datepicker")
                        .add(datepicker1)
                        .removeClass("k-textbox");

                        //combine MaskedTextBox with DatePicker (officially unsupported)
                        var datepicker2 = $("#datepicker2");

                        datepicker2.kendoMaskedTextBox({
                            mask: "00/00/0000"
                        });

                        datepicker2.kendoDatePicker({
                            format: "dd/MM/yyyy"
                        });

                        datepicker2.closest(".k-datepicker")
                        .add(datepicker2)
                        .removeClass("k-textbox");
                    });
                </script>
                <div class="span9">
                    <div id="map"></div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upbtncargar">
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

    <script type="text/javascript">
        function CargarDatosAuditoria() {

            var fini = $("#datepicker1").val();
            var ffin = $("#datepicker2").val();

            var ahini = $find("<%= cbohorai.ClientID %>");
            var hini = '' + ahini._text;
            var ahfin = $find("<%= cbohoraf.ClientID %>");
            var hfin = '' + ahfin._text;
            var aplaca = $find("<%= cboplaca.ClientID %>");
            //var placa = '' + aplaca._text;

            var gdvpgeo = document.getElementById("<%= gdvListaAudtoria.ClientID%>");
            var count = gdvpgeo.rows.length;
            if (count > 2) {
                destroy();
            }
            var c = 0;
            for (i = 1; i < count; i++) {
                var gdvRow = gdvpgeo.rows[i];
                var ckb = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                var patente = gdvRow.cells[1].innerHTML;
                var placa = gdvRow.cells[2].innerHTML;
                var fini = gdvRow.cells[3].innerHTML;
                var hini = gdvRow.cells[4].innerHTML;
                var ffin = gdvRow.cells[5].innerHTML;
                var hfin = gdvRow.cells[6].innerHTML;
                var cod = gdvRow.cells[7].innerHTML;
                var velocidad = gdvRow.cells[8].innerHTML;
                if (ckb.checked == true) {
                    c = c + 1;
                    if (c == 1) {
                        var _fini = fini + " " + hini;
                        //var _fini = '01-01-1970 00:03:44';

                        var _ffin = ffin + " " + hfin;
                        
                        if (verficardias(_fini, _ffin)) {
                            AuditoriaOpmizado(_fini, _ffin, placa, count);
                        } else {
                            error("No se puede hacer la Auditoria, mayor a 7 dias")
                        }
                        //verAuditoria(fini, hini, ffin, hfin, placa, cod, velocidad, count);

                    } else {
                        alert("Por favor seleccione uno...");
                    }
                } else {
                    error("Por favor seleccione uno...");
                }
            }
        }
        function verficardias(_fini, _ffin) {
            var dias = getDias(_fini, _ffin);
            if (dias < 8) {
                return true;
            }
            return false;

        }
        function dateString2Date(_data) {
            var ndata = _data.split('/').join('-');
            var dt = ndata.split(/\-|\s/);
            var ds = dt.slice(0, 3).reverse().join('/');
            return new Date(ds + ' ' + dt[3]);
            //dat = new Date(dt.slice(0, 3).reverse().join('/') + ' ' + dt[3]);
        }
        function getDias(_fini, _ffin) {
            var fi = dateString2Date(_fini);
            var fn = dateString2Date(_ffin);
            var timeStart = fn.getTime();
            var timeEnd = fi.getTime();
            var hourDiff = timeStart - timeEnd; //in ms
            var secDiff = hourDiff / 1000; //in s
            var minDiff = hourDiff / 60 / 1000; //in minutes
            var horasDiff = minDiff / (24 * 60);
            return horasDiff;
        }
        function exportar() {
            //alert("Exportando...");
            onBtExport();
        }

        function LimpiarAuditoria2() {
            $("table[id$='gdvListaAudtoria']").html("");
            LimpiarAuditoria();
            destroy();
        }

    </script>

    <script>
        $(document).ready(function () {
            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
        });
    </script>

            
    <script src="Content/js/ControlAuditoria.js" type="text/javascript"></script>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6goyEP05rA-H1u7YHb4CLGpBULn21kCY&signed_in=true&callback=initMap">
    </script>

</asp:Content>


