using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Encomiendas.Web.Api.Infrastructure.Exceptions
{
    public class RolNotFound:Exception
    {
        public RolNotFound(string rol)
            : base(string.Format("The Rol {0} was not configured", rol))
        {
            
        }
    }
}