function pageLoad() {
    keyPress();
    ShowEmptyDataHeader();
    //DeleteCert();    
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
function ShowEmptyDataHeader() {   
    var Grid = $('.grdCertificados');
    if (Grid[0].rows.length < 2) {
        $('.div_header').attr("style", "display:block; padding-left: 2%;");
        Grid.attr("style", "display:none");
    }       
}
function DeleteCert() {
    //$(".ibtnAnular").on("click", function () {
        var confirmar;
        var Grid = $('.grdCertificados');
        if (Grid[0].rows.length == 2) {                            
            var columna = $('.grdCertificados tr td');
            var certificado = $(columna[2]).html().toString();
            var estado = $(columna[6]).html().toString();
            if (certificado == $('.hdrealCertificado').val()) {
                if (estado == $('.hdEstado').val() || $('.hdEstado').val() == 'Anulado') {
                    alert('El certificado N°: ' + certificado + ' ya se encuentra anulado.');
                    confirmar = false;
                }
                else {
                    var mensaje = prompt('Está seguro de eliminar el certificado N°: ' + certificado, 'Se eliminó por mal ingreso de datos.');
                    if (mensaje != null) {
                        if (mensaje.trim().length > 0) {
                            $('.hdNroCertificado').val(certificado);
                            $('.hdMotivo').val(mensaje);
                            $('.hdEstado').val(estado);
                        }
                        else {
                            alert('Ingrese motivo.');
                            confirmar = false;
                        }
                    }
                    else {
                        confirmar = false;
                    }
                    
                }
            }
            else {
                alert('El certificado no coincide con el de la búsqueda.');
                confirmar = false;
            }                        
        }
        else if (Grid[0].rows.length>2) {
            alert('Hay más de un registro con el mismo número de certificado.');
            confirmar = false;
        }
        else {
            alert('No hay Registros para Anular.');
            confirmar = false;
        }        
        return confirmar;
    //});
}
function Mensaje() {
    alert('Mensaje');
}