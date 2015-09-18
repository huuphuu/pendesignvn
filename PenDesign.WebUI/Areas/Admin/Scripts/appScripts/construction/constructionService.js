'use strict';

angular.module("adminApp")
    .factory("constructionService", ['toaster', '$resource', function (toaster, $resource) {
        var constructionService = {};

        var constructionResource = $resource('/admin/api/construction/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        constructionService.getAllConstruction = function () {
            return constructionResource.query();
        }

        //constructionService.getAllNews = function (id) {
        //    return constructionResource.query({}, { 'Id': id });
        //}

        //constructionService.getNewsById = function (news) {
        //    return constructionResource.query({}, { 'Id': news.id });
        //}

        constructionService.addNewConstruction = function (construction) {
            return constructionResource.save(construction);
        }

        constructionService.deleteConstruction = function (construction) {
            return constructionResource.delete({ 'Id': construction.id });
        }

        constructionService.updateConstruction = function (construction) {
            return constructionResource.update({ 'Id': construction.id }, construction);
        }
        return constructionService;
    }])