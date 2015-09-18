'use strict';

angular.module("adminApp")
    .factory("projectService", ['toaster', '$resource', function (toaster, $resource) {
        var projectService = {};

        var projectResource = $resource('/admin/api/project/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        projectService.getAllProjects = function () {
            return projectResource.query();
        }

        //projectService.getAllProjects = function (id) {
        //    return projectResource.query({}, { 'Id': id });
        //}

        //projectService.getNewsById = function (news) {
        //    return projectResource.query({}, { 'Id': news.id });
        //}

        projectService.addNewProject = function (project) {
            return projectResource.save(project);
        }

        projectService.deleteProject = function (project) {
            return projectResource.delete({ 'Id': project.id });
        }

        projectService.updateProject = function (project) {
            return projectResource.update({ 'Id': project.id }, project);
        }
        return projectService;
    }])