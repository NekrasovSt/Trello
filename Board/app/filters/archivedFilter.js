'use strict';
app.filter('archivedFilter', function () {
    return function (input, archived) {
        if (!input)
            return [];
        return input.filter(function(item) {
            if (archived || item.needSave)
                return true;
            return item.Archived == false;
        });
    };
});