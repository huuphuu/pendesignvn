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