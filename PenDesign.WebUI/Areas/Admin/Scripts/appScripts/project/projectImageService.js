'use strict';

angular.module("adminApp")
    .factory("projectImageService", ['toaster', '$resource', function (toaster, $resource) {
        var projectImageService = {};

        var projectImageResource = $resource('/admin/api/projectImage/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        projectImageService.addNewProjectImage = function (projectImage) {
            return projectImageResource.save(projectImage);
        }

        projectImageService.updateProjectImage = function (projectImage) {
            return projectImageResource.update({ 'Id': projectImage.id }, projectImage);
        }

        projectImageService.deleteProjectImage = function (projectImage) {
            return projectImageResource.delete({ 'Id': projectImage.id });
        }

        return projectImageService;
    }])