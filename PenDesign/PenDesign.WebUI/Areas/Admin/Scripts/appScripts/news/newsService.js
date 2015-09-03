'use strict';

angular.module("adminApp")
    .factory("newsService", ['toaster', '$resource', function (toaster, $resource) {
        var newsService = {};

        var newsResource = $resource('/admin/api/news/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        newsService.getAllNews = function (id) {
            return newsResource.query({}, { 'Id': id });
        }

        //newsService.getNewsById = function (news) {
        //    return newsResource.query({}, { 'Id': news.id });
        //}

        newsService.addNewNews = function (news) {
            return newsResource.save(news);
        }

        newsService.deleteNews = function (news) {
            return newsResource.delete({ 'Id': news.id });
        }

        newsService.updateNews = function (news) {
            return newsResource.update({ 'Id': news.id }, news);
        }
        return newsService;
    }])