'use strict';
app.directive('feedback', function () {
    return {
        restrict: 'A',
        require: '^form',
        link: function (scope, el, attrs, formCtrl) {
            // find the text box element, which has the 'name' attribute
            var inputEl = el[0].querySelector("[name]");
            // convert the native text box element to an angular element
            var inputNgEl = angular.element(inputEl);
            // get the name on the text box so we know the property to check
            // on the form controller
            var inputName = inputNgEl.attr('name');

            // only apply the has-error class after the user leaves the text box
            inputNgEl.bind('blur', function () {
                el.addClass('has-feedback');
                el.toggleClass('has-error', formCtrl[inputName].$invalid);
                el.toggleClass('has-success', formCtrl[inputName].$valid);
                // Find Span elements with feedback icons
                var spanErrorEl = el[0].querySelector("[name='error_feedback']");
                var spanSuccessEl = el[0].querySelector("[name='success_feedback']");
                // Convert the native text Span elements to angular elements
                var spanErrorNgEl = angular.element(spanErrorEl);
                var spanSuccessNgEl = angular.element(spanSuccessEl);
                // Toggle classes
                spanErrorNgEl.toggleClass('hidden', formCtrl[inputName].$valid);
                spanSuccessNgEl.toggleClass('hidden', formCtrl[inputName].$invalid);
            });
            inputNgEl.bind('focus', function () {
                el.addClass('has-feedback');
                el.toggleClass('has-error', false);
                el.toggleClass('has-success', false);
                // Find Span elements with feedback icons
                var spanErrorEl = el[0].querySelector("[name='error_feedback']");
                var spanSuccessEl = el[0].querySelector("[name='success_feedback']");
                // Convert the native text Span elements to angular elements
                var spanErrorNgEl = angular.element(spanErrorEl);
                var spanSuccessNgEl = angular.element(spanSuccessEl);
                // Toggle classes
                spanErrorNgEl.toggleClass('hidden', true);
                spanSuccessNgEl.toggleClass('hidden', true);
            });
            inputNgEl.bind('reset', function () {
                console.log("si llega al reset")
                el.addClass('has-feedback');
                el.toggleClass('has-error', false);
                el.toggleClass('has-success', false);
                // Find Span elements with feedback icons
                var spanErrorEl = el[0].querySelector("[name='error_feedback']");
                var spanSuccessEl = el[0].querySelector("[name='success_feedback']");
                // Convert the native text Span elements to angular elements
                var spanErrorNgEl = angular.element(spanErrorEl);
                var spanSuccessNgEl = angular.element(spanSuccessEl);
                // Toggle classes
                spanErrorNgEl.toggleClass('hidden', true);
                spanSuccessNgEl.toggleClass('hidden', true);
            })
        }
    }
});