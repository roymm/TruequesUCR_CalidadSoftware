function Seleccionar(ids, urlString) {
    var button = document.getElementById(ids);
    if (button.innerHTML == "Seleccionar") {
        button.innerHTML = "Deseleccionar";
    }
    else {
        button.innerHTML = "Seleccionar";
    }

    $.ajax({
        type: "POST",
        url: urlString,
        data: JSON.stringify({ id: ids }),
        contentType: "application/json; charset=utf-8",
        success: function () { console.log("")},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown)
        }
    });
}