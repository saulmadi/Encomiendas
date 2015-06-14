﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class when_creating_a_new_user
    {
        static CreateEmailLoginUser _command;
        static IWriteableRepository _writeableRepository;
         static IReadOnlyRepository _readOnlyRepository;
        static ICommandHandler<CreateEmailLoginUser> _handler;
        static UserEmailCreated _expectedEvent;
        static object _eventRaised;
        static UserEmailLogin _userCreated;
        static IEnumerable<UserAbility> _userAbilities;
        static UserAbility _userAbility;
            
            
            Establish context =
            () =>
            {

                _userAbility = Builder<UserAbility>.CreateNew()
                                .Build();
                _userAbilities = new List<UserAbility>() {_userAbility};
                _command = new CreateEmailLoginUser("email", new EncryptedPassword("password"), "name", "password", _userAbilities);

                _userCreated = Builder<UserEmailLogin>.CreateNew()
                    .With(user => user.Id, Guid.NewGuid())
                    .With(user => user.Email, _command.Email)
                    .With(user => user.Name, _command.Name)
                    .With(user => user.EncryptedPassword,_command.EncryptedPassword.Password)
                    .With(user => user.PhoneNumber, _command.PhoneNumber)
                   
                    .Build();
                _userCreated.AddAbility(_userAbility);

                _writeableRepository = Mock.Of<IWriteableRepository>();
                _readOnlyRepository = Mock.Of<IReadOnlyRepository>();

                Mock.Get(_readOnlyRepository)
                    .Setup(repository => repository.GetById<UserAbility>(_userAbility.Id)).Returns(_userAbility);
                Mock.Get(_writeableRepository)
                    .Setup(repository => repository.Create(Moq.It.IsAny<UserEmailLogin>()))
                    .Returns(_userCreated);

                _handler = new UserEmailCreator(_writeableRepository, _readOnlyRepository);

                _expectedEvent = new UserEmailCreated(_userCreated.Id,_command.Email, _command.Name, _command.PhoneNumber);
                _handler.NotifyObservers += x => _eventRaised = x;
            };

        Because of =
            () => _handler.Handle(Mock.Of<IUserSession>(), _command);

        It should_create_the_new_user =
            () => Mock.Get(_writeableRepository).Verify(
                x =>
                    x.Create(Moq.It.Is<UserEmailLogin>(u =>
                        u.Name == _command.Name
                        && u.Email == _command.Email
                        && u.EncryptedPassword == _command.EncryptedPassword.Password
                        && u.PhoneNumber == _command.PhoneNumber
                        && u.UserAbilities.Contains(_userAbility) 
                        )));

      
        It should_throw_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}