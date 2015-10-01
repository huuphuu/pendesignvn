'use strict';

angular.module("adminApp")
    .factory("bannerService", ['toaster', '$resource', function (toaster, $resource) {
        var bannerService = {};

        var bannerResource = $resource('/admin/api/banner/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        bannerService.getAllBanners = function (id) {
            return bannerResource.query({}, { 'Id': id });
        }

        bannerService.addNewBanner = function (banner) {
            return bannerResource.save(banner);
        }

        bannerService.deleteBanner = function (banner) {
            return bannerResource.delete({ 'Id': banner.id });
        }

        bannerService.updateBanner = function (banner) {
            return bannerResource.update({ 'Id': banner.id }, banner);
        }
        return bannerService;
    }])