'use strict';

angular.module("adminApp")
    .controller("projectController", ['$rootScope', '$scope', 'toaster', 'projectService', 'projectNewsService', 'projectImageService', 'checkFileNameService', '$sce',
                                                '$location', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'dialogs', '$state', '$filter',
                                                function ($rootScope, $scope, toaster, projectService, projectNewsService, projectImageService, checkFileNameService, $sce,
                                                $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs, $state, $filter) {

        $scope.allProjects = {};
        $scope.getAllProjects = function () {
            $rootScope.showModal = true;
            return projectService.getAllProjects().$promise.then(
            function (data) {
                $rootScope.showModal = false;
                $scope.allProjects = data;
            }, function (response) {
                $rootScope.showModal = true;
                toaster.pop('error', "Lỗi!", response.data);
            });
        }
        if ($state.current.url == "/project-list")
            $scope.getAllProjects();


        $scope.defaultLanguageId = 129;
        $scope.orderReadonlyIndex = -1;

        //update zorder
        $scope.editZorder = function (index) {
            $scope.orderReadonlyIndex = index;
        }

        $scope.updateZorder = function (index) {
            var currentProject = $scope.allProjects[index];
            var bodyMessage = "Bạn muốn cập nhật: " + currentProject.projects.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;

                return projectService.updateProject(currentProject.projects).$promise.then(
                   function (response) {
                       $scope.getAllProjects();
                       $scope.orderReadonlyIndex = -1;
                       $rootScope.showModal = false;
                       toaster.pop('success', "Thành công!", "Đã cập nhật dự án " + currentProject.projects.name + " - " + response.message);
                       $('html,body').animate({ scrollTop: 0 });
                   }, function (response) {
                       $rootScope.showModal = false;
                       $scope.orderReadonlyIndex = -1;
                       toaster.pop('error', "Lỗi!", response.data);
                   })
            }, function () { $scope.orderReadonlyIndex = -1; })
        }


        $scope.addNewProject = function (project) {
            $rootScope.showModal = true;
            $('#uploadBanner').trigger('click');

            if ($scope.resourceUrl != "")
                project.resourceUrl = $scope.resourceUrl;

            project.detail = CKEDITOR.instances.detail.getData();

            projectService.addNewProject(project).$promise.then(
                function (response) {
                    //$scope.getAllProjects();
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã thêm bài viết " + project.name + " - " + response.message);
                    $location.path("/controlPanel/project-list");
                }
                , function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
        };

        $scope.currentGettingIndex = null;
        $scope.currentGettingLanguage = null;

        $scope.getProject = function (index, languageId) {
            $scope.currentGettingIndex = index;
            $scope.currentGettingLanguage = languageId;
            var currentProject = {};
            //project news
            currentProject = angular.copy($scope.allProjects[index].projects);

            for (var i = 0; i < currentProject.news[0].newsMappings.length; i++) {
                if (currentProject.news[0].newsMappings[i].languageId == languageId)
                    currentProject.news[0].currentNewsMappings = currentProject.news[0].newsMappings[i];
            }
            //project images
            currentProject.projectImages = angular.copy($scope.allProjects[index].projects.projectImages);
            currentProject.projectImages = $filter('orderBy')(currentProject.projectImages, 'zOrder');
            for (var i = 0; i < currentProject.projectImages.length; i++) {
                for (var j = 0; j < currentProject.projectImages[i].projectImageMappings.length; j++) {
                    if (currentProject.projectImages[i].projectImageMappings[j].languageId == languageId) {
                        currentProject.projectImages[i].currentProjectImageMappings = currentProject.projectImages[i].projectImageMappings[j];
                        $scope.currentEditingLanguageIndex = j;
                    }
                }
            }

            $scope.currentProjectLanguage = currentProject;
            $scope.currentLanguageId = languageId;

            //console.log('$scope.currentProjectLanguage', $scope.currentProjectLanguage);

            CKEDITOR.instances.detail.setData($scope.currentProjectLanguage.news[0].currentNewsMappings.detail);
            $('html,body').animate({ scrollTop: $('.currentProjectLanguage').offset().top });
        }


        $scope.deleteProject = function (index, project) {

            var bodyMessage = "Bạn muốn xóa dự án: " + project.name + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return projectService.deleteProject(project).$promise.then(
                function (response) {
                    $scope.currentProjectLanguage = null;
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa dự án " + project.name + " - " + response.message);
                    $scope.allProjects.splice(index, 1);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        };

        $scope.updateProjectLanguage = function (project) {
            var languageName = "";
            if ($scope.currentLanguageId == 29)
                languageName = "Tiếng Anh";
            else
                languageName = "Tiếng Việt";
            project.news[0].newsMappings[$scope.currentEditingLanguageIndex].detail = CKEDITOR.instances.detail.getData();

            var bodyMessage = "Bạn muốn cập nhật dự án: " + project.name + " (" + languageName + ") ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return projectService.updateProject(project).$promise.then(
                function (response) {
                    $scope.getAllProjects();
                    $rootScope.showModal = false;
                    $scope.currentProjectLanguage = null;
                    CKEDITOR.instances.detail.setData('');
                    toaster.pop('success', "Thành công!", "Đã cập nhật dự án " + project.name + " - " + response.message);
                    $('html,body').animate({ scrollTop: 0 });
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                })
            }, function () { })
        }

        $scope.sortableOptions = {
            containment: '#sortable-container',
            //restrict move across columns. move only within column.
            accept: function (sourceItemHandleScope, destSortableScope) {
                return sourceItemHandleScope.itemScope.sortableScope.$id === destSortableScope.$id;
            },
            orderChanged: function (event) {
                for (var i = 0; i < $scope.currentProjectLanguage.projectImages.length; i++) {
                    $scope.currentProjectLanguage.projectImages[i].zOrder = i + 1;
                }

                return projectService.updateProject($scope.currentProjectLanguage).$promise.then(
                function (response) {
                    $scope.getAllProjects();
                    //$scope.getProject($scope.currentGettingIndex, $scope.currentGettingLanguage);
                    $rootScope.showModal = false;
                    $('html,body').animate({ scrollTop: $('.currentProjectLanguage').offset().top });
                    toaster.pop('success', "Thành công!", "Đã cập nhật vị trí -" + response.message);
                }, function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.message);
                })
            }
        };

        $scope.deleteImage = function(projectImage) {
            $rootScope.showModal = true;
            return projectImageService.deleteProjectImage(projectImage).$promise.then(
                function(response) {
                    $scope.currentProjectLanguage.projectImages.splice($scope.currentProjectLanguage.projectImages.indexOf(projectImage), 1);
                    $rootScope.showModal = false;
                    toaster.pop('success', "Thành công!", "Đã xóa hình ảnh -" + response.message);
                }, function(response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.message);
                });
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
            //DTColumnDefBuilder.newColumnDef(6).notSortable()
            DTColumnDefBuilder.newColumnDef(5).notSortable()
        ];



        // Uploader Plugin Code
        $scope.thumbUrl = "";
        $scope.dropzoneConfigHome = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'maxFilesize': 10000,
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
                                $scope.resourceUrl = dz.files[0].name;
                            },
                            function () {
                                $scope.resourceUrl = dz.files[0].name;
                                toaster.pop("warning", "Lưu ý!", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
                            }
                        )
                    });
                    this.on("error", function (file, message) {
                        toaster.pop("error", "Lỗi", "Vui lòng chọn ảnh có dung lượng dưới 1 Mb!")
                        this.removeFile(file);
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

        $scope.addingProjectImage = { name: "", description: "", resourceUrl: "", projectId: "" };
        $scope.dropzoneAddProjectImage = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'maxFilesize': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    this.on("addedfile", function () {
                        if ($scope.addingProjectImage.description == "" || $scope.addingProjectImage.name == "") {
                            if (dz.files[0] != null) {
                                dz.removeFile(dz.files[0]);
                            }
                            toaster.pop('warning', "Vui lòng nhập tên và nội dung ảnh trước");
                            return;
                        }
                           
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                var bodyMessage = "Bạn muốn thêm ảnh này ?";
                                var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });
                                dlg.result.then(function (btn) {
                                    $rootScope.showModal = true;
                                    dz.processQueue();

                                    $scope.addingProjectImage.resourceUrl = dz.files[0].name;
                                    $scope.addingProjectImage.projectId = $scope.currentProjectLanguage.id;

                                    return projectImageService.addNewProjectImage($scope.addingProjectImage).$promise.then(
                                    function (response) {
                                        $scope.getAllProjects();

                                        $scope.$watch('allProjects', function (newVal, oldVal) {
                                            $scope.getProject($scope.currentGettingIndex, $scope.currentGettingLanguage);
                                            $rootScope.showModal = false;
                                            $('html,body').animate({ scrollTop: $('.currentProjectLanguage').offset().top });
                                        });
                                        $scope.addingProjectImage = { name: "", description: "", resourceUrl: "", projectId: "" };
                                        
                                        toaster.pop('success', "Thành công!", "Đã thêm hình ảnh " + response.message);
                                    }, function (response) {
                                        $rootScope.showModal = false;
                                        $scope.addingProjectImage = { name: "", description: "", resourceUrl: "", projectId: "" };
                                        toaster.pop('error', "Lỗi!", response.message);
                                    })
                                }, function () {
                                    if (dz.files[0] != null) {
                                        dz.removeFile(dz.files[0]);
                                    }
                                })
                            },
                            function () {
                                $scope.resourceUrl = dz.files[0].name;
                                if (dz.files[0] != null) {
                                    dz.removeFile(dz.files[0]);
                                }
                                toaster.pop("warning", "Lưu ý!", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
                            }
                        )


                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                    });
                    this.on("error", function (file, message) {
                        toaster.pop("error", "Lỗi", "Vui lòng chọn ảnh có dung lượng dưới 1 Mb!")
                        this.removeFile(file);
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
                    $rootScope.showModal = false;
                }
            }
        };

        $scope.dropzoneProjectImage = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'maxFilesize': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;

                    this.on("addedfile", function () {
                        var currentProjetImageIndex = dz.element.getAttribute('index');
                        
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                var bodyMessage = "Bạn muốn upload và thay đổi ảnh này ?";
                                var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });
                                dlg.result.then(function (btn) {
                                    $rootScope.showModal = true;
                                    dz.processQueue();

                                    var updateCurrentProjectImage = angular.copy($scope.currentProjectLanguage.projectImages[currentProjetImageIndex]);
                                    updateCurrentProjectImage.resourceUrl = dz.files[0].name;

                                    return projectImageService.updateProjectImage(updateCurrentProjectImage).$promise.then(
                                    function (response) {
                                        $scope.getAllProjects();

                                        $scope.$watch('allProjects', function (newVal, oldVal) {
                                            $scope.getProject($scope.currentGettingIndex, $scope.currentGettingLanguage);
                                            $rootScope.showModal = false;
                                            $('html,body').animate({ scrollTop: $('.currentProjectLanguage').offset().top });
                                        });
                                        
                                        
                                        toaster.pop('success', "Thành công!", "Đã cập nhật hình ảnh " + response.message);
                                    }, function (response) {
                                        $rootScope.showModal = false;
                                        toaster.pop('error', "Lỗi!", response.message);
                                    })
                                }, function () {
                                    if (dz.files[0] != null) {
                                        dz.removeFile(dz.files[0]);
                                    }
                                })
                            },
                            function () {
                                $scope.resourceUrl = dz.files[0].name;
                                if (dz.files[0] != null) {
                                    dz.removeFile(dz.files[0]);
                                }
                                toaster.pop("warning", "Lưu ý!", "Tên file này đã có trong thư mục, vui lòng đổi tên khác HOẶC file đã có sẽ bị chép đè!")
                            }
                        )
                        

                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                    });
                    this.on("error", function (file, message) {
                        toaster.pop("error", "Lỗi", "Vui lòng chọn ảnh có dung lượng dưới 1 Mb!")
                        this.removeFile(file);
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
                    $scope.uploadedFile = true;
                    $rootScope.showModal = false;
                }
            }
        };
    }])
.directive('editInPlace', ['$rootScope', 'projectImageService', 'toaster', function ($rootScope, projectImageService, toaster) {
    return {
        restrict: 'E',
        scope: { description: '=', name: '=', currentProjectLanguage: '=', currentEditingLanguageIndex: '=', rootScope: '=', projectImageIndex: '=' },
        transclude: true,
        template: '<div class="form-group text-center">' +
                        '<span class="todoName" ng-dblclick="editName()" ng-bind="name"></span>' +
                        '<span class="todoName text-muted" ng-dblclick="editName()" ng-if="name.length <= 1 " >Nhấp 2 lần để chỉnh sửa Tên ảnh</span>' +
                        '<input class="todoField form-control name" ng-model="name" placeholder="Tên ảnh:"></input>' +
                    '</div>' +
                    '<div class="form-group text-center">' +
                        '<span class="todoName" ng-dblclick="editDescription()" ng-bind="description"></span>' +
                        '<span class="todoName text-muted" ng-dblclick="editDescription()" ng-if="description.length <= 1 " >Nhấp 2 lần để chỉnh sửa nội dung ảnh</span>' +
                        '<input class="todoField form-control description" ng-model="description" placeholder="Nội dung ảnh: "></input>' +
                    '</div>'+
                    '<ng-transclude></ng-transclude>',
                    
        link: function ($scope, element, attrs) {
            // Let's get a reference to the input element, as we'll want to reference it.
            //var inputElement = angular.element(element.children()[1]);
            var inputDescriptionElement = angular.element(element.find('.todoField.description'));
            var inputNameElement = angular.element(element.find('.todoField.name'));
            //console.log("element.children() ---", inputElement);
            // This directive should have a set class so we can style it.
            element.addClass('edit-in-place');

            // Initially, we're not editing.
            $scope.editing = false;

            // ng-dblclick handler to activate edit-in-place
            $scope.editDescription = function () {
                $scope.editing = true;

                // We control display through a class on the directive itself. See the CSS.
                element.addClass('active');

                // And we must focus the element.
                // `angular.element()` provides a chainable array, like jQuery so to access a native DOM function,
                // we have to reference the first element in the array.
                inputDescriptionElement.focus();
            };
            $scope.editName = function () {
                $scope.editing = true;
                element.addClass('active');
                inputNameElement.focus();
            };

            // When we leave the input, we're done editing.
            //inputDescriptionElement.on("blur", function () {
            //    updateProjectLanguage($scope.currentProjectLanguage);
            //    $scope.editing = false;
            //    element.removeClass('active');
            //});
            inputDescriptionElement.on('keydown', function (e) {
                if (e.which == 13) {
                    updateProjectLanguage($scope.currentProjectLanguage);
                    $scope.editing = false;
                    element.removeClass('active');
                    e.preventDefault();
                }
            });

            //inputNameElement.on("blur", function () {
            //    updateProjectLanguage($scope.currentProjectLanguage);
            //    $scope.editing = false;
            //    element.removeClass('active');
            //});
            inputNameElement.on('keydown', function (e) {
                if (e.which == 13) {
                    updateProjectLanguage($scope.currentProjectLanguage);
                    $scope.editing = false;
                    element.removeClass('active');
                    e.preventDefault();
                }
            });

            var updateProjectLanguage = function (project) {
                $rootScope.showModal = true;
                var languageName = "";
                if (project.news[0].currentNewsMappings == 29)
                    languageName = "Tiếng Anh";
                else
                    languageName = "Tiếng Việt";

                //project.news[0].newsMappings[$scope.currentEditingLanguageIndex].detail = CKEDITOR.instances.detail.getData();
//                console.log("project.projectImages[projectImageIndex]", project.projectImages[$scope.projectImageIndex]);
                return projectImageService.updateProjectImage(project.projectImages[$scope.projectImageIndex]).$promise.then(
                    function (response) {
                        $rootScope.showModal = false;
                        toaster.pop('success', "Thành công!", "Đã cập nhật " + response.message);
                        CKEDITOR.instances.detail.setData('');

                        //$('html,body').animate({ scrollTop: 0 });
                    }, function (response) {
                        $rootScope.showModal = false;
                        toaster.pop('error', "Lỗi!", response.data);
                    })
            }

        }
    };
}])
.directive('uploadFileEditInPlace', function () {
    return {
        restrict: 'E',
        scope: { value: '=', dropzoneImage: '=', projectImageIndex: '='},
        template: '<div class="row form-group">' +
                    '<div class="col-md-12 text-center">' +
                        '<img ng-src="{{value}}" ng-dblclick="edit()" alt="" class="img-responsive display-inline-block upload-file-edit-in-place height-150" />' +
                        '<button dropzone="dropzoneImage" class="dropzone" index="{{projectImageIndex}}">' +
                            '<span class="dz-message">Chọn ảnh Dự án<br /></span> ' +
                        '</button> ' +
                        '<a class="btn btn-danger btn-xs cancel" ng-click="cancel()" style="margin-top:20px" title="Hủy">Hủy</a>' +
                    '</div>' +
                '</div>',
        link: function ($scope, element, attrs) {
            // Let's get a reference to the input element, as we'll want to reference it.
            //var inputElement = angular.element(element.children()[1]);
            var inputElement = angular.element(element.find('.dropzone'));
            var cancelElement = angular.element(element.find('.cancel'));
            //console.log("element.children() ---", inputElement);
            // This directive should have a set class so we can style it.
            element.addClass('upload-file-edit-in-place');

            $scope.cancel = function() {
                $scope.editing = false;
                element.removeClass('active');
            }

            // Initially, we're not editing.
            $scope.editing = false;

            // ng-dblclick handler to activate edit-in-place
            $scope.edit = function () {
                $scope.editing = true;

                // We control display through a class on the directive itself. See the CSS.
                element.addClass('active');
                cancelElement.addClass('active');

                // And we must focus the element.
                // `angular.element()` provides a chainable array, like jQuery so to access a native DOM function,
                // we have to reference the first element in the array.
                inputElement.focus();
            };

            // When we leave the input, we're done editing.
            inputElement.on('keyup', function (e) {
                if (e.which == 13) {
                    $scope.editing = false;
                    element.removeClass('active');
                    e.preventDefault();
                }
            });
        }
    }
})