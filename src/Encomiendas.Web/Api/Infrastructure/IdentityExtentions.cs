using AcklenAvenue.Commands;
using Nancy;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;

using Encomiendas.Web.Api.Infrastructure.Authentication;
using Encomiendas.Web.Api.Infrastructure.Exceptions;
using Encomiendas.Web.Api.Modules;

namespace Encomiendas.Web.Api.Infrastructure
{
    public static class IdentityExtentions
    {
        public static IUserSession UserSession(this NancyModule module)
        {
            var user = module.Context.CurrentUser as LoggedInUserIdentity;
            if (user == null) throw new NoCurrentUserException();
            return user.UserSession;
        }

        public static UserLoginSession UserLoginSession(this NancyModule module)
        {
            var user = module.Context.CurrentUser as LoggedInUserIdentity;
            if (user == null || user.UserSession.GetType() != typeof(UserLoginSession)) throw new NoCurrentUserException();
            return (UserLoginSession) user.UserSession;
        }
    }
}