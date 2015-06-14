using System;
using Encomiendas.Domain.ValueObjects;

namespace Encomiendas.Domain.Application.Commands
{
    public class ResetPassword
    {
        public Guid ResetPasswordToken { get; private set; }
        public EncryptedPassword EncryptedPassword { get; private set; }

        public ResetPassword(Guid resetPasswordToken, EncryptedPassword encryptedPassword)
        {
            ResetPasswordToken = resetPasswordToken;
            EncryptedPassword = encryptedPassword;
        }
    }
}