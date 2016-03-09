'use strict';
app.factory('cardsService', ['baseService', function (baseService) {

    return baseService.init("cards");
}]);