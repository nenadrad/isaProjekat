//$(document).ready(function () {
//    $("#regDiv").hide();
//});

function register() {

    if ($("#password").val() != $("#passwordRepeat").val()) {
        toastr.error("Lozinke se ne poklapaju");
    }
    else {

        values = JSON.stringify({
            ime: $("#name").val(),
            prezime: $("#lastName").val(),
            email: $("#email").val(),
            lozinka: $("#password").val()
        });

        $.ajax({
            type: "POST",
            url: "Registracija.aspx/Register",
            data: values,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var response = JSON.parse(data.d);
                if (response.status == 0)
                    toastr.error(response.retVal);
                else {
                    window.location.href = "Info.aspx?type=reg";
                }
            }
        });

    }
}

function showRegDiv() {
    $("#regDiv").show(250);
}