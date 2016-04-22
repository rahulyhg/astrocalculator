(function () {
    var svcWebApi = angular.module("astroapp").service("svcWebApi", function (webserver, $http, $q, $timeout) {
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