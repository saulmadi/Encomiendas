using System;

namespace Encomiendas.Domain
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}