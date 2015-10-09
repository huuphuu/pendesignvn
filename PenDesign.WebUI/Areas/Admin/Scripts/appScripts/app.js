'use strict';

var app = angular.module('adminApp', ['as.sortable','todo','ui.bootstrap', 'dialogs.main', 'toaster', 'angularMoment', 'ui.router', 'angularFileUpload', 'ngCookies', 'ngResource', 'ngMessages', 'datatables', 'datatables.tabletools']);

//ui.router
app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $urlRouterProvider.otherwise('/controlPanel/banner-list');

    $stateProvider
        //.state('login', {
        //    url: '/login',
        //    templateUrl: '/Areas/Admin/Templates/Views/Account/login.view.html'
        //})
        .state('controlPanel', {
            url: '/controlPanel',
            templateUrl: '/Areas/Admin/Templates/CP.index.html',
        })
        //Index
        .state('controlPanel.index', {
            url: '/index',
            templateUrl: '/Areas/Admin/Templates/Views/Index/index.html'
        })
        //Banner
        .state('controlPanel.addBanner', {
            url: '/add-banner',
            templateUrl: '/Areas/Admin/Templates/Views/Banner/addBanner.view.html'
        })
        .state('controlPanel.bannerList', {
            url: '/banner-list',
            templateUrl: '/Areas/Admin/Templates/Views/Banner/banner.list.view.html'
        })
        //User
        .state('controlPanel.register', {
            url: '/user-regiser',
            templateUrl: '/Areas/Admin/Templates/Views/User/user-register.view.html'
        })
        .state('controlPanel.userList', {
            url: '/users-list',
            templateUrl: '/Areas/Admin/Templates/Views/User/user.list.view.html'
        })
        //News
        .state('controlPanel.addNews', {
            url: '/add-News/:newsCategoryId',
            templateUrl: '/Areas/Admin/Templates/Views/News/addNews.view.html',
            controller: 'newsController'
        })
        .state('controlPanel.newsList', {
            url: '/news-list/:newsCategoryId',
            templateUrl: '/Areas/Admin/Templates/Views/News/news.list.view.html',
            controller: 'newsController'
        })
         //Project
        .state('controlPanel.addProject', {
            url: '/add-project',
            templateUrl: '/Areas/Admin/Templates/Views/Project/addProject.view.html'
        })
        .state('controlPanel.projectList', {
            url: '/project-list',
            templateUrl: '/Areas/Admin/Templates/Views/Project/project.list.view.html'
        })
         //Construction
        .state('controlPanel.addConstruction', {
            url: '/add-construction',
            templateUrl: '/Areas/Admin/Templates/Views/Construction/addConstruction.view.html'
        })
        .state('controlPanel.constructionList', {
            url: '/Construction-list',
            templateUrl: '/Areas/Admin/Templates/Views/Construction/construction.list.view.html'
        })
        //Video
        .state('controlPanel.addVideo', {
            url: '/add-video',
            templateUrl: '/Areas/Admin/Templates/Views/Video/addVideo.view.html'
        })
        .state('controlPanel.videoList', {
            url: '/video-list',
            templateUrl: '/Areas/Admin/Templates/Views/Video/video.list.view.html'
        })
        //Email
        .state('controlPanel.contactEmail', {
            url: '/contact-email-list',
            templateUrl: '/Areas/Admin/Templates/Views/Email/contactEmail.list.view.html'
        })
        .state('controlPanel.email', {
            url: '/email-list',
            templateUrl: '/Areas/Admin/Templates/Views/Email/email.list.view.html'
        })
        //Configs
        .state('controlPanel.configs', {
            url: '/configs',
            templateUrl: '/Areas/Admin/Templates/Views/Configs/configs.view.html',
            controller: 'configController'
        })
    //if (window.history && window.history.pushState) {
    //    $locationProvider.html5Mode(true);
    //}
})
.run(['$templateCache', function ($templateCache) {
    $templateCache.put('/Areas/Admin/Templates/dialogs/confirmDialog.html',
                        '<div class="modal-header">' +
                            '<h4 class="modal-title"><span class="glyphicon glyphicon-star"></span> User\'s Name</h4></div>' +
                        '<div class="modal-body">' +
                            '<ng-form name="nameDialog" novalidate role="form">' +
                                '<div class="form-group input-group-lg" ng-class="{true: \'has-error\'}[nameDialog.username.$dirty && nameDialog.username.$invalid]">' +
                                    '<label class="control-label" for="course">Name:</label>' +
                                    '<input type="text" class="form-control" name="username" id="username" ng-model="user.name" ng-keyup="hitEnter($event)" required><span class="help-block">Enter your full name, first &amp; last.</span></div>' +
                            '</ng-form>' +
                        '</div>' +
                        '<div class="modal-footer">' +
                            '<button type="button" class="btn btn-default" ng-click="cancel()">Cancel</button>' +
                            '<button type="button" class="btn btn-primary" ng-click="save()" ng-disabled="(nameDialog.$dirty && nameDialog.$invalid) || nameDialog.$pristine">Save</button>' +
                        '</div>');
}]);


/****CONSTANT*******************/
var controls = {
    BUTTON: 'button',
    ICON_AND_TEXT: 'button&text',
    LIST_ICON: 'listicon',
    IMAGE:'image'
}
