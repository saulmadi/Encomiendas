using System;
using System.Linq;
using AcklenAvenue.Commands;
using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.DomainEvents;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;

namespace Encomiendas.Domain.Application.CommandHandlers
{
    public class UserAbilitiesAdder: ICommandHandler<AddAbilitiesToUser>
    {
        public IWriteableRepository WriteableRepository { get; private set; }
        public IReadOnlyRepository ReadOnlyRepository { get; private set; }

        public UserAbilitiesAdder(IWriteableRepository writeableRepository, IReadOnlyRepository readOnlyRepository)
        {
            WriteableRepository = writeableRepository;
            ReadOnlyRepository = readOnlyRepository;
        }

        public void Handle(IUserSession userIssuingCommand, AddAbilitiesToUser command)
        {
            //TODO validate duplicate abilities
            var user = ReadOnlyRepository.GetById<User>(command.UserId);
            var abilities = command.AbilitiesID.ToList().Select(x => ReadOnlyRepository.GetById<UserAbility>(x));

            abilities.ToList().ForEach(user.AddAbility);

            WriteableRepository.Update(user);
            NotifyObservers(new UserAbilitiesAdded(user.Id, abilities.Select(x=>x.Id)));
        }

        public event DomainEvent NotifyObservers;
    }
}