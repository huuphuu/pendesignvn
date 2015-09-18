'use strict';

angular.module("adminApp")
    .factory("configService", ['toaster', '$resource', function (toaster, $resource) {
        var configService = {};

        var bannerResource = $resource('/admin/api/configs/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        configService.getConfig = function (id) {
            return bannerResource.query();
        }

        configService.updateConfig = function (config) {
            return bannerResource.update({ 'Id': config.id }, config);
        }
        return configService;
    }])