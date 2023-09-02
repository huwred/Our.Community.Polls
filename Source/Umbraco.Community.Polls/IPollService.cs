using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Community.Polls.Models;
using Umbraco.Community.Polls.Models.Repositories;

namespace Umbraco.Community.Polls
{
    public interface IPollService
    {
        Question GetQuestion(int questionId);
        Question Vote(int questionId, int answerId);
    }

    public class PollService : IPollService
    {
        private readonly IQuestions _questions;
        public PollService(IQuestions questions)
        {
            _questions = questions;
        }
        public Question GetQuestion(int questionId)
        {
            var question = _questions.GetById(questionId);
            question.Answers = _questions.GetAnswers(questionId).OrderBy(i => i.Index);

            var responses = _questions.GetResponses(questionId).ToList();

            question.ResponseCount = responses.Count;

            foreach (var answer in question.Answers)
            {
                var answerResponses = responses.Where(item => item.AnswerId.Equals(answer.Id)).ToList();

                answer.Responses = answerResponses;
                answer.Percentage = answerResponses.Any() ? Math.Round((double)(answerResponses.Count) / responses.Count * 100) : 0;
            }

            return question;
        }

        public Question Vote(int questionId, int answerId)
        {
            var question = _questions.GetById(questionId);

            var canVote = true;

            if(question.StartDate != null)
            {
                canVote = DateTime.Now > question.StartDate;
            }

            if (canVote && question.EndDate != null)
            {
                canVote = DateTime.Now < question.EndDate;
            }

            if (canVote)
            {
                var result = _questions.PostResponse(questionId, answerId);

                if (result != null)
                {
                    //PollsCacheRefresher.ClearCache(questionId);
                }
            }

            return GetQuestion(questionId);
        }
    }
}
