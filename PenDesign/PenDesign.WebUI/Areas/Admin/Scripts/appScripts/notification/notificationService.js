'use strict';

angular.module("adminApp")
    .factory("notificationService", ['$http', '$q', function ($http, $q) {
        var notificationService = {};


        notificationService.getAllNotifications = function () {
            var deferred = $q.defer();
            $http.get('/admin/api/Notification/getAllNotification')
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        }

        notificationService.updateNotification = function (notification) {
            var deferred = $q.defer();
            $http.post('/admin/api/Notification/Update', notification)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        notificationService.updateNotificationList = function (notificationList) {
            var deferred = $q.defer();
            $http.post('admin/api/Notification/UpdateList', notificationList)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        notificationService.ignoreNotification = function (notification) {
            var deferred = $q.defer();
            $http.post('/admin/api/Notification/ignore', notification)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

    notificationService.adminChecked = function (notification) {
            var deferred = $q.defer();
            $http.post('/admin/api/Notification/adminChecked', notification)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        return notificationService;
    }])