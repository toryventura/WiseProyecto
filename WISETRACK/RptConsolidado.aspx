<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptConsolidado.aspx.cs" Inherits="WISETRACK.RptConsolidado" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #map {
            width: auto;
            height: 620px;
        }

        body {
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }

        @media (min-width: 1024px) and (max-width: 1366px) {
            /* Enable use of floated navbar text */
            .navbar-text.pull-right {
                float: none;
                padding-left: 5px;
                padding-right: 5px;
            }

            .alto {
                max-height: 600px;
                min-height: 500px;
            }

            .col.ancho {
                width: 100px;
            }
        }


        /*aqui esta la separacion*/
        #overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #3a3636;
            width: 100%;
            height: 100%;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        #theprogress {
            /*background-color: #D3BB9C;*/
            width: 110px;
            height: 24px;
            text-align: center;
            filter: alpha(opacity=80);
            opacity: 1;
        }

        #modalprogress {
            position: absolute;
            top: 50%;
            left: 50%;
            margin: -11px 0 0 -55px;
            color: #312e2e;
        }

        body > #modalprogress {
            position: fixed;
        }

        #ParentDIV {
            width: 50%;
            height: 100%;
            font-size: 12px;
            font-family: Calibri;
        }

        .cCargando {
            width: 100%;
            height: 100%;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            position: fixed;
            background-color: #000;
            opacity: 0.8;
            filter: alpha(opacity=80); /* Internet Explorer 8*/
            z-index: 9999;
            transition: width 2s;
            -moz-transition: width 2s; /* Firefox 4 */
            -webkit-transition: width 2s; /* Safari and Chrome */
            -o-transition: width 2s; /* Opera */
            cursor: progress;
        }

        .cCargandoImg {
            cursor: progress;
            position: absolute;
            top: 32%;
            right: 45%;
            left: 35%;
            filter: alpha(opacity=80); /* Internet Explorer 8*/
            opacity: 0.8;
            margin: auto;
            width: 350px;
            text-align: center;
            height: 150px;
            padding: 10px;
            background-color: #000;
            /*border: 1px solid #000;*/
            color: #ffffff;
            font-size: 2em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Content/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="Content/black/kendo.common.min.css" />
    <link rel="stylesheet" href="Content/black/kendo.rtl.min.css" />
    <link rel="stylesheet" href="Content/black/kendo.silver.min.css" />
    <link rel="stylesheet" href="Content/black/kendo.mobile.all.min.css" />
    <script src="Scripts/jquery-2.2.1.min.js"></script>
    <script src="Content/black/angular.min.js"></script>
    <script src="Content/black/kendo.all.min.js"></script>
    <script src="Scripts/jquery-ui.js"></script>
    <div id="divProceso" class="cCargando" style="visibility: hidden;">
        <div id="divProcesoMsg" class="cCargandoImg">
            <br />
            En proceso 
            <br />
        </div>
    </div>
    <div class="container-fluid" style="padding-top: 33px" id="subcontainer">
        <div class="row-fluid" style="height: 100%">
            <div class="span12" style="height: 100%">
                <div class="span3" style="height: 100%">

                    <div class="panel panel-primary">
                        <div class="panel-heading">Reporte de Consolidado</div>
                        <div class="panel-title label-info" style="font-size: small" id="menu1"><b>Filtros</b></div>
                        <div class="panel-body" id="campo1" style="height: 100%">
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
                                    <telerik:RadComboBox ID="cbohorai" runat="server" DropDownCssClass="dropdown" AllowCustomText="true"
                                        EmptyMessage="Hora" Width="70px" CssClass="dropdown">
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
                                    </telerik:RadComboBox>
                                </div>
                            </div>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Label ID="lblplaca" runat="server" Text="<b>Móvil (Placa)</b>" Font-Size="Small"></asp:Label>
                                </div>
                            </div>

                            <div class="form-inline">
                                <div class="form-group" style="width: 100%; overflow-y: auto">
                                    <asp:UpdatePanel ID="upcboplaca" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <%--<telerik:RadComboBox ID="cboplaca" runat="server" 
                                                DropDownCssClass="dropdown" AllowCustomText="true"
                                                AutoPostBack="true" EmptyMessage="Placa" CssClass="dropdown">
                                            </telerik:RadComboBox>--%>
                                            <telerik:RadComboBox
                                                RenderMode="Lightweight"
                                                ID="cboplaca"
                                                runat="server"
                                                EnableLoadOnDemand="true"
                                                Filter="Contains"
                                                AllowCustomText="true"
                                                EmptyMessage="Movil"
                                                ItemsPerRequest="4">
                                            </telerik:RadComboBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="form-group"></div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="upcargar" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click1" Text="Buscar" CssClass="btn btn-primary btn-sm" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnExportar" OnClick="btnExportar_Click" Text="Exportar" CssClass="btn btn-primary btn-sm" />
                                </div>
                                <div class="form-group">
                                    <telerik:RadComboBox ID="rcbFormato" runat="server" DropDownCssClass="dropdown" AllowCustomText="false" EmptyMessage="" Width="100px" CssClass="dropdown">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="EXCEL" Selected="true" />
                                            <telerik:RadComboBoxItem Value="1" Text="PDF" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="panel-title label-info" style="font-size: small" id="menu2"><b>Configurar Filtros Adicionales</b></div>
                        <div class="panel-body" id="campo2">
                            <div class="form-group">
                                <div class="table-responsive" style="overflow-y: scroll; border-style: groove; border-color: #5bc0de">
                                    <asp:GridView ID="grvParametersDetails" runat="server" CssClass="table table-striped table-bordered table-hover"
                                        AutoGenerateColumns="False"
                                        CellPadding="2"
                                        Width="97%" Style="text-align: left">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVerificar" Text="" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="RowNumber" HeaderText="Reporte" />
                                            <asp:TemplateField HeaderText="Condition">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpCondition" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parametro">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtParametro" runat="server" MaxLength="50" type="number" Style="height: 20px; width: 50px;"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="table-responsive" style="overflow-y: scroll; border-style: groove; border-color: #5bc0de">
                                    <asp:GridView ID="grvParameters" runat="server" CssClass="table table-striped table-bordered table-hover"
                                        AutoGenerateColumns="False"
                                        CellPadding="2" OnRowDeleting="grvParameters_RowDeleting"
                                        Width="97%" Style="text-align: left">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chekVerificar1" Text="" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="RowNumber" HeaderText="Reporte" />

                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="span9" style="height: 100%">
                    <div class="panel panel-primary" style="height: 100%">

                        <div class="panel-body" style="height: 100%">

                            <div class="table-responsive" style="width: 100%; height: 100%;">

                                <div id="tabs" style="height: 100%">
                                    <ul>
                                        <li><a href="#tabs-1">Temperatura</a></li>
                                        <li><a href="#tabs-2">Detenciones</a></li>
                                        <li><a href="#tabs-3">Entrada-salida</a></li>
                                        <li><a href="#tabs-4">Kilometraje</a></li>
                                        <li><a href="#tabs-5">Velicidad Maxima</a></li>
                                        <li><a href="#tabs-6">Encendido Apagado</a></li>
                                        <%--<li><a href="#tabs-7">Encendido Apagado</a></li>--%>
                                    </ul>

                                    <div id="tabs-1">
                                        <div class="container-fluid" id="tabTemperatura" runat="server" style="height: 100%; width: 100%;">
                                            <div class="row-fluid" style="height: 100%; width: 100%;">
                                                <div class="table" style="width: 100%; height: 100%; overflow-y: scroll;">
                                                    <asp:UpdatePanel ID="UpdatePanelTemperatura" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Height="100%" Width="100%"
                                                                AutoDataBind="true" DisplayStatusbar="false" EnableTheming="true"
                                                                HasSearchButton="False" HasToggleGroupTreeButton="False" ToolPanelView="None"
                                                                HasToggleParameterPanelButton="False" HasZoomFactorList="False" OnUnload="CrystalReportViewer1_Unload"
                                                                SeparatePages="False" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="False"
                                                                ValidateRequestMode="Inherit" HasPrintButton="False" HasGotoPageButton="False" HasExportButton="False"
                                                                HasCrystalLogo="False" />
                                                            <asp:Label ID="lblf" runat="server" Text="Cargando..." Style="display: none"></asp:Label>
                                                            <div id="msnTemperatura" runat="server" visible="false">
                                                                <h3>
                                                                    <samp>No Existen datos para mostrar</samp></h3>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>


                                        <%--                                        <div class="container-fluid" style="width: 100%; height: 100%">
                                            <div class="row-fluid" style="width: 100%; height: 100%">
                                                <div class="table" style="width: 100%; height: 100%; overflow-y: scroll;">
                                                    <asp:UpdatePanel ID="UpdatePanelAudotiria" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server"
                                                                AutoDataBind="true" DisplayStatusbar="false" EnableTheming="true"
                                                                HasSearchButton="False" HasToggleGroupTreeButton="False" ToolPanelView="None"
                                                                HasToggleParameterPanelButton="False" HasZoomFactorList="False" OnUnload="CrystalReportViewer2_Unload"
                                                                SeparatePages="False" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="False"
                                                                ValidateRequestMode="Inherit" HasPrintButton="False" HasGotoPageButton="False"
                                                                HasExportButton="False" HasCrystalLogo="False" />
                                                            <div id="msnAuditoria" runat="server" visible="false">
                                                                <h3>
                                                                    <samp>
                                                                        <asp:Label ID="lblAuditoria" runat="server" Text="No Existen datos para mostrar" ></asp:Label></samp></h3>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div id="tabs-2">

                                        <div class="table-responsive" style="width: 100%; height: 398px; overflow-y: auto" id="tabDetencion" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanelDetencion" runat="server" UpdateMode="Conditional" AutoGenerateColumns="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gdvDetenciones" runat="server" OnRowCommand="gdvDetenciones_RowCommand"
                                                        CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Vehiculo" AutoGenerateColumns="false"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" ReadOnly="true" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="FechaInicio" HeaderText="Inicio Detención" SortExpression="FechaInicio" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="FechaFin" HeaderText="Fin Detención" SortExpression="FechaFin" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Tiempo" HeaderText="Tiempo (Min)" SortExpression="Tiempo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" ReadOnly="true" SortExpression="Ubicacion" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Longitud" HeaderText="Longitud" ReadOnly="true" SortExpression="Longitud" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                                            <asp:BoundField DataField="Latitud" HeaderText="Latitud" ReadOnly="true" SortExpression="Latitud" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkVerMapa" runat="server" CssClass="alert-link" CommandName="VerMapa" Text="Ver Ubicación" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                                                    <div id="msnDetencion" runat="server" visible="false">
                                                        <h3>
                                                            <samp>No Existen datos para mostrar</samp></h3>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                    <div id="tabs-3">

                                        <div class="table-responsive" style="width: 100%; height: 434px; overflow-y: auto" id="tabEntradaSalida" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanelEntradaSalida" runat="server" UpdateMode="Conditional" AutoGenerateColumns="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gdvEntradaSalida" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvEntradaSalida_RowCommand"
                                                        CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Vehiculo"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" ReadOnly="true" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Geocerca" HeaderText="Geocerca" ReadOnly="true" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="FechaEntrada" HeaderText="Fecha Entrada" SortExpression="FechaEntrada" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="FechaSalida" HeaderText="Fecha Salida" SortExpression="FechaSalida" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Tiempo" HeaderText="Tiempo (Min)" SortExpression="Tiempo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" ReadOnly="true" SortExpression="Ubicación" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Longitud" HeaderText="Longitud" ReadOnly="true" SortExpression="Longitud" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                                            <asp:BoundField DataField="Latitud" HeaderText="Latitud" ReadOnly="true" SortExpression="Latitud" HeaderStyle-CssClass="hidden-lg hiden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkVerMapa" runat="server" CssClass="alert-link" CommandName="VerMapa" Text="Ver Ubicación" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                                                    <div id="msnEntradaSalida" runat="server" visible="false">
                                                        <h3>
                                                            <samp>No Existen datos para mostrar</samp></h3>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                    <div id="tabs-4">

                                        <div class="table-responsive" style="width: 100%; height: 398px; overflow-y: auto" id="tabKilometraje" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanelKilometraje" runat="server" UpdateMode="Conditional" AutoGenerateColumns="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gdvKilometrajes" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvKilometrajes_RowCommand"
                                                        CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Vehiculo"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" ReadOnly="true" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" SortExpression="FechaInicio" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" SortExpression="FechaInicio" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Kilometraje" HeaderText="Distancia (Km)" SortExpression="Tiempo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />

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
                                                    <div id="msnKilometraje" runat="server" visible="false">
                                                        <h3>
                                                            <samp>No Existen datos para mostrar</samp></h3>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                    <div id="tabs-5">
                                        <div class="table-responsive" style="width: 100%; height: 398px; overflow-y: auto" id="tabVelocidadMax" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanelVelocidadMax" runat="server" UpdateMode="Conditional" AutoGenerateColumns="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gdvVelocidadesMax" runat="server" OnRowCommand="gdvVelocidadesMax_RowCommand" AutoGenerateColumns="false"
                                                        CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Vehiculo"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" ReadOnly="true" SortExpression="Vehiculo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="FechaInicio" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Velocidad" HeaderText="Velocidad (Km/h)" SortExpression="Tiempo" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" ReadOnly="true" SortExpression="Ubicacion" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Longitud" HeaderText="Longitud" ReadOnly="true" SortExpression="Longitud" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                                            <asp:BoundField DataField="Latitud" HeaderText="Latitud" ReadOnly="true" SortExpression="Latitud" HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkVerMapa" runat="server" CssClass="alert-link" CommandName="VerMapa" Text="Ver Ubicación" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                                                    <div id="msnVelocidadMax" runat="server" visible="false">
                                                        <h3>
                                                            <samp>No Existen datos para mostrar</samp></h3>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                    <div id="tabs-6">

                                        <div class="table-responsive" style="width: 100%; height: 400px;" id="tabEncendidoApagado" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanelEncendidoApagado" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <rsweb:ReportViewer ID="rptViewer1" runat="server" Width="96%" SizeToReportContent="false"></rsweb:ReportViewer>
                                                    <div id="msnEncendidoApagado" runat="server" visible="false">
                                                        <h3>
                                                            <samp>No Existen datos para mostrar</samp></h3>
                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                </div>
                                <%--   <div id="tabs-7">--%>

                                <%--                                        <div class="container-fluid" style="width: 100%; height: 100%">
                                            <div class="row-fluid" style="width: 100%; height: 100%">
                                                <div class="table" style="width: 100%; height: 100%; overflow-y: scroll;">
                                                    <asp:UpdatePanel ID="UpdatePanelAudotiria" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server"
                                                                AutoDataBind="true" DisplayStatusbar="false" EnableTheming="true"
                                                                HasSearchButton="False" HasToggleGroupTreeButton="False" ToolPanelView="None"
                                                                HasToggleParameterPanelButton="False" HasZoomFactorList="False" OnUnload="CrystalReportViewer2_Unload"
                                                                SeparatePages="False" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="False"
                                                                ValidateRequestMode="Inherit" HasPrintButton="False" HasGotoPageButton="False"
                                                                HasExportButton="False" HasCrystalLogo="False" />
                                                            <div id="msnAuditoria" runat="server" visible="false">
                                                                <h3>
                                                                    <samp>
                                                                        <asp:Label ID="lblAuditoria" runat="server" Text="No Existen datos para mostrar" ></asp:Label></samp></h3>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>--%>
                                <%--</div>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upcargar">
                    <ProgressTemplate>
                        <div id="overlay">
                            <div id="modalprogress">
                                <div id="theprogress">
                                    <img src="Content/img/cargando5.gif" alt="indicador" />
                                </div>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <script type="text/javascript" lang="javascript">
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
                    $(document).ready(function () {
                        var fechaActual = new Date();
                        $("#campo2").hide();
                        $("#datepicker1").data("kendoDatePicker").value(fechaActual);
                        $("#datepicker2").data("kendoDatePicker").value(fechaActual);
                        var ventana_ancho = $(window).width();
                        var ventana_alto = $(window).height() - 80;
                        $("#subcontainer").height(ventana_alto);

                    });
                    $(function () {
                        $("#tabs").tabs();
                        $("#MyAccordion").accordion();
                    });
                    $(document).ready(function () {
                        $("#menu1").click(function () {
                            $("#campo1").slideToggle("slow");
                            if ($('#campo2').is(":visible")) {
                                $('#campo2').hide();
                            } else { $('#campo2').show(); }
                        });
                    });
                    $(document).ready(function () {
                        //$("#campo2").hide();
                        $("#menu2").click(function () {
                            $("#campo2").slideToggle("slow");
                            if ($('#campo1').is(":visible")) {
                                $('#campo1').hide();
                            } else { $('#campo1').show(); }
                        });
                    });
                    function init() {
                        var cboGeocerca = $find("<%= grvParametersDetails.ClientID %>");

                        var geoItems = cboGeocerca.get_items();

                        for (var i = 0; i < geoItems.get_count() ; i++) {
                            var geoItem = geoItems.getItem(i);

                            geoItem.set_checked(true);
                        }

                        cboGeocerca.set_text("Seleccionar Geocercas");

                        var cboVehiculo = $find("<%= cboplaca.ClientID %>");

                        var vehItems = cboVehiculo.get_items();

                        for (var i = 0; i < vehItems.get_count() ; i++) {
                            var vehItem = vehItems.getItem(i);

                            vehItem.set_checked(true);
                        }

                        cboVehiculo.set_text("Seleccionar Vehículos");


                    }
                    function gfProceso() {
                        if (document.getElementById("divProceso").style.visibility == "visible")
                            document.getElementById("divProceso").style.visibility = "hidden";
                        else
                            document.getElementById("divProceso").style.visibility = "visible";
                    }
                    function diHola() {

                        alert("hola como estas");
                    }

                </script>
            </div>
        </div>
    </div>

</asp:Content>
