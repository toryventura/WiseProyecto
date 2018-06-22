<%@ Page Title="Contraseña olvidada" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="WISETRACK.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>

    <div class="row">
        <div class="col-md-8">
            <%--<asp:PlaceHolder ID="loginForm" runat="server">--%>
            <div id="loginForm">
                <div class="form-horizontal">
                    <h4>¿Olvidó su contraseña?</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Correo electrónico</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio."
                                ValidationGroup="EmailValidation" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <input type="button" class="btn btn-default" name="name" value="Vínculo de correo electrónico" onclick="setEnviar();" />
                            <%--<asp:Button runat="server" OnClientClick="setEnviar();" Text="Vínculo de correo electrónico" CssClass="btn btn-default" />--%>
                        </div>
                    </div>
                </div>
            </div>
            <%--</asp:PlaceHolder>--%>
            <%--<asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">--%>
            <div id="DisplayEmail" style="display: none;">
                <p class="text-info">
                    Compruebe el correo electrónico para restablecer la contraseña.
                </p>
            </div>

            <%--</asp:PlaceHolder>--%>
        </div>
    </div>
    <script>
        function isValid() {
            Page_ClientValidate("EmailValidation");
            if (Page_IsValid) {
                return true;
            } else
                return false;
        }
        function setEnviar() {
            if (isValid()) {
                BloquearPantalla();
                var email = document.getElementById('<%=Email.ClientID%>');
                var va = email.value;
                var action = "{'email': '" + va + "'}";
                $.ajax({
                    url: "/Account/Forgot.aspx/setEnviar",
                    data: action,
                    type: 'POST',
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (data) {
                    var datas = JSON.parse(data.d);
                    <%--document.getElementById('<%=loginForm.ClientID%>').style.display = 'none';
                    document.getElementById('<%=DisplayEmail.ClientID%>').style.display = 'block';--%>
                    if (datas[0].Key=="OK") {
                        $('#loginForm').hide();
                        $('#DisplayEmail').show();
                        alert(datas[0].Value);
                    } else {
                        error(datas[0].Value);
                    }
                
                DesbloquearPantalla();
                //DesbloquearPantalla();
            }).fail(function (jqXHR, textStatus) {
                DesbloquearPantalla();
                error("Administrador Error 500 -> " + textStatus);
                //DesbloquearPantalla();
            }).always(function (jqXHR, textStatus) {
                if (textStatus != "success") {
                    error("Administrador -> " + jqXHR.statusText);
                } else {
                    //gridOptions.api.setRowData(myData);
                }
                DesbloquearPantalla();
            });
        }
    }
    </script>
</asp:Content>
