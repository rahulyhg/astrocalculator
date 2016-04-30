(function () {
    var svcCache = angular.module("astroapp").service("svcCache", function ($q, $window, $timeout) {
        this.currentUser = null; //this is the user in action currently
        this.storeUser = function (ud) {
            var deferred = $q.defer();
            this.currentUser = ud;
            $window.localStorage["user"] = angular.toJson(ud);
            $timeout(function () {
                deferred.resolve({});
            }, 1500)
            return deferred.promise;
        }
        this.flushUser = function () {
            var deferred = $q.defer();
            this.currentUser = null;
            $window.localStorage["user"] = angular.toJson({});
            $timeout(function () {
                deferred.resolve({});
            }, 700)
            return deferred.promise;
        }
        this.getUser = function () {
            return this.currentUser != null ? this.currentUser : angular.fromJson($window.localStorage["user"]);
        }
    })
})();