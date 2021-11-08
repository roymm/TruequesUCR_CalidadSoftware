
    function equalPasswords(input) {
                    if (input.value != document.getElementById('Contraseña').value) {
        input.setCustomValidity('Las contraseñas deben ser iguales');
                    } else {
        input.setCustomValidity('');
                    }
                }
