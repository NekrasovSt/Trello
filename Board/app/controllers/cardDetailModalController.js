'use strict';
angular.module('app').controller('cardDetailModalController', ['$scope', '$uibModalInstance', 'item', 'listsService', function ($scope, $uibModalInstance, item, listsService) {
    $scope.model = item;
    if ($scope.model && $scope.model.PlaneDate && !(Object.prototype.toString.call($scope.model.PlaneDate) === '[object Number]'))
        $scope.model.PlaneDate = Date.parse($scope.model.PlaneDate);
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }
    $scope.altInputFormats = ['M!/d!/yyyy'];
    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };
    $scope.planeDatePopup = {
        opened: false
    };
    $scope.planeDateOpen = function () {
        $scope.planeDatePopup.opened = true;
    }
    $scope.ok = function () {
        $scope.model.PlaneDate =new Date($scope.model.PlaneDate);
        listsService.updateCard($scope.model).then(function () {
           $uibModalInstance.close(); 
        });
        
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}]);