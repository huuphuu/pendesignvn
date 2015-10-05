'use strict';

angular.module("adminApp")
    .factory("newsMappingService", ['toaster', '$resource', function (toaster, $resource) {
        var newsMappingService = {};

        var newsMappingResource = $resource('/admin/api/newsMapping/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });


        newsMappingService.updateNews = function (news) {
            return newsMappingResource.update({ 'Id': news.id }, news);
        }
        return newsMappingService;
    }])