$(document).ready(function () {
    $('.accordion-head').click(function () {

        $(this).addClass('active').next().slideDown();
    }, function () {
        // alert("hello");
        // $(this).removeClass('active').next().slideUp();
    });

    $('.accordion-head').click(function () {
        if ($(this).next().is(':hidden')) {
            $(this).removeClass('active').next().slideUp();
            $(this).toggleClass('active').next().slideDown();
        }
        return false;
    });

    //This will allow only alphanumeric character
    $('input.Alphanumericsonly').keyup(function () { inputControl($(this), 'char'); });

    $("span.right").on("drop", "input.Alphanumericsonly", function () {
        var oldValue = this.value;
        setTimeout($.proxy(function () {
            if (this.value != oldValue) {
                //$(this).val(oldValue);
                $(this).trigger('change');
                inputControl($(this), 'char');
            }
        }, this), 10);
    });


    //    var url = window.location.pathname, urlRegExp = new RegExp(url.replace(/\/$/, "") + "$");
    //    $(".accordion-head").each(function () {
    //        if (urlRegExp.test(this.href.replace(/\/$/, ""))) {
    //            $(this).removeClass('active').next().slideUp();
    //            $(this).toggleClass('active').next().slideDown();
    //        }
    //    });

    //    $('#tabs div').hide();
    //    $('#tabs div:first').show();
    //    $('#tabs ul li:first').addClass('active');
    //    $('#tabs ul li a').click(function () {
    //        $('#tabs ul li').removeClass('active');
    //        $(this).parent().addClass('active');
    //        var currentTab = $(this).attr('href');
    //        $('#tabs div').hide();
    //        $(currentTab).show();
    //        return false;
    //    });

    // $('#leftMenu div').hide();
    //    $('#leftMenu div:first').show();
    //    $('#leftMenu div:first').addClass('active');
    //    $('#leftMenu div a').click(function () {
    //        $('#leftMenu div').removeClass('active');
    //        $(this).parent().addClass('active');
    //        //debugger;
    //        var currentTab = $(this).attr('href');
    //        // $('#leftMenu div').hide();
    //        $(currentTab).show();
    //        return false;
    //    });




    var maxdate = new Date();

    $(".dp").datepicker({
        flat: true,
        constrainInput: true,
        showOn: 'both',
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        maxDate: maxdate,
        yearRange: "-113:+0"
    });

    //For 18yr 

    var maxEligibledate = new Date();
    var newdate = new Date(maxEligibledate);
    newdate.setFullYear(newdate.getFullYear() - 18);
    var nd = new Date(newdate);

    $(".Newdp").datepicker({
        flat: true,
        constrainInput: true,
        showOn: 'both',
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        maxDate: nd,
        yearRange: "-113:+0"
    });

});

function checkTextAreaMaxLength(textBox, e, length) {
    var mLen = textBox["MaxLength"];
    if (null == mLen)
        mLen = length;

    var maxLength = parseInt(mLen);
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
                e.returnValue = false;
            else//Firefox
                e.preventDefault();
        }
    }
}
function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}

function numbersonly(myfield, e, dec) {

    var key;
    var keychar;

    if (window.event)
        key = window.event.keyCode;
    else if (e)
        key = e.which;
    else
        return true;

    keychar = String.fromCharCode(key);

    // control keys
    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27))
        return true;

    // numbers
    else if ((("0123456789").indexOf(keychar) > -1))
        return true;
    else
        return false;
}




//Function used with keypress event of input cox to allow only numeric with dot
function numbersWithDotonly(myfield, e, dec) {

    var key;
    var keychar;

    if (window.event)
        key = window.event.keyCode;
    else if (e)
        key = e.which;
    else
        return true;

    keychar = String.fromCharCode(key);

    // control keys
    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27))
        return true;

    // numbers
    else if ((("0123456789.").indexOf(keychar) > -1)) {
        var re = new RegExp("^[-+]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,4})?|0(\.[0-9]{0,4})?|\.[0-9]{1,4})$");
        var result = re.exec(myfield.value + keychar);

        if (result == null)
            return false;
        else
            return true;
    }
    else
        return false;
}

function inputControl(input, format) {
    // debugger;
    var value = input.val();
    var values = value.split("");
    var update = "";
    var finalExpression;
    if (format == 'char') {
        finalExpression = /^[a-zA-Z2-9]+$/;
    }
    for (id in values) {
        if (finalExpression.test(values[id]) == true && values[id] != '') {
            update += '' + values[id];
        }
    }
    input.val(update);
}