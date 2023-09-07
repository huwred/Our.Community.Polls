using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Our.Community.Polls.Models;
using Our.Community.Polls.Models.Repositories;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace Our.Community.Polls.Controllers.ApiControllers
{
    public class OverviewApiController : UmbracoAuthorizedJsonController
    {
        private readonly IQuestions _questions;
        private readonly IAnswers _answers;
        private  readonly IResponses _responses;
        public OverviewApiController(IQuestions questions, IAnswers answers, IResponses responses)
        {
            _questions = questions;
            _answers = answers;
            _responses = responses;
        }
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            var questions = _questions.Get();
            var answers = _answers.Get();
            var responses = _responses.Get();

            foreach (var question in questions)
            {
                question.Answers = answers?.Where(answer => answer.QuestionId.Equals(question.Id));
                question.Responses = responses.Where(response => response.QuestionId.Equals(question.Id));
            }

            return questions;
        }
    }
}