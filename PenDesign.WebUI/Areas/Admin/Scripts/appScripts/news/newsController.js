'use strict';

angular.module("adminApp")
    .controller("newsController", function ($rootScope, $scope, toaster, newsService, newsMappingService, checkFileNameService, $sce,
                                            $location, DTOptionsBuilder, DTColumnDefBuilder, $stateParams, dialogs, $state) {

        $scope.newsCategoryId = $stateParams.newsCategoryId;
        var categoryName = "";
        if ($scope.newsCategoryId == 1)
            categoryName = "Xu hướng";
        if ($scope.newsCategoryId == 2)
            categoryName = "Khách hàng";

        $scope.categoryName = categoryName;

        $scope.defaultLanguageId = 129;
        $scope.orderReadonlyIndex = -1;

        $scope.allNews = {};
        $scope.getAllNews = function () {
            $rootScope.showModal = true;
            return newsService.getAllNews($scope.newsCategoryId).$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allNews = data;
            }, function (response) {
                $rootScope.showModal = true;
                toaster.pop('error', "Lỗi!", response.data);
            });
        }
        if ($state.current.url != "/add-News/1" || $state.current.url != "/add-News/1")
            $scope.getAllNews();


        //update zorder
        $scope.editZorder = function (index) {
            $scope.orderReadonlyIndex = index;
        }

        $scope.updateZorder = function (index) {
            var currentNews = $scope.allNews[index];
            var bodyMessage = "Bạn muốn cập nhật: " + currentNews.newses.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return newsService.updateNews(currentNews.newses).$promise.then(
                   function (response) {
                       $scope.getAllNews();
                       $scope.orderReadonlyIndex = -1;
                       $rootScope.showModal = false;
                       toaster.pop('success', "Thành công!", "Đã cập nhật bài viết " + currentNews.newses.name + " - " + response.message);
                       $('html,body').animate({ scrollTop: 0 });
                   }, function (response) {
                       $rootScope.showModal = false;
                       $scope.orderReadonlyIndex = -1;
                       toaster.pop('error', "Lỗi!", response.data);
                   })
            }, function () { $scope.orderReadonlyIndex = -1; })
        }


        $scope.addNewNews = function (news) {

            $rootScope.showModal = true;
            $('#uploadBanner').trigger('click');

            if ($scope.thumbUrl != "")
                news.thumbUrl = $scope.thumbUrl;

            news.detail = CKEDITOR.instances.detail.getData();
            news.newsCategoryId = $scope.newsCategoryId;

            newsService.addNewNews(news).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm bài viết " + news.name + " - " + response.message);
                    $scope.getAllNews();
                    $location.path("/controlPanel/news-list/" + $scope.newsCategoryId);
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };


        $scope.getNews = function (index, languageId) {
            var currentNews = $scope.allNews[index].newses.newsMappings;
            for (var i = 0; i < currentNews.length; i++) {
                if (currentNews[i].languageId == languageId)
                    $scope.currentNewsLanguage = currentNews[i];
            }
            CKEDITOR.instances.description.setData($scope.currentNewsLanguage.description);
            $('html,body').animate({ scrollTop: $('.currentNewsLanguage').offset().top });
        }

        $scope.deleteNews = function (index, news) {

            var bodyMessage = "Bạn muốn xóa bài viết: " + news.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return newsService.deleteNews(news).$promise.then(
                    function (response) {
                        $rootScope.showModal = false;
                        $scope.currentNewsLanguage = null;
                        toaster.pop('success', "Thành công!", "Đã xóa Banner " + news.name + " - " + response.message);
                        $scope.allNews.splice(index, 1);
                    }, function (response) {
                        $rootScope.showModal = false;
                        toaster.pop('error', "Lỗi!", response.data);
                    })
            }, function () { })
        };

        $scope.updateNewsLanguage = function (news) {
            $('#uploadBanner').trigger('click');
            var languageName = "";
            if (news.languageId == 29)
                languageName = "Tiếng Anh";
            else
                languageName = "Tiếng Việt";

            var bodyMessage = "Bạn muốn cập nhật bài viết: " + news.name + " (" + languageName + ") ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                if ($scope.thumbUrl != "")
                    news.thumbUrl = $scope.thumbUrl;

                return newsMappingService.updateNews(news).$promise.then(
                   function (response) {
                       $scope.getAllNews();
                       $rootScope.showModal = false;
                       $scope.currentNewsLanguage = null;
                       toaster.pop('success', "Thành công!", "Đã cập nhật bài viết " + news.name + " - " + response.message);
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
                        alert("dadada");
                        dz.processQueue();
                    });

                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.thumbUrl = dz.files[0].name;
                            },
                            function () {
                                $scope.thumbUrl = dz.files[0].name;
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