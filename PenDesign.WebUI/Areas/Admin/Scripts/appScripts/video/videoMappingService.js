'use strict';

angular.module("adminApp")
    .factory("videoMappingService", ['toaster', '$resource', function (toaster, $resource) {
        var videoMappingService = {};

        var videoMappingResource = $resource('/admin/api/videoMapping/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });
        
        videoMappingService.updateVideo = function (video) {
            return videoMappingResource.update({ 'Id': video.id }, video);
        }
        return videoMappingService;
    }])