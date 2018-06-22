<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WISETRACK.Vistas.Personas.Edit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //function CambiarTipoPersona(sender, eventArgs) {
        //    var item = eventArgs.get_item();
        //    var selec = item.get_value();

        //    switch (selec) {
        //        case '1':
        //            var divuser1 = document.getElementById("divUser").style;
        //            //if (document.getElementById("divUser").style.visibility != null) {
        //            //    document.getElementById("divUser").style.visibility = "hidden";
        //            //}
        //            //document.getElementById("divUser").style.display = 'none';
        //            //document.getElementById("divPass").style.display = 'none';
        //            //document.getElementById("divConfPass").style.display = 'none';
        //            //document.getElementById("divRoles").style.display = 'none';
        //            //document.getElementById("divContacto").style.display = 'none';
        //            //document.getElementById("divTelfContacto").style.display = 'none';
        //            //document.getElementById("divLicencia").style.display = 'none';
        //            break;

        //        case '2':
        //            alert("hola 2");
        //            //document.getElementById("divUser").style.display = 'block';
        //            //document.getElementById("divPass").style.display = 'block';
        //            //document.getElementById("divConfPass").style.display = 'block';
        //            //document.getElementById("divRoles").style.display = 'block';
        //            //document.getElementById("divContacto").style.display = 'block';
        //            //document.getElementById("divTelfContacto").style.display = 'block';
        //            //document.getElementById("divLicencia").style.display = 'none';
        //            break;

        //        case '3':
        //            alert("hola 3");
        //            //document.getElementById("divUser").style.display = 'none';
        //            //document.getElementById("divPass").style.display = 'none';
        //            //document.getElementById("divConfPass").style.display = 'none';
        //            //document.getElementById("divRoles").style.display = 'none';
        //            //document.getElementById("divContacto").style.display = 'blok';
        //            //document.getElementById("divTelfContacto").style.display = 'block';
        //            //document.getElementById("divLicencia").style.display = 'block';
        //            break;

        //    }
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row-fluid">
            <h3>Editar Persona</h3>
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
                                        AutoPostBack="true" runat="server" Font-Size="Small"
                                        DropDownCssClass="dropdown" OnSelectedIndexChanged="cboTipop_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%--OnClientSelectedIndexChanged="CambiarTipoPersona" --%>
                    <div class="form-group">
                        <asp:Label ID="lblCI" runat="server" Text="<b>CI</b>" CssClass="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtCI" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                title="Apellido Paterno es requerido. Mayor a 3 caracteres" required="true" pattern="[A-Za-z0-9 ]{3,20}">
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
                    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--<div id="divbody" runat="server">--%>
                            <%--usuario--%>
                            <div id="divUser" runat="server" class="form-group">
                                <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-sm-2 control-label">Nombre de Usuario*</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="UserName" CssClass="form-control" TextMode="SingleLine" Enabled="false" />
                                    <%--<asp:RequiredFieldValidator ID="reqUser" runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio." />--%>
                                </div>
                            </div>
                            <%--usuario--%>
                            <div id="divPass" runat="server" class="form-group">
                                <asp:Label runat="server" ID="lblPass" AssociatedControlID="Password" CssClass="col-sm-2 control-label">Contraseña*</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" Enabled="false" ID="Password" TextMode="Password" CssClass="form-control" />
                                    <%--<asp:RequiredFieldValidator ID="reqPass" runat="server" ControlToValidate="Password"
                                CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />--%>
                                </div>
                            </div>
                            <%--usuario--%>
                            <div id="divConfPass" runat="server" class="form-group">
                                <asp:Label ID="lblConfPass" runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-sm-2 control-label">Confirmar contraseña*</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="ConfirmPassword" Enabled="false"  TextMode="Password" CssClass="form-control" />
                                    <%--<asp:RequiredFieldValidator ID="reqConfPass" runat="server" ControlToValidate="ConfirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="El campo de confirmación de contraseña es obligatorio." />
                            <asp:CompareValidator ID="cmpConfPass" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="La contraseña y la contraseña de confirmación no coinciden." />--%>
                                </div>
                            </div>
                            <%--usuario--%>
                            <div id="divRoles" runat="server" class="form-group">
                                <asp:Label runat="server" ID="lblRoles" AssociatedControlID="dpdRoles" CssClass="col-sm-2 control-label">Rol de Usuario*</asp:Label>
                                <div class="col-sm-3 col-xs-11">
                                    <asp:DropDownList runat="server" ID="dpdRoles" CssClass="form-control" Enabled="false" />
                                </div>
                            </div>
                            <%--usuario--%>
                            <div id="divContacto" runat="server" class="form-group">
                                <asp:Label ID="lblContacto" runat="server" CssClass="col-sm-2 control-label" Text="Contacto"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control" placeHolder="Nombre"></asp:TextBox>
                                </div>
                            </div>
                            <%--usuario--%>
                            <div id="divTelfContacto" runat="server" class="form-group">
                                <asp:Label ID="lbltelefonoc" runat="server" CssClass="col-sm-2 control-label" Text="Teléfono Contacto"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txttelefonoc" runat="server" CssClass="form-control" pattern="[0-9]{7,20}"></asp:TextBox>
                                </div>
                            </div>
                            <%--conductor--%>
                            <div id="divLicencia" runat="server" class="form-group">
                                <asp:Label ID="lbllicencia" runat="server" CssClass="col-sm-2 control-label" Text="<b>Categoria Licencia*</b>"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtlicencia" runat="server" CssClass="form-control" placeHolder="e.g. P" pattern="[A-Z]{1}"></asp:TextBox>
                                </div>
                            </div>
                            <%--conductor--%>
                            <div id="divFechaVigL" runat="server" class="form-group">
                                <asp:Label ID="lblfechavigl" runat="server" CssClass="col-sm-2 control-label" Text="<b>Fecha de Vigencia Licencia*</b>"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtfechavigl" runat="server" CssClass="form-control" Font-Size="Small"
                                        Text="01/01/2016" type="text"
                                        pattern="(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[012])/[0-9]{4}">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <%--conductor--%>
                            <div id="divFechaVigDefL" runat="server" class="form-group">
                                <asp:Label ID="lblfechavigdefl" runat="server" CssClass="col-sm-2 control-label" Text="<b>Fecha de Vigencia Lic. Defensivo*</b>"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtfechavigdefl" runat="server" CssClass="form-control" Font-Size="Small"
                                        Text="01/01/2016" type="text"
                                        pattern="(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[012])/[0-9]{4}">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <%--</div>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnModificar_Click" />
                        </div>
                    </div>
                </div>
                <div>
                    <a href="/Vistas/Personas/Index">Volver átras</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
