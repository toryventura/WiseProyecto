<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.Seguimientos.Edit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.common.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.rtl.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.silver.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2016.1.226/styles/kendo.mobile.all.min.css" />

    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2016.1.226/js/angular.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2016.1.226/js/kendo.all.min.js"></script>

    <div class="container">
        <div class="row-fluid">
            <h3>Editar Asignacion Seguimiento</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblid" runat="server" CssClass="col-sm-2 control-label" Text="ID"></asp:Label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control" Font-Size="Small" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblfechai" runat="server" CssClass="col-sm-2 control-label" Text="Fecha Inicio"></asp:Label>
                        <div class="col-sm-3">
                            <input type="text" name="datepicker1" id="datepicker1" class="form-control"
                                pattern="(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}"
                                title="Favor de rellenar fecha valida" style="font-size: small; width: 160px" />
                            <asp:TextBox ID="txtFechaI" runat="server" CssClass="form-control" Font-Size="Small" Width="160px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblestado" runat="server" Text="Estado" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-3">
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkestado" runat="server" required="true" title="Estado es requerido"  />
                                    Activo
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblimei" runat="server" CssClass="col-sm-2 control-label" Text="IMEI"></asp:Label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="cboimei" runat="server" CssClass="form-control" title="IMEI es requerido" required="true" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblplaca" runat="server" CssClass="col-sm-2 control-label" Text="Placa"></asp:Label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="cboplaca" runat="server" title="Placa es requerido" required="true" CssClass="form-control"></asp:DropDownList>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                    <div>
                        <a href="/Vistas/Seguimientos/Index">Volver átras</a>
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
                });
            </script>

        </div>
    </div>
    <script>
        $(document).ready(function () {
            var txtFechaI = document.getElementById("<%= txtFechaI.ClientID %>");
            var fechaInicio = txtFechaI.value;

            $("#datepicker1").data("kendoDatePicker").value(fechaInicio);
            txtFechaI.style.display = 'none';
        });
    </script>
</asp:Content>
