'use strict';
app.controller('boardController', ['$scope', 'boardsService', 'listsService', '$uibModal', function ($scope, boardsService, listsService, $uibModal) {
    //Получаем последние доски для юзера
    boardsService.getBoards().then(function (value) {
        $scope.boards = value.data;
    });
    $scope.selectBoard = function (item) {
        //item.selected = true; 
        listsService.getLists(item.Id).then(function (value) {
            $scope.lists = value.data;
        });
    };
    //Подсветка классов
    $scope.levelClass = {
        '0': 'list-group-item-primary',
        '1': 'list-group-item-success',
        '2': 'list-group-item-info',
        '3': 'list-group-item-warning',
        '4': 'list-group-item-danger'
    }
    $scope.selectTask = function(card) {
        $scope.selectedTask = card;
    };
    //Посмотреть детально задачу
    $scope.openTask = function (size) {

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'cardDetailModal.html',
            controller: 'cardDetailModalController',
            size: size,
            resolve: {
                item: function () {
                    return $scope.selectedTask;
                }
            }
        });
    };
}]);