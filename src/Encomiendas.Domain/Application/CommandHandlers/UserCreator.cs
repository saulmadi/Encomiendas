using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.DomainEvents;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;

namespace Encomiendas.Domain.Application.CommandHandlers
{
    public class UserCreator : ICommandHandler<CreateEmailLoginUser>
    {
        readonly IWriteableRepository _writeableRepository;

        public UserCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        #region ICommandHandler Members

        public void Handle(IUserSession userIssuingCommand, CreateEmailLoginUser command)
        {
            _writeableRepository.Create(new UserEmailLogin(command.Name, command.Email, command.EncryptedPassword, command.PhoneNumber));
            NotifyObservers(new UserCreated(command.Email, command.Name, command.PhoneNumber));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}