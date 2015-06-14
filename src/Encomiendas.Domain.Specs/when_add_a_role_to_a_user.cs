using System;
using System.Collections.Generic;
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
using Encomiendas.Domain.ValueObjects;
using It = Machine.Specifications.It;

namespace Encomiendas.Domain.Specs
{
    public class when_add_a_role_to_a_user
    {
        static AddRoleToUser _command;
        static IWriteableRepository _writeableRepository;
        static IReadOnlyRepository _readOnlyRepository;
        static ICommandHandler<AddRoleToUser> _handler;
        static UserRoleAdded _expectedEvent;
        static object _eventRaised;
        static User _userCreated;
        static IIdentityGenerator<Guid> _identityGenerator;
        static Role _rolAdded;
            
            Establish context =
            () =>
            {
                
                _identityGenerator = Mock.Of<IIdentityGenerator<Guid>>();
                Mock.Get(_identityGenerator).Setup(generator => generator.Generate()).Returns(Guid.NewGuid);

                var _userId = _identityGenerator.Generate();
                var _rolId = _identityGenerator.Generate();

               
                _command = new AddRoleToUser(_userId,_rolId);

                _userCreated = Builder<User>.CreateNew()
                    .With(user => user.Email, "a@a.com")
                    .With(user => user.Name, "user")
                    .With(user => user.Id, _userId)
                    .Build();

                _rolAdded = Builder<Role>.CreateNew()
                    .With(role => role.Id, _rolId)
                    .With(role => role.Description, "Test Role")
                    .Build();

                _writeableRepository = Mock.Of<IWriteableRepository>();
                _readOnlyRepository = Mock.Of<IReadOnlyRepository>();

                
                Mock.Get(_writeableRepository)
                    .Setup(repository => repository.Update(Moq.It.Is<User>(user => user.Id == _userId)))
                    .Returns(_userCreated);

                Mock.Get(_readOnlyRepository)
                    .Setup(repository => repository.GetById<User>(_userCreated.Id)).Returns(_userCreated);

                Mock.Get(_readOnlyRepository)
                    .Setup(repository => repository.GetById<Role>(_rolAdded.Id)).Returns(_rolAdded);


                _handler = new UserRolAdder(_writeableRepository,_readOnlyRepository, _identityGenerator);

                _expectedEvent = new UserRoleAdded(_userCreated.Id, _rolAdded.Id);
                _handler.NotifyObservers += x => _eventRaised = x;
            };

        Because of =
            () => _handler.Handle(Mock.Of<IUserSession>(), _command);

        It should_create_the_new_user =
            () => Mock.Get(_writeableRepository).Verify(
                x =>
                    x.Update(Moq.It.Is<User>(u =>
                      u.Id == _userCreated.Id && u.UserRoles.Contains(_rolAdded))));

        It should_throw_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}
