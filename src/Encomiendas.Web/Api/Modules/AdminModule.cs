using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using AcklenAvenue.Commands;
using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Services;

using Encomiendas.Web.Api.Infrastructure;
using Encomiendas.Web.Api.Requests;
using Encomiendas.Web.Api.Requests.Admin;
using Encomiendas.Web.Api.Responses.Admin;

namespace Encomiendas.Web.Api.Modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule(IReadOnlyRepository readOnlyRepository, IMappingEngine mappingEngine,
            ICommandDispatcher commandDispatcher)
        {
            Get["/users"] =
                _ =>
                    {
                        this.RequiresClaims(new[] { "Administrator" });
                        var request = this.Bind<AdminUsersRequest>();
                      
                        var parameter = Expression.Parameter(typeof(User), "User");
                        var mySortExpression = Expression.Lambda<Func<User, object>>(Expression.Property(parameter, request.Field), parameter);
                        
                        IQueryable<User> users =
                            readOnlyRepository.Query<User>(x => x.Name != this.UserLoginSession().User.Name).AsQueryable();

                        var orderedUsers = users.OrderBy(mySortExpression);

                        IQueryable<User> pagedUsers = orderedUsers.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize);

                        List<AdminUserResponse> mappedItems = mappingEngine
                            .Map<IQueryable<User>, IEnumerable<AdminUserResponse>>(pagedUsers).ToList();

                        return new AdminUsersListResponse(mappedItems);
                    };

            Post["/users/enable"] =
                _ =>
                {
                   this.RequiresClaims(new[] {"Administrator"});
                    var request = this.Bind<AdminEnableUsersRequest>();
                    if (request.Enable)
                    {
                        commandDispatcher.Dispatch(this.UserSession(), new EnableUser(request.Id)); 
                    }
                    else
                    {
                        commandDispatcher.Dispatch(this.UserSession(), new DisableUser(request.Id));
                    }
                
                    return null;
                };

            Get["/user/{userId}"] =
                _ =>
                {
                    var userId = Guid.Parse((string)_.userId);
                    var user = readOnlyRepository.GetById<User>(userId);
                    var mappedUser = mappingEngine
                            .Map<User, AdminUserResponse>(user);
                    return mappedUser;
                };

            Post["/user"] =
                _ =>
                {
                    var request = this.Bind<AdminUpdateUserRequest>();
                    commandDispatcher.Dispatch(this.UserSession(), new UpdateUserProfile(request.Id, request.Name, request.Email));
                    return null;
                };

            

        }
    }
}