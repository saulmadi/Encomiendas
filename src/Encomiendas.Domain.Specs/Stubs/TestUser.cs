using System;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.ValueObjects;

namespace Encomiendas.Domain.Specs.Stubs
{
    public class TestUser : UserEmailLogin
    {
        public TestUser(Guid userId, string name, string password)
        {
            Id = userId;
            Name = name;
            this.EncryptedPassword = password;
        }
    }
}