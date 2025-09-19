
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

