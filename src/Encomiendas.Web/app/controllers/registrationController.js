angular.module('Encomiendas.Controllers').controller('registrationController', function ($scope, $location, accountService, userService, userAbilitiesService) {
    $('.multiselect').multiselect({
        includeSelectAllOption: true
});
    
    $scope.user = {};
    $scope.myAbilities = [];
    $scope.abilities = [];

    $scope.$parent.title = "Registration";

    $scope.cancel = function () {
        $location.path("/login");
        return false;
    };

    userAbilitiesService.GetAbilities()
    .then(function(data) {
        $scope.abilities = data;
    });

    var password1 = document.getElementById('password1');
    var password2 = document.getElementById('password2');

    var checkPasswordValidity = function () {
        if ($scope.user.password != $scope.user.passwordConfirm) {
            password1.setCustomValidity('Passwords must match.');
        } else {
            if ($scope.user.password.length < 8) {
                password1.setCustomValidity('Passwords must be at least 8 characters long.');
            }
            else {
                password1.setCustomValidity('');
            }
        }
            
       
    };

    password1.addEventListener('change', checkPasswordValidity, false);
    password2.addEventListener('change', checkPasswordValidity, false);
        
    $scope.register = function () {
            
        $scope.registered = false;
            
        accountService.Register($scope.user.email, $scope.user.password, $scope.user.name, $scope.user.phoneNumber, $scope.myAbilities)
            .then(function () {
                $scope.registered = true;
            }).catch(function(err1, err2, err3) {
                debugger;
            });
    };
    
    
});