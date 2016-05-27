'use strict';

angular.module("adminApp")
    .controller("videoController", ['$rootScope', '$scope', 'toaster', 'videoService', 'videoMappingService',
                                    'checkFileNameService', '$sce', '$location', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$stateParams', 'dialogs'
                                    , function ($rootScope, $scope, toaster, videoService, videoMappingService,
                                    checkFileNameService, $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {

        $scope.allVideos = {};
        $scope.getAllVideos = function () {
            $rootScope.showModal = true;
            return videoService.getAllVideos().$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allVideos = data;
            }, function (response) {
                $rootScope.showModal = true;
                //toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getAllVideos();

        $scope.addNewVideo = function (video) {
            $('#uploadBanner').trigger('click');
            $rootScope.showModal = true;
            if ($scope.thumbnail != "")
                video.thumbnail = $scope.thumbnail;

            return videoService.addNewVideo(video).$promise.then(
                function (response) {
                    $scope.getAllVideos();
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm video " + video.name + " - " + response.message);
                    $location.path("/controlPanel/video-list");
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.defaultLanguageId = 129;
        $scope.orderReadonlyIndex = -1;

        //update zorder
        $scope.editZorder = function (index) {
            $scope.orderReadonlyIndex = index;
        }

        $scope.updateZorder = function (index) {
            var currentVideo = $scope.allVideos[index];
            var bodyMessage = "Bạn muốn cập nhật: " + currentVideo.projectImages.projectImageMappings[0].name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return videoService.updateVideo(currentVideo.projectImages).$promise.then(
                   function (response) {
                       $scope.getAllVideos();
                       $scope.orderReadonlyIndex = -1;
                       $rootScope.showModal = false;
                       toaster.pop('success', "Thành công!", "Đã cập nhật Video " + currentVideo.projectImages.projectImageMappings[0].name + " - " + response.message);
                       $('html,body').animate({ scrollTop: 0 });
                   }, function (response) {
                       $rootScope.showModal = false;
                       $scope.orderReadonlyIndex = -1;
                       toaster.pop('error', "Lỗi!", response.data);
                   })
            }, function () { $scope.orderReadonlyIndex = -1; })
        }

        $scope.getVideo = function (index, languageId) {
            var currentVideo = angular.copy($scope.allVideos[index].projectImages);
            for (var i = 0; i < currentVideo.projectImageMappings.length; i++) {
                if (currentVideo.projectImageMappings[i].languageId == languageId)
                    currentVideo.currentImageMapping = currentVideo.projectImageMappings[i];
            }

            $scope.currenVideoLanguage = currentVideo;
            $('html,body').animate({ scrollTop: $('.currenVideoLanguage').offset().top });
        }

        $scope.deleteVideo = function (index, projectImage) {
            console.log("projectImage", projectImage);
            var bodyMessage = "Bạn muốn xóa Video: " + projectImage.projectImageMappings[0].name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return videoService.deleteVideo(projectImage).$promise.then(
                function (response) {
                    $scope.currenVideoLanguage = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa bài viết " + projectImage.projectImageMappings[0].name + " - " + response.message);
                    $scope.allVideos.splice(index, 1);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        };

        $scope.updateVideo = function (video) {
            $('#uploadBanner').trigger('click');
            var languageName = "";
            if (video.currentImageMapping.languageId == 29)
                languageName = "Tiếng Anh";
            else
                languageName = "Tiếng Việt";

          
            var bodyMessage = "Bạn muốn cập nhật bài viết: " + video.currentImageMapping.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                if ($scope.thumbnail != "")
                    video.thumbnail = $scope.thumbnail;

                $rootScope.showModal = true;
                return videoMappingService.updateVideo(video).$promise.then(
                function (response) {
                    $scope.getAllVideos();
                    $scope.currenVideoLanguage = null;
                    $rootScope.showModal = false;
                    
                    toaster.pop('success', "Thành công!", "Đã cập nhật bài viết " + video.currentImageMapping.name + " - " + response.message);
                    $('html,body').animate({ scrollTop: 0 });
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        }

        //DataTable
        $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withPaginationType('full_numbers')
        .withOption('responsive', true)
        .withDisplayLength(10)
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
            //DTColumnDefBuilder.newColumnDef(6).notSortable()
            DTColumnDefBuilder.newColumnDef(5).notSortable()
        ];


        // Uploader Plugin Code
        $scope.thumbnail = "";
        $scope.dropzoneConfigHome = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'maxFilesize': 10000,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#uploadBanner").click(function () {
                        dz.processQueue();
                    });

                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.thumbnail = dz.files[0].name;
                            },
                            function () {
                                $scope.thumbnail = dz.files[0].name;
                                toaster.pop("warning", "Lưu ý!", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
                            }
                        )
                    });
                    this.on("error", function (file, message) {
                        toaster.pop("error", "Lỗi", "Vui lòng chọn ảnh có dung lượng dưới 1 Mb!")
                        this.removeFile(file);
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
                'maxFilesize': 10000,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#addNews").click(function () {
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
                    this.on("error", function (file, message) {
                        toaster.pop("error", "Lỗi", "Vui lòng chọn ảnh có dung lượng dưới 1 Mb!")
                        this.removeFile(file);
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

    }])