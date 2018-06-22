<%@ Page Title="Empresas" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Panel1.aspx.cs" Inherits="WISETRACK.Vistas.Empresas.Panel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group">
                <h2><b>Empresas</b></h2>
                <p><a runat="server" href="~/Vistas/Empresas/Create">Crear Nueva..</a></p>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gdvEmpresa" runat="server" CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" AutoGenerateColumns="False" OnRowCommand="gdvEmpresa_RowCommand"
                    DataKeyNames="NIT" EmptyDataText="Inicie Sesion para utilizar el Sistema" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="NIT" HeaderText="NIT" />
                        <asp:BoundField DataField="RazonSocial" HeaderText="Nombre" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" 
                            ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm" />
                        <asp:BoundField DataField="email" HeaderText="Email" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" 
                            ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm" />
                        <asp:BoundField DataField="FechaReg" HeaderText="Fecha" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" 
                            ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbIngresar" runat="server" CommandName="Ingresar_Salir" CssClass="alert-link" Text="INGRESAR" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
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
            </div>
        </div>
    </div>
</asp:Content>
