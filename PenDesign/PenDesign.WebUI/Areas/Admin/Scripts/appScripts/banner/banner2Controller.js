'use strict';

angular.module("adminApp")
    .controller("banner2Controller", function ($rootScope, $scope, toaster, bannerService, checkFileNameService,
                                                $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs) {


        $scope.gridInfo = {
            gridID: 'bannergrid',
            table: null,
            cols: [
                { name: 'id', heading: '#', width: '2%', className: 'text-center' },
                { name: 'name', heading: 'Tên', width: '2%', className: 'text-center' },
                { name: 'mediaUrl', heading: 'Hình ảnh', width: '2%', className: 'text-center', type: controls.IMAGE },
                { name: 'type', heading: 'Trang', width: '2%', className: 'text-center' },
                { name: 'zOrder', heading: 'Vị trí', width: '2%', className: 'text-center' },
                { name: 'Action', heading: 'Thao tác', width: '3%', className: 'text-left', type: controls.LIST_ICON, listAction: [{ classButton: 'btn-warning', classIcon: 'fa fa-edit', action: 'getBanner', title: 'Sửa Banner' }, { classButton: 'btn-danger', classIcon: 'fa fa-times', action: 'deleteBanner', title: 'Xóa Banner' }] },

            ],
            showColMin: 6,
            data: [],
            sysViewID: 5,
            pageLength: 16,
            searchQuery: '',
            onActionClick: function (rowID, act) {
                var i = 0;
                for (; i < $scope.gridInfo.data.length; i++)
                    if ($scope.gridInfo.data[i].id == rowID) {
                        $scope.dataSeleted = $scope.gridInfo.data[i];
                        break;
                    }

                switch (act) {
                    case 'getBanner':
                        $scope.getBanner(i)
                        break;
                    case 'deleteBanner':
                        $scope.deleteBanner($scope.dataSeleted);
                        break;
                }
            },
            setData: function (row, col) {//ham nay dung xoa
            },

            getGridData: function () {
                $rootScope.showModal = true;
                return bannerService.getAllBanners().$promise.then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.gridInfo.display(data);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                });
            }
        }

        $scope.searchTable = function () {
            var query = $scope.gridInfo.searchQuery;
            $scope.gridInfo.tableInstance.search(query).draw();
        };


        $scope.gridInfo.getGridData();

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
            if ($scope.mediaUrl != "")
                banner.imageUrl = $scope.mediaUrl;

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

            if ($scope.mediaUrl != "")
                banner.mediaUrl = $scope.mediaUrl;

            var bodyMessage = "Bạn muốn cập nhật banner: " + banner.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return bannerService.updateBanner(banner).$promise.then(
               function (response) {
                   $scope.getAllBanners();
                   $rootScope.showModal = false;
                   $scope.currentBanner = null;
                   toaster.pop('success', "Thành công!", "Đã cập nhật Banner " + banner.name + " - " + response.message);
                   $('html,body').animate({ scrollTop: 0 });
               }, function (response) {
                   $rootScope.showModal = false;
                   toaster.pop('error', "Lỗi!", response.data);
               })
            }, function () { })
        }

        // Uploader Plugin Code
        $scope.mediaUrl = "";
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
                                $scope.mediaUrl = dz.files[0].name;
                            },
                            function () {
                                $scope.mediaUrl = dz.files[0].name;
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