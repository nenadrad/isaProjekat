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

    $("#nameCell" + id).html("<input type='text' value='"+object.name+"' />");
    $("#descCell" + id).html("<input type='text' value='" + object.desc + "' />");
    $("#priceCell" + id).html("<input type='text' value='" + object.price + "' />");
    $("#enableEditBtn" + id).html("Snimi");

}