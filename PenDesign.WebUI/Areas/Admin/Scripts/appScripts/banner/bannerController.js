'use strict';

angular.module("adminApp")
    .controller("bannerController", function ($rootScope, $scope, toaster, bannerService, bannerMappingService, checkFileNameService,
                                                $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs, $state) {


        //$scope.gridInfo = {
        //    gridID: 'bannergrid',
        //    table: null,
        //    cols: [
        //        { name: 'id', heading: '#', width: '2%', className: 'text-center' },
        //        { name: 'name', heading: 'Tên', width: '2%', className: 'text-center' },
        //        { name: 'mediaUrl', heading: 'Hình ảnh', width: '2%', className: 'text-center', type: controls.IMAGE },
        //        //{ name: 'type', heading: 'Trang', width: '2%', className: 'text-center' },
        //        { name: 'zOrder', heading: 'Vị trí', width: '2%', className: 'text-center' },
        //        { name: 'linkUrl', heading: 'Link trỏ đến', width: '2%', className: 'text-left' },
        //        { name: 'languageId', heading: 'Ngôn ngữ', width: '2%', className: 'text-left' },
        //        { name: 'Action', heading: 'Thao tác', width: '3%', className: 'text-left', type: controls.LIST_ICON, listAction: [{ classButton: 'btn-warning', classIcon: 'fa fa-edit', action: 'getBanner', title: 'Sửa Banner' }, { classButton: 'btn-danger', classIcon: 'fa fa-times', action: 'deleteBanner', title: 'Xóa Banner' }] },

        //    ],
        //    showColMin: 6,
        //    data: [],
        //    sysViewID: 5,
        //    pageLength: 16,
        //    searchQuery: '',
        //    onActionClick: function (rowID, act) {
        //        var i = 0;
        //        for (; i < $scope.gridInfo.data.length; i++)
        //            if ($scope.gridInfo.data[i].id == rowID) {
        //                $scope.dataSeleted = $scope.gridInfo.data[i];
        //                break;
        //            }

        //        switch (act) {
        //            case 'getBanner':
        //                $scope.getBanner(i)
        //                break;
        //            case 'deleteBanner':
        //                $scope.deleteBanner($scope.dataSeleted);
        //                break;
        //        }
        //    },
        //    setData: function (row, col) {//ham nay dung xoa
        //    },

        //    getGridData: function () {
        //        $rootScope.showModal = true;
        //        return bannerService.getAllBanners().$promise.then(
        //        function (data) {
        //            $rootScope.showModal = false;
        //            $scope.gridInfo.display(data);
        //        }, function (response) {
        //            $rootScope.showModal = false;
        //            toaster.pop('error', "Lỗi!", response.data);
        //        });
        //    }
        //}


        //$scope.searchTable = function () {
        //    var query = $scope.gridInfo.searchQuery;
        //    $scope.gridInfo.tableInstance.search(query).draw();
        //};
        //if ($state.current.url != "/add-banner")
        //    $scope.gridInfo.getGridData();


        $scope.defaultLanguageId = 129;
        $scope.orderReadonlyIndex = -1;

        $scope.getAllBanners = function () {
            $rootScope.showModal = true;
            return bannerService.getAllBanners().$promise.then(
            function (data) {
                $scope.bannerList = eval(angular.toJson(data));
                //$scope.gridInfo.display($scope.bannerList);
                $rootScope.showModal = false;
            }, function (response) {
                $rootScope.showModal = false;
                toaster.pop('error', "Lỗi!", response.data);
            });
        }
        //  $scope.getAllBanners();
        if ($state.current.url != "/add-banner")
            $scope.getAllBanners();

        //update zorder
        $scope.editZorder = function (index) {
            $scope.orderReadonlyIndex = index;
        }

        $scope.updateZorder = function (index) { //zOrder
            var currentBanner = $scope.bannerList[index];
            //currentBanner.banners.zOrder = zOrder;
            console.log("currentBanner",currentBanner);
            var bodyMessage = "Bạn muốn cập nhật: " + currentBanner.banners.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return bannerService.updateBanner(currentBanner.banners).$promise.then(
                   function (response) {
                       $scope.getAllBanners();
                       $scope.orderReadonlyIndex = -1;
                       $rootScope.showModal = false;
                       toaster.pop('success', "Thành công!", "Đã cập nhật Banner " + currentBanner.banners.name + " - " + response.message);
                       $('html,body').animate({ scrollTop: 0 });
                   }, function (response) {
                       $rootScope.showModal = false;
                       $scope.orderReadonlyIndex = -1;
                       toaster.pop('error', "Lỗi!", response.data);
                   })
            }, function () { })
        }

        $scope.addNewBanner = function (banner) {
            $rootScope.showModal = true;
            if ($scope.mediaUrl != "")
                banner.mediaUrl = $scope.mediaUrl;

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

        $scope.getBanner = function (index, languageId) {
            //$scope.currentBanner = $scope.bannerList[index];
            //$('html,body').animate({ scrollTop: $('.currentBanner').offset().top });

            var currentBanner = $scope.bannerList[index].banners.bannerMappings;
            for (var i = 0; i < currentBanner.length; i++) {
                if (currentBanner[i].languageId == languageId)
                    $scope.currentBannerLanguage = currentBanner[i];
            }
            $('html,body').animate({ scrollTop: $('.currentBannerLanguage').offset().top });
        }

        $scope.deleteBanner = function (index,banner) {

            var bodyMessage = "Bạn muốn xóa banner: " + banner.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return bannerService.deleteBanner(banner).$promise.then(
                    function (response) {
                        $rootScope.showModal = false;
                        toaster.pop('success', "Thành công!", "Đã xóa Banner " + banner.name + " - " + response.message);
                        //$scope.bannerList.splice($scope.bannerList.indexOf(banner), 1);
                        $scope.bannerList.splice(index, 1);
                    }, function (response) {
                        $rootScope.showModal = false;
                        toaster.pop('error', "Lỗi!", response.data);
                    })
            }, function () { })
        };

        $scope.updateBannerLanguage = function (banner) {
            $('#uploadBanner').trigger('click');
            var languageName = "";
            if (banner.languageId == 29)
                languageName = "Tiếng Anh";
            else
                languageName = "Tiếng Việt";

            var bodyMessage = "Bạn muốn cập nhật banner: " + banner.name + " (" + languageName + ") ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                if ($scope.mediaUrl != "")
                    banner.mediaUrl = $scope.mediaUrl;

                return bannerMappingService.updateBanner(banner).$promise.then(
                   function (response) {
                       $scope.getAllBanners();
                       $rootScope.showModal = false;
                       $scope.currentBannerLanguage = null;
                       //  angular.extend(dst, src);
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
            DTColumnDefBuilder.newColumnDef(6).notSortable()
        ];



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
                    //$("#addNewBanner, #updateBanner").click(function () {
                    //    dz.processQueue();
                    //});
                    $("#uploadBanner").click(function () {
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
                                toaster.pop("warning", "Lưu ý!", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
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