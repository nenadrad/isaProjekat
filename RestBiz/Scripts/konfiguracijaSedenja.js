$(document).ready(function () {

    var counter = 0;

    $('.stolovi').click(function(){
        $(this).toggleClass('button-selected');
        var value = $(this).html();
        if (value == '&nbsp;') {
            counter++;
            $(this).html(counter);
        }
        else {
            counter--;
            $(this).html('&nbsp');
        }
    });

});

function snimi() {

    var stolovi = document.getElementsByClassName('stolovi');

    var brojevi = new Array();
    var pozicije = new Array();

    for (var i = 0; i < stolovi.length; i++) {
        value = stolovi[i].innerHTML;
        if (value != '&nbsp;') {
            brojevi.push(value);
            pozicije.push(i);
        }
    }

    var values = JSON.stringify({
        brojeviStolova: brojevi,
        pozicijeStolova: pozicije,
        idRest : getURLParameter("idRest")
    });

    $.ajax({
        type: "POST",
        url: "KonfiguracijaSedenja.aspx/Snimi",
        data: values,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            if (response.status == 0)
                toastr.error(response.retVal);
            else {
                toastr.success(response.retVal);

                $('.stolovi').addClass("pure-button-disabled");
                $("#btnSnimi").attr('href', "ProfilRestorana.aspx?id=" + getURLParameter("idRest"));
                $("#btnSnimi").html('Povratak na profil restorana');
               
            }
        }
    });
    
}

function getURLParameter(name) {
    return decodeURIComponent(
        (location.search.match(RegExp("[?|&]" + name + '=(.+?)(&|$)')) || [, null])[1]
    );
}