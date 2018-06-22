function DesbloquearPantalla() {
    $.unblockUI();
}
function Redireccionar(url) {
    window.location = url;
}
function BloquearPantalla() {
    $.blockUI(
    {
        message: '<h2><img src="../../Content/img/cargando5.gif"/></h2>',
        css: { border: 'none', 'border-radius': '70px', padding: "0.3em", background: "none" }
    });

}
function MostrarMensaje(msj) {
    $("#Lbl_MensajeError").html(msj);
    DesbloquearPantalla();
}
function MostrarMensaje1(msj) {
    $("#Lbl_MensajeError1").html(msj);
    DesbloquearPantalla();
}