namespace Encomiendas.Domain
{
    public interface IUserSessionFactory
    {
        UserSession Create(User executor);
    }
}