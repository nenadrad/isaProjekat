function removeFriend(friendId, index) {

    var value = JSON.stringify({
        id: friendId
    });

    var cellId = "ContentPlaceHolder1_PrijateljiKorisnika_buttonCell_" + index;

    $.ajax({
        type: "POST",
        url: "Prijatelji.aspx/RemoveFriend",
        data: value,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            toastr.success('Prijatelj ' + response.ImePrezime + ' uspešno uklonjen');
            var buttonCell = document.getElementById(cellId);
            var td = buttonCell.parentNode.parentNode;
            td.remove();
            var rowsNum = document.getElementById("friendsTable").rows.length;
            if (rowsNum == 1)
                document.getElementById("ContentPlaceHolder1_friendsTableDiv").setAttribute("style", "display: none");    
        }
    });
}

function enableEdit() {

    document.getElementById("name").readOnly = false;
    document.getElementById("lastName").readOnly = false;
    document.getElementById("address").readOnly = false;

    var editButton = document.getElementById("editButton");
    editButton.setAttribute('href', 'javascript:updateUser()');
    editButton.innerText = "Snimi";

}

function updateUser() {

    var values = JSON.stringify({
        id: $("#id").val(),
        ime: $("#name").val(),
        prezime: $("#lastName").val(),
        adresa: $("#address").val()
    });

    $.ajax({
        type: "POST",
        url: "MojProfil.aspx/UpdateUser",
        data: values,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            if (response.status == 1) {
                toastr.success(response.retVal);
                disableEdit();
            }
            else {
                toastr.error(response.retVal);
                disableEdit();
            }
           

        }
    });
    
}

function disableEdit() {

    document.getElementById("name").readOnly = true;
    document.getElementById("lastName").readOnly = true;
    document.getElementById("address").readOnly = true;

    var editButton = document.getElementById("editButton");
    editButton.setAttribute('href', 'javascript:enableEdit()');
    editButton.innerText = "Izmeni";

}

function addFriend(friendId, index) {

    var value = JSON.stringify({
        id: friendId
    });


    var cellId = "ContentPlaceHolder1_KorisniciRepeater_buttonCell_" + index;

    $.ajax({
        type: "POST",
        url: "Prijatelji.aspx/AddFriend",
        data: value,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            toastr.success('Prijatelj ' + response.ImePrezime + ' uspešno dodat');
            var buttonCell = document.getElementById(cellId);
            buttonCell.removeAttribute('class');
            buttonCell.setAttribute('class', 'pure-button');
            var params = friendId + ", " + index;
            var href = "javascript:removeFriend(" + params + ")";
            buttonCell.setAttribute('href', href);
            buttonCell.innerText = "Ukloni iz prijatelja";

            document.getElementById("ContentPlaceHolder1_btnSample").click();

        }
    });
}

function enterEvent(e) {
    if (e.keyCode == 13) {
        __doPostBack('<%=SearchButton.ClientID%>', "");
    }
}