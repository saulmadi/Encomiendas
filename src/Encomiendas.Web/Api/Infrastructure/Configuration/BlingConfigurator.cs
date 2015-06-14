using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BlingBag;
using Castle.DynamicProxy;
using log4net;
using Encomiendas.Domain;
using AcklenAvenue.Commands;

namespace Encomiendas.Web.Api.Infrastructure.Configuration
{
    

    public class BlingConfigurator : IBlingConfigurator<DomainEvent>
    {
        readonly IBlingDispatcher _dispatcher;
        readonly ILog _logger;

        public BlingConfigurator(IBlingDispatcher dispatcher, ILog logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            BlingLogger.LogException = LogException;
            BlingLogger.LogInfo = LogInfo;
        }

        void LogInfo(Info info)
        {
            var message = "Date: " + info.DateTime + Environment.NewLine;
            message += "Info Message: " + info.Message + Environment.NewLine;
            _logger.Info(message);
        }

        void LogException(Error error)
        {
            var message = "Date: " + error.DateTime + Environment.NewLine;
            message += "Error Message: " + error.Exception + Environment.NewLine;
            _logger.Error(message);
        }

        #region IBlingConfigurator<DomainEvent> Members

        public object GetHandler(object obj)
        {
            
            return obj;
        }

        public Func<EventInfo, bool> EventSelector
        {
            get { return x => x.EventHandlerType == typeof(DomainEvent); }
        }

        public DomainEvent HandleEvent
        {
            get { return x =>
                         {
                             
                                 _dispatcher.Dispatch(x);
                             
                             
                         }; }
        }



        
        #endregion
    }
}