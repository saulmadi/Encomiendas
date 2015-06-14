using System;
using DomainDrivenDatabaseDeployer;
using Encomiendas.Domain;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;
using NHibernate;

namespace DatabaseDeployer
{
    public class UserSeeder : IDataSeeder
    {
        readonly ISession _session;

        public UserSeeder(ISession session)
        {
            _session = session;
        }

        #region IDataSeeder Members

        public void Seed()
        {
            var encryptor = new HashPasswordEncryptor();

            var admiRole = new Role(Guid.NewGuid(), "Administrator");
            var basicRole = new Role(Guid.NewGuid(), "Basic");
            var userEmailLogin = new UserEmailLogin("Test User", "test@test.com", encryptor.Encrypt("password"), "615-555-1212");
            userEmailLogin.AddRol(basicRole);
            var administratorUser = new UserEmailLogin("Admin User", "admin@test.com", encryptor.Encrypt("password"),
                "123");
            administratorUser.AddRol(admiRole);
            administratorUser.AddRol(basicRole);

            var userAbility = new UserAbility("Developer");
            _session.Save(userAbility);
            userEmailLogin.AddAbility(userAbility);

            _session.Save(admiRole);
            _session.Save(basicRole);


            _session.Save(userEmailLogin);
            _session.Save(administratorUser);


        }

        #endregion
    }
}