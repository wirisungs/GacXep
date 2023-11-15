var pass = document.getElementById("pwd")
var confirmPas = document.getElementById("cfpwd")

function validPass() {
    if (pass.value != confirmPas.value) {
        confirmPas.setCustomValidity("Password dont match");
    }
    else {
        confirmPas.setCustomValidity('');
    }
}
pass.onchange = validPass;
confirmPas.onkeyup = validPass;