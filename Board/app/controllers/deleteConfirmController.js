'use strict';
angular.module('app').controller('deleteConfirmController', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
    $scope.ok = function () {

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}]);