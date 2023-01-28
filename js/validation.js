
function upperCaseOnKeyUp(controlName) {
    $(controlName).keyup(function () {
        $(controlName).val($(controlName).val().toUpperCase());
    });
}

function isNumeric(evt) {
    debugger;
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        evt.value = "";
        alert('only numeric value allowed.');
        return false;
    }
        //return false;
    return true;
}

function chkLessGreater(val1, val2) {
    var a1 = parseFloat(val1);;
    var a2 = parseFloat(val2);

    if (a1 > 0) {
        if (a1 > a2) {
            alert('From Day cannot be greater then To Day');
            return false;
        }
    }
    return true;
};