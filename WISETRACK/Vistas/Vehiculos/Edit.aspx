<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.Vehiculos.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>EDITAR VEHICULO</h3>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label id="lblplaca" runat="server" class="col-sm-2 control-label" style="font-weight: normal">Nro Placa</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtplaca" Enabled="false" CssClass="form-control" runat="server" placeHolder="e.g. 2461LKX" type="text"
                                title="Placa es requerido. Longitud 6-7 digitos" required="true" pattern="[A-Za-z0-9]{6,9}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblchasis" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Nro Chasis</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtchasis" CssClass="form-control" runat="server" placeHolder="e.g. 105231122015" type="text" title="Chasis es requerido. Longitud 6-15 digitos" required="true" pattern="[A-Za-z0-9]{5,15}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblpatente" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Patente</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtpatente" CssClass="form-control" runat="server" placeHolder="e.g. Nombre de Patente" type="text" 
                                title="Patente tendra como maximo 6-30 caracteres" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblmotor" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Nro Motor</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtmotor" CssClass="form-control" runat="server" placeHolder="e.g. 4000" type="text" title="Motor es requerido. Longitud 3-10 digitos" required="true" pattern="[0-9]{3,10}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblmodelo" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Modelo</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtmodelo" CssClass="form-control" runat="server" placeHolder="e.g. XTERRA" type="text" title="Modelo es requerido. Longitud 3-10 digitos" required="true" pattern="[A-Za-z0-9]{3,30}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lbltipov" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Tipo Vehiculo</label>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList ID="cbotipov" CssClass="form-control" runat="server" placeholder="Tipo Vehiculo" required="true" title="Tipo Vehiculo es requerido"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblmarca" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Marca</label>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList ID="cbomarca" CssClass="form-control" runat="server" placeholder="Marca" required="true" title="Marca es requerido"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblfoto" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Foto</label>
                        <div class="col-sm-3 col-xs-11">
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon3">@</span>
                                <asp:FileUpload ID="fileupvehiculo" CssClass="form-control" text="Buscar" runat="server" aria-describedby="basic-addon3" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label id="lblanio" runat="server" class="col-sm-2 control-label" style="font-weight:normal">Año</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtanio" CssClass="form-control" runat="server" placeholder="Año" required="false" title="Año es opcional" pattern="[0-9]{3,4}"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-default" Text="Editar" OnClick="btnEditar_Click"/>
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/Vehiculos/Index">Volver átras</a>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
