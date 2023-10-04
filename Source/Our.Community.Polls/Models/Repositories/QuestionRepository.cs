using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NPoco;
using Our.Community.Polls.PollConstants;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Our.Community.Polls.Models.Repositories
{
    internal class QuestionRepository : IQuestions
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IAnswers _answers;
        private readonly IResponses _responses;

        public QuestionRepository(IScopeProvider scopeProvider,IAnswers answers,IResponses responses)
        {
            _scopeProvider = scopeProvider;
            _answers = answers;
            _responses = responses;
        }

        public IEnumerable<Question> Get()
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);

            var query = new Sql().Select("*").From(TableConstants.Questions.TableName);

            return scope.Database.Fetch<Question>();
        }

        public Question GetById(int id)
        {
            var query = new Sql().Select("*").From(TableConstants.Questions.TableName).Where($"id={id}");
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var question = scope.Database.Fetch<Question>(query).FirstOrDefault();
            question.Answers = GetAnswers(id);
            question.Responses = GetResponses(id);
            return question;

        }

        public Question Save(Question question)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            using (var transaction = scope.Database.GetTransaction())
            {
                if (question != null)
                {
                    if (question.Id > 0)
                    {
                        scope.Database.Update(question);
                    }
                    else
                    {
                        question.CreatedDate = DateTime.Now;
                        scope.Database.Save(question);
                    }
                    var oldAnswers = GetAnswers(question.Id).Where(a => !question.Answers.Any(r => r.Id.Equals(a.Id)));
                    foreach (var deletedAnswer in oldAnswers)
                    {
                        if (!_responses.DeleteByAnswerId(deletedAnswer.Id) || !_answers.Delete(deletedAnswer.Id))
                        {
                            return null;
                            //return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't delete old answers, Error add of update of the quesion");
                        }
                    }

                    // add or update answers
                    foreach (var answer in question.Answers)
                    {
                        var result = answer.Id != 0 ? _answers.Save(answer) : PostAnswer(question.Id, answer);

                        if (result == null)
                        {
                            return null; //this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't add answer to question");
                        }
                    }

                    transaction.Complete();
                }
            }

            return question;
        }

        public bool Delete(int id)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var result = 0;

            using (var transaction = scope.Database.GetTransaction())
            {
                var responses = GetResponses(id);
                var answers = GetAnswers(id);

                foreach (var response in responses)
                {
                    if (!_responses.Delete(response.Id))
                    {
                        return false;// this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't delete question, Error removing the responses");
                    }
                }

                foreach (var answer in answers)
                {
                    if (!_answers.Delete(answer.Id))
                    {
                        return false;// this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't delete question, Error removing the answers");
                    }
                }
                result = scope.Database.Delete<Question>(id);
                transaction.Complete();
            }

            return result > 0;
        }

        public IEnumerable<Answer> GetAnswers(int id)
        {
            var query = new Sql().Select("*").From(TableConstants.Answers.TableName).Where($"QuestionId = {id}");
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            return scope.Database.Fetch<Answer>(query);
        }

        public Answer PostAnswer(int id, Answer answer)
        {
            if (answer != null)
            {
                answer.QuestionId = id;
                using var scope = _scopeProvider.CreateScope(autoComplete: true);
                scope.Database.Save(answer);
            }

            return answer;
        }

        public IEnumerable<Response> GetResponses(int id)
        {
            var query = new Sql().Select("*").From(TableConstants.Responses.TableName).Where($"QuestionId = {id}");
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            return scope.Database.Fetch<Response>(query);
        }

        public Response PostResponse(int id, int answerId)
        {
            var response = new Response { QuestionId = id, AnswerId = answerId, ResponseDate = DateTime.Now.Date };
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            scope.Database.Save(response);

            return response;
        }
    }

    public interface IQuestions
    {
        Response PostResponse(int id, int answerId);
        IEnumerable<Response> GetResponses(int id);
        Answer PostAnswer(int id, Answer answer);
        bool Delete(int id);
        IEnumerable<Answer> GetAnswers(int id);
        Question Save(Question question);
        Question GetById(int id);

        IEnumerable<Question> Get();

    }
}
