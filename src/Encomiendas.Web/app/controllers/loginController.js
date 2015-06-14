angular.module('Encomiendas.Controllers').controller('loginController', function ($scope, $location, accountService, loginService, userService, facebookService, googleService, menuService) {
        
    if (userService.GetUser()) {
        $location.path("/home");
    }

    $scope.user = {};
    $scope.$parent.title = "Login";
    

    var setUserSession = function(data) {
        userService.SetUser($scope.user.email, data.name, data.token, $scope.rememberMe, data.expires,data.claims);
        $scope.$parent.user = userService.GetUser();
        $scope.$parent.menu = menuService.getMenuForUser($scope.$parent.user.claims || []);
        $location.path("/home");
    };
    
    $scope.login = function () {
        $scope.error = "";

        loginService.Login($scope.user.email, $scope.user.password).then(function (data) {
            loginService.SetLoggedIn(true);
            setUserSession(data);
        }).catch(function (error) {
            loginService.SetLoggedIn(false);
            $scope.error = error;
        });
    };
    
    $scope.loginFacebook = function() {
        facebookService.Login().then(function (data) {
            loginService.SetLoggedIn(true);
            setUserSession(data);
        }).catch(function (error) {
            loginService.SetLoggedIn(false);
            $scope.error = error;
        });
    };

    $scope.loginGoogle = function () {
        googleService.Login().then(function (data) {
            loginService.SetLoggedIn(true);
            setUserSession(data);
        }).catch(function (error) {
            loginService.SetLoggedIn(false);
            $scope.error = error;
        });
    };
    
});