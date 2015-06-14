angular.module('Encomiendas.Services').factory('adminService', function ($httpq) {

    return {
        GetUsers: function (payload) {
            return $httpq.get("/users?" + "PageNumber=" + payload.PageNumber + "&PageSize=" + payload.PageSize + "&Field=" + payload.Field);
        },
        EnableUser: function (payload) {
            return $httpq.post("/users/enable", payload);
        },
        GetUser: function(id) {
            return $httpq.get("/user/" + id );
        },
        UpdateProfile: function(payload) {
            return $httpq.post("/user/", payload);
        },
        GetRol:function() {
            return $httpq.get("/rol");
        }
        
    };
});