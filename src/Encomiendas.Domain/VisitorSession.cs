using System;
using AcklenAvenue.Commands;
using Encomiendas.Domain.Services;


namespace Encomiendas.Domain
{
    public class VisitorSession : IUserSession
    {
        #region IUserSession Members

        public Guid Id { get; private set; }

        #endregion
    }
}