import moment from "moment";

export default class Services {
    
    setCookie(name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    getCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    eraseCookie(name) {
        document.cookie = name + '=; Max-Age=-99999999;';
    }

    validateToken() {

        if (!sessionStorage.getItem('tokenID')) {
            let tokenID = this.getCookie("tokenID");
            let tokenExpiration = this.getCookie("tokenExpiration");

            if (tokenID != null) {
                sessionStorage.setItem('tokenID', tokenID);
                sessionStorage.setItem('tokenExpiration', tokenExpiration);
            } else {
                window.location.href = "/login";
            } 
        } else {

            let dateExpiration = moment(sessionStorage.getItem('tokenExpiration'));

            if (dateExpiration < moment()) {
                this.eraseCookie("tokenID");
                this.eraseCookie("tokenExpiration");

                window.location.href = "/login";
            }
        }
    }
}