using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public interface IProcessEventLogs
    {
        Task ProcessAsync(ILogger logger);
    }

    public class ProcessEventLogs : IProcessEventLogs
    {
        public Task ProcessAsync(ILogger logger)
        {
            throw new NotImplementedException();
        }
    }


}
