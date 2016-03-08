'use strict';
app.directive("error", [
    '$rootScope', function ($rootScope) {
        return function ($scope, element, attrs) {
            $scope.$on("error_show", function () {
                return element.show();
            });
            return $scope.$on("error_hide", function () {
                return element.hide();
            });
        };
    }
]);