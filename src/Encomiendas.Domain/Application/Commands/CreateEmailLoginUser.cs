using System;
using System.Collections.Generic;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.ValueObjects;

namespace Encomiendas.Domain.Application.Commands
{
    public class CreateEmailLoginUser
    {
        public string Email { get; private set; }
        public EncryptedPassword EncryptedPassword { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public IEnumerable<UserAbility> abilities { get; private set; }

        public CreateEmailLoginUser(string email, EncryptedPassword password, string name, string phoneNumber, IEnumerable<UserAbility> abilities)
        {
            Email = email;
            EncryptedPassword = password;
            Name = name;
            PhoneNumber = phoneNumber;
            this.abilities = abilities;
        }
    }



}