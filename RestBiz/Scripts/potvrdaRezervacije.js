function oceni() {

	var rating = $("#rateYo").rateYo("rating");
 	var id = getURLParameter("id");

 	var values = JSON.stringify({
 		ocena : rating,
 		idPoz : id
 	});

 	$.ajax({
        type: "POST",
        url: "PotvrdaRezervacije.aspx/Oceni",
        data: values,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var response = JSON.parse(data.d);
            if (response.status == 0)
                toastr.error(response.retVal);
            else {
                toastr.success(response.retVal);

                $("#ocenaDiv").attr('style', 'display:none');
                $("#controlDivOcena").attr('style', 'display:none');
               
            }
        }
    });
    

}

function getURLParameter(name) {
    return decodeURIComponent(
        (location.search.match(RegExp("[?|&]" + name + '=(.+?)(&|$)')) || [, null])[1]
    );
}