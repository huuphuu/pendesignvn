'use strict';

angular.module("adminApp")
    .factory("courseService", ['toaster', '$resource', function (toaster, $resource) {
        var courseService = {};

        var courseResource = $resource('/admin/api/course/:Id', { Id: '@Id' }, {
            'update': { method: 'PUT' }
        });

        courseService.getAllCourse = function () {
            return courseResource.query();
        }

        courseService.getCourseById = function (course) {
            return courseResource.query({}, {'Id': course.id});
        }

        courseService.addNewCourse = function (course) {
            return courseResource.save(course);
        }

        courseService.deleteCourse = function (course) {
            return courseResource.delete({'Id': course.id});
        }

        courseService.updateCourse = function (course) {
            return courseResource.update({ 'Id': course.id}, course);
        }
        return courseService;
    }])