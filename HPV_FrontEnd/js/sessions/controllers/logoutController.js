'use strict';

angular.module('prosperidad.sessions')
    .controller('LogoutController',
    ['$scope', 'SessionsBusiness', '$state','loading',
        function ($scope, SessionsBusiness, $state,loading) {

            loading.startLoading("LogoutController");
            SessionsBusiness.removeAllInfoFromLocalStorage();

            loading.stopLoading("LogoutController");
            $state.go('home');
        }
    ]
);