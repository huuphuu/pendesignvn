'use strict';

angular.module("adminApp")
    .controller("constructionController", function ($rootScope, $scope, toaster, constructionService, checkFileNameService, $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {

        $scope.allConstructions = {};
        $scope.getallConstructions = function () {
            $rootScope.showModal = true;
            return constructionService.getallConstructions().$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allConstructions = data;
            }, function (response) {
                $rootScope.showModal = true;
                //toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getallConstructions();

        $scope.addNewConstruction = function (construction) {
            $rootScope.showModal = true;
            if ($scope.HomeBannerImageUrl != "")
                construction.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                construction.SubBannerImageUrl = $scope.SubBannerImageUrl;

            construction.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            construction.contentIntro = CKEDITOR.instances.contentIntro.getData();
            construction.contentLeft = CKEDITOR.instances.contentLeft.getData();
            construction.contentRight = CKEDITOR.instances.contentRight.getData();
            construction.newsCategoryId = $scope.newsCategoryId;

            constructionService.addNewConstruction(construction).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm bài viết " + construction.name + " - " + response.message);
                    $scope.getallConstructions();
                    $location.path("/controlPanel/construction-list");
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.getConstruction = function (index) {
            $scope.currentConstruction = $scope.allConstructions[index];
            CKEDITOR.instances.bannerImageIntro.setData($scope.currentConstruction.bannerImageIntro);
            CKEDITOR.instances.contentIntro.setData($scope.currentConstruction.contentIntro);
            CKEDITOR.instances.contentLeft.setData($scope.currentConstruction.contentLeft);
            CKEDITOR.instances.contentRight.setData($scope.currentConstruction.contentRight);
            $('html,body').animate({ scrollTop: $('.editConstruction').offset().top });
        }

        $scope.deleteNews = function (news) {

            var bodyMessage = "Bạn muốn xóa bài viết: " + news.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return constructionService.deleteNews(news).$promise.then(
                function (response) {
                    $scope.currentConstruction = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa bài viết " + news.name + " - " + response.message);
                    $scope.allConstructions.splice($scope.allConstructions.indexOf(news), 1);
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
                return constructionService.updateNews(news).$promise.then(
                function (response) {
                    $scope.getallConstructions();
                    $rootScope.showModal = false;
                    $scope.currentConstruction = null;
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
        $scope.$watch('allConstructions', function (newVal, oldVal) {
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