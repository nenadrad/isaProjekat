function prikaziJelovnik() {

    $("#jelovnikTableDiv").toggle();
    var btn = $("#btn");

    if (btn.attr("class") == "pure-button") {
        btn.attr("class", "pure-button pure-button-active");
        btn.html("Sakrij jelovnik");
    }
    else {
        btn.attr("class", "pure-button");
        btn.html("Prikaži jelovnik");
    }

}

function enableEdit(id) {

    var object = {
        name: $("#nameCell" + id).html(),
        desc: $("#descCell" + id).html(),
        price: $("#priceCell" + id).html()
    }

    var nameInputId = "nameInput" + id;
    var descInputId = "descInput" + id;
    var priceInputId = "priceInput" + id;

    $("#nameCell" + id).html("<input id='"+nameInputId+"' type='text' value='"+object.name+"' />");
    $("#descCell" + id).html("<input id='"+descInputId+"' type='text' value='" + object.desc + "' />");
    $("#priceCell" + id).html("<input id ='"+priceInputId+"' type='text' value='" + object.price + "' />");
    $("#enableEditBtn" + id).html("Snimi");
    $("#enableEditBtn" + id).attr("href", "javascript:edit(" + id + ")");

}

function edit(id) {

    var value = JSON.stringify({
        id: id,
        naziv: $("#nameInput" + id).val(),
        opis: $("#descInput" + id).val(),
        cena: $("#priceInput" + id).val()
    });

    alert(value);

    $.ajax({
        type: "POST",
        url: "ProfilRestorana.aspx/Edit",
        data: value,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            if (response.status == 0)
                toastr.error("Greška pri izmeni stavke jelovnika.");
            else {
                toastr.success("Stavka jelovnika uspešno izmenjena.");

                $("#nameCell" + id).html(response.retVal.Naziv);
                $("#descCell" + id).html(response.retVal.Opis);
                $("#priceCell" + id).html(response.retVal.Cena);

                $("#enableEditBtn" + id).html("Izmeni");
                $("#enableEditBtn" + id).attr("href", "javascript:enableEdit(" + id + ")");
            }
        }
    });
}

function dodajStavku() {

    var table = $("#tableBody");

    table.append(" <tr id ='trId'></tr> ");
    $("#trId").append("<td id='nameCellNew'> <input id='newName' type='text' /> </td>");
    $("#trId").append("<td id='descCellNew'> <input id='newDesc' type='text' /> </td>");
    $("#trId").append("<td id='priceCellNew'> <input id='newPrice' type='text' /> </td>");
    $("#trId").append("<td> <a id='buttonSnimi' href ='javascript:snimiNovi()' class='pure-button' >Snimi</a> </td>");

    $("#trId").attr("id", "trIdOld");

}

function snimiNovi() {

    var values = JSON.stringify({
        idRest : $("#idRestorana").val(),
        naziv: $("#newName").val(),
        opis: $("#newDesc").val(),
        cena : $("#newPrice").val()
    });

    $.ajax({
        type: "POST",
        url: "ProfilRestorana.aspx/Add",
        data: values,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            if (response.status == 0)
                toastr.error("Greška pri dodavanju stavke jelovnika.");
            else {
                toastr.success("Stavka jelovnika uspešno dodata.");

                $("#nameCellNew").html(response.retVal.Naziv);
                $("#descCellNew").html(response.retVal.Opis);
                $("#priceCellNew").html(response.retVal.Cena);

                $("#buttonSnimi").html("Izmeni");
                $("#buttonSnimi").attr("href", "javascript:enableEdit(" + response.retVal.StavkaId + ")");

                $("#nameCellNew").attr('id', 'nameCell' + response.retVal.StavkaId);
                $("#descCellNew").attr('id', 'descCell'+response.retVal.StavkaId);
                $("#priceCellNew").attr('id', 'priceCell' + response.retVal.StavkaId);
                $("#buttonSnimi").attr('id', 'enableEditBtn' + response.retVal.StavkaId);

                $("#trIdOld").attr('id', 'trId' + response.retVal.StavkaId);

            }
        }
    });

}

function remove(id) {

    var value = JSON.stringify({
        id: id,
        idRest: $("#idRestorana").val()
    });

    $.ajax({
        type: "POST",
        url: "ProfilRestorana.aspx/Remove",
        data: value,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            if (response.status == 0)
                toastr.error("Greška pri uklanjanju stavke iz jelovnika.");
            else {
                toastr.success("Stavka jelovnika uspešno uklonjena.");

                $("#tr" + id).remove();
            }
            //toastr.success("obrisano");
        }
    });
}

