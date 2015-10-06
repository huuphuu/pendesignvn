'use strict';

angular.module("adminApp")
    .factory("projectNewsService", ['toaster', '$resource', function (toaster, $resource) {
        var projectNewsService = {};

        var projectNewsResource = $resource('/admin/api/projectNews/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });


        projectNewsService.updateNews = function (news) {
            return projectNewsResource.update({ 'Id': news.id }, news);
        }
        return projectNewsService;
    }])