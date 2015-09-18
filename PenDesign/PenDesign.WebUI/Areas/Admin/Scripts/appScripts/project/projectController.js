'use strict';

angular.module("adminApp")
    .controller("projectController", function ($rootScope, $scope, toaster, projectService, checkFileNameService, $sce, $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs) {

        $scope.allNews = {};
        $scope.getAllProjects = function () {
            $rootScope.showModal = true;
            return projectService.getAllProjects($scope.newsCategoryId).$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allNews = data;
            }, function (response) {
                $rootScope.showModal = true;
                //toaster.pop('error', "Lỗi!", response.data);
            });
        }
        $scope.getAllProjects();

        $scope.addNewProject = function (project) {
            $rootScope.showModal = true;
            if ($scope.HomeBannerImageUrl != "")
                project.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                project.SubBannerImageUrl = $scope.SubBannerImageUrl;

            project.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            project.contentIntro = CKEDITOR.instances.contentIntro.getData();
            project.contentLeft = CKEDITOR.instances.contentLeft.getData();
            project.contentRight = CKEDITOR.instances.contentRight.getData();
            project.newsCategoryId = $scope.newsCategoryId;

            projectService.addNewProject(project).$promise.then(
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm bài viết " + project.name + " - " + response.message);
                    $scope.getAllProjects();
                    $location.path("/controlPanel/project-list");
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.getProject = function (index) {
            $scope.currentProject = $scope.allProjects[index];
            CKEDITOR.instances.bannerImageIntro.setData($scope.currentProject.bannerImageIntro);
            CKEDITOR.instances.contentIntro.setData($scope.currentProject.contentIntro);
            CKEDITOR.instances.contentLeft.setData($scope.currentProject.contentLeft);
            CKEDITOR.instances.contentRight.setData($scope.currentProject.contentRight);
            $('html,body').animate({ scrollTop: $('.editProject').offset().top });
        }

        $scope.deleteProject = function (project) {

            var bodyMessage = "Bạn muốn xóa dự án: " + project.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return projectService.deleteProject(project).$promise.then(
                function (response) {
                    $scope.currentNews = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa bài viết " + project.name + " - " + response.message);
                    $scope.allProjects.splice($scope.allProjects.indexOf(project), 1);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        };

        $scope.updateProject = function (project) {

            if ($scope.HomeBannerImageUrl != "")
                project.HomeBannerImageUrl = $scope.HomeBannerImageUrl;
            if ($scope.SubBannerImageUrl != "")
                project.SubBannerImageUrl = $scope.SubBannerImageUrl;
            project.bannerImageIntro = CKEDITOR.instances.bannerImageIntro.getData();
            project.contentIntro = CKEDITOR.instances.contentIntro.getData();
            project.contentLeft = CKEDITOR.instances.contentLeft.getData();
            project.contentRight = CKEDITOR.instances.contentRight.getData();

            var bodyMessage = "Bạn muốn cập nhật bài viết: " + project.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return projectService.updateProject(project).$promise.then(
                function (response) {
                    $scope.getAllProjects();
                    $rootScope.showModal = false;
                    $scope.currentProject = null;
                    CKEDITOR.instances.bannerImageIntro.setData('');
                    CKEDITOR.instances.contentIntro.setData('');
                    CKEDITOR.instances.contentLeft.setData('');
                    CKEDITOR.instances.contentRight.setData('');
                    toaster.pop('success', "Thành công!", "Đã cập nhật bài viết " + project.name + " - " + response.message);
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