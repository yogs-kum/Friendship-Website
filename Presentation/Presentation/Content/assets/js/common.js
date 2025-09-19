$(document).ready(function(){
	$('#navIcon').click(function(){
		$(this).toggleClass('open');

		$("#nav").fadeToggle("slow");
		$("#nav").css('display', 'flex');
	});
	$('#nav ul li a').click(function(){
		$("#navIcon").removeClass('open');
		$("#nav").fadeOut("slow");
	});
});


$(window).on("load", function(){
	if ($(window).scrollTop() >= 100) {
		$("#header").addClass("bg-active");
	}
});
$(window).on("scroll", function(){
	if ($(this).scrollTop() > 0) {
		$("#header").addClass("bg-active");
	} else {
		$("#header").removeClass("bg-active");
	}
});


$(document).ready(function(){
	$("#profileToggleBtn").on("click", function(){
		$("#notifyPopup, #chatPopup").hide();
		$("#profilePopup").fadeToggle("slow");
		$(this).toggleClass("active");
		$("#notifyToggleBtn").removeClass("active");
		$("#userMenuOverlay").fadeIn("slow");
	});
	$("#notifyToggleBtn").on("click", function(){
		$("#profilePopup, #chatPopup").hide();
		$("#notifyPopup").fadeToggle("slow");
		$(this).toggleClass("active");
		if ($(this).hasClass("active")) {
			$("#profileToggleBtn").removeClass("active");
			$("#userMenuOverlay").fadeIn("slow");
		}
		else {
			$("#profileToggleBtn").addClass("active");
			$("#userMenuOverlay").fadeOut("slow");
		}
		
	});
	$("#userMenuOverlay").on("click", function(){
		$(this).fadeOut("slow");
		$("#profilePopup, #notifyPopup, #chatPopup").fadeOut("slow");
		$("#profileToggleBtn, #notifyToggleBtn").removeClass("active");
	});
});



$(document).ready(function () {
	$("#passResetPopupTrigger").click(function () {
		$("#passResetPopup").addClass("visible");
		$("#passResetPopupOverlay").addClass("visible");
	});
	$("#passResetPopupClose").click(function () {
		$("#passResetPopup").removeClass("visible");
		$("#passResetPopupOverlay").removeClass("visible");
	});
});


function NumericOnly(e) {
	//$("#" + e.target.id).bind('keypress', function (e) {
	//    if (e.keyCode == '9' || e.keyCode == '16') {
	//        return;
	//    }
	//    var code;
	//    if (e.keyCode) code = e.keyCode;
	//    else if (e.which) code = e.which;
	//    if (e.which == 46)
	//        return false;
	//    if (code == 8 || code == 46)
	//        return true;
	//    if (code < 48 || code > 57)
	//        return false;
	//}
	// );

	//$("#" + e.target.id).bind('mouseenter', function (e) {
	//    var val = $(this).val();
	//    if (val != '0') {
	//        val = val.replace(/[^0-9]+/g, "")
	//        $(this).val(val);
	//    }
	//});
	//e.preventDefault();
};

function fnValidateEmail(email) {
	var expr = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	return expr.test(email);
};


$(document).ready(function () {
	$(".add-user-pic-btn").click(function () {
		$("#profileImgPop").addClass("visible");
		$("#profileImgPopOverlay").addClass("visible");
	});
	$(".hide-profileImgPop").click(function () {
		$("#profileImgPop").removeClass("visible");
		$("#profileImgPopOverlay").removeClass("visible");
	});
});