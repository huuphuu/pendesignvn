'use strict';

angular.module("adminApp")
    .factory("videoService", ['toaster', '$resource', function (toaster, $resource) {
        var videoService = {};

        var videoResource = $resource('/admin/api/video/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });
        
        videoService.getAllVideos = function () {
            return videoResource.query();
        }

        //videoService.getAllVideos = function (id) {
        //    return videoResource.query({}, { 'Id': id });
        //}

        //videoService.getNewsById = function (news) {
        //    return videoResource.query({}, { 'Id': news.id });
        //}

        videoService.addNewVideo = function (video) {
            return videoResource.save(video);
        }

        videoService.deleteVideo = function (video) {
            return videoResource.delete({ 'Id': video.id });
        }

        videoService.updateVideo= function (video) {
            return videoResource.update({ 'Id': video.id }, video);
        }
        return videoService;
    }])