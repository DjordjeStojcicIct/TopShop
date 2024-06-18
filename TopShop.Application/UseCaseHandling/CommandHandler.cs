using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TopShop.Application.Exceptions;
using TopShop.Application.Logging;
using TopShop.Application.UseCases;
using static TopShop.Application.Logging.IUseCaseLogger;

namespace TopShop.Application.UseCaseHandling
{
    public interface ICommandHandler
    {
        void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data);
    }

    public class CommandHandler : ICommandHandler
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;

        public CommandHandler(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            if (!_actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedException(_actor.Username, command.Name);
            }

            _logger.Log(new UseCaseLogEntry
            {
                Actor =  _actor.Username,
                ActorId = _actor.Id,
                UseCaseName = command.Name,
            });

            command.Execute(data);
        }
    }
}
