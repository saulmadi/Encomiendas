angular.module('Encomiendas.Controllers').controller('resetPasswordController', function ($scope, $routeParams, $location, userService, accountService) {
        
    if (userService.GetUser()) {
        $location.path("/");
    }

    var token = $routeParams.token;
    if (!token) $location.path("/404");

    $scope.$parent.title = "Reset Password";
    
    $scope.resetPassword = function () {
        accountService.ResetPassword(token, $scope.password1).then(function () {
            $scope.success = true;
        });
    };
});