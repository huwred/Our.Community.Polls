using System.Collections.Generic;
using System.Linq;
using Our.Community.Polls.Models;
using Our.Community.Polls.PollConstants;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Our.Community.Polls.Repositories
{
    public interface IAnswers
    {
        IEnumerable<Answer> Get();
        Answer GetById(int id);
        Answer Save(Answer answer);
        bool Delete(int id);
    }

    internal class AnswerRepository : IAnswers
    {
        private readonly IScopeProvider _scopeProvider;

        public AnswerRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public IEnumerable<Answer> Get()
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);

            return scope.Database.Query<Answer>($@"SELECT * FROM {TableConstants.Answers.TableName}");

        }

        public Answer GetById(int id)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);

            return scope.Database.Query<Answer>($@"SELECT * FROM {TableConstants.Answers.TableName} WHERE id={id}").FirstOrDefault();

        }

        public Answer Save(Answer answer)
        {
            if (answer is { Id: > 0 })
            {
                using var scope = _scopeProvider.CreateScope(autoComplete: true);
                scope.Database.Update(answer);
            }

            return answer;
        }

        public bool Delete(int id)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var result = scope.Database.Delete<Answer>(id);

            return result > 0;
        }
    }
}