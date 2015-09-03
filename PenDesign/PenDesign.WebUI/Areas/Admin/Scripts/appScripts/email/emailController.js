'use strict';

angular.module("adminApp")
    .controller('emailController', function ($rootScope, $scope, toaster, emailService, $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs) {

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

       
        $scope.getAllEmails = function () {
            $rootScope.showModal = true;
            return emailService.getAllEmails().then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.emailList = data;
                },
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response);
                });
        };
        $scope.getAllEmails();


        //$scope.getContactEmails = function () {
        //    return emailService.getContactEmails().then(
        //        function (data) {
        //            console.log("contactEmailList", data);
        //            $scope.contactEmailList = data;
        //        },
        //        function (response) {
        //            //toaster.pop('error', "Lỗi!", response);
        //        });
        //};
        //$scope.getContactEmails();

        $scope.getToEmail = function (index) {
            $scope.currentEmail = $scope.emailList[index];
            $('html,body').animate({ scrollTop: $('.sendEmail').offset().top });
        }
        $scope.getContactDetail = function (index) {
            $scope.currentContactDetail = $scope.contactEmailList[index];
            $('html,body').animate({ scrollTop: $('.sendEmail').offset().top });
        }

        $scope.deleteEmail = function (email) {

            var bodyMessage = "Bạn muốn xóa email: " + email.email + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });
            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return emailService.deleteEmail(email.id).then(
               function (data) {
                   $rootScope.showModal = false;
                   $scope.emailList.splice($scope.emailList.indexOf(email), 1);
                   toaster.pop('success', "Thành công!", "Đã xóa email: " + email.email);
                   $('html,body').animate({ scrollTop: 0 });
               },
               function(response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response);
               });
            }, function () { })
        }

        $scope.sendEmail = function (currentEmail, sendEmailObj) {

            sendEmailObj.toEmail = currentEmail;
            sendEmailObj.body = CKEDITOR.instances.body.getData();

            console.log("email ", sendEmailObj);
            var bodyMessage = "Bạn muốn gửi email đến: " + currentEmail + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return emailService.sendEmail(sendEmailObj).then(
               function (data) {
                   $rootScope.showModal = false;
                   $scope.currentEmail = null;
                   toaster.pop('success', "Thành công!", "Đã gửi email đến: " + currentEmail);
                   $('html,body').animate({ scrollTop: 0 });
               },
               function(response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response);
               });
            }, function() {})
        }

    })