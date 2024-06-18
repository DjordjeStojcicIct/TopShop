using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Exceptions;
using TopShop.Application.Logging;
using TopShop.Application.UseCases;
using static TopShop.Application.Logging.IUseCaseLogger;

namespace TopShop.Application.UseCaseHandling
{
    public interface IQueryHandler
    {
        TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
            where TResult : class;
    }
    public class QueryHandler : IQueryHandler
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;
        public QueryHandler(IApplicationActor actor, IUseCaseLogger logger) 
        {
            _actor = actor;
            _logger = logger;
        }

        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
            where TResult : class
        {
            if (!_actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedException(_actor.Username, query.Name);
            }

            _logger.Log(new UseCaseLogEntry
            {
                Actor = _actor.Username,
                ActorId = _actor.Id,
                UseCaseName = query.Name,
            });

            return query.Execute(search);
        }
    }
}
