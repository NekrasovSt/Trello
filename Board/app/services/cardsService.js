'use strict';
app.factory('cardsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var serviceFactory = {};

    serviceFactory.add = function (data) {
        return $http.post(serviceBase + 'api/cards/post', data);
    };

    serviceFactory.update = function (data) {
        return $http.put(serviceBase + 'api/cards/put', data);
    };
    return serviceFactory;
}]);