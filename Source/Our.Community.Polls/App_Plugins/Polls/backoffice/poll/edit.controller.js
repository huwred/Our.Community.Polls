function EditController($scope, $routeParams, $filter, $location, notificationsService, navigationService, editorState, formHelper, localizationService, pollsResource) {
    $scope.page = { isLoading: false };
    $scope.model = { question: { answers: [] } };
    $scope.page.navigation = [
        {
            "name": "Content",//localizationService.localize("polls_tabContent"),
            "alias": "content",
            "icon": "icon-document",
            "view": "/App_Plugins/Polls/backoffice/poll/subviews/content/content.html",
            "active": true
        }
        ,{
            "name": "Responses", //localizationService.localize("polls_tabResponses"),
            "alias": "responses",
            "icon": "icon-poll",
            "view": "/App_Plugins/Polls/backoffice/poll/subviews/responses/responses.html"
        }
    ];


    if (!$routeParams.create && !($routeParams.id === "-1")) {
        $scope.page.isLoading = true;

        pollsResource.getQuestionById($routeParams.id).then(function (result) {
            $scope.model.question = result.data;

            //set a shared state
            editorState.set($scope.model.question);

            pollsResource.getQuestionAnswersById($routeParams.id).then(function (result) {
                $scope.model.question.answers = $filter('orderBy')(result.data, 'index');
                $scope.page.isLoading = false;
            }, function (result) {
                notificationsService.error(result.data.message);
            });
        }, function (result) {
            notificationsService.error(result.data.message);
        });
    }
    $scope.delete = function() {
        if (formHelper.submitForm({ scope: $scope, statusMessage: "Deleting..." })) {
            $scope.page.delButtonState = "busy";
            pollsResource.deleteQuestion($scope.model.question).then(function(result) {
                formHelper.resetForm({ scope: $scope });

                $scope.page.delButtonState = "success";

                $location.url("/settings/poll/overview");

            }, function (result) {
                notificationsService.error(result.data.message);
                $scope.page.delButtonState = "error";
            });
        }
    }
    $scope.save = function () {
        console.log($scope.model.question);
        if (formHelper.submitForm({ scope: $scope, statusMessage: "Saving..." })) {
            $scope.page.saveButtonState = "busy";

            if ($scope.model.question.startDate && $scope.model.question.endDate) {
                if (new Date($scope.model.question.startDate) >= new Date($scope.model.question.endDate)) {
                    notificationsService.error(localizationService.localize("polls_startDateBeforeEndDate"));
                    $scope.page.saveButtonState = "error";

                    return;
                }
            }

            pollsResource.saveQuestion($scope.model.question).then(function (result) {
                
                formHelper.resetForm({ scope: $scope });

                $scope.page.saveButtonState = "success";

                $scope.model.question = result.data;

                //set a shared state
                editorState.set($scope.model.question);

                navigationService.syncTree({ tree: 'poll', path: ['-1', $scope.model.question.id.toString()], forceReload: true, activate: true });

                if ($routeParams.create) {
                    console.log($scope.model.question.id);
                    debugger;
                    $location.url("/settings/poll/edit/" + $scope.model.question.id);
                }
            }, function (result) {
                notificationsService.error(result.data.message);
                $scope.page.saveButtonState = "error";
            });
        }
    };
}

angular.module("umbraco").controller("Polls.EditController", EditController);