(function () {
    var ctrollerSunrises = angular.module("astroapp").controller("ctrollerSunrises", function ($scope, svcWebApi) {
        $scope.model = {};
        $scope.selection = {
            city :"",year:0, month:""
        };
        var init = function () {
            //this is where you can go ahead to get the data from the server
            //data  - solar configuration and the options
            svcWebApi.getSolarConfig().then(function (data) {
                $scope.model = angular.copy(data, $scope.model);
                $scope.selection.city = $scope.model.citychoices[0].title;
                $scope.selection.year = $scope.model.yearchoices[0].toString();
                $scope.selection.month = $scope.model.monthchoices[0];
            }, function (status) {
                //this is when the model was not being populated .. 
                $scope.model = {};
            })
        }; init();
        $scope.$watch("selection.city", function (after, before) {
            if (after !== undefined && after != null && after != "" && $scope.model.citychoices!==undefined) {
                $scope.model.city = $scope.model.citychoices.filter(function (el) {
                    return el.title == after;
                })[0];
            }
        })
        $scope.$watch("selection.year", function (after, before) {
            if (after !== undefined && after != null && $scope.model.yearchoices!==undefined) {
                $scope.model.year = $scope.model.yearchoices.filter(function (el) {
                    return el == after;
                })[0];
            }
        });
        $scope.$watch("selection.month", function (after, before) {
            if (after !== undefined && after != null && after != "" && $scope.model.monthchoices!==undefined) {
                $scope.model.month = $scope.model.monthchoices.filter(function (el) {
                    return el == after;
                })[0];
            }
        })
    })
})()