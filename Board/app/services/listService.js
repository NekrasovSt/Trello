'use strict';
app.factory('listsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var serviceFactory = {};

    var getLists = function (parentId,last) {

        last = last || 100;
        return $http.get(serviceBase + 'api/lists/GetListByParent?last=' + last + '&parentId=' + parentId).then(function (results) {
            return results;
        });
    };

    serviceFactory.getLists = getLists;

    serviceFactory.updateList = function(obj) {
        return $http.put('api/lists/' + obj.Id, obj);
    };
    serviceFactory.updateCard = function(obj) {
        return $http.put('api/cards/' + obj.Id, obj);
    }

    return serviceFactory;

}]);