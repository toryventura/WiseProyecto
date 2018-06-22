<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="WISETRACK.Contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h3>Contactenos.</h3>
            <address>
                <b>Wisetrack GPS</b><br />
                Plaza Comercial Urubo #112<br />
                <abbr title="Teléfono">T:</abbr>
                (591-3) 388-8400 
        <br />
                <abbr title="Celular">C:</abbr>
                (591-3) 388-8244
            </address>

            <address>
                <strong>Suporte:</strong>   <a href="mailto:helpdesk@wisetrack.bo">helpdesk@wisetrack.bo</a><br />
                <strong>Marketing:</strong> <a href="mailto:info@wisetrack.bo">info@wisetrack.bo</a>
            </address>
            <br />
            <p><b><a style="font-size: 28px" href="/RptTramas">Ver Tramas en Tiempo Real</a></b></p>
            <br />
        </div>
    </div>

</asp:Content>
