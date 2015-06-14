using System;

namespace Encomiendas.Domain
{
    public interface ITokenExpirationProvider
    {
        DateTime GetExpiration(DateTime now);
    }
}