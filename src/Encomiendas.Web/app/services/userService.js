angular.module('Encomiendas.Services').factory('userService', function () {

    var key = "user";
   
    return {
        SetUser: function (email, name, token, remember, expires, claims) {
            var user = {
                email: email,
                name: name,
                token: token,
                expires: expires,
                claims: claims
            };
            
            var userString = JSON.stringify(user);

            if (remember) {
                window.localStorage.setItem(key, userString);
            } else {
                window.sessionStorage.setItem(key, userString);
            }            
        },
        GetUser: function () {
            var user;
            var userFromLocalStorage = window.localStorage.getItem(key);
            if (userFromLocalStorage) {
                user = JSON.parse(userFromLocalStorage);
            } else {
                var userFromSession = window.sessionStorage.getItem(key);
                if (!userFromSession) return false;
                user = JSON.parse(userFromSession);
            }
            var expires = new Date(user.expires);
            var isExpired = expires < new Date();
            if (isExpired) {
                this.RemoveUser();
                return false;
            }
            return user;
        },
        RemoveUser: function () {
            window.sessionStorage.removeItem("user");
            window.localStorage.removeItem("user");
        },
       
    };
});