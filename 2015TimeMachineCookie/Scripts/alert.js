alert = function (message) {
    if (! typeof message == "string") return;
    var alertbox = "<div style='z-index:10000;position:fixed;bottom:10px;left:30px;min-width:300px' class='alert alert-info alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" + message + "</div>";
    $("body").append(alertbox);
};