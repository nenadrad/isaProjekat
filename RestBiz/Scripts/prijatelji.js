
function addFriend(friendId, index) {

	var value = JSON.stringify({
		id : friendId
	});


      var cellId = "ContentPlaceHolder1_KorisniciRepeater_buttonCell_"+index;

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
                  var href = "javascript:removeFriend("+params+")";
                  buttonCell.setAttribute('href', href);
                  buttonCell.innerText = "Ukloni iz prijatelja";

            }
        });
}

function removeFriend(friendId, index) {

      var value = JSON.stringify({
            id : friendId
      });

      var cellId = "ContentPlaceHolder1_KorisniciRepeater_buttonCell_"+index;

      $.ajax({
            type: "POST",
            url: "Prijatelji.aspx/RemoveFriend",
            data: value,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                  var response = JSON.parse(data.d);
                  toastr.success('Prijatelj '+response.ImePrezime+' uspešno uklonjen');
                  var buttonCell = document.getElementById(cellId);
                  buttonCell.removeAttribute('class');
                  buttonCell.setAttribute('class', 'pure-button pure-button-primary');
                  var params = friendId + ", " + index;
                  var href = "javascript:addFriend("+params+")";
                  buttonCell.setAttribute('href', href);
                  buttonCell.innerText = "Dodaj za prijatelja";
            }
        });
}