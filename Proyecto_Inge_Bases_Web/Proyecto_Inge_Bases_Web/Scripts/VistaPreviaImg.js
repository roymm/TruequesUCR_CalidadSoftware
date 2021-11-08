//Func: Muestra la vista previa de la imagen que se va a subir del producto
//Como son 3 imágenes distintas, cada una coloca un id distinto
function muestreImgPre(img, id) {
    var imagen = "#imagen" + id;
    if (img.files && img.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(imagen)
                .attr('src', e.target.result)
                .width(200)
                .height(200);
        };

        reader.readAsDataURL(img.files[0]);
    }
}