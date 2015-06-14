angular.module('Encomiendas.Services').factory('$httpq', function ($http, $q) {

    return {
        post: function (resource, payload) {
            var defer = $q.defer();
            $http.post(resource, payload)
              .success(function (data) {
                  defer.resolve(data);
              }).error(function (data) {
                  defer.reject(data);
              });
            return defer.promise;
        },
        put: function (resource, payload) {
            var defer = $q.defer();
            $http.put(resource, payload)
              .success(function (data) {
                  defer.resolve(data);
              }).error(function (data) {
                  defer.reject(data);
              });
            return defer.promise;
        },
        get: function (resource) {
            var defer = $q.defer();
            $http.get(resource)
              .success(function (data) {
                  defer.resolve(data);
              }).error(function (data) {
                  defer.reject(data);
              });
            return defer.promise;
        },
        delete: function (resource) {
            var defer = $q.defer();
            $http.delete(resource)
              .success(function (data) {
                  defer.resolve(data);
              }).error(function (data) {
                  defer.reject(data);
              });
            return defer.promise;
        },

    };
});