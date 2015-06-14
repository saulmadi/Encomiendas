angular.module('Encomiendas.Services').factory('accountService', function ($httpq) {

    return {
        Register: function (email, password, name, phoneNumber, abilities) {
            return $httpq.post("/register", { email: email, password: password, name: name, phoneNumber: phoneNumber, abilities: abilities});            
        },
        RegisterFacebook: function (payload) {
            return $httpq.post("/register/facebook",payload);
        },
        RegisterGoogle: function (payload) {
            return $httpq.post("/register/google",payload);
        },
        RequestToResetPassword: function (email) {
            return $httpq.post("/password/requestReset", { email: email });
        },
        ResetPassword: function (token, password) {
            return $httpq.put("/password/reset/" + token, { password: password });
        }
    };
});