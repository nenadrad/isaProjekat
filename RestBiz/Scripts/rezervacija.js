$(document).ready(function () {

    $('.free-table').click(function () {

        $(this).toggleClass("selected");

    });

    /*$("#testBtn").click(function () {

        var selectedButtons = $('.selected');
        var izabraniStolovi = new Array();
        alert(selectedButtons.length);
        for (var i = 0; i < selectedButtons.length; i++) {
            izabraniStolovi.push(selectedButtons[i].innerHTML);
        }

        var values = JSON.stringify({
            stolovi: izabraniStolovi
        });

        alert(values);

    });*/

});

function enterEvent(e) {
    if (e.keyCode == 13) {
        __doPostBack('<%=SearchButton.ClientID%>', "");
    }
}

function inviteFriend(id) {
    
    $("#btnCell" + id).toggleClass("pure-button-primary");
    $("#btnCell" + id).html("Ukloni iz poziva");
    $("#btnCell" + id).attr('href', 'javascript:removeInvite(' + id + ')');
    $("#user" + id).val("1");

}

function removeInvite(id) {

    $("#btnCell" + id).toggleClass("pure-button-primary");
    $("#btnCell" + id).html("Pozovi");
    $("#btnCell" + id).attr('href', 'javascript:inviteFriend(' + id + ')');
    $("#user" + id).val("0");

}

function snimi() {

    var selectedButtons = $('.free-table');
    alert(selectedButtons.length);
    var izabraniStolovi = new Array();
    for (var i = 0; i < selectedButtons.length; i++) {
        izabraniStolovi.push(selectedButtons[i].innerHTML);
    }

    var values = JSON.stringify({
        restoran: $("#Name").val(),
        datum: $("#Date").val(),
        vreme: $("#DateTime").val(),
        trajanje: $("#Time").val(),
        stolovi: izabraniStolovi
    });

    alert(values);
}

function GetIzabraniStolovi() {

    var selectedButtons = $('.selected');
    var izabraniStolovi = new Array();
    for (var i = 0; i < selectedButtons.length; i++) {
        izabraniStolovi.push(selectedButtons[i].innerHTML);
    }

    var values = JSON.stringify({
        stolovi: izabraniStolovi
    });

    $.ajax({
        type: "POST",
        url: "Rezervacija.aspx/SaveTables",
        data: values,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            $("#RasporedStolovaDiv").hide();
            $("#InviteDiv").show();
        }
    });

}