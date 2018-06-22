<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.Geocercas.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #map {
            width: auto;
            height: 480px;
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="form-group">
                <h3><b>Geocercas</b></h3>
                <%--<a href="#" class="alert-link" data-toggle="modal" font-size="Small" data-target="#myModal">Visualizar todas las Geocercas</a>--%>
            </div>
            <div class="form-inline">
                <label>
                    Nombre:
                    <asp:TextBox ID="txtsearchgeocerca" runat="server" Hint="Nombre" CssClass="form-control small" Font-Size="Small"></asp:TextBox>
                </label>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click" />
            </div>
            <div class="form-group"></div>

            <div class="table">
                <div class="table-responsive">
                    <asp:GridView ID="gdvGeocerca" runat="server" CssClass="table table-striped table-bordered table-hover"
                        Font-Size="Smaller" AutoGenerateColumns="False" DataKeyNames="CodigoGEO" EmptyDataText="Sin Datos"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        OnRowCommand="gdvGeocerca_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="CodigoGEO" HeaderText="ID" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                            <asp:BoundField DataField="Zona" HeaderText="Zona" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                            <asp:BoundField DataField="NIT" HeaderText="NIT" HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CssClass="alert-link" Text="Eliminar" CommandName="Eliminar" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" />
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

            <!-- Modal -->
            <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="myModalLabel">Visualizacion de Geocercas </h4>
                        </div>
                        <div class="modal-body">
                            <div id="map"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="../../Content/js/ControlZona.js">
    </script>

    <<script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCmLvI0sO799B4cTvOJ0cpwS7Voll1CUDY&callback=initMap">
    </script>

</asp:Content>
