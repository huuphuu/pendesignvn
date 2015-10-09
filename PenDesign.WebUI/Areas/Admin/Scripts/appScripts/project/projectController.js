'use strict';

angular.module("adminApp")
    .controller("projectController", function ($rootScope, $scope, toaster, projectService, projectNewsService, projectImageService, checkFileNameService, $sce,
                                            $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs, $state) {

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


        $scope.getProject = function (index, languageId) {
            var currentProject = {};
            //project news
            currentProject = angular.copy($scope.allProjects[index].projects);
            console.log("currentProject", currentProject);
            for (var i = 0; i < currentProject.news[0].newsMappings.length; i++) {
                if (currentProject.news[0].newsMappings[i].languageId == languageId)
                    currentProject.news[0].currentNewsMappings = currentProject.news[0].newsMappings[i];
            }
            //project images
            currentProject.projectImages = angular.copy($scope.allProjects[index].projects.projectImages);
            for (var i = 0; i < currentProject.projectImages.length; i++) {
                for (var j = 0; j < currentProject.projectImages[i].projectImageMappings.length; j++) {
                    if (currentProject.projectImages[i].projectImageMappings[j].languageId == languageId) {
                        currentProject.projectImages[i].currentProjectImageMappings = currentProject.projectImages[i].projectImageMappings[j];
                    }
                }
            }

            $scope.currentProjectLanguage = currentProject;
            $scope.currentLanguageId = languageId;

            console.log('$scope.currentProjectLanguage', $scope.currentProjectLanguage);

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

            $('#uploadBanner').trigger('click');
            var languageName = "";
            if (news.languageId == 29)
                languageName = "Tiếng Anh";
            else
                languageName = "Tiếng Việt";

            if ($scope.resourceUrl != "")
                project.resourceUrl = $scope.resourceUrl;

            project.detail = CKEDITOR.instances.detail.getData();

            var bodyMessage = "Bạn muốn cập nhật dự án: " + project.name + " (" + languageName + ") ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return projectNewsService.updateProject(project).$promise.then(
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

        //Sortable & inline editing
        var i;
        $scope.itemsList = {
            items1: [],
            items2: []
        };

        for (i = 0; i <= 5; i += 1) {
            $scope.itemsList.items1.push({ 'Id': i, 'Label': 'Item ' + i });
        }

        $scope.sortableOptions = {
            containment: '#sortable-container',
            //restrict move across columns. move only within column.
            accept: function (sourceItemHandleScope, destSortableScope) {
                return sourceItemHandleScope.itemScope.sortableScope.$id === destSortableScope.$id;
            }
        };


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
.directive('editInPlace', function () {
    return {
        restrict: 'E',
        scope: { value: '=' },
        template: '<div class="form-group">' +
                        '<span class="todoName" ng-dblclick="edit()" ng-bind="value"></span>' +
                        '<input class="todoField form-control" ng-model="value"></input>' +
                    '</div>',
        link: function ($scope, element, attrs) {
            // Let's get a reference to the input element, as we'll want to reference it.
            var inputElement = angular.element(element.children()[1]);

            // This directive should have a set class so we can style it.
            element.addClass('edit-in-place');

            // Initially, we're not editing.
            $scope.editing = false;

            // ng-dblclick handler to activate edit-in-place
            $scope.edit = function () {
                $scope.editing = true;

                // We control display through a class on the directive itself. See the CSS.
                element.addClass('active');

                // And we must focus the element.
                // `angular.element()` provides a chainable array, like jQuery so to access a native DOM function,
                // we have to reference the first element in the array.
                inputElement.focus();
            };

            // When we leave the input, we're done editing.
            inputElement.on("blur", function () {
                $scope.editing = false;
                element.removeClass('active');
            });

        }
    };
})