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
var namePattern = "^[a-z A-Z]{4,30}$";
var emailPattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}$";

function checkInput(idInput, pattern) {
    return $(idInput).val().match(pattern) ? true : false;
}
//comprobacion de chektextArea
function checkTextarea(idText) {
    return $(idText).val().length > 12 ? true : false;
}
function checkTextareaLen(idText, len) {
    return $(idText).val().length > len ? true : false;
}
//comprobacin de radio
function checkRadioBox(nameRadioBox) {
    return $(nameRadioBox).is(":checked") ? true : false;
}
//comprobacion una selection 
function checkSelect(idSelect) {
    return $(idSelect).val() ? true : false;
}
//Activación y desactivación del Submit
function enableSubmit(idForm) {
    $(idForm).removeAttr("disabled");
}

function disableSubmit(idForm) {
    $(idForm).attr("disabled", true);
}
//validacion que solo permite, escribir mumeros con decimales
function jsDecimals(e) {

    var evt = (e) ? e : window.event;
    var key = (evt.keyCode) ? evt.keyCode : evt.which;
    if (key != null) {
        key = parseInt(key, 10);
        if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
            //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
            if (!jsIsUserFriendlyChar(key, "Decimals")) {
                return false;
            }
        }
        else {
            if (evt.shiftKey) {
                return false;
            }
        }
    }
    return true;
}
// Función para las teclas especiales
//------------------------------------------
function jsIsUserFriendlyChar(val, step) {
    // Backspace, Tab, Enter, Insert, y Delete
    if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
        return true;
    }
    // Ctrl, Alt, CapsLock, Home, End, y flechas
    if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
        return true;
    }
    if (step == "Decimals") {
        if (val == 190 || val == 110) {  //Check dot key code should be allowed
            return true;
        }
    }
    // The rest
    return false;
}
