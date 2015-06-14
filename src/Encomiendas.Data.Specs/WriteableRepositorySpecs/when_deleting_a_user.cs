using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Data.NHibernate.Testing;
using Encomiendas.Domain;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;
using Encomiendas.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Encomiendas.Data.Specs.WriteableRepositorySpecs
{
    public class when_deleting_a_user
    {
        static IWriteableRepository _writeableRepository;
        static ISession _session;
        static UserEmailLogin _userToDelete;

        Establish context =
            () =>
                {
                    _session = InMemorySession.New(new MappingScheme("Test"));
                    _writeableRepository = new WriteableRepository(_session);

                    var users = new List<UserEmailLogin>
                                    {
                                        new UserEmailLogin("test1", "test1@test.com", new EncryptedPassword("password")),
                                        new UserEmailLogin("test2-match", "test2@test.com", new EncryptedPassword("password")),
                                        new UserEmailLogin("test3", "test2@test.com", new EncryptedPassword("password"))
                                    };
                    users.ForEach(x => _session.Save(x));
                    _session.Flush();

                    _userToDelete = users.First(x => x.Name == "test2-match");
                    _userToDelete.ChangeEmailAddress("another@test.com");
                };

        Because of =
            () => _writeableRepository.Delete<UserEmailLogin>(_userToDelete.Id);

        It should_make_the_change_in_the_session =
            () => ShouldExtensionMethods.ShouldBeNull(_session.Get<UserEmailLogin>(_userToDelete.Id));
    }
}