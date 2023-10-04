function DeleteController($scope, $location, navigationService, treeService, editorState, notificationsService, pollsResource) {
    $scope.performDelete = function () {
        // stop from firing again on double-click
        if ($scope.busy) {
            return false;
        }

        //mark it for deletion (used in the UI)
        $scope.currentNode.loading = true;
        $scope.busy = true;

        pollsResource.deleteQuestion($scope.currentNode.id).then(function () {
            $scope.currentNode.loading = false;

            treeService.removeNode($scope.currentNode);

            navigationService.hideMenu();
            $location.path("/settings/poll/overview");

        }, function (err) {
            $scope.currentNode.loading = false;
            $scope.busy = false;

            //check if response is ysod
            if (err.status && err.status >= 500) {
                alert(err);
            }

            if (err.data && angular.isArray(err.data.notifications)) {
                for (var i = 0; i < err.data.notifications.length; i++) {
                    notificationsService.showNotification(err.data.notifications[i]);
                }
            }
        });

        return true;
    };

    $scope.cancel = function () {
        navigationService.hideDialog();
    };
}

angular.module("umbraco").controller("Polls.DeleteController", DeleteController);