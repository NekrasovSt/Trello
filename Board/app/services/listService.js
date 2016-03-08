'use strict';
app.factory('listsService', ['baseService', function (baseService) {

    //var serviceBase = ngAuthSettings.apiServiceBaseUri;

    //var serviceFactory = {};

    //var getLists = function (parentId, showeAcrhive) {
    //    showeAcrhive = showeAcrhive || false;
    //    return $http.get(serviceBase + 'api/lists/GetList?boardId=' + parentId + '&showeAcrhive=' + showeAcrhive).then(function (results) {
    //        return results;
    //    });
    //};

    //serviceFactory.getLists = getLists;

    //serviceFactory.updateList = function (obj) {
    //    return $http.put('api/lists/' + obj.Id, obj);
    //};
    //serviceFactory.delete = function(obj) {
    //    return $http.delete('api/lists/' + obj.Id);
    //};

    //serviceFactory.updateCard = function (obj) {
    //    return $http.put('api/cards/' + obj.Id, obj);
    //}
    //serviceFactory.newList = function (data) {
    //    return $http.post(serviceBase + 'api/lists/post', data).then(function (results) {
    //        return results;
    //    });
    //};


    return baseService.init('lists');

}]);