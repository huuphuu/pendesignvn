'use strict';


var registerApp = angular.module("registerApp", ['ngResource', 'toaster', 'vcRecaptcha', 'ngMessages'])
    .controller("formController", ['$scope', '$resource', 'toaster', '$http', 'vcRecaptchaService', function ($scope, $resource, toaster, $http, vcRecaptchaService) {
        $scope.showModal = false;

        $scope.sendContact = function (contact) {
            $scope.showModal = true;
            $http.post('/api/Contact/EmailRegister?recaptchaResponse=' + $scope.response, contact).then(
                function () {
                    document.getElementById("form").reset();
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('success', "Cám ơn bạn đã liên hệ, yêu cầu của bạn đang được xử lý!");
                },
                function () {
                    document.getElementById("form").reset();
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('error', "Liên hệ thất bại, vui lòng thử lại!");
                });
        }

        $scope.emailPattern = /^[a-z0-9!#$%&'*+\/=?^_`{|}~.-]+@[a-z0-9-]+.[a-z0-9-]/;
        $scope.phoneNumberPattern = /^[0-9]{10}|^[0-9]{11}/;


        $scope.response = null;
        $scope.widgetId = null;
        $scope.setResponse = function (response) {
            console.info('Response available', response);
            $scope.response = response;

        };

        $scope.setWidgetId = function (widgetId) {
            console.info('Created widget ID: %s', widgetId);

            $scope.widgetId = widgetId;
        };

        $scope.cbExpiration = function () {
            console.info('Captcha expired. Resetting response object');
            $scope.response = null;
        };
    }])