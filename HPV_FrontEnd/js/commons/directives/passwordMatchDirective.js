'use strict';
app.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val()===$(firstPassword).val();
                    ctrl.$setValidity('passwordMatch', v);
                });
            });
            $(firstPassword).on('blur',function(){
                if(!confPristine)
                    elem.blur();
            });
            var confPristine = true;
            $(firstPassword).on('focus',function(){
                confPristine = elem[0].className.indexOf("ng-untouched")>=0?true:false;
            });
        }
    }
}]);