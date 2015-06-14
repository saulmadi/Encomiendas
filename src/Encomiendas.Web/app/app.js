'use strict';
var app = angular.module('Encomiendas', ['ng', 'ngRoute', 'Encomiendas.Controllers', 'Encomiendas.Services', 'Encomiendas.Directives']);


angular.module('Encomiendas.Controllers', []);
angular.module('Encomiendas.Services', []);
angular.module('Encomiendas.Directives', []);

app.config(function($routeProvider) {
        $routeProvider
            .when('/login', {
                templateUrl: 'app/views/login.html',
                controller: 'loginController'
            })
            .when('/forgot-password', {
                templateUrl: 'app/views/forgot-password.html',
                controller: 'forgotPasswordController'
            })
            .when('/reset-password', {
                templateUrl: 'app/views/reset-password.html',
                controller: 'resetPasswordController'
            })
            .when('/register', {
                templateUrl: 'app/views/registration.html',
                controller: 'registrationController'
            })
            .when('/home', {
                templateUrl: 'app/views/home.html',
                controller: 'homeController'
            })
            .when('/activate-deactivate-users', {
                templateUrl: 'app/views/activate-deactivateUsers.html',
                controller: 'homeController'
            
                 }

            )
            .when('/404', {
                templateUrl: 'App/Views/404.html'
            })
            .when('/profile/:userId', {
                templateUrl: 'app/views/profile.html',
                controller: 'profileController'
            })
            .otherwise({
                redirectTo: '/404'
            });
})
    
    .config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push([
                '$q', '$location', 'userService', function($q, $location, userService) {
                    return {
                        'responseError': function(rejection) {
                            //log here
                            
                            if (rejection.status == 401) {
                                console.log("Not logged in. Redirecting to login page.");
                                userService.RemoveUser();
                                $location.path('/login');
                            }
                            return $q.reject(rejection);
                        }
                    };
                }
            ]);
        }
    ])
    .config([
        '$httpProvider', function ($httpProvider) {

            $httpProvider.interceptors.push([
                '$q', function ($q) {

                    var logRejection = function (rejection) {
                        LE.error({
                            method: rejection.config.method,
                            url: rejection.config.url,
                            data: rejection.config.data,
                            headers: rejection.config.headers,
                            status: rejection.status,
                            statusText: rejection.statusText
                        });
                        return $q.reject(rejection);
                    };

                    return {
                        'request': function(config) {
                            LE.info(config);
                            return config || $q.when(config);
                        },
                        'responseError': logRejection,
                        'requestError': logRejection
                    };
                }
            ]);
        }
    ])
    
    .config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push([
                '$q', 'userService', function($q, userService) {
                    return {
                        'request': function(config) {

                            console.log(config.method + " " + config.url);

                            var user = userService.GetUser();
                            if (user) {
                           
                                config.headers["Authorization"] = 'Bearer ' + user.token;

                            }
                            if (config.method == "POST" || config.method == "PUT") {
                                console.log(JSON.stringify(config.data));
                            }
                            return config || $q.when(config);
                        },
                        'requestError': function(rejection) {

                            //log here
                            console.log("RequestError: " + JSON.stringify(rejection));
                            return $q.reject(rejection);
                        }
                    };
                }
            ]);
        }
    ])
    .run(function ($rootScope, $location, loginService, userService, menuService) {
        var routesThatDontRequireAuth = ['/login', '/forgot-password','/reset-password','/register'];
        
        var routesThatRequireRole = menuService.getFeatures();


        var routeClean = function (route) {
           return  routesThatDontRequireAuth.some(function(value, index, array) {
               return route.toString().indexOf(value) !== -1;
            });
        };

        var routeWithRoles = function (route) {   
            return routesThatRequireRole.some(function (value) {
                return route === value.route;
            });
        };

        var urlFeature = function (url, features) {
           
            var feauturesNames = features.filter(function(value) {
               return  url === value.route;
            });
            return feauturesNames[0];
        };
        
        var featureInUserClaims = function(feature, claims) {

            return claims.some(function(value) {
                return value === feature;
            });
        }

        $rootScope.$on('$routeChangeStart', function (event, next, current) {

            var features = menuService.getFeatures();//menuService.features;
            var userClaims = userService.GetUser().claims;
            var url = $location.url();

            // if route requires auth and user is not logged in

            var route = routeClean(url);
            var loggedUser = loginService.GetLoggedIn();
            

            if (!route && !loggedUser) {
                // redirect back to login

                event.preventDefault();
                $location.path('/login');
            } 
            else {
                
                if (routeWithRoles(url) ) {
                    var feature = urlFeature(url, features);
                    if (!featureInUserClaims(feature.name, userClaims)) {
                        event.preventDefault();
                        $location.path("/404");
                    }
                   
                }

            }
        });
        
    })
;