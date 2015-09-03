'use strict';

angular.module("adminApp")
    .factory("galleryService", ['toaster', '$resource', function (toaster, $resource) {
        var galleryService = {};

        var galleryResource = $resource('/admin/api/gallery/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });


        galleryService.getAllGalleries = function (galleryCategoryId) {
            return galleryResource.query({}, { 'Id': galleryCategoryId });
        }

        galleryService.addNewGallery = function (gallery) {
            return galleryResource.save(gallery);
        }

        galleryService.deleteGallery = function (gallery) {
            return galleryResource.delete({'Id': gallery.id});
        }

        galleryService.updateGallery = function (gallery) {
            return galleryResource.update({ 'Id': gallery.id}, gallery);
        }
        return galleryService;
    }])