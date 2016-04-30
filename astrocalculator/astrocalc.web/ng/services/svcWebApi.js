(function () {
    var svcWebApi = angular.module("astroapp").service("svcWebapi", function (webserver, $http, $q, $timeout, $rootScope) {
        this.userLogin = function (username, pin) {
            var url = webserver.baseUrl + "useraccounts/{u}/{p}".replace(/{u}/, username).replace(/{p}/, pin);
            var deferred = $q.defer();
            $rootScope.$broadcast("busy", { message: "Just a moment.." });
            $timeout(function () {
                deferred.reject({username:"niranjanawati", location:"chandigarh", status:400});
                $rootScope.$broadcast("standby", { message: "" });
            }, 3000)
            //$http.get(url).then(function (response) {
            //    deferred.resolve(response.data);
            //    $rootScope.$broadcast("standby", { message: "" });
            //}, function (response) {
            //    deferred.reject(response);
            //    $rootScope.$broadcast("standby", { message: "" })
            //});
            return deferred.promise;
        }
        this.postLocation = function (location) {
            var url = webserver.baseUrl + "locations/cities";
            var deferred = $q.defer();
            $http.post(url, location).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely zenith choices from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.getStates = function () {
            var url = webserver.baseUrl + "locations/states";
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely zenith choices from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.getSolarEphemeris = function (lat, lng, zen, yr, mn) {
            var url = webserver.baseUrl + "ephemeris/solar/"+lat +"/"+ lng+"/"+zen+"/"+yr+"/"+mn;
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely zenith choices from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.zenithSuggestions = function () {
            var url = webserver.baseUrl + "ephemeris/range/zeniths";
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely zenith choices from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.monthSuggestions = function () {
            var url = webserver.baseUrl + "ephemeris/range/months";
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the likely month choices from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.yearSuggestions = function () {
            var url = webserver.baseUrl + "ephemeris/range/years";
            var deferred = $q.defer();
            $http.get(url).then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                console.error("failed to get the year choices from the server");
                console.error(response.status);
                deferred.reject(response.status);
            });
            return deferred.promise;
        }
        this.citySuggestions = function (phrase) {
            var url = webserver.baseUrl + "locations/cities/likely/{p}".replace(/{p}/, phrase);
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