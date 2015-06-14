using System;

namespace Encomiendas.Domain
{
    public class GuidTokenGenerator : ITokenGenerator<Guid>
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}