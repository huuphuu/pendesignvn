'use strict';

angular.module("adminApp")
    .factory("projectImageService", ['toaster', '$resource', function (toaster, $resource) {
        var projectImageService = {};

        var projectImageResource = $resource('/admin/api/projectImage/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });


        projectImageService.updateImage = function (news) {
            return projectImageResource.update({ 'Id': news.id }, news);
        }
        return projectImageService;
    }])