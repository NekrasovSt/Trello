'use strict';
app.factory('boardsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var serviceFactory = {};

    var getBoards = function (last) {

        return $http.get(serviceBase + 'api/boards/GetList?last=' + last||100).then(function (results) {
            return results;
        });
    };

    serviceFactory.getBoards = getBoards;

    return serviceFactory;

}]);