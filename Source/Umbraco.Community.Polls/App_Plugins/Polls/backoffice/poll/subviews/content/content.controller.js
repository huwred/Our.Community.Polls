function ContentController($scope, $routeParams, angularHelper) {
    $scope.answerInput = { value: '', hasError: false };

    $scope.amountOfAnswers = "polls:AmountOfAnswers"; //Umbraco.Sys.ServerVariables.polls.AmountOfAnswers;

    $scope.sortableOptions = {
        axis: 'y',
        containment: 'parent',
        cursor: 'move',
        items: '> div.control-group',
        tolerance: 'pointer',
        stop: function (ev, ui) {
            angularHelper.safeApply($scope, function () {
                angular.forEach($scope.model.question.answers, function (val, index) {
                    val.index = index;
                });
            });
        }
    };
    $scope.dateTimeConfig = {
        enableTime: false,
        dateFormat: "Y-m-d",
        time_24hr: true
    };
    $scope.datePickerFromChange = datePickerFromChange;
    $scope.datePickerToChange = datePickerToChange;
    function datePickerFromChange(selectedDates, dateStr, instance) {
        console.log(selectedDates);
        $scope.model.question.startDate = dateStr;
    }
    function datePickerToChange(selectedDates, dateStr, instance) {
        console.log(selectedDates);
        $scope.model.question.endDate = dateStr;
    }

    $scope.addAnswer = function (event) {
        event.preventDefault();

        if ($scope.answerInput.value) {
            if (!_.find($scope.model.question.answers, function (item) { return item.value === $scope.answerInput.value })) {
                var answer = { value: $scope.answerInput.value, index: $scope.model.question.answers.length };

                $scope.model.question.answers.push(answer);
                $scope.answerInput = { value: '', hasError: false };
            }
        } else {
            $scope.answerInput.hasError = true;
        }
    };

    $scope.updateAnswer = function (answer) {
        if (!_.find($scope.model.question.answers, function (item) { return item.value === answer.value && item.id !== answer.id })) {
            answer.hasError = false;
        } else {
            answer.hasError = true;
        }
    };

    $scope.removeAnswer = function (answer, event) {
        event.preventDefault();

        $scope.model.question.answers = _.reject($scope.model.question.answers, function (x) {
            return x.index === answer.index;
        });

        angular.forEach($scope.model.question.answers, function (val, index) {
            val.index = index;
        });
    };
}

angular.module("umbraco").controller("Polls.ContentController", ContentController);