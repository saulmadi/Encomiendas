using System;

namespace Encomiendas.Web.Api.Infrastructure.Configuration
{
    public interface IBootstrapperTask<in T>
    {
        Action<T> Task { get; }
    }
}
