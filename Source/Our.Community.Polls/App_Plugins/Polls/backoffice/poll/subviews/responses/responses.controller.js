﻿function ResponsesController($scope, $routeParams, $filter, notificationsService, pollsResource) {

    $scope.responses = [];
    console.log("ResponsesController");
    if (!$routeParams.create) {
        pollsResource.getQuestionResponsesById($routeParams.id).then(function (result) {
            $scope.responses = result.data;
        }, function (result) {
            notificationsService.error(result.data.message);
        });
    }

    $scope.getResponsesPercentage = function (id) {
        var amount = $filter('filter')($scope.responses, { answerId: id }).length;
        return amount > 0 ? Math.round(amount / $scope.responses.length * 100) : 0;
    };

    $scope.getResponses = function (id) {
        return $filter('filter')($scope.responses, { answerId: id }).length;
    };
}

angular.module("umbraco").controller("Polls.ResponsesController", ResponsesController);