'use strict';

angular.module("adminApp")
    .factory("accountService", ['$resource', '$http', '$q', function ($resource, $http, $q) {
        var accountService = {};

        //Account Manager
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

        accountService.getUserInfo = function () {
            var deferred = $q.defer();
            $http.get('admin/api/userAccount/getUserInfo')
                .success(deferred.resolve)
                .error(deferred.reject)
            return deferred.promise;
        }


        accountService.updateUser = function (user) {
            var deferred = $q.defer();
            $http.post('api/userAccount/UpdateUser', user)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.deleteUser = function (user) {
            var deferred = $q.defer();
            $http.post('api/userAccount/DeleteUser',user)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.register = function (user) {
            var deferred = $q.defer();
            $http.post('/account/register/', user)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.logOff = function () {
            var deferred = $q.defer();
            $http.post('/account/logOff')
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        //Role manager
        accountService.getAllUsers = function () {
            var deferred = $q.defer();
            $http.get('/api/userAccount/GetAllUsers')
                .success(deferred.resolve)
                .error(deferred.reject)
            return deferred.promise;
        }


        accountService.getAllRoles = function () {
            var deferred = $q.defer();
            $http.get('admin/api/userAccount/GetAllRoles')
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.getRole = function (roleName) {
            var deferred = $q.defer();
            $http.get('/admin/api/userAccount/GetRole', roleName)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.createRole = function (roleName) {
            var deferred = $q.defer();
            $http.post('/admin/api/userAccount/CreateRole/', roleName)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.deleteRole = function (roleName) {
            var deferred = $q.defer();
            $http.post('/admin/api/userAccount/DeleteRole/', roleName)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.editRole = function (roleObj) {
            var deferred = $q.defer();
            $http.put('/admin/api/userAccount/EditRole/', roleObj)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.roleAddToUser = function (roleName, userName) {
            var deferred = $q.defer();
            $http.post('/admin/api/userAccount/RoleAddToUser/', { roleName: roleName, userName: userName })
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.getUserRoles = function (userName) {
            var deferred = $q.defer();
            $http.get('/admin/api/userAccount/GetUserRoles/', userName)
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        accountService.DeleteRoleForUser = function (roleName, userName) {
            var deferred = $q.defer();
            $http.post('/admin/api/userAccount/DeleteRoleForUser/', { roleName: roleName, userName: userName })
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        }

        //Account Manager
        accountService.changePassword = function (user) {
            var deferred = $q.defer();
            $http.post('/admin/api/userAccount/ChangePassword/', user)
                .success(deferred.resolve)
                .error(deferred.reject)
            return deferred.promise;
        }

        return accountService;
    }])