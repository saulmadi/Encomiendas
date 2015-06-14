﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcklenAvenue.Commands;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Encomiendas.Domain.Application.CommandHandlers;
using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.DomainEvents;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;

using It = Machine.Specifications.It;

namespace Encomiendas.Domain.Specs
{
    public class when_enabling_a_user
    {
        static EnableUser _command;
        static IWriteableRepository _writeableRepository;
        static IReadOnlyRepository _readOnlyRepository;
        static ICommandHandler<EnableUser> _handler;
        static UserEnabled _expectedEvent;
        static object _eventRaised;
        static User _userEnabled;

        Establish context =
            () =>
            {
                _command = Builder<EnableUser>.CreateNew().Build();

                _userEnabled = Builder<User>.CreateNew().With(user => user.Id, _command.id).Build();
                _writeableRepository = Mock.Of<IWriteableRepository>();
                _readOnlyRepository = Mock.Of<IReadOnlyRepository>();

                Mock.Get(_readOnlyRepository)
                    .Setup(repository => repository.GetById<User>(_command.id))
                    .Returns(_userEnabled);

                _handler = new EnablingUser(_writeableRepository, _readOnlyRepository);

                _expectedEvent = new UserEnabled(_command.id);
                _handler.NotifyObservers += x => _eventRaised = x;
            };

        Because of =
            () => { _handler.Handle(Mock.Of<IUserSession>(),_command);};

        It should_enable_user =
            () => { _userEnabled.IsActive.ShouldBeTrue();};

        It should_throw_the_expected_event =
         () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}
