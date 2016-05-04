(function () {
    var ctrollerEphemeris = angular.module("astroapp").controller("ctrollerEphemeris", function ($scope, svcWebapi, $location) {
        $scope.dt = null;
        $scope.location = null;
        $scope.calcmethod = null;
        $scope.ephemeris = [];
        $scope.locationCreating = false; //this just denotes if the location is being created or no
        var checkIfOkForDownload = function () {
            return $scope.dt !== null &&
                $scope.location.lat !== null &&
                $scope.location.lng !== null &&
                $scope.calcmethod != null;
        }
        var downloadEphemeris = function () {
            if ($scope.calcmethod.id == "Vedic") {
                svcWebapi.getVedicSolarEphemeris($scope.location.lat,
               $scope.location.lng,
              
               $scope.dt.year,
               $scope.dt.month).then(function (data) {
                   $scope.ephemeris = data;
               });
            }
            else {
                svcWebapi.getSolarEphemeris($scope.location.lat,
               $scope.location.lng,
               
               $scope.dt.year,
               $scope.dt.month).then(function (data) {
                   $scope.ephemeris = data;
               });
            }
        }
        $scope.$watch("dt", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        }, true);
        $scope.$watch("location.lat", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        });
        $scope.$watch("location.lng", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        });
        $scope.$watch("calcmethod", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        });

    })
})();