using System;

namespace Encomiendas.Notifications
{
    public interface IUserSession
    {
        Guid Id { get; }
    }
}