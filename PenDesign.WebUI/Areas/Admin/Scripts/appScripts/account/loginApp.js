'use strict';

angular.module("loginApp", [])
    .controller('loginController', function ($scope, loginService, $window) {
        $scope.avatar = "";
        $scope.clicked = false;
        $scope.signIn = function (user) {
            $scope.clicked = true;
            $(".feedback-loading").show().animate({ "opacity": "1", "bottom": "-80px" }, 400);
            loginService.login(user).then(
                function (data, status, headers, config) {
                    //$location.path("/home/index");
                    $(".feedback-loading").hide().animate({ "opacity": "1", "bottom": "-80px" }, 400);
                    $scope.error = false;
                    if ($scope.error == false) {
                        $(".feedback-success").show().animate({ "opacity": "1", "bottom": "-80px" }, 400);
                        $(".feedback-error").hide().animate({ "opacity": "1", "bottom": "-80px" }, 400);
                    }
                    $window.location.href = "/Admin-area/";
                }, function (status, headers, config) {
                    $scope.clicked = false;
                    $scope.error = true;
                    $(".feedback-loading").hide().animate({ "opacity": "1", "bottom": "-80px" }, 400);
                    if ($scope.error == true) {
                        $(".feedback-error").show().animate({ "opacity": "1", "bottom": "-80px" }, 400);
                        $(".feedback-success").hide().animate({ "opacity": "1", "bottom": "-80px" }, 400);
                    }
                        
                })
        }
    })
    .factory("loginService", ['$http', '$q', function ($http, $q) {
        var accountService = {};

        accountService.login = function (user) {
            var deferred = $q.defer();
            $http.post('/account/login/', user)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        return accountService;
    }])
