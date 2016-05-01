(function () {
    var busyAnimation = angular.module("astroapp").directive("busyAnimation", function () {
        return {
            restrict: "EA",
            transclude: true,
            templateUrl: "ng/templates/busy-animation.html",
            scope: {
                showContent:"@"
            },
            controller: function ($scope, $rootScope) {
                $scope.content = $scope.showContent!==undefined && $scope.showContent!==""? $scope.$eval($scope.showContent) :true;
                $scope.message = "";
                $rootScope.$on("busy", function (evt, data) {
                    $scope.content = false;
                    $scope.message = data.message;
                });
                $rootScope.$on("standby", function (evt, data) {
                    $scope.content = true;
                    $scope.message = "";
                })
            }
        }
    })
})();