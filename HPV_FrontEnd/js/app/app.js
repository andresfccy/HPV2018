var app = angular.module('prosperidad', [
      'ngResource'
    , 'pascalprecht.translate'
    , 'angular-growl'
    , 'ngCookies'
    , 'ui.router'
    , 'ngSanitize'
    , 'ui.select'
    , 'ngMaterial'
    , 'ngMessages'
    , 'angularMoment'
    , 'ui.bootstrap'
    , 'LocalStorageModule'
    , 'ui.router.title'
    , 'ui.select'
    , 'am.multiselect'
    , 'angular-md5'
    , 'ngStorage'
    , 'ngFileUpload'

    //Modulos registrados en la aplicación
    , 'prosperidad.commons'
    , 'prosperidad.inscription',
    , 'prosperidad.sessions'
    , 'prosperidad.users'
    , 'prosperidad.roles'
    , 'prosperidad.allocation'
    , 'prosperidad.attendance'
    , 'prosperidad.searches'
    , 'prosperidad.reallocation'
    , 'prosperidad.uploadable'
    , 'prosperidad.feedback'
    , 'prosperidad.participantState'
    , 'prosperidad.lifeStory'
    , 'prosperidad.successCase'
    , 'prosperidad.finalSurvey'
    , 'prosperidad.leadership'
    , 'prosperidad.parameters'
    , 'prosperidad.reports'
    , 'prosperidad.systematization'
])
    .run(
    ['$rootScope', '$state', '$stateParams', 
        function ($rootScope, $state, $stateParams) {

            // It's very handy to add references to $state and $stateParams to the $rootScope
            // so that you can access them from any scope within your applications.For example,
            // <li ng-class="{ active: $state.includes('contacts.list') }"> will set the <li>
            // to active whenever 'contacts.list' or one of its decendents is active.
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            var bypass;
            $rootScope.$on('$stateChangeStart', function (evt, toState, toParams) {
                if (bypass) return;
                // Halt state change from even starting
                evt.preventDefault();
                // Perform custom logic
                $rootScope.opcProm.then(function (o) {
                    //console.log("Funcionó: ", o);
                    bypass = true;
                    // Continue with the update and state transition if logic allows
                    $state.go(toState, toParams);
                })
            });
        }
      ]
    )
    .config(function ($translateProvider, $translatePartialLoaderProvider, growlProvider, $mdDateLocaleProvider) {

        $translateProvider.useLoader('$translatePartialLoader', {
            urlTemplate: 'assets/i18n/{part}/{part}-{lang}.json'
        });
        $translateProvider.preferredLanguage('es');
        $translateProvider.useLocalStorage();

        growlProvider.globalTimeToLive(5000);
        growlProvider.globalPosition('middle-center');

        // Spanish localization
        $mdDateLocaleProvider.months = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
        $mdDateLocaleProvider.shortMonths = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
        $mdDateLocaleProvider.days = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];
        $mdDateLocaleProvider.shortDays = ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'];
        
        // Can change week display to start on Monday.
        // $mdDateLocaleProvider.firstDayOfWeek = 1;
        // Optional.
        // $mdDateLocaleProvider.dates = [1, 2, 3, 4, 5, 6, 7];
        // Example uses moment.js to parse and format dates.
        /*
        $mdDateLocaleProvider.parseDate = function(dateString) {
            var m = moment(dateString, 'YYYY/MM/DD', true);
            return m.isValid() ? m.toDate() : new Date(NaN);
        };
        */
        
        $mdDateLocaleProvider.formatDate = function(date) {
            return moment(date).format('YYYY/MM/DD');
        };

        // In addition to date display, date components also need localized messages
        // for aria-labels for screen-reader users.
        
        $mdDateLocaleProvider.weekNumberFormatter = function(weekNumber) {
            return 'Semana ' + weekNumber;
        };
        
        $mdDateLocaleProvider.msgCalendar = 'Calendario';
        $mdDateLocaleProvider.msgOpenCalendar = 'Abrir el calendario';
        
    });
