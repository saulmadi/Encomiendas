using Nancy.Security;

namespace Encomiendas.Web.Api.Infrastructure.Authentication
{
    public interface IApiUserMapper<in T>
    {
        IUserIdentity GetUserFromAccessToken(T token);
    }
}