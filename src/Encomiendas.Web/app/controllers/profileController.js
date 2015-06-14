angular.module('Encomiendas.Controllers').controller('profileController', function ($scope, $location, $routeParams, adminService) {
    $scope.userId = $routeParams.userId;
    $scope.success = false;
    $scope.saveChanges = false;
    var getUserInfo = function() {
        adminService.GetUser($scope.userId).then(function(data) {
            $scope.user = data;
            $scope.name = data.name;
            $scope.email = data.email;
            console.log(data);
        }).catch(function() {
        });
    };

    $scope.updateProfile = function() {
        adminService.UpdateProfile({ Id: $scope.userId, Email: $scope.email, Name: $scope.name }).then(function() {
            $scope.success = true;
        });
    };

    getUserInfo();
});