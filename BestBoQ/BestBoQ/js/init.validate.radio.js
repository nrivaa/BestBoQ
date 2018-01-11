/*
* Check Radio All Page
*/
function checkValidateWithRadioOne() {

    if (!validateRadioOneAll()) {
        return false;
    }
    else if (!$('.form').isValid()) {
        return false;
    }

    return true;

}

function validateRadioOneAll() {
    var isValid = true;

    if (!validateRadioOne()) {
        isValid = false;
    }

    return isValid;
}

function validateRadioOne() {
    var count = 0;

    $('[type="radio"]').each(function () {
        if ($(this).is(':checked')) {
            count++;
        }
    });

    if (count === 0) {
        showAlertWithMessage("กรุณาเลือกให้ครบทุกตัวเลือก");

        return false;
    }

    return true;
}

function fixRadiogroupOneBug() {
    $('[type="radio"]').on('ifChecked', function (event) {
        $('[type="radio"]').not(this).iCheck('indeterminate');
    });
}

/**
 * Check Radio Group Validate
 */
function checkValidateWithRadio() {

    if (!validateRadioAll()) {
        return false;
    }
    else if (!$('.form').isValid()) {
        return false;
    }

    return true;

}

function validateRadioAll() {
    var isValid = true;
    findRadioGroup().forEach(function (element) {
        if (!validateRadio(element)) {
            console.log(element + ' is invalid');
            isValid = false;
        }
    });
    return isValid;
}

function validateRadio(idRadio) {
    var count = 0;

    $('[type="radio"][value=' + idRadio + ']').each(function () {
        if ($(this).is(':checked')) {
            count++;
        }
    });

    if (count === 0) {
       showAlertWithMessage("กรุณาเลือกให้ครบทุกตัวเลือก");

        return false;
    }

    return true;
}

function fixRadiogroupAll() {
    findRadioGroup().forEach(function (element) {
        fixRadiogroupBug(element)
    });
}

function fixRadiogroupBug(idRadio) {
    $('[type="radio"][value=' + idRadio + ']').on('ifChecked', function (event) {
        $('[type="radio"][value=' + idRadio + ']').not(this).iCheck('indeterminate');
    });
}

function findRadioGroup() {
    var array = new Array();

    $('[type="radio"]').each(function () {
        var val = $(this).val();
        if ($.inArray(val, array) < 0) {
            array.push(val);
        }
    });

    return array;
}

var radioOne;

$(document).ready(function () {
    if (!radioOne){
        fixRadiogroupAll(); 
    }
    else {
        fixRadiogroupOneBug();
    }
});

function showAlertWithMessage(message){
    $("#alert-message").html(message);
    $("#alertError").show();
    $(window).scrollTop(0);
}