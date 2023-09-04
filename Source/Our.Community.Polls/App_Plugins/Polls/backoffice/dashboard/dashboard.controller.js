function DashboardController($scope, $location, $filter, notificationsService, pollsResource) {
    $scope.page = { loading: false };
    $scope.content = { questions: [] };

    pollsResource.getOverview().then(function (result) {
        $scope.content.questions = result.data;
        $scope.page.isLoading = false;
    }, function (result) {
        notificationsService.error(result.data.message);
    });

    $scope.navigate = function (id) {
        var location = "/settings/poll/edit/" + id;
        if (id === "-1") {
            location += ('?create');
        }
        
        $location.url(location);
    }

    $scope.getResponsesPercentage = function (question, id) {
        var amount = $filter('filter')(question.Responses, { AnswerId: id }).length;
        return amount > 0 ? Math.round(amount / question.Responses.length * 100) : 0;
    }

    $scope.getResponses = function (question, id) {
        return $filter('filter')(question.Responses, { AnswerId: id }).length;
    }
}

angular.module("umbraco").controller("Polls.DashboardController", DashboardController);