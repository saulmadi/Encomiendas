angular.module('Encomiendas.Services').factory('googleService', function ($q, $httpq, loginService, accountService) {
    var login = function (payload) {
        var def = $q.defer();
        loginService.LoginGoogle(payload).then(function (data) {
            def.resolve(data);
        }).catch(function (error) {
            def.reject(error);
        });
        return def.promise;
    };

    var register = function (payload) {
        var def = $q.defer();
        accountService.RegisterGoogle(payload).then(function () {
            def.resolve();
        }).catch(function () {
            def.reject();
        });
        return def.promise;
    };

    var isDisable = function (error) {
        return error === "\"Your account has been disabled. Please contact your administrator for help.\"";
    };
    return {
        Login: function () {
            var def = $q.defer();
            gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: false }, function(authResult) {
                if (authResult && !authResult.error) {
                    gapi.client.load('plus', 'v1', function() {
                        var request = gapi.client.plus.people.get({
                            'userId': 'me'
                        });
                        request.execute(function (response) {
                            response["email"] = response.emails[0].value;
                            login({ Id: response.id, Email: response.email }).then(function (data) {
                                def.resolve(data);
                            }).catch(function (error) {
                                if (isDisable(error)) {
                                    def.reject(error);
                                } else {
                                    register(response).then(function () {
                                        login({ Id: response.id, Email: response.email }).then(function (data) {
                                            def.resolve(data);
                                        });
                                    });
                                }
                            });
                        });
                    });
                } else {
                    def.reject();
                }
            });
            return def.promise;
        }
    };
});