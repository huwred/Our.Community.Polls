function PickerOverlayController($scope, pollsResource) {

    $scope.isLoading = true;
    $scope.content = { questions: [], error: null }

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
}

angular.module("umbraco").controller("Polls.PickerOverlayController", PickerOverlayController);