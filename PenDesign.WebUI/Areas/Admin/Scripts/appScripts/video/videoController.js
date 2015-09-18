'use strict';

angular.module("adminApp")
    .controller("newsController", function ($rootScope, $scope, toaster, videoService, checkFileNameService, $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {

        $scope.getAllVideos = function () {
            $rootScope.showModal = true;
            return videoService.getAllVideos().$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allNews = data;
            }, function (response) {
                $rootScope.showModal = true;
                //toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getAllVideos();

        $scope.addNewVideo = function (video) {
            $rootScope.showModal = true;
            if ($scope.HomeBannerImageUrl != "")
                video.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                video.SubBannerImageUrl = $scope.SubBannerImageUrl;

            video.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            video.contentIntro = CKEDITOR.instances.contentIntro.getData();
            video.contentLeft = CKEDITOR.instances.contentLeft.getData();
            video.contentRight = CKEDITOR.instances.contentRight.getData();
            video.newsCategoryId = $scope.newsCategoryId;

            videoService.addNewVideo(video).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm bài viết " + video.name + " - " + response.message);
                    $scope.getAllVideos();
                    $location.path("/controlPanel/news-list/" + $scope.newsCategoryId);
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.getNews = function (index) {
            $scope.currentVideo = $scope.allNews[index];
            CKEDITOR.instances.bannerImageIntro.setData($scope.currentVideo.bannerImageIntro);
            CKEDITOR.instances.contentIntro.setData($scope.currentVideo.contentIntro);
            CKEDITOR.instances.contentLeft.setData($scope.currentVideo.contentLeft);
            CKEDITOR.instances.contentRight.setData($scope.currentVideo.contentRight);
            $('html,body').animate({ scrollTop: $('.editNews').offset().top });
        }

        $scope.deleteVideo = function (news) {

            var bodyMessage = "Bạn muốn xóa bài viết: " + news.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return videoService.deleteVideo(news).$promise.then(
                function (response) {
                    $scope.currentVideo = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa bài viết " + news.name + " - " + response.message);
                    $scope.allNews.splice($scope.allNews.indexOf(news), 1);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        };

        $scope.updateNews = function (news) {

            if ($scope.HomeBannerImageUrl != "")
                news.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                news.SubBannerImageUrl = $scope.SubBannerImageUrl;
            news.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            news.contentIntro = CKEDITOR.instances.contentIntro.getData();
            news.contentLeft = CKEDITOR.instances.contentLeft.getData();
            news.contentRight = CKEDITOR.instances.contentRight.getData();

            var bodyMessage = "Bạn muốn cập nhật bài viết: " + news.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return videoService.updateNews(news).$promise.then(
                function (response) {
                    $scope.getAllVideos();
                    $rootScope.showModal = false;
                    $scope.currentVideo = null;
                    CKEDITOR.instances.bannerImageIntro.setData('');
                    CKEDITOR.instances.contentIntro.setData('');
                    CKEDITOR.instances.contentLeft.setData('');
                    CKEDITOR.instances.contentRight.setData('');
                    toaster.pop('success', "Thành công!", "Đã cập nhật bài viết " + news.name + " - " + response.message);
                    $('html,body').animate({ scrollTop: 0 });
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        }

        //DataTable
        $scope.$watch('allNews', function (newVal, oldVal) {
            if (newVal && newVal != null) {
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
                    DTColumnDefBuilder.newColumnDef(6).notSortable()
                ];
            }
        })

        // Uploader Plugin Code
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
                    $("#addNews").click(function () {
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