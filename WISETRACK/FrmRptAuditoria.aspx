<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmRptAuditoria.aspx.cs" Inherits="WISETRACK.FrmRptAuditoria" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="aspnet_client/system_web/4_0_30319/crystalreportviewers13/js/crviewer/crv.js" type="text/javascript"></script>
    <link href="aspnet_client/system_web/4_0_30319/crystalreportviewers13/js/crviewer/images/style.css" rel="stylesheet" />
    <script src="aspnet_client/system_web/4_6_79/crystalreportviewers13/js/crviewer/crv.js" type="text/javascript"></script>
    <link href="aspnet_client/system_web/4_6_79/crystalreportviewers13/js/crviewer/images/style.css" rel="stylesheet" />
    <script src="aspnet_client/system_web/4_6_81/crystalreportviewers13/js/crviewer/crv.js" type="text/javascript"></script>
    <link href="aspnet_client/system_web/4_6_81/crystalreportviewers13/js/crviewer/images/style.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        

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
        }
    </style>
     <style type="text/css">
        #overlay {
            position: fixed;
            z-index: 98;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ffffff;
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
            color: white;
        }

        body > #modalprogress {
            position: fixed;
        }
    </style>

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
                                    <telerik:RadComboBox ID="cbohorai" runat="server" DropDownCssClass="dropdown" AllowCustomText="true"
                                        EmptyMessage="Hora" Width="70px" CssClass="dropdown">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="00:00" />
                                            <telerik:RadComboBoxItem Value="0" Text="00:30" />
                                            <telerik:RadComboBoxItem Value="1" Text="01:00" />
                                            <telerik:RadComboBoxItem Value="0" Text="01:30" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:00" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:30" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:00" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:30" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:00" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:30" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:00" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:30" />
                                            <telerik:RadComboBoxItem Value="6" Text="06:00" />
                                            <telerik:RadComboBoxItem Value="5" Text="06:30" />
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
                                            <telerik:RadComboBoxItem Value="17" Text="17:30" />
                                            <telerik:RadComboBoxItem Value="18" Text="18:00" />
                                            <telerik:RadComboBoxItem Value="18" Text="18:30" />
                                            <telerik:RadComboBoxItem Value="19" Text="19:00" />
                                            <telerik:RadComboBoxItem Value="19" Text="19:30" />
                                            <telerik:RadComboBoxItem Value="20" Text="20:00" />
                                            <telerik:RadComboBoxItem Value="20" Text="20:30" />
                                            <telerik:RadComboBoxItem Value="21" Text="21:00" />
                                            <telerik:RadComboBoxItem Value="21" Text="21:30" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:00" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:30" />
                                            <telerik:RadComboBoxItem Value="23" Text="23:00" />
                                            <telerik:RadComboBoxItem Value="23" Text="23:30" />
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
                                            <telerik:RadComboBoxItem Value="0" Text="01:30" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:00" />
                                            <telerik:RadComboBoxItem Value="2" Text="02:30" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:00" />
                                            <telerik:RadComboBoxItem Value="3" Text="03:30" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:00" />
                                            <telerik:RadComboBoxItem Value="4" Text="04:30" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:00" />
                                            <telerik:RadComboBoxItem Value="5" Text="05:30" />
                                            <telerik:RadComboBoxItem Value="6" Text="06:00" />
                                            <telerik:RadComboBoxItem Value="5" Text="06:30" />
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
                                            <telerik:RadComboBoxItem Value="17" Text="17:30" />
                                            <telerik:RadComboBoxItem Value="18" Text="18:00" />
                                            <telerik:RadComboBoxItem Value="18" Text="18:30" />
                                            <telerik:RadComboBoxItem Value="19" Text="19:00" />
                                            <telerik:RadComboBoxItem Value="19" Text="19:30" />
                                            <telerik:RadComboBoxItem Value="20" Text="20:00" />
                                            <telerik:RadComboBoxItem Value="20" Text="20:30" />
                                            <telerik:RadComboBoxItem Value="21" Text="21:00" />
                                            <telerik:RadComboBoxItem Value="21" Text="21:30" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:00" />
                                            <telerik:RadComboBoxItem Value="22" Text="22:30" />
                                            <telerik:RadComboBoxItem Value="23" Text="23:00" />
                                            <telerik:RadComboBoxItem Value="23" Text="23:30" />
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
                                        <%--<telerik:RadComboBox ID="cboplaca" runat="server" 
                                            DropDownCssClass="dropdown" AllowCustomText="true"
                                            AutoPostBack="true" EmptyMessage="Placa" 
                                            CssClass="dropdown">
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
                                            <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-primary btn-xs" Text="Cargar" type="submit" OnClick="btnCargar_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnExportar" runat="server" CssClass="btn btn-primary btn-xs" Text="Exportar" type="submit" OnClick="btnExportar_Click" />
                                </div>
                            </div>
                            <div class="form-group"></div>
                        </div>
                    </div>
                </div>
                <script>
                    $(function () {
                        http://dojo.telerik.com/

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
                    <div class="container-fluid">
                        <div class="row-fluid">
                            <div class="table" style="width: 100%; height: 550px; overflow-y: scroll;">
                                <asp:UpdatePanel ID="upcrystal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
                                            AutoDataBind="true" DisplayStatusbar="false" EnableTheming="true"
                                            HasSearchButton="False" HasToggleGroupTreeButton="False" ToolPanelView="None"
                                            HasToggleParameterPanelButton="False" HasZoomFactorList="False" OnUnload="CrystalReportViewer1_Unload" 
                                            SeparatePages="False" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="False" 
                                            ValidateRequestMode="Inherit" HasPrintButton="False" HasGotoPageButton="False" 
                                            HasExportButton="False" HasCrystalLogo="False" />
                                        <asp:Label ID="lblf" runat="server" Text="Cargando..." Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
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

    <script>
        $(document).ready(function () {
            var fechaActual = new Date();

            $("#datepicker1").data("kendoDatePicker").value(fechaActual);
            $("#datepicker2").data("kendoDatePicker").value(fechaActual);
        });
    </script>
</asp:Content>
