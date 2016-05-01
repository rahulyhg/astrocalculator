(function () {
    var optionPicker = angular.module("astroapp").directive("optionPicker", function ($injector) {
        return {
            restrict: "EA",
            scope: {
                option:"="
            },
            templateUrl: "ng/templates/option-picker.html",
            controller: function ($scope) {
                $scope.options = [];
                $scope.dropMenu = false;
               
                $scope.$watch("fn", function (after, before) {
                    if (after != null && after !== undefined) {
                        //here we can get the options for the control
                        $scope.fn().then(function (data) {
                            $scope.options = data;
                            $scope.options.forEach(function (el) {
                                el.sel = false;
                            });
                            if ($scope.options!==null && $scope.options.length>0) {
                               $scope.selectOption( $scope.options[0]);
                            }
                        })
                    }
                });
                $scope.selectOption = function (option) {
                    $scope.option = option;
                    $scope.options.forEach(function (el) {
                        if (el.id == option.id) { el.sel = true; }
                        else { el.sel = false; }
                    });
                    $scope.dropMenu = false;
                }
            },
            link: function (scope, elem, attrs) {
                var service_fn = attrs.srv;
                if (service_fn != "" && service_fn!=null) {
                    var service = $injector.get(service_fn.split('.')[0]);
                    if (service != null) {
                        scope.fn = service[service_fn.split('.')[1]];
                    }
                }
            }
        }
    })
})()