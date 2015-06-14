using Autofac.Extras.DynamicProxy2;

namespace Encomiendas.Notifications
{
   
    public interface ICommandDispatcher
    {
        void Dispatch(IUserSession userSession, object command);
    }
}