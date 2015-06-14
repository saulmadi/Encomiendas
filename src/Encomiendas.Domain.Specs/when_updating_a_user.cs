using System;
using AcklenAvenue.Commands;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Encomiendas.Domain.Application.CommandHandlers;
using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.DomainEvents;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;
using Encomiendas.Domain.ValueObjects;
using It = Machine.Specifications.It;

namespace Encomiendas.Domain.Specs
{
    public class when_updating_a_user
    {
        static ICommandHandler<UpdateUserProfile> _handler;
        static IReadOnlyRepository _readonlyRepo;
        static User _user;
        static object _eventRaised;
        static object _expectedEvent;
        static UpdateUserProfile _command;

        Establish context =
            () =>
            {
                _readonlyRepo = Mock.Of<IReadOnlyRepository>();
                _handler = new UserProfileUpdater(_readonlyRepo);
                _user = new User("Test User", "test@email.com");

                _command = new UpdateUserProfile(_user.Id, "Test User Updated", "updated@email.com");

                Mock.Get(_readonlyRepo).Setup(x => x.GetById<User>(_user.Id))
                    .Returns(_user);

                _handler.NotifyObservers += x => _eventRaised = x;
                _expectedEvent = new UserProfileUpdated(_user.Id, _command.Name, _command.Email);
            };

        Because of =
            () =>
            _handler.Handle(new UserLoginSession(Guid.NewGuid(), _user, DateTime.Now), _command);

        It should_throw_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);

        It should_update_the_user_name =
            () => _user.Name.ShouldEqual(_command.Name);

        It should_update_the_user_email =
            () => _user.Email.ShouldEqual(_command.Email);

    }
}