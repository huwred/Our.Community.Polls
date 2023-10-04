using System.Collections.Generic;
using System.Linq;
using NPoco;
using Our.Community.Polls.Models;
using Our.Community.Polls.PollConstants;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Our.Community.Polls.Repositories
{
    public interface IResponses
    {
        IEnumerable<Response> Get();
        Response GetById(int id);
        bool Delete(int id);
        bool DeleteByAnswerId(int id);
    }

    internal class ResponseRepository : IResponses
    {
        private readonly IScopeProvider _scopeProvider;

        public ResponseRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public IEnumerable<Response> Get()
        {
            var query = new Sql().Select("*").From(TableConstants.Responses.TableName);
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            return scope.Database.Fetch<Response>(query);
        }

        public Response GetById(int id)
        {
            var query = new Sql().Select("*").From(TableConstants.Responses.TableName).Where($"id={id}");
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            return scope.Database.Fetch<Response>(query).FirstOrDefault();
        }

        public bool Delete(int id)
        {
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            var result = scope.Database.Delete<Response>(id);

            return result > 0;
        }

        public bool DeleteByAnswerId(int id)
        {
            var query = new Sql().Where($"answerid={id}");
            using var scope = _scopeProvider.CreateScope(autoComplete: true);
            return scope.Database.Delete<Response>(query) > -1;
        }
    }
}