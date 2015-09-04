'use strict';


var registerApp = angular.module("registerApp", ['ngResource', 'toaster', 'vcRecaptcha', 'ngMessages'])
    .controller("formController", function ($scope, $resource, toaster, $http, vcRecaptchaService) {
        var emailResource = $resource('/admin/api/email');
        $scope.showModal = false;


        $scope.registerCourse = function (student, courseId) {
            console.log("student::", student);
            console.log("courseId::", courseId);
            $scope.showModal = true;
            $http.post('/admin/api/Register/CourseRegister?recaptchaResponse=' + $scope.response + '&courseId=' + courseId, student).then(
                function () {
                    document.getElementById("registerCourseForm").reset();
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('success', "Đăng ký khóa học thành công, vui lòng kiểm tra email để biết thêm thông tin về khóa học!");
                },
                function () {
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('error', "Đăng ký thất bại, vui lòng thử lại!");
                });
        }

        $scope.sendContact = function (contact) {
            $scope.showModal = true;
            contact.type = "contact";
            $http.post('/admin/api/Register/EmailRegister?recaptchaResponse=' + $scope.response, contact).then(
                function () {
                    document.getElementById("contactForm").reset();
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('success', "Cám ơn bạn đã liên hệ, yêu cầu của bạn đang được xử lý!");
                },
                function () {
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('error', "Liên hệ thất bại, vui lòng thử lại!");
                });
        }


        $scope.registerEmail = function (email) {
            $scope.showModal = true;
            email.type = "register";
            $http.post('/admin/api/Register/EmailRegister?recaptchaResponse=' + $scope.response, email).then(
                function () {
                    document.getElementById("registerEmailForm").reset();
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('success', "Cám ơn bạn đã đăng ký email!");
                },
                function () {
                    $scope.response = null;
                    $scope.showModal = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    toaster.pop('error', "Đăng ký thất bại, vui lòng thử lại sau!");
                });
        }

        $scope.emailPattern = /^[a-z0-9!#$%&'*+\/=?^_`{|}~.-]+@[a-z0-9-]+.[a-z0-9-]/;
        $scope.phoneNumberPattern = /^[0-9]{10}|^[0-9]{11}/;


        $scope.response = null;
        $scope.widgetId = null;
        $scope.setResponse = function (response) {
            //console.info('Response available', response);
            $scope.response = response;

        };

        $scope.setWidgetId = function (widgetId) {
            //console.info('Created widget ID: %s', widgetId);

            $scope.widgetId = widgetId;
        };

        $scope.cbExpiration = function () {
            //console.info('Captcha expired. Resetting response object');
            $scope.response = null;
        };
    })