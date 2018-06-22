<%@ Page Title="Roles" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Roles.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group" >
                <h2><b>Roles</b></h2>
                <p><a runat="server" href="~/Vistas/Roles/Create">Crear Nuevo..</a></p>
            </div>
            <div class="table-responsive" style="width: 100%; height: 394px; overflow-y: auto">
                <asp:GridView ID="gdvRoles" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvPrivilegios_RowCommand" 
                    CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Name" 
                    EmptyDataText="Inicie Sesion para utilizar el Sistema" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Rol" ReadOnly="true" SortExpression="Name" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandName="VerPrivilegios" CssClass="alert-link">Ver Privilegios</asp:LinkButton>
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
            <div class="form-group">

            </div>
        </div>
    </div>
</asp:Content>
