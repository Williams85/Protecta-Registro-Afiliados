function nobackbutton() {
    window.location.hash = "Protecta";
    window.location.hash = "Again-Protecta" //chrome
    window.onhashchange = function () { window.location.hash = "Protecta"; }
}