using System;

namespace Encomiendas.Domain.Application.Commands
{
    public class DisableUser
    {
        public Guid id { get; protected set; }

        public DisableUser(Guid id)
        {
            this.id = id;
        }

        protected DisableUser()
        {
            
        }
    }
}