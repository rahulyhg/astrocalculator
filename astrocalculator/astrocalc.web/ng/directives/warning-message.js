(function () {
    var warningMessage = angular.module("astroapp").directive("warningMessage", function () {
        return {
            restrict: "EA",
            replace:true,
            scope: {
                display: "=",
                message:"="
            },
            templateUrl: "/ng/templates/warning-message.html",
        }
    })
})();