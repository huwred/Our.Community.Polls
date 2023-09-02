function PickerController($scope, localizationService, pollsResource, editorService) {
    $scope.content = { selection: null, error: null };

    if ($scope.model.value) {
        pollsResource.getQuestionById($scope.model.value).then(function(result) {
            $scope.content.selection = result.data;
        }, function(error) {
            var currentValue = angular.copy($scope.model.value);
            $scope.content.error = "The poll with id '" + currentValue + "' no longer exists. Please chose another poll";
        });
    }

    $scope.openPollPicker = function () {
        
        if (!$scope.content.pollPickerOverlay) {
            $scope.content.pollPickerOverlay = {
                view: "/App_Plugins/Polls/backoffice/overlays/picker/picker.overlay.html",
                title: localizationService.localize("polls_selectQuestions"),
                show: true,
                size: "small",
                hideSubmitButton: true,
                submit: function (model) {
                    
                    if (model.selection && model.selection.length > 0) {
                        $scope.content.selection = model.selection[0];
                        $scope.model.value = model.selection[0].id;
                    }

                    $scope.content.pollPickerOverlay.show = false;
                    $scope.content.pollPickerOverlay = null;
                    $scope.content.error = null;
                    editorService.close($scope.content.pollPickerOverlay);
                },
                close: function () {
                    editorService.close($scope.content.pollPickerOverlay);
                    $scope.content.pollPickerOverlay.show = false;
                    $scope.content.pollPickerOverlay = null;
                    $scope.content.error = null;
                }
            };
        }
        editorService.open($scope.content.pollPickerOverlay);
    };

    $scope.remove = function () {
        $scope.content.selection = null;
        $scope.model.value = null;
    };
}

angular.module("umbraco").controller("Polls.PickerController", PickerController);