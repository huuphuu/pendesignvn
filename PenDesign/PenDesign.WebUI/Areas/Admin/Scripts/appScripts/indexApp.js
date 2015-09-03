﻿'use strict';

angular.module('adminApp')
    .config(function ($httpProvider) {
        $httpProvider.defaults.withCredentials = true;
        $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    })
//-------------------------------------------------------------------------------------------------------------------------------------
// Controller 
//-------------------------------------------------------------------------------------------------------------------------------------
    .controller('BodyController', function ($rootScope, $scope, toaster, accountService, $http, $interval) {
        $scope.navigation = $adminCMS.data.navigation;
        //$scope.sidebarNavigation = $adminCMS.data.navigation.sidebarNav;
        $scope.dateTimeFormat = 'dd-MM-yyyy HH:mm:ss';
        $scope.dateFormat = 'dd-MM-yyyy';

        $rootScope.showModal = true;
        //$scope.currentUser = $adminCMS.data.currentUser;
        $scope.currentUser = function () {
            $rootScope.showModal = true;
            return accountService.getUserInfo().then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.currentUser = data;
                },
                function(response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                }
            );
        };
        $scope.currentUser();

        $interval($scope.currentUser(), 2000);

        $scope.$on('updateNotification', function () {
            $rootScope.showModal = true;
            return accountService.getUserInfo().then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.currentUser = data;
                },
                function(response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response.data);
                }
            );
        });

        $scope.getAdminMenu = function () {

            $http.get('admin/api/adminMenu/GetMenus').then(
                function (response) {
                    $scope.sidebarNavigation = response.data;
                    setTimeout(function () {
                        $.AdminLTE.tree('.sidebar');
                    }, 100);
                },
                function () {
                    //toaster.pop("error", "Lỗi!");
                }
            )
        }
        $scope.getAdminMenu();

    })

    .controller('uploadController', function ($scope, FileUploader, toaster) {
        //angular file upload module

        var uploader = $scope.uploader = new FileUploader({
            url: window.location.protocol + '//' + window.location.host + '/admin/api/Upload'
            //window.location.pathname + 
        });

        // FILTERS

        uploader.filters.push({
            name: 'extensionFilter',
            fn: function (item, options) {
                var filename = item.name;
                var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                if (extension == "pdf" || extension == "doc" || extension == "docx" ||
                    extension == "rtf" || extension == "png" || extension == "js" || extension == ".min.js" || extension == "jpg")
                    return true;
                else {
                    //alert('Invalid file format. Please select a file with pdf/doc/docs/png/js/.min.js/jpg or rtf format and try again.');
                    toaster.pop('error', "Invalid file format. Please select a file with pdf/doc/docs/png/js/.min.js/jpg or rtf format and try again.");
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'sizeFilter',
            fn: function (item, options) {
                var fileSize = item.size;
                fileSize = parseInt(fileSize) / (1024 * 1024);
                if (fileSize <= 5)
                    return true;
                else {
                    alert('Selected file exceeds the 5MB file size limit. Please choose a new file and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'itemResetFilter',
            fn: function (item, options) {
                if (this.queue.length < 5)
                    return true;
                else {
                    //alert('You have exceeded the limit of uploading files.');
                    toaster.pop('error', "You have exceeded the limit of uploading files.");
                    return false;
                }
            }
        });

        // CALLBACKS
        uploader.previewItem = function (fileItem, serverPath) {
            var path = fileItem.file.serverPath.replace(/[\\]/g, '/');
            myFactory.popupNotification({
                popupType: 'open',  //confirm, alert, open
                popupStatus: 'info', //success, info, warning, danger
                title: fileItem.file.name,
                message: '',
                htmlTemplate: '', //insert html template
                iframe: encodeURIComponent(path)
            });
        };
        uploader.deleteItem = function (fileItem, serverPath) {
            alert("delete File")
        };
        uploader.onWhenAddingFileFailed = function (item, filter, options) {
            console.info('onWhenAddingFileFailed', item, filter, options);
            toaster.pop('error', item.name + " has been removed.");
        };
        uploader.onAfterAddingFile = function (fileItem) {
            //alert('Files ready for upload.');
        };

        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            //$scope.uploader.queue = [];
            fileItem.file.serverPath = response;
            //$scope.uploader.queue = $scope.uploader.queue.slice(1);
            //$scope.uploader.progress = 0;
            //alert('Selected file has been uploaded successfully.');

            toaster.pop('success', fileItem.file.name + " File Uploaded");
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            //alert('We were unable to upload your file. Please try again.');
            toaster.pop('error', "We were unable to upload your file. Please try again.");
        };
        uploader.onCancelItem = function (fileItem, response, status, headers) {
            alert('File uploading has been cancelled.');
            toaster.pop('error', "File uploading has been cancelled.");
        };

        uploader.onAfterAddingAll = function (addedFileItems) {
            //console.info('onAfterAddingAll', addedFileItems);
        };
        uploader.onBeforeUploadItem = function (item) {
            item.timeStamp = Date.now();
            item.prevProgress = 0;
            console.info('onBeforeUploadItem', item);
        };
        uploader.onProgressItem = function (fileItem, progress) {
            var time = Date.now() - fileItem.timeStamp;
            var percent = (progress - fileItem.prevProgress) / 100;
            var chunk = percent * fileItem.file.size;
            var speed = ((chunk / 1024 / 1024) / (time / 1000)).toFixed(2);

            fileItem.timeStamp = Date.now();
            fileItem.prevProgress = progress;

            fileItem.speed = speed;
            $scope.speed = speed;
            $scope.percent = percent;
            //console.info('speed', speed, ' mb/sec');
            //console.info('speed', percent, ' mb/sec');
            //console.info('onProgressItem', fileItem, progress);
        };
        uploader.onProgressAll = function (progress) {
            //console.info('onProgressAll', progress);
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (status == 200) {
                //$scope.uploader.queue.file.serverPath = response;
                fileItem.uploader.queue.serverPath = response;
            }
            //console.info('onCompleteItem', fileItem, response, status, headers);
        };
        uploader.onCompleteAll = function () {
            //console.info('onCompleteAll');
        };

        console.info('uploader', uploader);
    })

//-------------------------------------------------------------------------------------------------------------------------------------
//Service 
//-------------------------------------------------------------------------------------------------------------------------------------
    .factory('checkFileNameService', function ($q, $http) {
        var checkFileNameService = {};
        checkFileNameService.checkFileName = function (fileName) {
            var deferred = $q.defer();
            $http.get('admin/api/upload/CheckFileNameExist', { params: { fileName: fileName } })
                .success(function () {
                    deferred.resolve();
                })
                .error(function () {
                    deferred.reject();
                })
            return deferred.promise;
        };
        return checkFileNameService;
    })

//Filter ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Directive /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    .directive('headerNavbarDropdown', function ($timeout, accountService, $window) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Areas/Admin/Templates/Directives/header/nav/header-Navbar-Menu-dropdown.html',
            link: function (scope, element, attrs) {
                $timeout(function () {
                    if ($.AdminLTE.options.navbarMenuSlimscroll && typeof $.fn.slimscroll != 'undefined') {
                        $(".navbar .menu").slimscroll({
                            height: "200px",
                            alwaysVisible: false,
                            size: "3px"
                        }).css("width", "100%");
                    }
                }, 100);
            },
            controller: function ($scope, $element, $attrs) {
                $scope.signOut = function () {
                    accountService.logOff().then(
                        function (data) {
                            $window.location.href = "/Admin";
                        },
                        function () { alert("singgout failed"); }
                    );
                }
            }
        };
    })
    .directive('headerNavbarMenu', function () {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                navigation: '=',
                currentUser: '='
            },
            templateUrl: '/Areas/Admin/Templates/Directives/header/nav/header-Navbar-Menu.html'
        };
    })

    .directive('sidebarNavigation', function () {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                navigationSource: '=',
                currentUser: '='
            },
            templateUrl: '/Areas/Admin/Templates/Directives/navigation/navigation.html',
            link: function (scope, element, attrs) {
                //setTimeout(function () {
                //    $.AdminLTE.tree('.sidebar');
                //}, 100);
            }
        };
    })
    .directive('navigationMultipleMenu', function ($compile) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                menu: '='
            },
            templateUrl: '/Areas/Admin/Templates/Directives/navigation/navigation-childs.html',
            compile: function (el) {
                var contents = angular.element(el).contents().remove();
                var compiled;
                return function (scope, el) {
                    if (!compiled)
                        compiled = $compile(contents);

                    compiled(scope, function (clone) {
                        el.append(clone);
                    });
                };
            }

        };
    })

    .directive('dropzone', function () {
        return function (scope, element, attrs) {
            var config, dropzone;

            config = scope[attrs.dropzone];

            // create a Dropzone for the element with the given options
            dropzone = new Dropzone(element[0], config.options);

            // bind the given event handlers
            angular.forEach(config.eventHandlers, function (handler, event) {
                dropzone.on(event, handler);
            });
        };
    })

    .directive("compareTo", function () {
        return {
            require: "ngModel",
            scope: {
                otherModelValue: "=compareTo"
            },
            link: function (scope, element, attributes, ngModel) {
                ngModel.$validators.compareTo = function (modelValue) {
                    return modelValue == scope.otherModelValue;
                };

                scope.$watch("otherModelValue", function () {
                    ngModel.$validate();
                });
            }
        };
    });




