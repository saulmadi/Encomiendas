using Encomiendas.Domain.ValueObjects;

namespace Encomiendas.Domain.Services
{
    public interface IPasswordEncryptor
    {
        EncryptedPassword Encrypt(string clearTextPassword);
    }
}