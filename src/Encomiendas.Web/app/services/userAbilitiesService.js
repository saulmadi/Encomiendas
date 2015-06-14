angular.module('Encomiendas.Services').factory('userAbilitiesService', function ($httpq) {

    return {
        GetAbilities: function () {
            return $httpq.get('/abilities');
        },
        AddAbilities: function (userId, abilities) {
            var response = $httpq.post('/user/abilites', {userId:userId, abilities:abilities});
            return response;
        }
    };
});