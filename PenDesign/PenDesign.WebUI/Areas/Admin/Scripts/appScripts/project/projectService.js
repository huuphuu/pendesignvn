'use strict';

angular.module("adminApp")
    .factory("projectService", ['toaster', '$resource', function (toaster, $resource) {
        var projectService = {};

        var newsResource = $resource('/admin/api/project/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        projectService.getAllProjects = function (id) {
            return newsResource.query({}, { 'Id': id });
        }

        //projectService.getNewsById = function (news) {
        //    return newsResource.query({}, { 'Id': news.id });
        //}

        projectService.addNewProject = function (project) {
            return newsResource.save(project);
        }

        projectService.deleteProject = function (project) {
            return newsResource.delete({ 'Id': project.id });
        }

        projectService.updateProject = function (project) {
            return newsResource.update({ 'Id': project.id }, project);
        }
        return projectService;
    }])