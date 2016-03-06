'use strict';
app.controller('boardController', ['$scope', 'boardsService', 'listsService', '$uibModal', function($scope, boardsService, listsService, $uibModal) {
    $scope.query = {
        includeArchived: false
    };
    //Получаем последние доски для юзера
    function loadboards() {
        boardsService.getBoards($scope.query.includeArchived).then(function(value) {
            $scope.boards = value;
            console.log(value);
        });
    }
    
    loadboards();
    $scope.$watch('query.includeArchived', loadboards);
    
    
    
    $scope.selectBoard = function(item) {
        $scope.selectedBoard = item;
        //item.selected = true; 
        $scope.lists = item.Lists;
        //listsService.getLists(item.Id).then(function(value) {
        //    $scope.lists = value.data;
        //});
    }
    ;
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
    }
    ;
    //Посмотреть детально задачу
    $scope.openTask = function(size) {
        
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'cardDetailModal.html',
            controller: 'cardDetailModalController',
            size: size,
            resolve: {
                item: function() {
                    return $scope.selectedTask;
                }
            }
        });
    }
    ;
    $scope.placement = 'bottom';
    //Новая доска
    $scope.newBoard = function() {
        var obj = {
            Name: $scope.newBoardName
        };
        boardsService.newBoard(obj).then(function() {
        
        });
    }
    ;
    //Новый список
    $scope.newList = function() {
        var obj = {
            Name: $scope.newListName,
            BoardId: $scope.selectedBoard.Id
        };
        listsService.newList(obj).then(function(result) {
            $scope.lists.push(result.data);
        });
    }
    ;
}
]);
