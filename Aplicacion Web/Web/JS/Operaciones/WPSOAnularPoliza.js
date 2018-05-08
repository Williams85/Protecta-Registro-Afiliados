function pageLoad() {
    keyPress();
      
}
function keyPress() {
    $('.texboxcss').live("keypress", function (e) {
        if (e.keyCode == 13) {
            var certificado = $(this).val();
            if (certificado.length == 8) {
                $('.ibtnBuscar').click();
            }
            return false;
        }
    });
}

function validar() {
    var valor = true;
    var txt = $(".ddlestadocss option:selected").text();
    var val = $(".ddlestadocss option:selected").val();
    if (val==0) {
        alert('Seleccione una opción de anulación.');
        $('.lbletiquetasupError').text('Estado (*)');
        $('.lbletiquetasupError').addClass('EtiquetaError');
        valor=false;
    }
    
    return valor;
}