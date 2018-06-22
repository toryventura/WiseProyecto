<%@ Page Title="Agregar Privilegio" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="AddPrivilegio.aspx.cs" Inherits="WISETRACK.Roles.AddPrivilegio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CheckAll(Checkbox) {
            var gdvPrivilegios = document.getElementById("<%= gdvPrivilegios.ClientID %>");
            var count = gdvPrivilegios.rows.length;

            for (i = 1; i <= count; i++) {
                var gdvRow = gdvPrivilegios.rows[i];
                var ckbPrivilegio = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                ckbPrivilegio.checked = Checkbox.checked;
            }
        }

        function UncheckAll(Checkbox) {
            var gdvPrivilegios = document.getElementById("<%= gdvPrivilegios.ClientID %>");
            var ckbAll = gdvPrivilegios.rows[0].cells[0].getElementsByTagName("INPUT")[0];

            if (!Checkbox.checked)
                ckbAll.checked = false;
        }
    </script>
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
                <h2><b>Agregar Privilegio (<% =Request.QueryString["rol"] %>)</b></h2>
                <p class="alert-info">
                    <asp:Literal runat="server" ID="InfoMessage" />
                </p>
            </div>
            <div class="table-responsive" style="width: 100%; height: 385px; overflow-y: auto">
                <asp:GridView ID="gdvPrivilegios" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                    CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="CodPrivilegio" 
                    EmptyDataText="No hay Privilegios para agregar" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="SelecAllPriv" runat="server" Font-Size="Smaller" onclick="CheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="SelecPriv" runat="server" Font-Size="Smaller" Visible="true" onclick="UncheckAll(this);" />
                            </ItemTemplate>
                            <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                            <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                        </asp:TemplateField>
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
            <div class="form-group"></div>
            <div class="form-group">
                <asp:Button runat="server" ID="btnGuardar" Visible="false" OnClick="Guardar_Click" Text="Guardar" CssClass="btn btn-primary btn-sm"/>
            </div>
        </div>
    </div>
</asp:Content>
