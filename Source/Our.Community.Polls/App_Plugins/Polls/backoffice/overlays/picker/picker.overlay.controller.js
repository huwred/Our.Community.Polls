function PickerOverlayController($scope, $location, pollsResource) {

    $scope.isLoading = true;
    $scope.content = { questions: [], error: null };
    $scope.model.title = "Poll Selector";
    pollsResource.getQuestions().then(function (result) {
        
        $scope.content.questions = result.data;
        $scope.isLoading = false;

    }, function () {
        $scope.content.error = "An Error has occured while loading!";
        $scope.isLoading = false;
    });

    if (!$scope.model.selection) {
        $scope.model.selection = [];
    }

    $scope.pickPoll = function (question) {
        $scope.model.selection.push(question);
        $scope.model.submit($scope.model);
    }
    $scope.close = function () {
        if ($scope.model.close) {
            $scope.model.close();
        }
    }
    $scope.navigate = function (id) {
        var location = "/settings/poll/edit/" + id;
        if (id === "-1") {
            location += ('/?create');
        }
        console.log(location);
        $location.url(location);
    }
}

angular.module("umbraco").controller("Polls.PickerOverlayController", PickerOverlayController);