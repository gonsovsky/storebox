//Вспомгоательный класс
function setCookie(name, value) {
    var expires = "";
    var date = new Date();
    date.setTime(date.getTime() + (1 * 24 * 60 * 60 * 1000));
    expires = "; expires=" + date.toUTCString();
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
};

function getCookie(name) {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === " ") c = c.substring(1, c.length);
        if (c.indexOf(nameEq) === 0) return c.substring(nameEq.length, c.length);
    }
    return null;
};

function refreshImageById(id) {
    refreshImage(document.getElementById(id));
};

function refreshImage(el) {
    el.src = el.src + "?" + new Date().getTime();
};