<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WISETRACK.Vistas.Personas.Create" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CambiarTipoPersona(sender, eventArgs) {
            var item = eventArgs.get_item();
            var selec = item.get_value();

            switch (selec) {
                case '1':
                    document.getElementById("divUser").style.display = 'none';
                    document.getElementById("divPass").style.display = 'none';
                    document.getElementById("divConfPass").style.display = 'none';
                    document.getElementById("divRoles").style.display = 'none';
                    document.getElementById("divContacto").style.display = 'none';
                    document.getElementById("divTelfContacto").style.display = 'none';
                    document.getElementById("divLicencia").style.display = 'none';
                    document.getElementById("divFechaVigL").style.display = 'none';
                    document.getElementById("divFechaVigDefL").style.display = 'none';
                    break;

                case '2':
                    document.getElementById("divUser").style.display = 'block';
                    document.getElementById("divPass").style.display = 'block';
                    document.getElementById("divConfPass").style.display = 'block';
                    document.getElementById("divRoles").style.display = 'block';
                    document.getElementById("divContacto").style.display = 'block';
                    document.getElementById("divTelfContacto").style.display = 'block';
                    document.getElementById("divLicencia").style.display = 'none';
                    document.getElementById("divFechaVigL").style.display = 'none';
                    document.getElementById("divFechaVigDefL").style.display = 'none';
                    break;

                case '3':
                    document.getElementById("divUser").style.display = 'none';
                    document.getElementById("divPass").style.display = 'none';
                    document.getElementById("divConfPass").style.display = 'none';
                    document.getElementById("divRoles").style.display = 'none';
                    document.getElementById("divContacto").style.display = 'blok';
                    document.getElementById("divTelfContacto").style.display = 'block';
                    document.getElementById("divLicencia").style.display = 'block';
                    document.getElementById("divFechaVigL").style.display = 'block';
                    document.getElementById("divFechaVigDefL").style.display = 'block';
                    break;

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Registrar Persona</h3>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="table">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lbltipop" runat="server" CssClass="col-sm-2 control-label" Text="<b>Tipo</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="upcbotipo" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox CssClass="dropdown" ID="cboTipop" AllowCustomText="false"
                                        AutoPostBack="false" runat="server" Font-Size="Small"
                                        DropDownCssClass="dropdown" OnClientSelectedIndexChanged="CambiarTipoPersona">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCI" runat="server" Text="<b>CI*</b>" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtCI" runat="server" CssClass="form-control"
                                type="text" title="CI es requerido. Mayor a 7 dígitos" required="true" pattern="[0-9]{7,20}">
                            </asp:TextBox>
                            <span class="input-group-btn">
                                <button class="glyphicon glyphicon-search btn btn-default" type="button" onclick="BuscarPersona()"></button>
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblNombre" runat="server" CssClass="col-sm-2 control-label" Text="<b>Nombre*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                                title="Nombre es requerido. Mayor a 3 caracteres" required="true" pattern="[A-Za-z0-9 ]{3,20}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblApellidop" runat="server" CssClass="col-sm-2 control-label" Text="<b>Apellido Paterno*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtApellidop" runat="server" CssClass="form-control"
                                title="Apellido Paterno es requerido. Mayor a 3 caracteres" required="true" pattern="[A-Za-z0-9 ñÑ]{3,20}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblapellidom" runat="server" CssClass="col-sm-2 control-label" Text="Apellido Materno"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtApellidom" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbldireccion" runat="server" CssClass="col-sm-2 control-label" Text="Direccion"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtdireccion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbltelefono" runat="server" CssClass="col-sm-2 control-label" Text="<b>Telefono*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txttelefono" runat="server" CssClass="form-control"
                                title="Teléfono es requerido. Mayor a 7 caracteres" required="true" pattern="[0-9]{7,20}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" CssClass="col-sm-2 control-label" Text="<b>Email*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                placeHolder="e.g. info@example.com" title="Email es requerido." required="true" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="divUser" class="form-group">
                        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-sm-2 control-label">Nombre de Usuario*</asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" TextMode="SingleLine" />
                        </div>
                    </div>
                    <div id="divPass" class="form-group">
                        <asp:Label runat="server" ID="lblPass" AssociatedControlID="Password" CssClass="col-sm-2 control-label">Contraseña*</asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                        </div>
                    </div>
                    <div id="divConfPass" class="form-group">
                        <asp:Label ID="lblConfPass" runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-sm-2 control-label">Confirmar contraseña*</asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                        </div>
                    </div>
                    <div id="divRoles" class="form-group">
                        <asp:Label runat="server" ID="lblRoles" AssociatedControlID="dpdRoles" CssClass="col-sm-2 control-label"><b>Rol de Usuario</b></asp:Label>
                        <div class="col-sm-3 col-xs-11">
                            <asp:DropDownList runat="server" ID="dpdRoles" CssClass="form-control" />
                        </div>
                    </div>
                    <div id="divContacto" class="form-group">
                        <asp:Label ID="lblContacto" runat="server" CssClass="col-sm-2 control-label" Text="Contacto"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control" placeHolder="Nombre"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divTelfContacto" class="form-group">
                        <asp:Label ID="lbltelefonoc" runat="server" CssClass="col-sm-2 control-label" Text="Teléfono Contacto"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txttelefonoc" runat="server" CssClass="form-control" pattern="[0-9]{7,20}"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divLicencia" class="form-group">
                        <asp:Label ID="lbllicencia" runat="server" CssClass="col-sm-2 control-label" Text="<b>Categoria Licencia*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtlicencia" runat="server" CssClass="form-control" placeHolder="e.g. P" pattern="[A-Z]{1}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="divFechaVigL" class="form-group">
                        <asp:Label ID="lblfechavigl" runat="server" CssClass="col-sm-2 control-label" Text="<b>Fecha de Vigencia Licencia*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtfechavigl" runat="server" CssClass="form-control" Font-Size="Small"
                                Text="01/01/2016" type="text"
                                pattern="(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[012])/[0-9]{4}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="divFechaVigDefL" class="form-group">
                        <asp:Label ID="lblfechavigdefl" runat="server" CssClass="col-sm-2 control-label" Text="<b>Fecha de Vigencia Lic. Defensivo*</b>"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtfechavigdefl" runat="server" CssClass="form-control" Font-Size="Small"
                                Text="01/01/2016" type="text"
                                pattern="(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[012])/[0-9]{4}">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/Personas/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var cboTP = $find("<%= cboTipop.ClientID %>");
            cboTP.get_items().getItem(0).select();
        });

        function BuscarPersona() {
            var ci = document.getElementById('<%=txtCI.ClientID%>').value;
            var httpRequest = new XMLHttpRequest();
            httpRequest.open('POST', '/WebServices/WisetrackServices.asmx/BuscarSiExistePersona', true);
            httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            httpRequest.send("ci=" + ci);
            httpRequest.onreadystatechange = function () {
                if (httpRequest.readyState == 4 && httpRequest.status == 200) {
                    var resultado = httpRequest.responseText;
                    resultado = resultado.substring(76);
                    resultado = resultado.replace('</string>', '');
                    var myobject = JSON.parse(resultado);
                    RellenarCampos(myobject);
                }
            };
        }

        function RellenarCampos(lista) {
            if (lista == '0') {
                alert('La Persona (Carnet Identidad) esta disponible');
            } else {
                Limpiar();
                alert('La Persona esta en uso, Favor de elegir otro Persona (Carnet Identidad)');
            }
        }

        function Limpiar() {
            document.getElementById('<%=txtCI.ClientID %>').value = "";
            document.getElementById('<%=txtApellidop.ClientID %>').value = "";
            document.getElementById('<%=txtApellidom.ClientID %>').value = "";
            document.getElementById('<%=txtdireccion.ClientID %>').value = "";
            document.getElementById('<%=txtNombre.ClientID %>').value = "";
            document.getElementById('<%=txttelefono.ClientID %>').value = "";
            document.getElementById('<%=txtEmail.ClientID %>').value = "";
        }

    </script>
</asp:Content>
