'use strict';
app.factory('boardsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var serviceFactory = {};

    var update = function() {
        var obj = this;
        return $http.put(serviceBase + 'api/boards/'+obj.Id, obj).then(function (results) {
            return results;
        });

    };
    var del = function() {
        var obj = this;
        return $http.delete(serviceBase + 'api/boards/' + obj.Id).then(function (results) {
            return results;
        });

    };

    var getBoards = function (showeAcrhive) {
        showeAcrhive = showeAcrhive || false;
        return $http.get(serviceBase + 'api/boards/GetList?showeAcrhive=' + showeAcrhive).then(function (results) {
            var obj = results.data;
            obj.forEach(function(item) {
                item.update = update;
                item.delete = del;
            });
            return obj;
        });
    };

    serviceFactory.getBoards = getBoards;

    serviceFactory.newBoard = function (data) {
        return $http.post(serviceBase + 'api/boards/post',data).then(function (results) {
            return results;
        });
    };

    return serviceFactory;

}]);