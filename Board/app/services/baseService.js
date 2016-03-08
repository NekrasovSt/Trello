'use strict';
app.factory('baseService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    return {
        init: function (name) {
            return {
                add: function (data) {
                    return $http.post(serviceBase + 'api/' + name + '/post', data);
                },
                update: function (data) {
                    return $http.put(serviceBase + 'api/' + name + '/put', data);
                },
                delete: function (data) {
                    return $http.delete(serviceBase + 'api/' + name + '/' + data.Id);
                }
            }
        }
    };
}]);