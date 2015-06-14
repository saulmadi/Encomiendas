using Encomiendas.Domain.Entities;

namespace Encomiendas.Domain.Services
{
    public interface IUserSessionFactory
    {
        UserLoginSession Create(User executor);
    }
}