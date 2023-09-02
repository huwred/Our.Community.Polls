function PollsResource($http) {
    return {
        // Overview
        getOverview: function () {
            return $http.get(Umbraco.Sys.ServerVariables.polllinks.getOverview);
        },

        // Questions
        getQuestions: function () {
            return $http.get(Umbraco.Sys.ServerVariables.polllinks.getQuestions);
        },
        getQuestionById: function (id) {
            return $http.get(Umbraco.Sys.ServerVariables.polllinks.getQuestionById + "?id=" + id);
        },
        getQuestionAnswersById: function (id) {
            return $http.get(Umbraco.Sys.ServerVariables.polllinks.getQuestionAnswersById + "?id=" + id);
        },
        getQuestionResponsesById: function (id) {
            return $http.get(Umbraco.Sys.ServerVariables.polllinks.getQuestionResponsesById + "?id=" + id);
        },
        saveQuestion: function (question) {

            return $http.post(Umbraco.Sys.ServerVariables.polllinks.saveQuestion, question);
        },
        postQuestionAnswer: function (id, question) {
            return $http.post(Umbraco.Sys.ServerVariables.polllinks.postQuestionAnswer + "?id=" + id, question);
        },
        deleteQuestion: function (id) {
            return $http.delete(Umbraco.Sys.ServerVariables.polllinks.deleteQuestion + "?id=" + id);
        }
    };
};

angular.module("umbraco.resources").factory("pollsResource", PollsResource)