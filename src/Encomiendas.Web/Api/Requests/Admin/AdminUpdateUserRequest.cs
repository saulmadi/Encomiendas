﻿using System;

namespace Encomiendas.Web.Api.Requests.Admin
{
    public class AdminUpdateUserRequest
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }

        public string Name { get; set; }
    }
}