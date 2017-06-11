$(document).ready(function(){

	// Código de slide exibindo proposta completa 
	
	var rightVal = -480;
	
	$("#slide-resumo-proposta-botao").click(function () {
		rightVal = (rightVal * -1) - 480;
		$(this).parent().animate({right: rightVal + 'px'}, {queue: false, duration: 500});
	});
	
	// Código para fechar o menu do mobile quando clicar fora
	//http://jsfiddle.net/52VtD/5718/
	
    $(document).click(function (event) {
        var clickover = $(event.target);
        var _opened = $(".navbar-collapse").hasClass("navbar-collapse in");
        if (_opened === true && !clickover.hasClass("navbar-toggle")) {
            $("button.navbar-toggle").click();
        }
    });
	
	$(document).ready(function(){
		$('[data-toggle="tooltip"]').tooltip();   
	});

});
	
	// Para menu lateral mobile
	
		function openNav() {
			document.getElementById("abrir-menu-lateral").style.width = "250px";
		}

		function closeNav() {
			document.getElementById("abrir-menu-lateral").style.width = "0";
		}