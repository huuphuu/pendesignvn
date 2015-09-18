/// <reference path="configController.js" />
'use strict';

angular.module("adminApp")
    .controller("configController", function ($rootScope, $scope, toaster, configService, checkFileNameService,
                                                $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {


        $scope.getConfig = function () {
            $rootScope.showModal = true;
            return configService.getConfig().$promise.then(
            function (data) {
                console.log("config", data[0]);
                $rootScope.showModal = false;
                $scope.config = data[0];

                CKEDITOR.instances.about.setData($scope.config.about);
                CKEDITOR.instances.footerContent.setData($scope.config.footerContent);
                CKEDITOR.instances.contactForm.setData($scope.config.contactForm);
                //CKEDITOR.instances.registerForm.setData($scope.config.registerForm);
                CKEDITOR.instances.emailSignature.setData($scope.config.emailSignature);

            }, function (response) {
                $rootScope.showModal = false;
                toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getConfig();


        $scope.updateConfig = function (config) {

            if ($scope.logoUrl != "")
                config.logoUrl = $scope.logoUrl;
            if ($scope.bannerLogo != "")
                config.bannerLogo = $scope.bannerLogo;

            config.about = CKEDITOR.instances.about.getData();
            config.footerContent = CKEDITOR.instances.footerContent.getData();
            config.contactForm = CKEDITOR.instances.contactForm.getData();
            config.registerForm = CKEDITOR.instances.registerForm.getData();
            config.emailSignature = CKEDITOR.instances.emailSignature.getData();


            var bodyMessage = "Hành động này sẽ <strong>cập nhật tất cả nội dung trong trang hiện tại. </strong> <br />Nếu bạn chỉ chỉnh sửa Phần này, vui lòng <b>KHÔNG</b> chỉnh sửa các Phần khác. <br />Bạn muốn cập nhật cấu hình?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return configService.updateConfig(config).$promise.then(
               function (response) {
                   $scope.getConfig();
                   $rootScope.showModal = false;


                   toaster.pop('success', "Thành công!", "Đã cập nhật cấu hình!");
                   $('html,body').animate({ scrollTop: 0 });
               }, function (response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response.data);
               })
            }, function () { })
        }

        // Uploader Plugin Code
        $scope.logoUrl = "";
        $scope.bannerLogo = "";
        $scope.dropzoneConfigHome = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#updateConfigGeneral").click(function () {
                        dz.processQueue();
                    });

                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.logoUrl = dz.files[0].name;
                            },
                            function () {
                                $scope.logoUrl = dz.files[0].name;
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
                    $("#updateConfigGeneral").click(function () {
                        dz.processQueue();
                    });

                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.bannerLogo = dz.files[0].name;
                            },
                            function () {
                                $scope.bannerLogo = dz.files[0].name;
                                toaster.pop("warning", "Chú ý!", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
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
    })