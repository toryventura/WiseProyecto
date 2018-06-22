<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmPruebita.aspx.cs" Inherits="WISETRACK.FrmPruebita" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row-fluid">
            <input id="txtfechaini" name="txtfechaini" />
            <input id="datepicker2" name="datepicker2" />
        </div>
    </div>

    <asp:Button ID="btnver" runat="server" OnClick="btnver_Click" />

    <script>
        $(function () {
            

                $("#datepicker1").kendoDatePicker({
                    format: "dd/MM/yyyy"
                });

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
</asp:Content>
