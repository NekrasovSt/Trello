'use strict';
app.controller('boardController', ['$scope', 'boardsService', 'listsService', 'cardsService', '$uibModal', function ($scope, boardsService, listsService, cardsService, $uibModal) {
    $scope.query = {
        includeArchived: false
    };
    //Получаем последние доски для юзера
    function loadboards() {
        boardsService.getBoards($scope.query.includeArchived).then(function (value) {
            $scope.boards = value;
            console.log(value);
        });
    }

    loadboards();
    $scope.$watch('query.includeArchived', loadboards);

    function removeItem(array, item) {
        for (var i in array) {
            if (array[i] == item) {
                array.splice(i, 1);
                break;
            }
        }
    }

    $scope.selectBoard = function (item) {
        $scope.selectedBoard = item;
        //item.selected = true; 
        $scope.lists = item.Lists;
        //listsService.getLists(item.Id).then(function(value) {
        //    $scope.lists = value.data;
        //});
    };
    //Подсветка классов
    $scope.levelClass = {
        '0': 'list-group-item-primary',
        '1': 'list-group-item-success',
        '2': 'list-group-item-info',
        '3': 'list-group-item-warning',
        '4': 'list-group-item-danger'
    }
    $scope.selectTask = function (card) {
        $scope.selectedTask = card;
    }
    ;
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
    }
    ;
    $scope.placement = 'bottom';
    //Новая доска
    $scope.newBoard = function () {
        var obj = {
            Name: $scope.newBoardName,
            CreationDate: new Date()
        };
        boardsService.add(obj).then(function (result) {
            $scope.boards.push(result.data);
            $scope.newBoardName = null;
        });
    };
    $scope.updateBoard = function (item) {
        boardsService.update(item);
    };
    //Удалить доску
    $scope.deleteBoard = function (item) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'deleteConfirmModal.html',
            controller: 'deleteConfirmController'
        });
        modalInstance.result.then(function () {
            boardsService.delete(item).then(function () {
                removeItem($scope.boards, item);
                $scope.selectedBoard = null;
            });
        });
    };
    //Новый список
    $scope.newList = function () {
        var obj = {
            Name: $scope.newListName,
            BoardId: $scope.selectedBoard.Id,
            CreationDate: new Date(),
            MaxCardsCount: 10
        };
        listsService.add(obj).then(function (result) {
            $scope.lists.push(result.data);
            $scope.newListName = null;
        });
    };
    //Редактировать список
    $scope.updateList = function (list) {
        if (list.MaxCardsCount < list.Cards.length) {
            list.MaxCardsCountError = "Максимальное количество не может быть меньше общего количества уже созданных задач " + list.Cards.length;
            return;
        } else {
            delete list.MaxCardsCountError;
        }
        listsService.update(list);
    };
    //Удалить список
    $scope.deleteList = function (list) {
        listsService.delete(list).then(function (item) {
            removeItem($scope.selectedBoard.Lists, list);
        });
    };
    //Добавить карту/таску
    $scope.addCard = function (obj, name) {
        cardsService.add({
            Name: name,
            ListId: obj.Id,
            CreationDate: new Date(),
            PlaneDate: new Date()
        }).then(function (result) {
            obj.Cards.push(result.data);
            $scope.query.newTaskName = null;
        });
    };
}
]);
