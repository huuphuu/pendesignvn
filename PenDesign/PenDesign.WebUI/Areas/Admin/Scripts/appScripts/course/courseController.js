'use strict';

angular.module("adminApp")
    .controller("courseController", function ($rootScope, $scope, toaster, courseService, checkFileNameService, $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs) {
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
                    $("#addCourse").click(function () {
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
                    $("#addCourse").click(function () {
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

        $scope.addCourse = function (course) {
            $rootScope.showModal = true;
            if ($scope.HomeBannerImageUrl != "")
                course.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                course.SubBannerImageUrl = $scope.SubBannerImageUrl;

            course.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            course.contentIntro = CKEDITOR.instances.contentIntro.getData();
            course.contentLeft = CKEDITOR.instances.contentLeft.getData();
            course.contentRight = CKEDITOR.instances.contentRight.getData();
            return courseService.addNewCourse(course).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm khóa học " + course.name + response.message);
                    $scope.getAllCourse();
                    $location.path("/controlPanel/course-list");
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.getAllCourse = function () {
            $rootScope.showModal = true;
            return courseService.getAllCourse().$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allCourse = data;
            }, function (response) {
                $rootScope.showModal = false;
                //toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getAllCourse();

        $scope.getCourse = function (index) {
            $scope.currentCourse = $scope.allCourse[index];
            CKEDITOR.instances.bannerImageIntro.setData($scope.currentCourse.bannerImageIntro);
            CKEDITOR.instances.contentIntro.setData($scope.currentCourse.contentIntro);
            CKEDITOR.instances.contentLeft.setData($scope.currentCourse.contentLeft);
            CKEDITOR.instances.contentRight.setData($scope.currentCourse.contentRight);
            $('html,body').animate({ scrollTop: $('.editCourse').offset().top });
        }

        $scope.deleteCourse = function (course) {
            var bodyMessage = "Bạn muốn xóa: " + course.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return courseService.deleteCourse(course).$promise.then(
                function (response) {
                    $scope.currentCourse = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa khóa học " + course.name + " - " + response.message);
                    $scope.allCourse.splice($scope.allCourse.indexOf(course), 1);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        };

        $scope.updateCourse = function (course) {
            
            if ($scope.HomeBannerImageUrl != "")
                course.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                course.SubBannerImageUrl = $scope.SubBannerImageUrl;

            course.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            course.contentIntro = CKEDITOR.instances.contentIntro.getData();
            course.contentLeft = CKEDITOR.instances.contentLeft.getData();
            course.contentRight = CKEDITOR.instances.contentRight.getData();

            var bodyMessage = "Bạn muốn cập nhật: " + course.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return courseService.updateCourse(course).$promise.then(
                function (response) {
                    $scope.getAllCourse();
                    $rootScope.showModal = false;
                    $scope.currentCourse = null;
                    CKEDITOR.instances.bannerImageIntro.setData('');
                    CKEDITOR.instances.contentIntro.setData('');
                    CKEDITOR.instances.contentLeft.setData('');
                    CKEDITOR.instances.contentRight.setData('');
                    toaster.pop('success', "Thành công!", "Đã cập nhật khóa học " + course.name + " - " + response.message);
                    $('html,body').animate({ scrollTop: 0 });
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        }
    })