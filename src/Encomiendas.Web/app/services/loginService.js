angular.module('Encomiendas.Services').factory('loginService', function($httpq) {
    var loggedIn = false;

   

    return {
        Login: function (email, password) {

            var response = $httpq.post("/login", { email: email, password: password });
            return response;
        },
        LoginFacebook: function (payload) {
          
            var response = $httpq.post("/login/facebook", payload);
            return response;
        },
        LoginGoogle: function (payload) {

            var response = $httpq.post("/login/google", payload);
            return response;
        },

        GetLoggedIn : function() {
            return loggedIn;
        },

        SetLoggedIn: function(value) {
            loggedIn = value;
        }
    };
});