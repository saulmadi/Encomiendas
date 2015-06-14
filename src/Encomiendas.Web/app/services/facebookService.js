angular.module('Encomiendas.Services').factory('facebookService', function ($q, $httpq, loginService, accountService) {

    var login = function (payload) {
        var def = $q.defer();
        loginService.LoginFacebook(payload).then(function (data) {
            def.resolve(data);
        }).catch(function (error) {
            def.reject(error);
        });
        return def.promise;
    };

    var register = function (payload) {
        var def = $q.defer();
        accountService.RegisterFacebook(payload).then(function () {
            def.resolve();
        }).catch(function () {
            def.reject();
        });
        return def.promise;
    };

    var isDisable = function(error) {
        return error === "\"Your account has been disabled. Please contact your administrator for help.\"";
    };
    
    return {
        Login: function () {
            var def = $q.defer();
            FB.login(function (response) {
                if (response.authResponse) {
                    FB.api('/me', function (response) {
                        login(response).then(function(data) {
                            def.resolve(data);
                        }).catch(function (error) {
                            if (isDisable(error)) {
                                def.reject(error);
                            } else {
                                register(response).then(function () {
                                    login(response).then(function (data) {
                                        def.resolve(data);
                                    });
                                });
                            }
                        });
                    });
                } else {
                    def.reject();
                }
            }, { scope: 'email'});
            return def.promise;
        }
    };
});