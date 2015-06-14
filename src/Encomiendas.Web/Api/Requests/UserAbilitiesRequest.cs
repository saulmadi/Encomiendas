using System;
using System.Collections.Generic;
using Encomiendas.Web.Api.Modules;

namespace Encomiendas.Web.Api.Requests
{
    public class UserAbilitiesRequest
    {
        public IEnumerable<UserAbilityRequest> Abilities { get; set; }
        public Guid UserId { get; set; }
    }
}