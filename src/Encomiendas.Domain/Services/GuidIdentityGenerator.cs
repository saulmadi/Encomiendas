using System;

namespace Encomiendas.Domain.Services
{
    public class GuidIdentityGenerator : IIdentityGenerator<Guid>
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}