'use strict';

angular.module("adminApp")
    .controller("bannerController", function ($rootScope, $scope, toaster, bannerService, checkFileNameService,
                                                $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {


        $scope.getAllBanners = function () {
            $rootScope.showModal = true;
            return bannerService.getAllBanners().$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.bannerList = data;
            }, function (response) {
                $rootScope.showModal = false;
                toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getAllBanners();

        $scope.addNewBanner = function (banner) {
            $rootScope.showModal = true;
            if ($scope.HomeBannerImageUrl != "")
                banner.imageUrl = $scope.HomeBannerImageUrl;

            //banner.text1 = CKEDITOR.instances.text1.getData();
            //banner.text2 = CKEDITOR.instances.text2.getData();

            bannerService.addNewBanner(banner).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm Banner " + banner.name + " - " + response.message);
                    $scope.getAllBanners();
                    $location.path("/controlPanel/banner-list");
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.getBanner = function (index) {
            $scope.currentBanner = $scope.bannerList[index];
            //CKEDITOR.instances.text1.setData($scope.currentBanner.text1);
            //CKEDITOR.instances.text2.setData($scope.currentBanner.text2);
            $('html,body').animate({ scrollTop: $('.currentBanner').offset().top });
        }

        $scope.deleteBanner = function (banner) {

            var bodyMessage = "Bạn muốn xóa banner: " + banner.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return bannerService.deleteBanner(banner).$promise.then(
                    function (response) {
                        $scope.currentNews = null;
                        $rootScope.showModal = false;
                        toaster.pop('success', "Thành công!", "Đã xóa Banner " + banner.name + " - " + response.message);
                        $scope.bannerList.splice($scope.bannerList.indexOf(banner), 1);
                    }, function (response) {
                        $rootScope.showModal = false;
                        toaster.pop('error', "Lỗi!", response.data);
                    })
            }, function () { })
        };

        $scope.updateBanner = function (banner) {

            if ($scope.HomeBannerImageUrl != "")
                banner.imageUrl = $scope.HomeBannerImageUrl;

            //banner.text1 = CKEDITOR.instances.text1.getData();
            //banner.text2 = CKEDITOR.instances.text2.getData();

            var bodyMessage = "Bạn muốn cập nhật banner: " + banner.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return bannerService.updateBanner(banner).$promise.then(
               function (response) {
                   $scope.getAllBanners();
                   $rootScope.showModal = false;
                   $scope.currentNews = null;
                   CKEDITOR.instances.text1.setData('');
                   CKEDITOR.instances.text2.setData('');
                   toaster.pop('success', "Thành công!", "Đã cập nhật Banner " + banner.name + " - " + response.message);
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
            DTColumnDefBuilder.newColumnDef(5).notSortable()
        ];

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
                    $("#addNewBanner, #updateBanner").click(function () {
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
    })