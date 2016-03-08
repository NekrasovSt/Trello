'use strict';
app.filter('clientPagination', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});