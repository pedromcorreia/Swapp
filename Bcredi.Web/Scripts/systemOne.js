//Recupera a data atual do cliente no formato DD/MM/YYYY
function dataAtualToString() {
    return moment(new Date()).format('DD/MM/YYYY')
}

function showLoadingModal() {
    $('body').loadingModal({
        text: 'Por favor, aguarde',
        animation: 'circle',
        color: '#FFFFFF',
        opacity: '0.8',
        backgroundColor: 'rgb(0,0,0)',
        position: 'auto'
        // Opcoes de animação além da animação já configurada (circle):
        //rotatingPlane
        //wave
        //wanderingCubes
        //spinner
        //chasingDots
        //threeBounce
        //circle
        //cubeGrid
        //fadingCircle
        //foldingCube
        //doubleBounce
    });
}

function hideLoadingModal() {
	$('body').loadingModal('hide');
	$('body').loadingModal('destroy');
}

/* 
*  Funcao para formatacao de numero em valor (R$ 00,00)
*  Como utilizar: 
*  var valorStr = currencyFormatted(16.00, 'R$');
*  valorStr será igual a R$ 16,00
*/
function currencyFormatted(value, str_cifrao) {
	return str_cifrao + ' ' + value.formatMoney(2, ',', '.');
}

Number.prototype.formatMoney = function (c, d, t) {
	var n = this,
		c = isNaN(c = Math.abs(c)) ? 2 : c,
		d = d == undefined ? "." : d,
		t = t == undefined ? "," : t,
		s = n < 0 ? "-" : "",
		i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
		j = (j = i.length) > 3 ? j % 3 : 0;
	return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

$(document).ready(function () {

    $("#btnMinhaAgenda").click(function () {        
        $("form").attr('action', '/Agenda/Index');
        $("form").submit();
    });


    $.fn.dataTableExt.afnFiltering.push(
        function (oSettings, aData, iDataIndex) {

            //Tratar somente a tabela com de atrasos(tblAtraso)
            if (oSettings.nTable.id == 'tblAtraso') {
                var idComboTipoCobranca = 0;
                idComboTipoCobranca = $("#cmbTipoCobranca").val();

                var quantidadeItensTipoCobranca = $('#cmbTipoCobranca option').length;
                var diasEmAtrasoMinimo = $('#cmbTipoCobranca option:selected').attr('data-diasatraso');
                var diasEmAtrasoMaximo = 0;
                var opcaoSelecionada = $('#cmbTipoCobranca option:selected').index();
                var proximaOpcaoSelecionada = opcaoSelecionada;

                //verificar se nao eh a ultima opcao da combo
                if (proximaOpcaoSelecionada < quantidadeItensTipoCobranca) {
                    proximaOpcaoSelecionada = opcaoSelecionada + 1;
                }

                var diasEmAtrasoMaximo = $('#cmbTipoCobranca option').eq(proximaOpcaoSelecionada).attr('data-diasatraso');

                //Selecionou a ultima opcao
                if (opcaoSelecionada == $('#cmbTipoCobranca option:last').index()) {
                    diasEmAtrasoMinimo = $('#cmbTipoCobranca option:last').attr('data-diasatraso');
                    diasEmAtrasoMaximo = 10000;                    
                }

                //Retornar todos os registros
                if (idComboTipoCobranca == 0) {
                    return true;
                } else {
                    var valorColunaDiasEmAtraso = aData[3];

                    if (parseInt(valorColunaDiasEmAtraso) > parseInt(diasEmAtrasoMinimo) && parseInt(valorColunaDiasEmAtraso) < parseInt(diasEmAtrasoMaximo)) {
                        return true;
                    }

                    return false;
                }
            }

            return true;

        }
    );

    /* Configuracao de plugin de notificacao Alertify */
    alertify.set({
        // buttons order will be OK, Cancel
        buttonReverse: true,
        buttonFocus: "cancel",
        labels: {
            ok: "Ok",
            cancel: "Cancelar"
        }
    });

    //Mascara para CEP    
    $(".cep").mask("99.999-999")
    $(".cpf").mask("999.999.999-99")
    $(".cnpj").mask("99.999.999/9999-99")
    $(".cpfCnpj").mask("99.999.999/9999-99")

    $(".dinheiro").maskMoney({
        symbol: 'R$ ',
        showSymbol: true, thousands: '.', decimal: ',', symbolStay: true
    });

    $('.percentual').mask('##0,00%', { reverse: true });
    $('.calendario').mask('99/99/9999');
    $('.dias').mask('999');

    $(document).on("focus", ".cep", function () {
        $(this).unmask("99.999-999");
        $(this).mask("99.999-999");
    });

    $('.somente_numero_e_virgula').on("keydown", function (event) {
        // Allow special chars + arrows 
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
            || event.keyCode == 27 || event.keyCode == 13
            || (event.keyCode == 65 && event.ctrlKey === true)
            || (event.keyCode >= 35 && event.keyCode <= 39)
            || (event.keyCode == 188) // comma (",")
            || (event.keyCode == 110)) { // comma keypad (",")
            return;
        } else {
            // If it's not a number stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });

    $('.somenteNumero').on("keydown", function (event) {
        // Allow special chars + arrows 
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
            || event.keyCode == 27 || event.keyCode == 13
            || (event.keyCode == 65 && event.ctrlKey === true)
            || (event.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        } else {
            // If it's not a number stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });

    $('.telefone').on("keydown", function (event) {
        // Allow special chars + arrows 
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
            || event.keyCode == 27 || event.keyCode == 13
            || (event.keyCode == 65 && event.ctrlKey === true)
            || (event.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        } else {
            // If it's not a number stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });

    //Setar o foco inicial do campo. Quando estiver oculto, vai para o proximo campo texto
    var campoFocoInicial = (function () {

        //verificar se existe um campo com foco default
        if ($(".foco-default").length) {
            $(".foco-default").focus();
        } else {
            $("input:text").focus();
        }

    })();

    $(".email").on("focusout", function () {

        var input = $(this);
        var valorCampo = $(this).val();

        // Remove as classes de válido e inválido                
        input.closest("div").removeClass('has-success');
        input.closest("div").removeClass('has-error');


        er = /^[a-zA-Z0-9][a-zA-Z0-9\._-]+@([a-zA-Z0-9\._-]+\.)[a-zA-Z-0-9]{2}/;

        if (er.exec(valorCampo)) {
            input.closest("div").addClass('has-success');
        } else {
            input.closest("div").addClass('has-error');
        }
    });

    $('.calendario').datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
    });

    $('.calendarioComHora').datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        onSelect: function (datetext) {
            var d = new Date(); // for now
            var h = d.getHours();
            h = (h < 10) ? ("0" + h) : h;

            var m = d.getMinutes();
            m = (m < 10) ? ("0" + m) : m;

            var s = d.getSeconds();
            s = (s < 10) ? ("0" + s) : s;

            datetext = datetext + " " + h + ":" + m + ":" + s;

            $(this).val(datetext);
        }
    });

    var jsonDataTableSemFiltroSemPaginacao = {
        "sPaginationType": "full_numbers",        
        bFilter: false,
        bPaginate: false,
        bInfo: false
    };

    // Objeto json (TABLE SEM FILTRO)
    var jsonDataTableSemFiltro = {
        "sPaginationType": "full_numbers",        
        bFilter: false,
        "oLanguage": {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Não foram encontrados resultados",
            "sInfo": "Mostrando de _START_ at&eacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando de 0 at&eacute; 0 de 0 registros",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "  Primeiro  ",
                "sPrevious": "  Anterior  ",
                "sNext": "  Pr&oacute;ximo  ",
                "sLast": "  &Uacute;ltimo  "
            }
        }
    };

    // Objeto json (TABLE COM FILTRO)
    var jsonDataTableComFiltro = {
        "sPaginationType": "full_numbers",        
        bFilter: true,
        "oLanguage": {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Não foram encontrados resultados",
            "sInfo": "Mostrando de _START_ at&eacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando de 0 at&eacute; 0 de 0 registros",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "  Primeiro  ",
                "sPrevious": "  Anterior  ",
                "sNext": "  Pr&oacute;ximo  ",
                "sLast": "  &Uacute;ltimo  "
            }
        }
    };

    // Objeto json (TABLE COM FILTRO) da agenda de tarefas
    var jsonDataTableComFiltroAgendaTarefas = {
        "sPaginationType": "full_numbers",
         "sScrollX": '100%',
         bFilter: true,
        "oLanguage": {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Não foram encontrados resultados",
            "sInfo" : "Mostrando de _START_ at&eacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando de 0 at&eacute; 0 de 0 registros",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "  Primeiro  ",
                "sPrevious": "  Anterior  ",
                "sNext": "  Pr&oacute;ximo  ",
                "sLast": "  &Uacute;ltimo  "
            }
        }
        };

    // CONFIG DATATABLE (SEM FILTRO)
    $('.dataTableOneSystemSemFiltro').DataTable(jsonDataTableSemFiltro);

    // CONFIG DATATABLE (COM FILTRO)
    $('.dataTableOneSystemComFiltro').DataTable(jsonDataTableComFiltro);

    /* Inicio - Customizacao para tarefas da Agenda (TELA PRINCIPAL) */
    var tblAgendaTarefasColumnAlignment = { "aoColumnDefs": [{ "sClass": "", "aTargets": [2, 3, 5] }] };
    var configtblAgendaTarefas = $.extend({}, jsonDataTableComFiltroAgendaTarefas, tblAgendaTarefasColumnAlignment);
    if ($("#tblAgendaTarefas").length) {
        $('#tblAgendaTarefas').DataTable(configtblAgendaTarefas);
    }
    /* Fim - Customizacao para tblAtraso */


    /* Inicio - Customizacao para tblAtraso (TELA PRINCIPAL) */
    var tblAtrasoColumnAlignment = { "aoColumnDefs": [{ "sClass": "", "aTargets": [2, 3, 5] }] };
    var configTblAtraso = $.extend({}, jsonDataTableComFiltro, tblAtrasoColumnAlignment);
    if ($("#tblAtraso").length) {
        $('#tblAtraso').DataTable(configTblAtraso);
    }
    /* Fim - Customizacao para tblAtraso */

    /* Inicio - Customizacao para tblParcelasEmAtraso (ABA PARCELAS EM ATRASO) */
    var tblParcelasEmAtrasoColumnAlignment = { "aoColumnDefs": [{ "sClass": "", "aTargets": [0, 1, 2] }] };
    var configTblParcelasEmAtraso = $.extend({}, jsonDataTableComFiltro, tblParcelasEmAtrasoColumnAlignment);
    if ($("#tblParcelasEmAtraso").length) {
        $('#tblParcelasEmAtraso').DataTable(configTblParcelasEmAtraso);
    }
    /* Fim - Customizacao para tblDetalhesIntegracao */

    /* Inicio - Customizacao para tblDetalhesIntegracao (ABA DETALHES DA INTEGRACAO) */
    var tblDetalhesIntegracaoColumnAlignment = { "aoColumnDefs": [{ "sClass": "", "aTargets": [0, 1, 3, 4] }, { "sClass": "dt-right", "aTargets": [2] }] };
    var configTblDetalhesIntegracao = $.extend({}, jsonDataTableComFiltro, tblDetalhesIntegracaoColumnAlignment);
    if ($("#tblDetalhesIntegracao").length) {
        $('#tblDetalhesIntegracao').DataTable(configTblDetalhesIntegracao);
    }
    /* Fim - Customizacao para tblDetalhesIntegracao */

    /* Inicio - Customizacao para tblRatingParametro */
    var tblRatingParametroColumnAlignment = {
        "aoColumnDefs": [
            {
                "sClass": ""
                , "aTargets": [0, 1, 2, 3]
            },
            { "sWidth": 140, "aTargets": [3] }

        ],
        "aaSorting": [] // Desabilitado sorting. Manter ordenação proveniente da consulta.
    };
    var configTblRatingParametro = $.extend({}, jsonDataTableSemFiltroSemPaginacao, tblRatingParametroColumnAlignment);
    if ($("#tblRatingParametro").length) {
        $('#tblRatingParametro').DataTable(configTblRatingParametro);
    }
    /* Fim - Customizacao para tblRatingParametro */

    /* Inicio - Customizacao para tblRatingContrato */
    var tblRatingContratoColumnAlignment = {
        "aoColumnDefs": [
             {
                 /* "sClass": "" retirada de classe para centralizar */
                 "aTargets": [0, 1, 2, 5]
             }
        ]
    };
    var configTblRatingContrato = $.extend({}, jsonDataTableComFiltro, tblRatingContratoColumnAlignment);
    if ($("#tblRatingContrato").length) {
        $('#tblRatingContrato').DataTable(configTblRatingContrato);
    }
    /* Fim - Customizacao para tblRatingContrato */

});

