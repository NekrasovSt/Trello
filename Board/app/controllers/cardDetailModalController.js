'use strict';
angular.module('app').controller('cardDetailModalController', ['$scope', '$uibModalInstance', 'item', 'cardsService', 'commentsService', function ($scope, $uibModalInstance, item, cardsService, commentsService) {
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
        $scope.model.PlaneDate = new Date($scope.model.PlaneDate);
        cardsService.update($scope.model).then(function () {
            $uibModalInstance.close();
        });

    };
    $scope.delete = function() {
        cardsService.delete($scope.model).then(function () {
            $uibModalInstance.close('delete');
        });
    };
    $scope.addComment = function (name) {
        commentsService.add({
            Description: name,
            CardId: $scope.model.Id,
            CreationDate: new Date()
        }).then(function (result) {
            $scope.model.Comments.push(result.data);
        });
    };
    $scope.levelOptions = [
        { key: 0, value: 'Обычный' },
        { key: 1, value: 'Маленький' },
    { key: 2, value: 'Ниже среднего' },
    { key: 3, value: 'Высокий' },
    { key: 4, value: 'Срочно' }];

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
    $scope.commentsPaging = {
        curPage: 0,
        pageSize: 3,
        numberOfPages :function(count) {
            return Math.ceil(count / this.pageSize);
        }
};
}]);