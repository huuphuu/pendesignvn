'use strict';

angular.module("adminApp")
    .factory("emailService", ['$http', '$q', function ($http, $q) {
        var emailService = {};


        emailService.getAllEmails = function () {
            var deferred = $q.defer();
            $http.get('/admin/api/email/GetAllEmails')
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        }

        emailService.getContactEmails = function () {
            var deferred = $q.defer();
            $http.get('/admin/api/email/getContactEmails')
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        }

        emailService.sendEmail = function (email) {
            var deferred = $q.defer();
            $http.post('/admin/api/email/sendEmail', email)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        emailService.deleteEmail = function (id) {
            var deferred = $q.defer();
            $http.post('admin/api/email/DeleteEmail/'+ id)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

  

        return emailService;
    }])