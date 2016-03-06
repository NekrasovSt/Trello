'use strict';
app.controller('logOutController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    authService.logOut();
    $location.path('/login');

    $scope.authentication = authService.authentication;

}]);