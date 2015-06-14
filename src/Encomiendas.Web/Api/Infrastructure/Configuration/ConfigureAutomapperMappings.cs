using System;
using AutoMapper;
using Autofac;
using Encomiendas.Domain.Entities;
using Encomiendas.Web.Api.Requests;
using Encomiendas.Web.Api.Responses.Admin;

namespace Encomiendas.Web.Api.Infrastructure.Configuration
{
    public class ConfigureAutomapperMappings : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                    {
                        Mapper.CreateMap<User, AdminUserResponse>();
                        Mapper.CreateMap<UserAbility, UserAbilityRequest>().ReverseMap();
                    };
            }
        }

        #endregion
    }
}