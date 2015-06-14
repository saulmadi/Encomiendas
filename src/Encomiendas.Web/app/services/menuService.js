angular.module('Encomiendas.Services').factory('menuService', function () {
    'use strict';

    return {
        menu: [
            {
                display: 'Admin',
                features: [
                    {
                        name: 'ActivateDeactivateUsers',
                        display: 'User Management',
                        route: '/activate-deactivate-users'
                    }
                ]
            },
            {
                display: 'General',
                features: [
                    {
                        name: 'Home',
                        display: 'Go to Home',
                        route: '/home'
                    }
                ]
            }
        ],

        getFeatures: function() {
            var features = [];
            this.menu.forEach(function (value, index, array) {
                var tmpFeature = features.concat(value.features);
                features = tmpFeature;
            });

            return features;
        },
        getMenuForUser: function (claims) {

            var menuUser = [];
            this.menu.forEach(function (menuValue, index, array) {
                
             
                var userFeatures = menuValue.features.filter(function (feature) {
                    return claims.some(function(value) {
                        return value === feature.name;
                    });
                });
                if (userFeatures.length > 0) {

                    var userMenu = { display: menuValue.display, features: [] };
                    userFeatures.forEach(function (value, index, array) {
                        userMenu.features.push(value);

                    });
                    menuUser.push(userMenu);
                }
                

              
            });

            return menuUser;

        }
        


    };
});