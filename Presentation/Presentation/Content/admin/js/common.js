/*$("#contentMenu").toggle(function () {
	$(".fix-header").animate({ left: 60 }, "fast");

});*/

$("#contentMenu").on('click', function () {
    if ($(".fix-header").hasClass("active")) {
        $(".fix-header").removeClass("active");
    }
    else {
        $(".fix-header").removeClass("active");
        $(".fix-header").addClass("active");
    }
});



//$(".show-menu-btn").on('click', function () {
//	$("#navBgOverlay").show();
//	$("#nav").animate({ right: 0 }, "fast");
//	$("#header").animate({ left: -280 }, "fast");
//	$("#header, .st-container, #footer").animate({ right: 280 }, "fast");
//});
//$(".hide-menu-btn,#navBgOverlay").on('click', function () {
//	$("#navBgOverlay").hide();
//	$("#nav").animate({ right: -280 }, "fast");
//	$("#header").animate({ left: 0 }, "fast");
//	$("#header, .st-container, #footer").animate({ right: 0 }, "fast");
//}););
//});


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

