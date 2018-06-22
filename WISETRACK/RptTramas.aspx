<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="RptTramas.aspx.cs" Inherits="WISETRACK.RptTramas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group" >
                <h2><b>WISETRACK.dbo.Prueba</b></h2>
            </div>
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="udpPrincipal">
                    <ContentTemplate>
                        <%--<asp:Label runat="server" ID="lblPrincipal" Text="Tiempo en Ejecución: 00:00:00" Font-Size="18px"></asp:Label>
                        <br />--%>
                        <asp:ListBox ID="lstMensajes" runat="server" CssClass="form-control" Font-Size="11px" Height="444px"></asp:ListBox>
                        <%--<asp:GridView ID="gdvPrueba" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="Nro" 
                            EmptyDataText="Inicie Sesion para utilizar el Sistema" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <Columns>
                                <asp:BoundField DataField="Nro" HeaderText="Nro" ReadOnly="true" SortExpression="Nro" ItemStyle-Font-Size="9px"
                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                                <asp:BoundField DataField="Mensaje" HeaderText="Mensaje" ReadOnly="true" SortExpression="Mensaje" ItemStyle-Font-Size="9px"
                                    HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs"/>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="true" SortExpression="FechaReg" ItemStyle-Font-Size="9px"
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
                        </asp:GridView>--%>
                        <asp:Timer runat="server" ID="tmrPrincipal" Interval="2500" OnTick="tmrPrincipal_Tick"></asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
