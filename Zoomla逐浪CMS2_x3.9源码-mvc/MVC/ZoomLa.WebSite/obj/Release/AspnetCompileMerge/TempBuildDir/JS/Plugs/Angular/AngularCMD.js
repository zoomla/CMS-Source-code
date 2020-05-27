function InitAngularCMD() {
    app.directive('contenteditable', function () {
        return {
            require: '?ngModel',
            link: function (scope, element, attrs, ctrl) {
                // Do nothing if this is not bound to a model  
                if (!ctrl) { return; }
                // Checks for updates (input or pressing ENTER)  
                // view -> model  
                element.bind('input enterKey', function () {
                    var rerender = false;
                    var html = element.html();
                    if (attrs.noLineBreaks) {
                        //html = html.replace(/<div>/g, '').replace(/<br>/g, '').replace(/<\/div>/g, '');
                        rerender = true;
                    }
                    scope.$apply(function () {
                        ctrl.$setViewValue(html);
                        if (rerender) {
                            ctrl.$render();
                        }
                    });
                });
                // model -> view  
                ctrl.$render = function () {
                    element.html(ctrl.$viewValue);
                };
                // load init value from DOM  
                ctrl.$render();
            }
        };
    });

}
