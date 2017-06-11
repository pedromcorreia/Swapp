//Recupera a data atual do cliente no formato DD/MM/YYYY
function dataAtualToString() {
    return moment(new Date()).format('DD/MM/YYYY')
}



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

///Carregar todas as mascaras e scripts de validacao quando a tela sofrer alteracao
function reloadAllValidation() {

    //Mascara para CEP
    $(".cep").mask("99999-999")
    $(".agencia").mask("9999-9")
    $(".cpf").mask("999.999.999-99")
    $(".conta").mask("9999 9")
    $(".cnpj").mask("99.999.999/9999-99")
    $(".cpfCnpj").mask("99.999.999/9999-99")

    $(".dinheiro").maskMoney({
        symbol: 'R$ ',
        showSymbol: true, thousands: '.', decimal: ',', symbolStay: true
    });

    $('.percentual').mask('##0,00%', { reverse: true });
    $('.calendario').mask('99/99/9999');


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

    /*
    var maskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
options = {
    onKeyPress: function (val, e, field, options) {
        field.mask(maskBehavior.apply({}, arguments), options);
    }
};

    $('.telefoneSomenteNumero').mask(maskBehavior, options);
    */


    $('.telefoneSomenteNumero').on("keydown", function (event) {
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
    /*
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
    */
    /*
    $(".telefone").inputmask({
        mask: ["(99) 9999-9999", "(99) 99999-9999", ],
        keepStatic: true
    });
    */

    /*
    $(".telefone_9dig").mask("(99) 99999-9999");

    $(".telefone_9_8_dig").mask("(99) 99999-999?9");
*/
    

}

//Recarregar os arquivos javascript
//Resolucao do problema de quando adiciona elementos dinamicamente e o listener
//do jquery nao encontra o elemento para tratar
function reloadJavascript() {
    
    var salt = Math.floor(Math.random() * 1000),
    time;

    $("head script").each(function () {
        var oldScript = this.getAttribute("src");
        
        salt = Math.floor(Math.random() * 1000);
        oldScript + '?'+salt;

        $(this).remove();
        var newScript;
        newScript = document.createElement('script');
        newScript.type = 'text/javascript';
        newScript.src = oldScript;
        
        time = setTimeout(function() {
            document.getElementsByTagName("head")[0].appendChild(newScript);
        }, 10);

    });
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

function EstadoTexto(num) {
    switch (num) {
        case 0: num = ""; break;
        case 1: num = "Acre"; break;
        case 2: num = "Alagoas"; break;
        case 3: num = "Amazonas"; break;
        case 4: num = "Amapá"; break;
        case 5: num = "Bahia"; break;
        case 6: num = "Ceará"; break;
        case 7: num = "Distrito Federal"; break;
        case 8: num = "Espírito Santo"; break;
        case 9: num = "Goiás"; break;
        case 10: num = "Maranhão"; break;
        case 11: num = "Mato Grosso"; break;
        case 12: num = "Mato Grosso do Sul"; break;
        case 13: num = "Minas Gerais"; break;
        case 14: num = "Pará"; break;
        case 15: num = "Paraíba"; break;
        case 16: num = "Paraná"; break;
        case 17: num = "Pernambuco"; break;
        case 18: num = "Piauí"; break;
        case 19: num = "Rio de Janeiro"; break;
        case 20: num = "Rio Grande do Norte"; break;
        case 21: num = "Rio Grande do Sul"; break;
        case 22: num = "Rondônia"; break;
        case 23: num = "Roraima"; break;
        case 24: num = "Santa Catarina"; break;
        case 25: num = "Sergipe"; break;
        case 26: num = "São Paulo"; break;
        case 27: num = "Tocantins"; break;
        case "": num = "";
    }
    return num
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
    /*

    //Mascara para CEP    
    $(".cep").mask("99999-999")
    $(".agencia").mask("9999-9")
    $(".cpf").mask("999.999.999-99")
    $(".conta").mask("9999 9")
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
        $(this).unmask("99999-999");
        $(this).mask("99999-999");
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

    $(".telefone_9dig").mask("(99) 99999-9999");
    */
    //$(".telefone_9dig").blur(function (event) {
    //    if($(this).val().length == 15){
    //        $('.telefone_9dig').mask("(99) 99999-9999");
    //    } else {
    //        $('.telefone_9dig').mask("(99) 9999-9999");
    //    }
    //});

    //$('.telefone_9dig').mask("(99) 99999-9999");

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

    /*
    $('.calendario').datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
    });
    */

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

   

});

