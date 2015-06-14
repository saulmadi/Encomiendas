angular.module('Encomiendas.Controllers').controller('parentController', function ($scope, userService, $location, loginService, menuService) {
        var user = userService.GetUser();
        if (user) {
            loginService.SetLoggedIn(true);
        }

        $scope.userMenuBar = [];
        $scope.user = user;
        $scope.title = "Welcome";
        $scope.logout = function() {
            userService.RemoveUser();
            $scope.user = false;
            $location.path("/login");
            $scope.menu = [];
        };

  //  $scope.menu = menuService.menu;
         $scope.menu = menuService.getMenuForUser(user.claims || []);
});