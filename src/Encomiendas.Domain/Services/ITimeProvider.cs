using System;

namespace Encomiendas.Domain.Services
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}