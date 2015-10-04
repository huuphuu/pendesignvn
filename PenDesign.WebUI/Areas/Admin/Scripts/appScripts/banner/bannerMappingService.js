'use strict';

angular.module("adminApp")
    .factory("bannerMappingService", ['toaster', '$resource', function (toaster, $resource) {
        var bannerMappingService = {};

        var bannerMappingResource = $resource('/admin/api/bannerMapping/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

//        bannerMappingService.getAllBanners = function (id) {
//            return bannerMappingResource.query({}, { 'Id': id });
//        }
//
//        bannerMappingService.addNewBanner = function (banner) {
//            return bannerMappingResource.save(banner);
//        }
//
//        bannerMappingService.deleteBanner = function (banner) {
//            return bannerMappingResource.delete({ 'Id': banner.id });
//        }

        bannerMappingService.updateBanner = function (banner) {
            return bannerMappingResource.update({ 'Id': banner.id }, banner);
        }
        return bannerMappingService;
    }])