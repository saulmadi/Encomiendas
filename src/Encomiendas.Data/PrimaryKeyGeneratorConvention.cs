using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Encomiendas.Data
{
    public class PrimaryKeyGeneratorConvention : IIdConvention
    {
        public bool Accept(IIdentityInstance id)
        {
            return true;
        }
        public void Apply(IIdentityInstance id)
        {
            id.GeneratedBy.Assigned();
        }
    }
}