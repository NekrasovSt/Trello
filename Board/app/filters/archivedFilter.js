'use strict';
app.filter('archivedFilter', function () {
    return function (input, archived) {
        if (!input)
            return [];
        return input.filter(function(item) {
            if (archived || (item.form && item.form.$dirty))
                return true;
            return item.Archived == false;
        });
    };
});