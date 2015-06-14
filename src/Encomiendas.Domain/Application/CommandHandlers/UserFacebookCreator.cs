using System;
using AcklenAvenue.Commands;
using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.DomainEvents;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;


namespace Encomiendas.Domain.Application.CommandHandlers
{
    public class UserFacebookCreator : ICommandHandler<CreateFacebookLoginUser>
    {
        readonly IWriteableRepository _writeableRepository;

        public UserFacebookCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        public void Handle(IUserSession userIssuingCommand, CreateFacebookLoginUser command)
        {
            var userCreated = _writeableRepository.Create(new UserFacebookLogin(command.name,command.email,command.id,command.firstName,command.lastName,command.imageUrl,command.link));
            NotifyObservers(new UserFacebookCreated(userCreated.Id, command.email, command.name, command.id));
        }

        public event DomainEvent NotifyObservers;
    }
}