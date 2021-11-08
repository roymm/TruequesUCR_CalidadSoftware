function save(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Producto.aspx/Seleccionar",
        data: "{id : '" + id + "'}",
        dataType: "json",
        success: function (data) {
            alert(data)
        },
        failure: function () {
            alert("no funca");
        }
    })
    alert("no funca");
}

function save(id, correo) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Producto.aspx/Seleccionar",
        data: "{id : '" + id + "'}",
        dataType: "json",
        success: function (data) {
            $("#result").html(data.d);
        }
    })
    alert('This button does nothing.')
}