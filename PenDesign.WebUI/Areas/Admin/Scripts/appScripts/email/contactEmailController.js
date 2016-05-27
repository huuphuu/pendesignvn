'use strict';

angular.module("adminApp")
    .controller('contactEmailController', ['$rootScope', '$scope', 'toaster', 'emailService', '$location', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'dialogs',
                                            function ($rootScope, $scope, toaster, emailService, $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs) {

        ////////////////////////////////////////////////////////////
        //DataTable
        ////////////////////////////////////////////////////////////
        $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withPaginationType('full_numbers')
        .withOption('responsive', true)
        .withDisplayLength(10)
        .withOption('processing', true)
        .withDataProp('aaData')
        .withLanguageSource('/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/vnLanguageDataTable.json')
        .withTableTools('/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/copy_csv_xls_pdf.swf')
        .withTableToolsButtons([
            'copy',
            'print', {
                'sExtends': 'collection',
                'sButtonText': 'Save',
                'aButtons': ['csv', 'xls', 'pdf']
            }
        ]);

        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            //DTColumnDefBuilder.newColumnDef(1).notVisible(),
            DTColumnDefBuilder.newColumnDef(3).notSortable()
        ];

        $scope.getContactEmails = function () {
            $rootScope.showModal = true;
            return emailService.getContactEmails().then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.contactEmailList = data;
                },
                function (response) {
                    $rootScope.showModal = false;
                    //toaster.pop('error', "Lỗi!", response);
                });
        };
        $scope.getContactEmails();

        $scope.getToEmail = function (index) {
            $scope.currentEmail = $scope.contactEmailList[index];
            $('html,body').animate({ scrollTop: $('.sendEmail').offset().top });
        }

        $scope.getContactDetail = function (index) {
            $scope.currentContactDetail = $scope.contactEmailList[index];
            $('html,body').animate({ scrollTop: $('.contactDetail').offset().top });
        }

        $scope.deleteEmail = function (email) {

            var bodyMessage = "Bạn muốn xóa tin nhắn từ: " + email.email + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });
            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return emailService.deleteEmail(email.id).then(
               function (data) {
                   $rootScope.showModal = false;
                   $scope.contactEmailList.splice($scope.contactEmailList.indexOf(email), 1);
                   toaster.pop('success', "Thành công!", "Đã xóa email: " + email.email);
                   $('html,body').animate({ scrollTop: 0 });
               },
               function(response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response);
               });
            }, function () { })
        }

        $scope.sendEmail = function (email) {
            email.toEmail = $scope.currentEmail.email;
            email.body = CKEDITOR.instances.body.getData();

            var bodyMessage = "Bạn muốn gửi email đến: " + $scope.currentEmail.email + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return emailService.sendEmail(email).then(
               function (data) {
                   toaster.pop('success', "Thành công!", "Đã gửi email đến: " + $scope.currentEmail.email);
                   $scope.currentEmail = null;
                   $rootScope.showModal = false;
                   
                   $('html,body').animate({ scrollTop: 0 });
               },
               function(response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response);
               });
            }, function() {})
        }

    }])