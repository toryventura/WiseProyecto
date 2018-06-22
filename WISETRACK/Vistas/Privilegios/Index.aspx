<%@ Page Title="Privilegios" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Privilegios.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .pagination table {  
            margin: 5px 0;  
        }  
  
        .pagination td {  
            border-width: 0;  
            padding: 0 6px;  
            border-left: solid 1px #666;  
            font-weight: bold;  
            color: #666;  
            line-height: 12px;  
        }  
  
        .pagination a {  
            color: #666;  
            text-decoration: none;  
        }  
  
        .pagination a:hover {  
            color: #000;  
            text-decoration: underline;  
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group" >
                <h2><b>Privilegios</b></h2>
                <p><a runat="server" href="~/Vistas/Privilegios/Create">Crear Nuevo..</a></p>
            </div>
            <div class="table-responsive" style="width: 100%; height: 394px; overflow-y: auto">
                <asp:GridView ID="gdvPrivilegios" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvPrivilegios_RowCommand"
                    CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="CodPrivilegio" 
                    EmptyDataText="Inicie Sesion para utilizar el Sistema" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="CodPrivilegio" HeaderText="Código" ReadOnly="true" SortExpression="CodPrivilegio" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="true" SortExpression="Descripcion" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                        <asp:BoundField DataField="DirPagina" HeaderText="Direccion de Página" ReadOnly="true" SortExpression="DirPagina" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                        <asp:BoundField DataField="UsuaReg" HeaderText="Registrado por" ReadOnly="true" SortExpression="UsuaReg" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                        <asp:BoundField DataField="FechaReg" HeaderText="Fecha de Registro" ReadOnly="true" SortExpression="FechaReg" 
                            HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkEditar" runat="server" CssClass="alert-link" CommandName="Editar" Text="Editar" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkEliminar" runat="server" CssClass="alert-link" CommandName="Eliminar" Text="Eliminar" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
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
