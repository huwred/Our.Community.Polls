using System;
using System.Linq;
using Our.Community.Polls.Models;
using Our.Community.Polls.PollConstants;
using Our.Community.Polls.Repositories;
using Umbraco.Cms.Core.Cache;
using Umbraco.Extensions;

namespace Our.Community.Polls.Interfaces
{
    public interface IPollService
    {
        Question GetQuestion(int questionId);
        Question Vote(int questionId, int answerId);
    }

    public class PollService : IPollService
    {
        private readonly IQuestions _questions;
        private readonly IAppPolicyCache _runtimeCache;
        public PollService(IQuestions questions, AppCaches appCaches)
        {
            _runtimeCache = appCaches.RuntimeCache;
            _questions = questions;
        }
        public Question GetQuestion(int questionId)
        {
            return _runtimeCache.GetCacheItem($"{RuntimeCacheConstants.RuntimeCacheKeyPrefix}{questionId}", () =>
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
            }, TimeSpan.FromMinutes(RuntimeCacheConstants.DefaultExpiration),true);


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
                    _runtimeCache.ClearByKey($"{RuntimeCacheConstants.RuntimeCacheKeyPrefix}{questionId}");
                }
            }

            return GetQuestion(questionId);
        }
    }
}
