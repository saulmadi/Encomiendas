using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Encomiendas.Domain.Entities;

namespace Encomiendas.Data
{
    public class UserLoginSessionAutoMappingOverride : IAutoMappingOverride<UserLoginSession>
    {
        public void Override(AutoMapping<UserLoginSession> mapping)
        {            
        }
    }
}