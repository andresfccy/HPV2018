app.directive('integerValidation', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {

            /*modelCtrl.$parsers.push(function (inputValue) {

                var transformedInput = parseInt(inputValue);

                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });*/
            element.on('keyup', function () {
                scope.$apply(function () {
                    var v = element.val() == parseInt(element.val());
                    modelCtrl.$setValidity('integer', v);
                });
            });
        }
    };
});