'use strict';
app.factory('httpInterceptor', function ($q, $rootScope, $log) {

    var numLoadings = 0;

    return {
        request: function (config) {

            numLoadings++;

            // Show loader
            $rootScope.$broadcast("loader_show");
            $rootScope.$broadcast("error_hide");
            return config || $q.when(config);

        },
        response: function (response) {

            if ((--numLoadings) === 0) {
                // Hide loader
                $rootScope.$broadcast("loader_hide");
                $rootScope.$broadcast("error_hide");
            }

            return response || $q.when(response);

        },
        responseError: function (response) {

            if (!(--numLoadings)) {
                // Hide loader
                $rootScope.$broadcast("loader_hide");
                $rootScope.$broadcast("error_show");
            }

            return $q.reject(response);
        }
    };
})
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpInterceptor');
});