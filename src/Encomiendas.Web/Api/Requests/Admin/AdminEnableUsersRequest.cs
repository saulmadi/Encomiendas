using System;

namespace Encomiendas.Web.Api.Requests.Admin
{
    public class AdminEnableUsersRequest
    {
        public Guid Id { get; set; }
        public bool Enable { get; set; }
    }
}