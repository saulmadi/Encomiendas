using System;

namespace Encomiendas.Domain.Entities
{
    public class ProfileAdministrator : IProfile
    {
        public virtual string Name { get; protected set; }

        public ProfileAdministrator()
        {
            Name = "Administrador";
        }

    }

    public interface IProfile
    {
        string Name { get;}
    }
}