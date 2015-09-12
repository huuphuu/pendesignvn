'use strict';

angular.module("adminApp")
    .controller('accountController', function ($rootScope, $scope, toaster, accountService, checkFileNameService, $location, DTOptionsBuilder, DTColumnDefBuilder, dialogs) {
        //DataTable
        $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withPaginationType('full_numbers')
        .withOption('responsive', true)
        .withDisplayLength(10)
        .withDataProp('aaData')
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


        //upload plugin
        $scope.avatar = "";
        $scope.dropzoneConfigHome = {
            'options': { // passed into the Dropzone constructor
                'url': '/admin/api/upload',
                'acceptedFiles': "image/*",
                'maxFiles': 1,
                'autoProcessQueue': false,
                'addRemoveLinks': true,
                init: function () {
                    var dz = this;
                    $("#registerBtn").click(function () {
                        dz.processQueue();
                    });
                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                        checkFileNameService.checkFileName(this.files[0].name).then(
                            function () {
                                $scope.avatar = dz.files[0].name;
                            },
                            function () {
                                $scope.avatar = dz.files[0].name;
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

        $scope.register = function (user) {
            $scope.clicked = true;
            $rootScope.showModal = true;
            if ($scope.avatar != "")
                user.avatar = $scope.avatar;
            return accountService.register(user).then(
                    function () {
                        $rootScope.showModal = false;
                        toaster.pop('success', "Thành công!", "Đã thêm người dùng " + user.userName);
                        $scope.getAllUsers();
                        $location.path("/controlPanel/users-list");
                    },
                    function (response) {
                        $rootScope.showModal = false;
                        $scope.clicked = false;
                        toaster.pop('error', "Lỗi!", response.data);
                    });
        }

        $scope.logOff = function () {
            return accountService.logOff();
        }

        //Roles
        $scope.getAllUsers = function () {
            return accountService.getAllUsers().then(
                    function (data) {
                        $scope.allUsers = data;
                    },
                    function (response) {
                        toaster.pop('error', "Lỗi!", response);
                    });
        }
        $scope.getAllUsers();

        $scope.getEditingUser = function (userIndexPosition) {
            //$scope.showModal = true;
            $scope.currentUserEditing = $scope.allUsers[userIndexPosition];
            $('html,body').animate({ scrollTop: $('.editCurrentUserEditing').offset().top });
        }

        $scope.updateUser = function (user) {
            $scope.clicked = true;
            user.userInfo.avatar = $scope.avatar;

            var bodyMessage = "Bạn muốn cập nhật người dùng: " + user.userName + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return accountService.updateUser(user).then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.currentUserEditing = null;
                    $scope.getAllUsers();
                    toaster.pop('success', "Thành công!", "Đã chỉnh sửa người dùng " + user.userName);
                    $('html,body').animate({ scrollTop: 0 });
                },
                function (response) {
                    $rootScope.showModal = false;
                    $scope.clicked = false;
                    toaster.pop('error', "Lỗi!", response);
                });
            }, function () { })
        }

        $scope.deleteUser = function (user) {

            var bodyMessage = "Bạn muốn xóa người dùng: " + user.userName + " ?";
            var dlg = dialogs.confirm('Xác nhận', bodyMessage, { size: 'md', keyboard: true, backdrop: false, windowClass: 'my-class' });

            dlg.result.then(function (btn) {
                $rootScope.showModal = true;
                return accountService.deleteUser(user).then(
                function (data) {
                    $rootScope.showModal = false;
                    $scope.getAllUsers();
                    toaster.pop('success', "Thành công!", "Đã xóa người dùng " + user.userName);
                    $scope.allUsers.splice($scope.allUsers.indexOf(user), 1);
                    $('html,body').animate({ scrollTop: 0 });
                },
                function (response) {
                    $rootScope.showModal = false;
                    toaster.pop('error', "Lỗi!", response);
                });
            }, function () { })
        }

        $scope.getAllRoles = function () {
            $rootScope.showModal = true;
            return accountService.getAllRoles().then(
                    function (data) {
                        $rootScope.showModal = false;
                        //console.log("all roles", data);
                    },
                    function () { $rootScope.showModal = true; });
        }
        $scope.getAllRoles();

        $scope.getRole = function (roleName) {
            return accountService.getRole(roleName).then(
                    function () { },
                    function () { });
        }

        $scope.createRole = function (roleName) {
            return accountService.createRole(roleName).then(
                    function () { },
                    function () { });
        }

        $scope.deleteRole = function (roleName) {
            return accountService.deleteRole(roleName).then(
                    function () { },
                    function () { });
        }

        $scope.editRole = function (roleObj) {
            return accountService.editRole(roleObj).then(
                    function () { },
                    function () { });
        }

        $scope.roleAddToUser = function (roleName, userName) {
            return accountService.roleAddToUser(roleName, userName).then(
                    function () { },
                    function () { });
        }

        $scope.getUserRoles = function (userName) {
            return accountService.roleAddToUser(userName).then(
                    function () { },
                    function () { });
        }

        $scope.DeleteRoleForUser = function (roleName, userName) {
            return accountService.DeleteRoleForUser(roleName, userName).then(
                    function () { },
                    function () { });
        }
    })
   .controller('changePasswordDialogCtrl', function ($scope, $modalInstance, data) {
       $scope.title = data.title;
       $scope.enableChange = true;
       $scope.ConfirmNewPassword = '';
       $scope.CurrentPassword = '';
       $scope.NewPassword = '';
       $scope.execAction = data.execAction;
       $scope.cancel = function () {
           $modalInstance.dismiss('Canceled');
       }; // end cancel
       $scope.checkDisabled = function () {
           $scope.enableChange = false;
           if ($scope.NewPassword == '' || $scope.ConfirmNewPassword == '' || $scope.CurrentPassword == '' || ($scope.ConfirmNewPassword != $scope.NewPassword) || $scope.NewPassword.length < 1)
               $scope.enableChange = true;


       }; // 
       $scope.Change = function () {
           $scope.execAction({ OldPassword: $scope.CurrentPassword, NewPassword: $scope.NewPassword, Sys_ViewID: 7, Action: 'UPDATE::CHANGEPASS' }, function () { alert('qq'); $modalInstance.dismiss('Canceled'); });
       }; // end save

       $scope.hitEnter = function (evt) {
           //if (angular.equals(evt.keyCode, 13) && !(angular.equals($scope.user.name, null) || angular.equals($scope.user.name, '')))
           //    $scope.save();
       };

       $scope.IsRequestObject = function (object) {
           return ($scope.dataSelected.RequestObjects & object == object) ? true : false;
       }
   })