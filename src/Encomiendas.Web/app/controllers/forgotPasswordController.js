angular.module('Encomiendas.Controllers').controller('forgotPasswordController', function ($scope, $location, userService, accountService) {
        
    if (userService.GetUser()) {
        $location.path("/");
    }

    $scope.user = {};

    $scope.$parent.title = "Reset Password";

    $scope.resetPassword = function () {
        accountService.RequestToResetPassword($scope.user.email).then(function () {
            $scope.success = true;
        });
    };
});