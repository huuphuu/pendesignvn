'use strict';

angular.module("adminApp")
    .controller("galleryController", function ($rootScope, $scope, toaster, galleryService, checkFileNameService, $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {

        $scope.galleryCategoryId = $stateParams.galleryCategoryId;
        var categoryName = "";
        if ($scope.galleryCategoryId == 1)
            categoryName = "Hình ảnh";
        if ($scope.galleryCategoryId == 2)
            categoryName = "Video Clip";
        $scope.categoryName = categoryName;


        $scope.addNewGallery = function (gallery) {
            $rootScope.showModal = true;
            if ($scope.previewImage != "")
                gallery.previewImage = $scope.previewImage;

            gallery.galleryCategoryId = $scope.galleryCategoryId;

            return galleryService.addNewGallery(gallery).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm " + gallery.name + response.message);
                    $scope.getAllGalleries();
                    $location.path("/controlPanel/gallery-list/" + +$scope.galleryCategoryId);
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.allGalleries = {};
        $scope.getAllGalleries = function () {
            $rootScope.showModal = true;
            return galleryService.getAllGalleries($scope.galleryCategoryId).$promise.then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.allGalleries = data;
                }, function (response) {
                    $rootScope.showModal = false;
                    //toaster.pop('error', "Lỗi!", response.data);
                }); 
        };
        $scope.getAllGalleries();

        $scope.getGallery = function (index) {
            $scope.currentGallery = $scope.allGalleries[index];
            $('html,body').animate({ scrollTop: $('.editGallery').offset().top });
        };

        $scope.deleteGallery = function (gallery) {
            var bodyMessage = "Bạn muốn xóa: " + gallery.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return galleryService.deleteGallery(gallery).$promise.then(
                function (response) {
                    $scope.currentGallery = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa " + gallery.name + " - " + response.message);
                    $scope.allGalleries.splice($scope.allGalleries.indexOf(gallery), 1);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        };

        $scope.updateGallery = function (gallery) {

            if ($scope.previewImage != "")
                gallery.HomeBannerImageUrl = $scope.previewImage;

            var bodyMessage = "Bạn muốn cập nhật: " + gallery.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return galleryService.updateGallery(gallery).$promise.then(
                function (response) {
                    $scope.getAllGalleries();
                    $rootScope.showModal = false;
                    $scope.currentGallery = null;
                    toaster.pop('success', "Thành công!", "Đã cập nhật " + gallery.name + " - " + response.message);
                    $('html,body').animate({ scrollTop: 0 });
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        }

        //DataTable
        $scope.$watch('allGalleries', function (newVal, oldVal) {
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
                    DTColumnDefBuilder.newColumnDef(8).notSortable()
                ];
            }
        })



        //$scope.dtOptions = DTOptionsBuilder.newOptions()
        //.withPaginationType('full_numbers')
        //.withOption('responsive', true)
        //.withDisplayLength(10)
        //.withLanguageSource('/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/vnLanguageDataTable.json')
        //.withTableTools('/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/copy_csv_xls_pdf.swf')
        //.withTableToolsButtons([
        //    'copy',
        //    'print', {
        //        'sExtends': 'collection',
        //        'sButtonText': 'Save',
        //        'aButtons': ['csv', 'xls', 'pdf']
        //    }
        //]);

        //$scope.dtColumnDefs = [
        //    DTColumnDefBuilder.newColumnDef(0),
        //    //DTColumnDefBuilder.newColumnDef(1).notVisible(),
        //    DTColumnDefBuilder.newColumnDef(8).notSortable()
        //];

        // Uploader Plugin Code
        $scope.previewImage = "";
        $scope.dropzoneConfigHome = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#addGallery").click(function () {
                        dz.processQueue();
                    });

                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.previewImage = dz.files[0].name;
                            },
                            function () {
                                $scope.previewImage = dz.files[0].name;
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