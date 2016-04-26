(function () {
    var svcWebApi = angular.module("astroapp").service("svcWebapi", function (webserver, $http, $q, $timeout) {

        this.yearSuggestions = function () {
            var url = webserver.baseUrl + "ephemeris/range/years";
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely cities from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.citySuggestions = function (phrase) {
            var url = webserver.baseUrl + "ephemeris/locations/likely/{p}".replace(/{p}/, phrase);
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely cities from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
       
    })
})()