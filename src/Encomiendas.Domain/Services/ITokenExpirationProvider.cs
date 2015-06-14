using System;

namespace Encomiendas.Domain.Services
{
    public interface ITokenExpirationProvider
    {
        DateTime GetExpiration(DateTime now);
    }
}