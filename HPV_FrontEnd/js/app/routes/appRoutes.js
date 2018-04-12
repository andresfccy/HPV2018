app.config(['$stateProvider', '$urlRouterProvider','$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {

        $locationProvider.html5Mode(true);

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: 'views/commons/home.html',
                controller: 'HomeController as ctrl',
                resolve: {
                    $title: function () { return '¡Bienvenido!'; }
                }
        });
    }
]);