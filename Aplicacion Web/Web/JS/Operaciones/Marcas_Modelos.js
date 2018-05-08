$(document).ready(function () {
    
});
function pageLoad() {
    //keyPress();
    BuscarGridMarca();
    BuscarGridModelo();
    KeytxtMarca();
    KeytxtModelo();
    CabeceraMarca();
    CabeceraModelo();
    alternar();
    seleccionarMarca();
    seleccionarModelo();
    ShowEmptyDataHeader();
}

function BuscarGridMarca() {
    $('.grdMarca tr:has(td)').each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
                    .hide().text(t).appendTo(this);
    });
};
function BuscarGridModelo() {
    $('.grdModelo tr:has(td)').each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
                    .hide().text(t).appendTo(this);
    });
};
function KeytxtMarca() {
    $('.txtMarca').keypress(function (event) {
        if (event.keyCode === 10 || event.keyCode === 13)
            event.preventDefault();
    });
    $('.txtMarca').keyup(function (e) {        
        var s = $(this).val().toLowerCase().split(" ");        
        $('.grdMarca tr:hidden').show();
        $.each(s, function () {
            $(".grdMarca tr:visible .indexColumn:not(:contains('"
               + this + "'))").parent().hide();
        });
        if (s[0] == '') {            
            $('.btnCargarMarca').click();
        }
    });
};
function KeytxtModelo() {
    $('.txtModelo').keypress(function (event) {
        if (event.keyCode === 10 || event.keyCode === 13)
            event.preventDefault();
    });
    $('.txtModelo').keyup(function () {
        var s = $(this).val().toLowerCase().split(" ");
        $('.grdModelo tr:hidden').show();
        $.each(s, function () {
            $(".grdModelo tr:visible .indexColumn:not(:contains('"
               + this + "'))").parent().hide();
        });
        if (s[0] == '') {
            $('.btnCargarModelo').click();
        }
    });
};

function alternar() {
    $('.PrettyGridView2').each(function () {
        $('tr:odd', this).addClass('odd').removeClass('even');
        $('tr:even', this).addClass('even').removeClass('odd');
    });
    $('.PrettyGridView3').each(function () {
        $('tr:odd', this).addClass('odd').removeClass('even');
        $('tr:even', this).addClass('even').removeClass('odd');
    });
};
function seleccionarMarca() {
    $('.PrettyGridView2').delegate('tr', 'click', function () {
        var clase = $(this).attr("class");
        var columna = $(this).children();
        var id = $(columna[0]).html().toString();
        var marca = $(columna[1]).html().replace(/&nbsp;/g, '').toString();
        
        $(".PrettyGridView2 tbody tr").each(function () { $(this).removeClass('seleccionar'); });
        alternar();
        $(this).addClass('seleccionar').removeClass(clase);
        if (id != 'ID') {
            $('.txtidMarca').val(id);//$('.lblidMarca').text(id);//txtidMarca
            $('.txtMarca').val(marca.replace(/&amp;/g, '&'));
            $('.hdidMarca').val(id);

            $('.btnCargarModelo').click();
            //BuscarGridModeloMarca(marca);
            //KeytxtModeloMarca(marca);
        }
        
    });
};
function seleccionarModelo() {
    $('.PrettyGridView3').delegate('tr', 'click', function () {
        var clase = $(this).attr("class");
        var columna = $(this).children();
        var id = $(columna[0]).html().toString();
        var marca = $(columna[1]).html().toString();
        var idmodel = $(columna[2]).html().toString();
        var modelo = $(columna[3]).html().replace(/&nbsp;/g, '').toString();
        if (id != 'ID') {
            $('.txtMidMarca').val(id);//$('.lblMidMarca').text(id);
            $('.txtMmarca').val(marca.replace(/&amp;/g, '&'));//$('.lblMmarca').text(marca);--
            $('.txtidModelo').val(idmodel);//$('.lblidModelo').text(idmodel);//txtidModelo
            $('.txtModelo').val(modelo.replace(/&amp;/g, '&'));
            $('.hdidModelo').val(idmodel);
        }
        $(".PrettyGridView3 tbody tr").each(function () { $(this).removeClass('seleccionar'); });
        alternar();
        $(this).addClass('seleccionar').removeClass(clase);
    });
};
function CabeceraMarca() {
    $('.PrettyGridView2 th').click(function () {
        var col = $(this).parent().children().index($(this));
        var row = $(this).parent().parent().children().index($(this).parent());
        if (col == 0) {
            $('.hdSortMarca').val(1);
        } else  {
            $('.hdSortMarca').val(2);
        }
        $('.btnCargarMarca').click();
    });
};
function CabeceraModelo() {
    $('.PrettyGridView3 th').click(function () {
        var col = $(this).parent().children().index($(this));
        var row = $(this).parent().parent().children().index($(this).parent());
        if (col == 0) {
            $('.hdSortModelo').val(1);
        } else if (col == 1){
            $('.hdSortModelo').val(2);
        } else {
            $('.hdSortModelo').val(3);
        }
        $('.btnCargarModelo').click();
    });
};
function ShowEmptyDataHeader() {
    var Grid = $('.grdModelo');
    if (Grid[0].rows.length < 2) {
        $('.div_header').attr("style", "display:block;padding-left:2%;");
        //Grid.attr("style", "display:none");
    }
}
function BuscarGridModeloMarca(marca) {
    $('.grdModelo tr:has(td)').each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
                    .hide().text(t).appendTo(this);
    });
};
function KeytxtModeloMarca(marca) {
    var txtmarca = $('.txtMarca').val().toLowerCase();
    var s = marca.toLowerCase().split(" ");//$(this).val().toLowerCase().split(" ");
        $('.grdModelo tr:hidden').show();
        $.each(s, function () {
            $(".grdModelo tr:visible .indexColumn:not(:contains('"+txtmarca+"'))").parent().hide();
        });
        if (s[0] == '') {
            $('.btnCargarModelo').click();
        }
    
};
function keyPress() {
    $('.txtMarca').live("keypress", function (e) {
        if (e.keyCode == 13) {            
            return false;
        }
    });
    $('.txtModelo').live("keypress", function (e) {
        if (e.keyCode == 13) {
            return false;
        }
    });
}
/**/
function CargarMarcas() {
    jQuery("#grdMarcas").jqGrid({
        datatype: "local",
        height: 'auto',
        //colNames: ['Inv No', 'Date', 'Client', 'Amount', 'Tax', 'Total', 'Notes'],
        colNames: ['Inv No', 'Client'],
        colModel: [{ name: 'id', index: 'id', width: 50, sorttype: "int" },
            //{ name: 'invdate', index: 'invdate', width: 90, sorttype: "date" },
            { name: 'name', index: 'name', width: 300 }
            //{ name: 'amount', index: 'amount', width: 80, align: "right", sorttype: "float" },
            //{ name: 'tax', index: 'tax', width: 80, align: "right", sorttype: "float" },
            //{ name: 'total', index: 'total', width: 80, align: "right", sorttype: "float" },
            //{ name: 'note', index: 'note', width: 150, sortable: false }
        ],
        multiselect: false,
        caption: "MARCAS",
        rowNum: 10,
        rowList: [5, 10],
        pager: '#grdMarcaspager'
    });
    //jQuery("#grdMarcas").jqGrid('navGrid', '#grdMarcaspager', { edit: false, add: false, del: false }, {}, {}, {}, { multipleSearch: true, multipleGroup: true });
    jQuery("#grdMarcas").jqGrid('navGrid', "#grdMarcaspager", { edit: false, add: false, del: false });
    var mydata =
        [{ id: "1", invdate: "2007-10-01", name: "test", note: "note", amount: "200.00", tax: "10.00", total: "210.00" },
        { id: "2", invdate: "2007-10-02", name: "test2", note: "note2", amount: "300.00", tax: "20.00", total: "320.00" },
        { id: "3", invdate: "2007-09-01", name: "test3", note: "note3", amount: "400.00", tax: "30.00", total: "430.00" },
        { id: "4", invdate: "2007-10-04", name: "test", note: "note", amount: "200.00", tax: "10.00", total: "210.00" },
        { id: "5", invdate: "2007-10-05", name: "test2", note: "note2", amount: "300.00", tax: "20.00", total: "320.00" },
        { id: "6", invdate: "2007-09-06", name: "test3", note: "note3", amount: "400.00", tax: "30.00", total: "430.00" },
        { id: "7", invdate: "2007-10-04", name: "test", note: "note", amount: "200.00", tax: "10.00", total: "210.00" },
        { id: "8", invdate: "2007-10-03", name: "test2", note: "note2", amount: "300.00", tax: "20.00", total: "320.00" },
        { id: "9", invdate: "2007-09-02", name: "test4", note: "note4", amount: "400.00", tax: "30.00", total: "430.00" },
        { id: "10", invdate: "2007-09-01", name: "test3", note: "note3", amount: "400.00", tax: "30.00", total: "430.00" },
        { id: "11", invdate: "2007-09-02", name: "test5", note: "note5", amount: "400.00", tax: "30.00", total: "430.00" }, ];
    for (var i = 0; i <= mydata.length; i++) jQuery("#grdMarcas").jqGrid('addRowData', i + 1, mydata[i]);
};