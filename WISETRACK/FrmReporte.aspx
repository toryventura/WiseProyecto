<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="FrmReporte.aspx.cs" Inherits="WISETRACK.FrmReporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="jumbotron">
            <p class="text-danger">Responsive GridView in ASP.NET </p>
            <span class="text-info">Desktop Tablet Phone Different layout </span>
        </div>
        <div class="row">
            <div class="col-lg-12 ">
                <div class="table-responsive">
                    <asp:GridView ID="grdCustomer" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="NIT" EmptyDataText="There are no data records to display.">
                        <Columns>
                            <asp:BoundField DataField="NIT" HeaderText="NIT" ReadOnly="True" SortExpression="NIT" />
                            <asp:BoundField DataField="CI" HeaderText="CI" SortExpression="CI" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="UsuaReg" HeaderText="UsuaReg" SortExpression="UsuaReg" ItemStyle-CssClass="visible-xs" HeaderStyle-CssClass="visible-xs" />
                            <asp:BoundField DataField="FechaReg" HeaderText="FechaReg" SortExpression="FechaReg" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" />
                            <asp:BoundField DataField="UsuaModif" HeaderText="UsuaModif" SortExpression="UsuaModif" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />
                            <asp:BoundField DataField="FechaModif" HeaderText="FechaModif" SortExpression="FechaModif" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />
                            
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
