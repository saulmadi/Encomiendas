namespace Encomiendas.Domain
{
    public interface IPasswordEncryptor
    {
        EncryptedPassword Encrypt(string clearTextPassword);
    }
}