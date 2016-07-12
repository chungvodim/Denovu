function GetNearestLocations() {
    var postData = {};
    postData.longtitude = $('#longtitude').val();
    postData.lattitude = $('#lattitude').val();
    postData.numberOfLocations = $('#numberOfLocations').val();
    $.post("/Home/GetNearestLocations", postData)
    .done(function (result) {
        if (result.status == 1) {
            $('#divResult').empty();
            $('#divResult').append(result.data);
        } else {
            alert(result.message);
            $('#divResult').empty();
        }
    })
    .fail(function (data) {
        alert("Something went wrong: " + data.responseText);
    });
}