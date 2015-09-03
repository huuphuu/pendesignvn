'use strict';

angular.module("adminApp")
    .controller('notificationController', function ($rootScope, $scope, toaster, notificationService, checkFileNameService, $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs) {
        //demo dialog
        //$scope.launch = function (which, obj) {
        //    switch (which) {
        //        case 'confirm':
        //            if (obj.draftType == "Delete")
        //                var bodyMessage = "Bạn muốn xóa bài viết: " + obj.name + " ?";
        //            if (obj.draftType == "Update")
        //                var bodyMessage = "Bạn muốn cập nhật bài viết: " + obj.name + " ?";
        //            var dlg = dialogs.confirm('Xác nhận thao tác', bodyMessage);

        //            dlg.result.then(function (btn) {
        //                $scope.alert(obj);
        //            }, function (btn) {
        //                $scope.confirmed = function () { console.log("no"); };
        //            });
        //            break;
        //        case 'custom':
        //            var dlg = dialogs.create('/Areas/Admin/Templates/dialogs/confirmDialog.html', 'notificationController', {}, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });
        //            dlg.result.then(function (name) {
        //                $scope.name = name;
        //            }, function () {
        //                if (angular.equals($scope.name, ''))
        //                    $scope.name = 'You did not enter in your name!';
        //            });
        //            break;
        //        case 'custom2':
        //            var dlg = dialogs.create('/dialogs/custom2.html', 'customDialogCtrl2', $scope.custom, { size: 'lg' });
        //            break;
        //    }
        //}; // end launch

        $scope.alert = function (obj) {
            console.log("obj", obj);
        }
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
            DTColumnDefBuilder.newColumnDef(6).notSortable()
        ];

        ////////////////////////////////////////////////////////////
        //Dropzone
        ////////////////////////////////////////////////////////////
        $scope.HomeBannerImageUrl = "";
        $scope.SubBannerImageUrl = "";
        $scope.dropzoneConfigHome = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#addNotification, #updateNotification").click(function () {
                        dz.processQueue();
                    });
                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.HomeBannerImageUrl = dz.files[0].name;
                            },
                            function () {
                                $scope.HomeBannerImageUrl = dz.files[0].name;
                                toaster.pop("warning", "Lỗi", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
                            }
                        )
                    });


                }
            },
            'eventHandlers': {
                'sending': function (file, xhr, formData) {
                },
                'success': function (file, response) {
                    if (this.files[0] != null) {
                        this.removeFile(this.files[0]);
                    }
                }
            }
        };

        $scope.dropzoneConfigSub = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#addNotification, #updateNotification").click(function () {
                        dz.processQueue();
                    });
                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.SubBannerImageUrl = dz.files[0].name;
                            },
                            function () {
                                $scope.SubBannerImageUrl = dz.files[0].name;
                                toaster.pop("warning", "Lỗi", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
                            }
                        )
                    });
                }
            },
            'eventHandlers': {
                'sending': function (file, xhr, formData) {

                },
                'success': function (file, response) {
                    if (this.files[0] != null) {
                        this.removeFile(this.files[0]);
                    }
                }
            }
        };



        $scope.getAllNotifications = function () {
            $rootScope.showModal = true;
            return notificationService.getAllNotifications().then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.notificationList = data;
                },
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi! Không thể tải thông báo mới!", response);
                });
        };
        $scope.getAllNotifications();


        $scope.getEditingNotification = function (index) {
            $scope.currentCheckingNotification = $scope.notificationList.newsDrafts[index];
            CKEDITOR.instances.bannerImageIntro.setData($scope.currentCheckingNotification.bannerImageIntro);
            CKEDITOR.instances.contentIntro.setData($scope.currentCheckingNotification.contentIntro);
            CKEDITOR.instances.contentLeft.setData($scope.currentCheckingNotification.contentLeft);
            CKEDITOR.instances.contentRight.setData($scope.currentCheckingNotification.contentRight);
            $('html,body').animate({ scrollTop: $('.editNotification').offset().top });
        }

        $scope.updateNotification = function (notification) {

            if ($scope.HomeBannerImageUrl != "")
                notification.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                notification.SubBannerImageUrl = $scope.SubBannerImageUrl;

            notification.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            notification.contentIntro = CKEDITOR.instances.contentIntro.getData();
            notification.contentLeft = CKEDITOR.instances.contentLeft.getData();
            notification.contentRight = CKEDITOR.instances.contentRight.getData();

            var bodyMessage = "Bạn muốn cập nhật: " + notification.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return notificationService.updateNotification(notification).then(
               function (data) {
                   $rootScope.showModal = false;
                   $scope.currentCheckingNotification = null;
                   $scope.getAllNotifications();
                   toaster.pop('success', "Thành công!", "Đã chỉnh sửa bài viết: " + notification.name);
                   $('html,body').animate({ scrollTop: 0 });
               },
               function (response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response);
               });
            }, function () { })
        }



        $scope.acceptNotification = function (notification) {

            if (notification.draftType == "Add")
                var bodyMessage = "Bạn muốn thêm bài viết: " + notification.name + " ?";
            if (notification.draftType == "Delete")
                var bodyMessage = "Bạn muốn xóa bài viết: " + notification.name + " ?";
            if (notification.draftType == "Update")
                var bodyMessage = "Bạn muốn cập nhật bài viết: " + notification.name + " ?";
            var dlg = dialogs.confirm('Xác nhận thao tác', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return notificationService.updateNotification(notification).then(
                    function (data) {
                        $rootScope.showModal = false;
                        $scope.$emit("updateNotification", function () { });
                        $scope.getAllNotifications();
                        toaster.pop('success', "Thành công!", "Đã phê duyệt bài viết: " + notification.name);
                    },
                    function (response) {
                        $rootScope.showModal = false;
                         toaster.pop('error', "Lỗi!", response);
                    });
            }, function (btn) {
            });

        }


        $scope.ignoreNotification = function (notification) {

            var bodyMessage = "Bạn muốn hủy bỏ thao tác: " + notification.name + " ?";
            var dlg = dialogs.confirm('Hủy bỏ thao tác', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return notificationService.ignoreNotification(notification).then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.getAllNotifications();
                    toaster.pop('success', "Thành công!", "Đã bỏ thao tác " + notification.draftType + " của bài viết " + notification.name);
                },
                function(response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response);
                });
            }, function (btn) { })
        }

    })