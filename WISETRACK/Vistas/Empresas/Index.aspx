<%@ Page Title="" Language="C#" MasterPageFile="~/SitePrincipal.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WISETRACK.Vistas.Empresas.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container marketing">
        <!-- Three columns of text below the carousel -->
        <div class="row">
            <div class="col-lg-12" style="padding-top:140px; padding-bottom:50px;" >
                <div class="col-lg-3">
                    <img class="img-circle" src="../../Imagenes/seguimientomenu.jpg" alt="Generic placeholder image" width="120" height="120" />
                    <h3>Seguimiento</h3>
                    <%--<p>Donec sed odio dui. Etiam porta sem malesuada magna mollis euismod. Nullam id dolor id nibh ultricies vehicula ut id elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Praesent commodo cursus magna.</p>--%>
                    <p><a class="btn btn-default" href="../../FrmSeguimiento" role="button">Ver Detalle &raquo;</a></p>
                </div>
                <!-- /.col-lg-4 -->
                <div class="col-lg-3">
                    <img class="img-circle" src="../../Imagenes/auditoriamenu.jpg" alt="Generic placeholder image" width="120" height="120" />
                    <%--<img class="img-circle" src="data:image/gif;base64,R0lGODlhAQABAIAAAHd3dwAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" alt="Generic placeholder image" width="140" height="140">--%>
                    <h3>Auditoria</h3>
                    <%--<p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Cras mattis consectetur purus sit amet fermentum. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh.</p>--%>
                    <p><a class="btn btn-default" href="../../FrmAuditoria" role="button">Ver Detalle &raquo;</a></p>
                </div>
                <!-- /.col-lg-4 -->
                <div class="col-lg-3">
                    <img class="img-circle" src="../../Imagenes/reporte.png" alt="Generic placeholder image" width="120" height="120" />
                    <h3>Reporte de Alertas</h3>
                    <%--<p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>--%>
                    <p><a class="btn btn-default" href="../../RptAlarmas" role="button">Ver Detalle &raquo;</a></p>
                </div>
                <!-- /.col-lg-4 -->
            </div>

        </div>
        <!-- /.row -->

    </div>
   
</asp:Content>
