angular.module('Encomiendas.Controllers').controller('homeController', function ($scope, userService, $location, adminService) {
    var user = userService.GetUser();
    if (!user) {
        $location.path("/login");
    } 
        
    $scope.$parent.title = "Inicio";
    $scope.users = [];
    $scope.paginationPayload = { PageNumber: 1, PageSize: 20, Field: "Name" };
    $scope.sortCriteria = "";
    
    
    $scope.GetUsers = function (payload) {
        adminService.GetUsers(payload).then(function (data) {
            $scope.users = data.adminUsers;
        });
    };

    $scope.next = function() {
        $scope.paginationPayload.PageNumber += 1;
        $scope.GetUsers($scope.paginationPayload);
    };

    $scope.back = function () {
        $scope.paginationPayload.PageNumber -= 1;
        $scope.GetUsers($scope.paginationPayload);
    };

    $scope.sort = function (param) {
        $scope.sortCriteria = param;
        $scope.paginationPayload.Field = param;
        $scope.GetUsers($scope.paginationPayload);
    };
    
    $scope.EnableUser = function(payload) {
        adminService.EnableUser(payload).then(function () {
            $scope.GetUsers($scope.paginationPayload);
        });
    };
    
    $scope.GetUsers($scope.paginationPayload);
});