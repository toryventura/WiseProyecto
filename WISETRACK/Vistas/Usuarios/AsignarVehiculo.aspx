<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="AsignarVehiculo.aspx.cs" Inherits="WISETRACK.Vistas.Usuarios.AsignarVehiculo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="span6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Asignacion de Vehiculos a usuarios
                    </div>
                    <p class="alert-info">
                        <asp:Literal runat="server" ID="InfoMessage" />
                    </p>
                    <div class="panel-body" id="panelbody1" runat="server">
                        <div class="table">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="lblpersona" runat="server" Text="<b>Persona</b>" CssClass="col-sm-2 control-label"></asp:Label>
                                    <div class="col-sm-5">
                                        <asp:UpdatePanel ID="upcbopersona" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cbopersona" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbopersona_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblvehiculo" runat="server" Text="<b>Moviles</b>" CssClass="col-sm-2 control-label"></asp:Label>
                                    <div class="col-lg-9">
                                        <asp:UpdatePanel ID="uptodovehiculo" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive" style="width: 75%; height: 300px; overflow-y: auto">
                                                    <asp:GridView ID="gdvvehiculo" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                                        CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="NroPlaca"
                                                        EmptyDataText="No hay vehiculos sin asignar"
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
                                                            <asp:BoundField DataField="NroPlaca" HeaderText="Placa" ReadOnly="true" SortExpression="Placa"
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <asp:BoundField DataField="Patente" HeaderText="Patente" ReadOnly="true" SortExpression="Patente"
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />
                                                            <%--<asp:BoundField DataField="idempresa" HeaderText="Nit" ReadOnly="true" SortExpression="Nit"
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group"></div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <asp:Button runat="server" ID="btnGuardar" Visible="true" Text="Guardar" CssClass="btn btn-primary btn-sm" OnClick="btnGuardar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="span6">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        Vehiculos Asignados
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="<b>Moviles</b>" CssClass="col-sm-2 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:UpdatePanel ID="upvehiculoasig" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive" style="width: 100%; height: 400px; overflow-y: auto">
                                            <asp:GridView ID="gdvvehiculoasignado" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                                CssClass="table table-striped table-bordered table-hover" Font-Size="Smaller" DataKeyNames="NroPlaca"
                                                EmptyDataText="No hay vehiculos asignados"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="SelecAllPriv" runat="server" Enabled="false" Font-Size="Smaller" onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SelecPriv" runat="server" Enabled="false" Font-Size="Smaller" Visible="true" onclick="UncheckAll(this);" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="visible-lg visible-md visible-xs" />
                                                        <HeaderStyle CssClass="visible-lg visible-md visible-xs" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="ID"
                                                        HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" />
                                                    <asp:BoundField DataField="ci" HeaderText="CI" ReadOnly="true" SortExpression="CI"
                                                        HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" />
                                                    <asp:BoundField DataField="usuario" HeaderText="Usuario" ReadOnly="true" SortExpression="Usuario"
                                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm" />
                                                    <asp:BoundField DataField="nroplaca" HeaderText="Placa" ReadOnly="true" SortExpression="Placa"
                                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm" />
                                                    <asp:BoundField DataField="nit" HeaderText="Nit" ReadOnly="true" SortExpression="Nit"
                                                        HeaderStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" ItemStyle-CssClass="hidden-lg hidden-md hidden-xs hidden-sm" />
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" ReadOnly="true" SortExpression="Empresa"
                                                        HeaderStyle-CssClass="visible-lg visible-md visible-xs visible-sm" ItemStyle-CssClass="visible-lg visible-md visible-xs visible-sm" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Button runat="server" ID="btnEditar" Visible="true" Text="Editar" CssClass="btn btn-primary btn-sm" OnClick="btnEditar_Click" />
                            </div>
                            <div class="col-sm-2">
                                <input type="button" runat="server" class="btn btn-primary btn-sm" value="Quitar" id="lnkquitar" visible="false" style="font-size: smaller" data-toggle="modal" data-target="#myModal" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Button runat="server" ID="btnCancel" Visible="false" Text="Cancelar" CssClass="btn btn-primary btn-sm" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Procesar</h4>
                </div>
                <div class="modal-body">
                    ¿Esta seguro que desear eliminar la Asignacion Usuario - Vehiculo?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnAceptar" Text="Procesar" CssClass="btn btn-primary btn-sm" OnClick="btnAceptar_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function CheckAll(Checkbox) {
            var gdvPrivilegios = document.getElementById("<%= gdvvehiculo.ClientID %>");
            var count = gdvPrivilegios.rows.length;

            for (i = 1; i <= count; i++) {
                var gdvRow = gdvPrivilegios.rows[i];
                var ckbPrivilegio = gdvRow.cells[0].getElementsByTagName("INPUT")[0];
                ckbPrivilegio.checked = Checkbox.checked;
            }
        }

        function UncheckAll(Checkbox) {
            var gdvPrivilegios = document.getElementById("<%= gdvvehiculo.ClientID %>");
            var ckbAll = gdvPrivilegios.rows[0].cells[0].getElementsByTagName("INPUT")[0];

            if (!Checkbox.checked)
                ckbAll.checked = false;
        }
    </script>
</asp:Content>
