(function () {
    var svcWebApi = angular.module("astroapp").service("svcWebapi", function (webserver, $http, $q, $timeout) {
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
        this.getSolarConfig = function () {
            var url = webserver.baseUrl + "sunrises";
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                console.log("we have received the 200 response from the api");
                console.debug(response.data);
                deferred.resolve(response.data);
            }, function (response) {
                console.error("we have not received 200 response from the api");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.getSunrises = function (model) {
            var url = webserver.baseUrl + "sunrises";
            var deferred = $q.defer();
            $http.post(url, model).then(function (response) {
                console.log("we have received the 200 response from the api");
                console.debug(response.data);
                deferred.resolve(response.data);
            }, function (response) {
                console.error("we have not received 200 response from the api");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
    })
})()