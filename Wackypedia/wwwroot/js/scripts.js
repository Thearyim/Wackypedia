$(document).ready(function () {
    $("#contentEdit").hide();

    $("#contentDisplay a").click(function() {
        $("#contentEdit").show();
        $("#contentDisplay").hide();
    });
});