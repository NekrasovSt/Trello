'use strict';
app.factory('commentsService', ['baseService', function (baseService) {

    return baseService.init("Comments");

}]);